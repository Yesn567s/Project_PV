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

        private void SearchProducts(string keyword)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query =
                    "SELECT p.ID, p.Nama AS Produk, p.Harga, k.Nama AS Kategori, " +
                    "GROUP_CONCAT(t.Nama SEPARATOR ', ') AS Tags " +
                    "FROM Produk p " +
                    "JOIN Kategori k ON p.kategori_id = k.ID " +
                    "LEFT JOIN Produk_Tag pt ON p.ID = pt.produk_id " +
                    "LEFT JOIN Tag t ON pt.tag_id = t.ID " +
                    "WHERE p.Nama LIKE @keyword " +
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
                    "SELECT p.ID, p.Nama AS Produk, p.Harga, k.Nama AS Kategori, " +
                    "GROUP_CONCAT(t.Nama SEPARATOR ', ') AS Tags " +
                    "FROM Produk p " +
                    "JOIN Kategori k ON p.kategori_id = k.ID " +
                    "LEFT JOIN Produk_Tag pt ON p.ID = pt.produk_id " +
                    "LEFT JOIN Tag t ON pt.tag_id = t.ID " +
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

                query += "GROUP BY p.ID, p.Nama, p.Harga, k.Nama ";

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

                string query = "SELECT p.ID, p.Nama AS Produk, p.Harga, k.Nama AS Kategori, GROUP_CONCAT(t.Nama SEPARATOR ', ') AS Tags " +
                               "FROM Produk p JOIN Kategori k ON p.kategori_id = k.ID " +
                               "LEFT JOIN Produk_Tag pt ON p.ID = pt.produk_id " +
                               "LEFT JOIN Tag t ON pt.tag_id = t.ID " +
                               "GROUP BY p.ID, p.Nama, p.Harga, k.Nama;";

                MySqlCommand cmd = new MySqlCommand(query, connection);
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

        
    }
}
