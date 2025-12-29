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
        public AddItemForm()
        {
            InitializeComponent();
            //LoadTags();
            LoadComboBoxCategories();
            isEditMode = false;
            SetupInsertMode();
        }

        public AddItemForm(int id, string name, int price, int kategoriId, string merk, string tag) // input ada 5 values
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

            if (name == "" || merk == "")
            {
                MessageBox.Show("Item name and brand is required");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;"))
            {
                conn.Open();

                // Insert product
                string query = "INSERT INTO Produk (Nama, Merk, Harga, kategori_id, Tag) VALUES (@nama, @merk, @harga, @kategori, @tag)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nama", name);
                cmd.Parameters.AddWithValue("@merk", merk);
                cmd.Parameters.AddWithValue("@harga", price);
                cmd.Parameters.AddWithValue("@kategori", categoryId);
                cmd.Parameters.AddWithValue("@tag", tag);
                cmd.ExecuteNonQuery();

                long newProductId = cmd.LastInsertedId;

                //// Insert selected tags
                //foreach (var item in checkedListBoxTags.CheckedItems)
                //{
                //    dynamic tag = item;
                //    int tagId = tag.Value;

                //    string tagQuery = "INSERT INTO Produk_Tag (produk_id, tag_id) VALUES (@pid, @tid)";
                //    MySqlCommand tagCmd = new MySqlCommand(tagQuery, conn);
                //    tagCmd.Parameters.AddWithValue("@pid", newProductId);
                //    tagCmd.Parameters.AddWithValue("@tid", tagId);
                //    tagCmd.ExecuteNonQuery();
                //}

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

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;"))
            {
                conn.Open();

                // Update product
                string query = "UPDATE Produk SET Nama=@nama, Merk=@merk, Harga=@harga, kategori_id=@kategori, Tag=@tag WHERE ID=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nama", name);
                cmd.Parameters.AddWithValue("@merk", merk);
                cmd.Parameters.AddWithValue("@harga", price);
                cmd.Parameters.AddWithValue("@kategori", categoryId);
                cmd.Parameters.AddWithValue("@tag", tag);
                cmd.Parameters.AddWithValue("@id", productId);
                cmd.ExecuteNonQuery();

                //// Remove all old tags
                //string deleteTagQuery = "DELETE FROM Produk_Tag WHERE produk_id = @pid";
                //MySqlCommand deleteCmd = new MySqlCommand(deleteTagQuery, conn);
                //deleteCmd.Parameters.AddWithValue("@pid", productId);
                //deleteCmd.ExecuteNonQuery();

                //// Insert new tags
                //foreach (var item in checkedListBoxTags.CheckedItems)
                //{
                //    dynamic tag = item;
                //    int tagId = tag.Value;

                //    string insertTagQuery = "INSERT INTO Produk_Tag (produk_id, tag_id) VALUES (@pid, @tid)";
                //    MySqlCommand tagCmd = new MySqlCommand(insertTagQuery, conn);
                //    tagCmd.Parameters.AddWithValue("@pid", productId);
                //    tagCmd.Parameters.AddWithValue("@tid", tagId);
                //    tagCmd.ExecuteNonQuery();
                //}

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

                // Delete tags first (FK: product_id)
                //string deleteTags = "DELETE FROM Produk_Tag WHERE produk_id = @id";
                //MySqlCommand cmd1 = new MySqlCommand(deleteTags, conn);
                //cmd1.Parameters.AddWithValue("@id", productId);
                //cmd1.ExecuteNonQuery();

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
            string link = textBoxLinkImage.Text;
            openFileDialog1.Filter = "Image Files(*.jpeg;*.bmp;*.png;*.jpg)|*.jpeg;*.bmp;*.png;*.jpg|All files (*.*)|*.*";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Get the selected file path
                    string selectedFilePath = openFileDialog1.FileName;

                    // Load the image into the PictureBox control
                    pictureBox1.Image = new System.Drawing.Bitmap(selectedFilePath);

                    // Optional: Adjust the SizeMode property for better display (e.g., Zoom, StretchImage)
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private async void textBoxLinkImage_TextChanged(object sender, EventArgs e) // (jpg, png, jpeg, bmp) only
        {
            string imageUrl = textBoxLinkImage.Text.Trim();

            if (string.IsNullOrEmpty(imageUrl))
                return;

            // Simple validation
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
            }
            catch
            {
                // Ignore errors silently (bad URL, no internet, etc.)
                pictureBox1.Image = null;
            }
        }


    }
}
