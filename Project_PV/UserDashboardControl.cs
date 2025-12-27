using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project_PV
{
    public partial class UserDashboardControl : UserControl
    {
        // Events to notify parent form
        // These events will be handled in UserDashboard to load respective controls when buttons are clicked in this UserControl
        // For example, when Explore Membership button is clicked, UserDashboard will load UserMembershipControl
        public event EventHandler ExploreMembershipClicked;
        public event EventHandler BrowseCatalogClicked;
        public UserDashboardControl()
        {
            InitializeComponent();
            LoadFeaturedProducts();
        }

        // Button Click Handlers
        // Membership Section
        private void exploreMembershipBtn_Click(object sender, EventArgs e)
        {
            ExploreMembershipClicked?.Invoke(this, EventArgs.Empty); // Raise event to notify parent form
        }

        private void joinMembershipButton_Click(object sender, EventArgs e)
        {
            ExploreMembershipClicked?.Invoke(this, EventArgs.Empty);
        }

        // Catalog Section
        private void browseCatalogButton_Click(object sender, EventArgs e)
        {
            BrowseCatalogClicked?.Invoke(this, EventArgs.Empty);
        }

        // Function
        private void LoadFeaturedProducts()
        {
            string connStr = "Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;";
            string query = @"
        SELECT 
            p.Nama,
            p.Merk,
            k.Nama AS Kategori,
            p.tag,
            p.Harga,
            p.image_url,
            SUM(td.Qty) AS total_sold
        FROM Transaksi_Detail td
        JOIN Produk p ON td.produk_id = p.ID
        JOIN Kategori k ON p.kategori_id = k.ID
        GROUP BY 
            p.ID, p.Nama, p.Merk, k.Nama, p.tag, p.Harga, p.image_url
        ORDER BY total_sold DESC
        LIMIT 3;
    ";

            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    int index = 0;

                    while (reader.Read())
                    {
                        string nama = reader.GetString("Nama");
                        string merk = reader.GetString("Merk");
                        string kategori = reader.GetString("Kategori");
                        string tag = reader.GetString("tag");
                        int harga = reader.GetInt32("Harga");
                        string imageUrl = reader.GetString("image_url");

                        if (index == 0)
                        {
                            labelCardTitle1.Text = nama;
                            labelMerk1.Text = "Merk: " + merk;
                            labelCategory1.Text = "Kategori: " + kategori;
                            labelTag1.Text = tag;
                            labelPrice1.Text = "Rp " + harga.ToString("N0");
                            LoadImageSafe(pictureBox1, imageUrl);
                        }
                        else if (index == 1)
                        {
                            labelCardTitle2.Text = nama;
                            labelMerk2.Text = "Merk: " + merk;
                            labelCategory2.Text = "Kategori: " + kategori;
                            labelTag2.Text = tag;
                            labelPrice2.Text = "Rp " + harga.ToString("N0");
                            LoadImageSafe(pictureBox2, imageUrl);
                        }
                        else if (index == 2)
                        {
                            labelCardTitle3.Text = nama;
                            labelMerk3.Text = "Merk: " + merk;
                            labelCategory3.Text = "Kategori: " + kategori;
                            labelTag3.Text = tag;
                            labelPrice3.Text = "Rp " + harga.ToString("N0");
                            LoadImageSafe(pictureBox3, imageUrl);
                        }

                        index++;
                    }
                }
            }
        }

        private void LoadImageSafe(PictureBox pb, string imageUrl)
        {
            try
            {
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    pb.LoadAsync(imageUrl);
                }
                else
                {
                    pb.Image = Properties.Resources.placeholders; // fallback
                }
            }
            catch
            {
                pb.Image = Properties.Resources.placeholders;
            }

            pb.SizeMode = PictureBoxSizeMode.Zoom;
        }

    }
}
