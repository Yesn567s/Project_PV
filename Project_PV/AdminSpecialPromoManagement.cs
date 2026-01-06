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
    public partial class AdminSpecialPromoManagement : UserControl
    {
        string connectionString = "Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;";
        MySqlConnection connection;
        public AdminSpecialPromoManagement()
        {
            InitializeComponent();
            connectDatabase();
            refreshDGVList();
            //loadComboBox();
            LoadDefaultSetting();
            searchBox.TextChanged += (s, e) => ApplyFilters();
            filterByComboBox.SelectedIndexChanged += (s, e) => ApplyFilters();
            sortByComboBox.SelectedIndexChanged += (s, e) => ApplyFilters();
        }

        private void ApplyFilters()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string keyword = searchBox.Text.Trim();

                string selectedCategory = "";
                if (filterByComboBox.SelectedValue != null)
                {
                    selectedCategory = filterByComboBox.SelectedValue.ToString();
                }

                string sortOption = sortByComboBox.SelectedItem?.ToString() ?? "None";

                // Base SQL
                string query =
                    "SELECT * " +
                    "FROM Promo_special p " +
                    "WHERE 1=1 ";

                // SEARCH condition
                if (!string.IsNullOrWhiteSpace(keyword))
                {
                    query += "AND p.Nama_Promo LIKE @keyword ";
                }

                // FILTER condition
                if (selectedCategory != "") // 0 means "None"
                {
                    query += "AND p.kategori = @kategori ";
                }

                query += "GROUP BY p.ID, p.Nama_Promo, p.kategori ";

                // SORT condition
                switch (sortOption)
                {
                    case "Name (Ascending)":
                        query += "ORDER BY p.Nama_Promo ASC ";
                        break;

                    case "Name (Descending)":
                        query += "ORDER BY p.Nama_Promo DESC ";
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

                if (selectedCategory != "")
                    cmd.Parameters.AddWithValue("@kategori", selectedCategory);

                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                dataGridViewItems.DataSource = dt;
                labelItemCount.Text = $"Showing {dt.Rows.Count} Promo";
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

                string query = "SELECT * from promo_special;";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridViewItems.DataSource = dt;

                //UpdateItemCount();
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

        //public void loadComboBox()
        //{
        //    connection.Open();

        //    string query = "SELECT Target_type FROM promo GROUP BY Target_type";
        //    MySqlCommand cmd = new MySqlCommand(query, connection);
        //    MySqlDataReader reader = cmd.ExecuteReader();
        //    DataTable dt = new DataTable();
        //    dt.Load(reader);

        //    // Create a "None" row
        //    DataRow noneRow = dt.NewRow();
        //    noneRow["Target_type"] = "";
        //    dt.Rows.InsertAt(noneRow, 0); // insert at top

        //    filterByComboBox.DisplayMember = "Target_type";
        //    filterByComboBox.ValueMember = "Target_type";
        //    filterByComboBox.DataSource = dt;

        //    filterByComboBox.SelectedIndex = 0; // Select "None"

        //    filterByComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

        //    connection.Close();
        //}

        

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

        private void AddBookButton_Click(object sender, EventArgs e)
        {
            AddSpecialPromoForm addItemForm = new AddSpecialPromoForm();
            addItemForm.FormClosed += (s, args) => refreshDGVList();
            addItemForm.ShowDialog();
        }
    }
}
