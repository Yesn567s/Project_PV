using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Project_PV
{
    public partial class AdminDashboardControl : UserControl
    {
        string connectionString = "Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;";
        MySqlConnection connection;

        private int monthlyProfit = 0;
        public AdminDashboardControl()
        {
            InitializeComponent();
            connectDatabase();
            refreshDGVListMembers();
            LoadRecentOrders();
            LoadTopSellingProducts();

            UpdateInventoryCount();
            UpdateCustomerCount();
            LoadTodayOrdersCount();
            LoadSalesToday();

            LoadMonthlyProfit();
            UpdateProgressBar();
        }

        private void LoadMonthlyProfit()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
            SELECT IFNULL(SUM(Total),0) 
            FROM Transaksi
            WHERE MONTH(Tanggal) = MONTH(CURDATE())
            AND YEAR(Tanggal) = YEAR(CURDATE())
        ";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                monthlyProfit = Convert.ToInt32(cmd.ExecuteScalar());
                labelCurrentProfit.Text = "Rp " + monthlyProfit.ToString("N0");
            }
        }

        private void UpdateProgressBar()
        {
            int goal = 10000000; // Rp 10.000.000
            int percent = (int)((monthlyProfit / (double)goal) * 100);

            if (percent > 100) percent = 100;

            progressBarSale.Value = percent;
        }


        private void UpdateInventoryCount()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM Produk";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    labelInventoryCount.Text = $"{count} Items";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateCustomerCount()
        {
            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;"))
            {
                conn.Open();

                string query = "SELECT COUNT(*) FROM Member";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                labelCustomerCount.Text = $"{count} Members";
            }
        }

        private void LoadTodayOrdersCount()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM Transaksi WHERE DATE(Tanggal) = CURDATE()";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                labelRecentOrders.Text = "Today's Orders: " + count;
            }
        }

        private void LoadSalesToday()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT IFNULL(SUM(Total), 0) FROM Transaksi WHERE DATE(Tanggal) = CURDATE()";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                decimal total = Convert.ToDecimal(cmd.ExecuteScalar());
                salesTodayLabel.Text = $"Rp {total:N0}";
            }
        }

        public void LoadRecentOrders()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query = @"
                SELECT 
                t.ID AS transaction_id,
                COALESCE(m.Nama, 'Guest') AS member_name,
                t.Total,
                t.Tanggal
                FROM Transaksi t
                LEFT JOIN Member m ON t.member_id = m.ID
                ORDER BY t.Tanggal DESC
                LIMIT 10;
                ";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                dataGridViewMembersList.DataSource = dt;
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

        public void LoadTopSellingProducts()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query =
                    "SELECT p.ID, p.Nama AS Produk, " +
                    "SUM(td.Qty) AS TotalTerjual, " +
                    "SUM(td.Qty * td.Harga) AS TotalPendapatan " +
                    "FROM Transaksi_Detail td " +
                    "JOIN Produk p ON td.produk_id = p.ID " +
                    "GROUP BY p.ID, p.Nama " +
                    "ORDER BY TotalTerjual DESC " +
                    "LIMIT 10;";

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);

                dataGridViewTopSellingBooks.DataSource = dt;
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

        public void refreshDGVListMembers()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                string query = "SELECT * FROM member";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridViewRecentOrders.DataSource = dt;
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
