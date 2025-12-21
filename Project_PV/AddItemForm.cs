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

        public AddItemForm(int id, string name, int price, int kategoriId)
        {
            InitializeComponent();
            //LoadTags();
            LoadComboBoxCategories();
            isEditMode = true;
            productId = id;

            textBox1.Text = name;
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

        //private void LoadProductTags(int productId)
        //{
        //    checkedListBoxTags.ClearSelected();

        //    using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;"))
        //    {
        //        conn.Open();
        //        string query = @"SELECT tag_id FROM Produk_Tag WHERE produk_id = @id";
        //        MySqlCommand cmd = new MySqlCommand(query, conn);
        //        cmd.Parameters.AddWithValue("@id", productId);

        //        MySqlDataReader dr = cmd.ExecuteReader();
        //        List<int> tagIds = new List<int>();

        //        while (dr.Read())
        //            tagIds.Add(dr.GetInt32(0));

        //        // Check the tags in the CheckedListBox
        //        for (int i = 0; i < checkedListBoxTags.Items.Count; i++)
        //        {
        //            dynamic item = checkedListBoxTags.Items[i];
        //            int tagId = item.Value;
        //            if (tagIds.Contains(tagId))
        //                checkedListBoxTags.SetItemChecked(i, true);
        //        }
        //    }
        //}

        //private void LoadTags()
        //{
        //    checkedListBoxTags.Items.Clear();
        //    checkedListBoxTags.DisplayMember = "Text";
        //    checkedListBoxTags.ValueMember = "Value";
        //    using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;"))
        //    {
        //        conn.Open();
        //        string query = "SELECT ID, Nama FROM Tag";
        //        MySqlCommand cmd = new MySqlCommand(query, conn);
        //        MySqlDataReader dr = cmd.ExecuteReader();

        //        while (dr.Read())
        //        {
        //            checkedListBoxTags.Items.Add(
        //                new { Text = dr["Nama"].ToString(), Value = dr["ID"] },
        //                false
        //            );
        //        }
        //    }
        //}

        
    }
}
