using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project_PV
{
    public partial class UserCatalogControl : UserControl
    {
        private FlowLayoutPanel productCardsPanel;
        private List<Product> allProducts = new List<Product>();
        private List<Product> filteredProducts = new List<Product>();

        string connectionString = "Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;";

        public UserCatalogControl()
        {
            InitializeComponent();
            InitializeProductCardsPanel();
            LoadCategories();
            LoadSortOptions();
            LoadProducts();
            SetupEventHandlers();
        }

        private void InitializeProductCardsPanel()
        {
            if (this.Controls.Contains(dataGridView1))
            {
                this.Controls.Remove(dataGridView1);
            }

            productCardsPanel = new FlowLayoutPanel
            {
                Location = new Point(64, 145),
                Size = new Size(1080, 520),
                AutoScroll = true,
                BackColor = Color.FromArgb(248, 248, 248),
                Padding = new Padding(10),
                BorderStyle = BorderStyle.None
            };

            this.Controls.Add(productCardsPanel);
        }

        private void LoadCategories()
        {
            filterByComboBox.Items.Clear();
            filterByComboBox.Items.Add("All Categories");

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT DISTINCT Nama FROM Kategori ORDER BY Nama";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            filterByComboBox.Items.Add(reader["Nama"].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            filterByComboBox.SelectedIndex = 0;
        }

        private void LoadSortOptions()
        {
            sortByComboBox.Items.Clear();
            sortByComboBox.Items.AddRange(new object[] {
                "Name (A-Z)",
                "Name (Z-A)",
                "Price (Low to High)",
                "Price (High to Low)",
                "Category"
            });
            sortByComboBox.SelectedIndex = 0;
        }

        private void LoadProducts()
        {
            allProducts.Clear();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        SELECT p.ID, p.Nama, p.Merk, p.Harga, p.tag, p.image_url, k.Nama as KategoriNama
                        FROM Produk p
                        INNER JOIN Kategori k ON p.kategori_id = k.ID
                        ORDER BY p.Nama";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            allProducts.Add(new Product
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Nama = reader["Nama"].ToString(),
                                Merk = reader["Merk"].ToString(),
                                Harga = Convert.ToInt32(reader["Harga"]),
                                Tag = reader["tag"].ToString(),
                                ImageUrl = reader["image_url"].ToString(),
                                KategoriNama = reader["KategoriNama"].ToString()
                            });
                        }
                    }
                }

                filteredProducts = new List<Product>(allProducts);
                DisplayProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupEventHandlers()
        {
            searchBox.TextChanged += (s, e) => FilterProducts();
            filterByComboBox.SelectedIndexChanged += (s, e) => FilterProducts();
            sortByComboBox.SelectedIndexChanged += (s, e) => SortProducts();
        }

        private void FilterProducts()
        {
            string searchText = searchBox.Text.ToLower();
            string selectedCategory = filterByComboBox.SelectedItem?.ToString();

            filteredProducts = allProducts.FindAll(p =>
            {
                bool matchesSearch = string.IsNullOrEmpty(searchText) ||
                    p.Nama.ToLower().Contains(searchText) ||
                    p.Merk.ToLower().Contains(searchText) ||
                    p.Tag.ToLower().Contains(searchText);

                bool matchesCategory = selectedCategory == "All Categories" ||
                    p.KategoriNama == selectedCategory;

                return matchesSearch && matchesCategory;
            });

            SortProducts();
        }

        private void SortProducts()
        {
            string sortOption = sortByComboBox.SelectedItem?.ToString();

            switch (sortOption)
            {
                case "Name (A-Z)":
                    filteredProducts.Sort((a, b) => a.Nama.CompareTo(b.Nama));
                    break;
                case "Name (Z-A)":
                    filteredProducts.Sort((a, b) => b.Nama.CompareTo(a.Nama));
                    break;
                case "Price (Low to High)":
                    filteredProducts.Sort((a, b) => a.Harga.CompareTo(b.Harga));
                    break;
                case "Price (High to Low)":
                    filteredProducts.Sort((a, b) => b.Harga.CompareTo(a.Harga));
                    break;
                case "Category":
                    filteredProducts.Sort((a, b) => a.KategoriNama.CompareTo(b.KategoriNama));
                    break;
            }

            DisplayProducts();
        }

        private void DisplayProducts()
        {
            productCardsPanel.Controls.Clear();
            labelItemCount.Text = $"Showing {filteredProducts.Count} Items";

            foreach (var product in filteredProducts)
            {
                Panel card = CreateProductCard(product);
                productCardsPanel.Controls.Add(card);
            }
        }

        private Panel CreateProductCard(Product product)
        {
            Panel card = new Panel
            {
                Size = new Size(240, 360),
                BackColor = Color.White,
                Margin = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };

            // Product Image
            PictureBox pictureBox = new PictureBox
            {
                Location = new Point(0, 0),
                Size = new Size(240, 180),
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.FromArgb(245, 245, 245)
            };

            try
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    pictureBox.Load(product.ImageUrl);
                }
            }
            catch
            {
                pictureBox.BackColor = Color.FromArgb(224, 224, 224);
            }

            card.Controls.Add(pictureBox);

            // Category Label
            Label categoryLabel = new Label
            {
                Text = product.KategoriNama,
                Location = new Point(10, 190),
                AutoSize = true,
                BackColor = Color.FromArgb(224, 224, 224),
                ForeColor = Color.FromArgb(64, 64, 64),
                Padding = new Padding(5, 2, 5, 2),
                Font = new Font("Segoe UI", 8F)
            };
            card.Controls.Add(categoryLabel);

            // Product Name
            Label nameLabel = new Label
            {
                Text = product.Nama,
                Location = new Point(10, 215),
                Size = new Size(220, 40),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 30, 30)
            };
            card.Controls.Add(nameLabel);

            // Brand
            Label merkLabel = new Label
            {
                Text = product.Merk,
                Location = new Point(10, 258),
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                ForeColor = Color.Gray
            };
            card.Controls.Add(merkLabel);

            // Price
            Label priceLabel = new Label
            {
                Text = $"Rp {product.Harga:N0}",
                Location = new Point(10, 310),
                AutoSize = true,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(230, 100, 20)
            };
            card.Controls.Add(priceLabel);

            // Add to Cart Button
            Button addToCartBtn = new Button
            {
                Text = "Add to Cart",
                Location = new Point(140, 307),
                Size = new Size(90, 30),
                BackColor = Color.Black,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 8F, FontStyle.Bold),
                Cursor = Cursors.Hand,
                Tag = product // Store product in Tag for easy access
            };
            addToCartBtn.FlatAppearance.BorderSize = 0;
            addToCartBtn.Click += AddToCartBtn_Click;
            card.Controls.Add(addToCartBtn);

            return card;
        }

        private void AddToCartBtn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Product product = btn.Tag as Product;

            if (product != null)
            {
                // Add to cart using CartManager
                CartManager.AddToCart(
                    product.ID,
                    product.Nama,
                    product.Merk,
                    product.KategoriNama,
                    product.Harga,
                    product.ImageUrl,
                    1 // Default quantity
                );

                MessageBox.Show(
                    $"{product.Nama} added to cart!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
        
        private class Product
        {
            public int ID { get; set; }
            public string Nama { get; set; }
            public string Merk { get; set; }
            public int Harga { get; set; }
            public string Tag { get; set; }
            public string ImageUrl { get; set; }
            public string KategoriNama { get; set; }
        }
    }
}