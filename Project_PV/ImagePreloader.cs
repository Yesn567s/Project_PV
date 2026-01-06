using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Project_PV
{
    // Lightweight image preloader and cache.
    // Preload writes files to disk to avoid memory spikes. Memory cache is small and LRU-evicted.
    public static class ImagePreloader
    {
        private static readonly string cacheFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Project_PV", "imagecache");
        private static readonly SemaphoreSlim downloadSemaphore = new SemaphoreSlim(4);
        private static readonly HttpClient httpClient = new HttpClient();

        // small in-memory cache to avoid big memory spikes
        private static readonly Dictionary<int, Image> memoryCache = new Dictionary<int, Image>();
        private static readonly LinkedList<int> lruList = new LinkedList<int>();
        private static readonly Dictionary<int, LinkedListNode<int>> lruNodes = new Dictionary<int, LinkedListNode<int>>();
        private const int MaxMemoryCacheItems = 25; // tune this to limit memory

        static ImagePreloader()
        {
            try
            {
                Directory.CreateDirectory(cacheFolder);
            }
            catch { }
        }

        // Start preloading images from Produk table, but only save to disk to limit memory use.
        public static async Task PreloadFromDatabaseAsync(string connectionString)
        {
            List<Tuple<int, string>> list = new List<Tuple<int, string>>();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ID, image_url FROM Produk WHERE image_url IS NOT NULL AND image_url <> ''";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("ID");
                            string url = reader.IsDBNull(reader.GetOrdinal("image_url")) ? null : reader.GetString("image_url");
                            if (!string.IsNullOrEmpty(url))
                                list.Add(Tuple.Create(id, url));
                        }
                    }
                }
            }
            catch
            {
                // ignore DB read errors
            }

            List<Task> tasks = new List<Task>();
            foreach (var t in list)
            {
                // schedule downloads but PreloadSingle will limit concurrency and only write to disk
                tasks.Add(PreloadSingleToDiskAsync(t.Item1, t.Item2));
            }

            try
            {
                await Task.WhenAll(tasks);
            }
            catch { }
        }

        private static string GetFileNameForUrl(int id, string url)
        {
            try
            {
                Uri u;
                if (Uri.TryCreate(url, UriKind.Absolute, out u))
                {
                    string name = Path.GetFileName(u.LocalPath);
                    if (string.IsNullOrWhiteSpace(name))
                        name = u.Host + "_" + Math.Abs(url.GetHashCode()).ToString();
                    return id + "_" + name;
                }
            }
            catch { }
            return id + "_" + Math.Abs(url.GetHashCode()).ToString();
        }

        // Preload image and save to disk, but do NOT load into memory cache here to avoid spikes.
        private static async Task PreloadSingleToDiskAsync(int id, string url)
        {
            if (string.IsNullOrEmpty(url)) return;

            string fileName = Path.Combine(cacheFolder, GetFileNameForUrl(id, url));
            if (File.Exists(fileName)) return;

            await downloadSemaphore.WaitAsync();
            try
            {
                byte[] data = null;
                try
                {
                    data = await httpClient.GetByteArrayAsync(url);
                }
                catch
                {
                    data = null;
                }

                if (data != null && data.Length > 0)
                {
                    try
                    {
                        File.WriteAllBytes(fileName, data);
                    }
                    catch { }
                }
            }
            finally
            {
                downloadSemaphore.Release();
            }
        }

        // Try get image from memory cache
        public static bool TryGetImage(int id, out Image img)
        {
            lock (memoryCache)
            {
                if (memoryCache.TryGetValue(id, out img))
                {
                    // update LRU
                    if (lruNodes.TryGetValue(id, out var node))
                    {
                        lruList.Remove(node);
                        lruList.AddFirst(node);
                    }
                    return true;
                }
            }
            img = null;
            return false;
        }

        // Get image (from memory, disk or download). This will load into memory cache (bounded).
        public static async Task<Image> GetImageAsync(int id, string url)
        {
            Image img;
            if (TryGetImage(id, out img)) return img;

            string fileName = Path.Combine(cacheFolder, GetFileNameForUrl(id, url));

            if (File.Exists(fileName))
            {
                try
                {
                    byte[] bytes = File.ReadAllBytes(fileName);
                    using (var ms = new MemoryStream(bytes))
                    {
                        var loaded = Image.FromStream(ms);
                        AddToMemoryCache(id, new Bitmap(loaded));
                        return memoryCache[id];
                    }
                }
                catch
                {
                    // fallthrough to download
                }
            }

            await downloadSemaphore.WaitAsync();
            try
            {
                byte[] data = null;
                try
                {
                    data = await httpClient.GetByteArrayAsync(url);
                }
                catch
                {
                    data = null;
                }

                if (data != null && data.Length > 0)
                {
                    try
                    {
                        try
                        {
                            File.WriteAllBytes(fileName, data);
                        }
                        catch { }

                        using (var ms = new MemoryStream(data))
                        {
                            var loaded = Image.FromStream(ms);
                            AddToMemoryCache(id, new Bitmap(loaded));
                            return memoryCache[id];
                        }
                    }
                    catch { }
                }
            }
            finally
            {
                downloadSemaphore.Release();
            }

            return null;
        }

        private static void AddToMemoryCache(int id, Image img)
        {
            lock (memoryCache)
            {
                if (memoryCache.ContainsKey(id)) return;
                // evict if necessary
                while (memoryCache.Count >= MaxMemoryCacheItems)
                {
                    // remove last (LRU)
                    var last = lruList.Last;
                    if (last == null) break;
                    int removeId = last.Value;
                    lruList.RemoveLast();
                    lruNodes.Remove(removeId);
                    if (memoryCache.TryGetValue(removeId, out var remImg))
                    {
                        try { remImg.Dispose(); } catch { }
                        memoryCache.Remove(removeId);
                    }
                }

                memoryCache[id] = img;
                var node = lruList.AddFirst(id);
                lruNodes[id] = node;
            }
        }
    }
}
