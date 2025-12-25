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
    public partial class AdminManagementControl : UserControl
    {
        string connectionString = "Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;";
        MySqlConnection connection;
        public AdminManagementControl()
        {
            InitializeComponent();
            connectDatabase();
            refreshDGVList();
            loadComboBox();
            LoadDefaultSetting();
            searchBox.TextChanged += (s, e) => ApplyFilters();
            filterByComboBox.SelectedIndexChanged += (s, e) => ApplyFilters();
            sortByComboBox.SelectedIndexChanged += (s, e) => ApplyFilters();
        }

        private void AddBookButton_Click(object sender, EventArgs e)
        {
            AddItemForm addItemForm = new AddItemForm();
            addItemForm.FormClosed += (s, args) => refreshDGVList();
            addItemForm.ShowDialog();
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(searchBox.Text))
                refreshDGVList();  // reload all
            else
                SearchProducts(searchBox.Text);
        }

        private void dataGridViewItems_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow row = dataGridViewItems.Rows[e.RowIndex];

            int id = Convert.ToInt32(row.Cells["ID"].Value);
            string name = row.Cells["Produk"].Value.ToString();
            int price = Convert.ToInt32(row.Cells["Harga"].Value);
            string merk = row.Cells["Merk"].Value.ToString();
            string tag = row.Cells["tag"].Value.ToString();

            int kategoriId = GetCategoryIdFromProduct(id);

            AddItemForm form = new AddItemForm(id, name, price, kategoriId, merk, tag);
            form.FormClosed += (s, args) => refreshDGVList();
            form.ShowDialog();
        }

        private void SearchProducts(string keyword)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query =
                    "SELECT p.ID, p.Nama AS Produk, p.Merk, p.Harga, k.Nama AS Kategori, Tag " +
                    "FROM Produk p " +
                    "JOIN Kategori k ON p.kategori_id = k.ID " +
                    "WHERE p.Nama LIKE @keyword OR p.Merk LIKE @keyword " +
                    "GROUP BY p.ID, p.Nama, p.Harga, k.Nama;";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                dataGridViewItems.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void ApplyFilters()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string keyword = searchBox.Text.Trim();

                int selectedCategory = 0;
                if (filterByComboBox.SelectedValue != null &&
                    filterByComboBox.SelectedValue is int)
                {
                    selectedCategory = (int)filterByComboBox.SelectedValue;
                }

                string sortOption = sortByComboBox.SelectedItem?.ToString() ?? "None";

                // Base SQL
                string query =
                    "SELECT p.ID, p.Nama AS Produk, p.Merk, p.Harga, k.Nama AS Kategori, Tag " +
                    "FROM Produk p " +
                    "JOIN Kategori k ON p.kategori_id = k.ID " +
                    "WHERE 1=1 ";

                // SEARCH condition
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    query += "AND p.Nama LIKE @keyword ";
                }

                // FILTER condition
                if (selectedCategory != 0) // 0 means "None"
                {
                    query += "AND p.kategori_id = @kategori ";
                }

                //query += "GROUP BY p.ID, p.Nama, p.Harga, k.Nama "; // idk error nya agak spesifik, tak comment ae :v (di comment ga ada bedanya)

                // SORT condition
                switch (sortOption)
                {
                    case "Name (Ascending)":
                        query += "ORDER BY p.Nama ASC ";
                        break;

                    case "Name (Descending)":
                        query += "ORDER BY p.Nama DESC ";
                        break;

                    case "ID (Ascending)":
                        query += "ORDER BY p.ID ASC ";
                        break;

                    case "ID (Descending)":
                        query += "ORDER BY p.ID DESC ";
                        break;

                    case "None":
                    default:
                        // no sorting
                        break;
                }

                MySqlCommand cmd = new MySqlCommand(query, connection);

                // Add parameters only when needed
                if (!string.IsNullOrWhiteSpace(keyword))
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                if (selectedCategory != 0)
                    cmd.Parameters.AddWithValue("@kategori", selectedCategory);

                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                dataGridViewItems.DataSource = dt;
                labelItemCount.Text = $"Showing {dt.Rows.Count} Items";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void LoadDefaultSetting()
        {
            filterByComboBox.SelectedItem = "None";
            searchBox.Text = "";

            // SORT BY LIST
            sortByComboBox.Items.Clear();
            sortByComboBox.Items.Add("None");
            sortByComboBox.Items.Add("Name (Ascending)");
            sortByComboBox.Items.Add("Name (Descending)");
            sortByComboBox.Items.Add("ID (Ascending)");
            sortByComboBox.Items.Add("ID (Descending)");

            sortByComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            sortByComboBox.SelectedItem = "None";
        }

        public void refreshDGVList()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string query = "SELECT p.ID, p.Nama AS Produk, p.Merk, p.Harga, k.Nama AS Kategori, Tag " +
                               "FROM Produk p JOIN Kategori k ON p.kategori_id = k.ID " +
                               "GROUP BY p.ID, p.Nama, p.Harga, k.Nama;";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridViewItems.DataSource = dt;

                UpdateItemCount();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void loadComboBox()
        {
            connection.Open();

            string query = "SELECT ID, Nama FROM kategori";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(reader);

            // Create a "None" row
            DataRow noneRow = dt.NewRow();
            noneRow["ID"] = 0;          // dummy value
            noneRow["Nama"] = "None";
            dt.Rows.InsertAt(noneRow, 0); // insert at top

            filterByComboBox.DisplayMember = "Nama";
            filterByComboBox.ValueMember = "ID";
            filterByComboBox.DataSource = dt;

            filterByComboBox.SelectedIndex = 0; // Select "None"

            filterByComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            connection.Close();
        }

        private int GetCategoryIdFromProduct(int id)
        {
            int kategori = 0;

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT kategori_id FROM Produk WHERE ID = @id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);

                object result = cmd.ExecuteScalar();
                kategori = Convert.ToInt32(result);
            }

            return kategori;
        }

        public void connectDatabase()
        {
            connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                connection.Close();
                //MessageBox.Show("database connected");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateItemCount()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM Produk";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    labelItemCount.Text = $"Showing {count} Items";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error counting items: " + ex.Message);
            }
        }
    }
}
