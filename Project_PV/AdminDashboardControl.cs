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
        public AdminDashboardControl()
        {
            InitializeComponent();
            connectDatabase();
            refreshDGVListMembers();
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
