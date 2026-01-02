using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace Project_PV
{
    public partial class AddItemForm : Form
    {
        public int productId = 0;
        public bool isEditMode = false;
        string selectedImagePath = null;
        public AddItemForm()
        {
            InitializeComponent();
            //LoadTags();
            LoadComboBoxCategories();
            isEditMode = false;
            SetupInsertMode();
        }

        public AddItemForm(int id, string name, int price, int kategoriId, string merk, string tag, string image_url) // input ada 5 values
        {
            InitializeComponent();
            //LoadTags();
            LoadComboBoxCategories();
            isEditMode = true;
            productId = id;

            // sangar ngono nama textbox e
            textBox1.Text = name; // Nama
            textBox2.Text = merk; // Merk
            textBox3.Text = tag; // Tag
            numericUpDown1.Value = price;
            comboBox1.SelectedValue = kategoriId;
            selectedImagePath = image_url;

            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            if (Uri.IsWellFormedUriString(image_url, UriKind.Absolute))
            {
                // Load image from URL
                textBoxLinkImage.Text = image_url;
            }
            else if (File.Exists(image_url))
            {
                // Load image from local path
                pictureBox1.Image = new Bitmap(image_url);
            }else
            {
                pictureBox1.Image = null;
            }
            SetupEditMode();
            //LoadProductTags(id);
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text.Trim();
            string merk = textBox2.Text.Trim();
            int price = (int)numericUpDown1.Value;
            int categoryId = Convert.ToInt32(comboBox1.SelectedValue);
            string tag = textBox3.Text;
            string imagePath = selectedImagePath;

            if (name == "" || merk == "")
            {
                MessageBox.Show("Item name and brand is required");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;"))
            {
                conn.Open();

                // Insert product
                string query = "INSERT INTO Produk (Nama, Merk, Harga, kategori_id, tag, image_url) VALUES (@nama, @merk, @harga, @kategori, @tag, @image)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nama", name);
                cmd.Parameters.AddWithValue("@merk", merk);
                cmd.Parameters.AddWithValue("@harga", price);
                cmd.Parameters.AddWithValue("@kategori", categoryId);
                cmd.Parameters.AddWithValue("@tag", tag);
                cmd.Parameters.AddWithValue("@image", imagePath);
                cmd.ExecuteNonQuery();

                long newProductId = cmd.LastInsertedId;

                MessageBox.Show("Product added successfully!");
                this.Close();
            }
        }


        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (productId == 0)
            {
                MessageBox.Show("Invalid product ID");
                return;
            }

            string name = textBox1.Text.Trim();
            string merk = textBox2.Text.Trim();
            int price = (int)numericUpDown1.Value;
            int categoryId = Convert.ToInt32(comboBox1.SelectedValue);
            string tag = textBox3.Text;
            string imagePath = selectedImagePath;

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;"))
            {
                conn.Open();

                // Update product
                string query = "UPDATE Produk SET Nama=@nama, Merk=@merk, Harga=@harga, kategori_id=@kategori, tag=@tag, image_url=@imagepath WHERE ID=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nama", name);
                cmd.Parameters.AddWithValue("@merk", merk);
                cmd.Parameters.AddWithValue("@harga", price);
                cmd.Parameters.AddWithValue("@kategori", categoryId);
                cmd.Parameters.AddWithValue("@tag", tag);
                cmd.Parameters.AddWithValue("@imagepath", imagePath);
                cmd.Parameters.AddWithValue("@id", productId);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Product updated successfully!");
                this.Close();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (productId == 0)
            {
                MessageBox.Show("Invalid product ID");
                return;
            }

            if (MessageBox.Show(
                "Are you sure you want to delete this product?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
                ) == DialogResult.No)
                return;

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;"))
            {
                conn.Open();

                // Delete product
                string deleteProduct = "DELETE FROM Produk WHERE ID = @id";
                MySqlCommand cmd2 = new MySqlCommand(deleteProduct, conn);
                cmd2.Parameters.AddWithValue("@id", productId);
                cmd2.ExecuteNonQuery();

                MessageBox.Show("Product deleted successfully!");
                this.Close();
            }
        }

        private void SetupInsertMode()
        {
            buttonInsert.Enabled = true;
            buttonUpdate.Enabled = false;
            buttonDelete.Enabled = false;
        }

        private void SetupEditMode()
        {
            buttonInsert.Enabled = false;
            buttonUpdate.Enabled = true;
            buttonDelete.Enabled = true;
        }

        private void LoadComboBoxCategories()
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;"))
            {
                conn.Open();
                string query = "SELECT ID, Nama FROM Kategori";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                comboBox1.DataSource = dt;
                comboBox1.DisplayMember = "Nama";
                comboBox1.ValueMember = "ID";
            }
        }


        // Unfinished Image Upload Section 
        private void buttonUploadImage_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    selectedImagePath = openFileDialog1.FileName;

                    pictureBox1.Image = new Bitmap(selectedImagePath);
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                    // Clear URL textbox to avoid conflict
                    textBoxLinkImage.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message);
                }
            }
        }

        private async void textBoxLinkImage_TextChanged(object sender, EventArgs e)
        {
            string imageUrl = textBoxLinkImage.Text.Trim();

            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
                return;

            try
            {
                using (WebClient client = new WebClient())
                {
                    byte[] imageData = await client.DownloadDataTaskAsync(imageUrl);
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }

                // Save URL and clear local image
                selectedImagePath = imageUrl;
            }
            catch
            {
                pictureBox1.Image = null;
                selectedImagePath = null;
            }
        }

    }
}
