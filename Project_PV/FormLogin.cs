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
    public partial class FormLogin : Form
    {
        string connectionString = "Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;";
        public FormLogin()
        {
            InitializeComponent();
        }

        private void createAccountLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (this == null) return;
            FormRegister registerForm = new FormRegister();
            registerForm.Show();
            this.Hide();
        }

        private void signInButton_Click(object sender, EventArgs e)
        {
            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;
            bool rememberMe = rememberCheckBox.Checked;

            if ((email == "Admin" || email == "admin") && (password == "Admin" || password == "admin"))
            {
                AdminDashboard adminDashboard = new AdminDashboard();
                adminDashboard.Show();
                this.Hide();
            }
            else
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();

                        string query = @"
                SELECT ID, Nama, Email, Tanggal_Lahir, Is_Member, membership_start, membership_end
                FROM MEMBER
                WHERE Email = @Email AND Password = @Password";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Email", email);
                            cmd.Parameters.AddWithValue("@Password", password);

                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Check and Expire Membership if needed
                                    GlobalData.CheckAndExpireMembership(connectionString);

                                    // Store to GlobalData
                                    GlobalData.UserID = reader.GetInt32(0);
                                    GlobalData.Nama = reader.GetString(1);
                                    GlobalData.Email = reader.GetString(2);
                                    GlobalData.TanggalLahir = reader.GetDateTime(3);
                                    GlobalData.IsMember = reader.GetBoolean(4);
                                    GlobalData.MembershipStart = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5);
                                    GlobalData.MembershipEnd = reader.IsDBNull(6) ? DateTime.MinValue : reader.GetDateTime(6);

                                    //MessageBox.Show(GlobalData.UserID.ToString());

                                    UserDashboard userDashboard = new UserDashboard();
                                    userDashboard.Show();
                                    this.Hide();
                                }
                                else
                                {
                                    MessageBox.Show(
                                        "Invalid email or password.",
                                        "Login Failed",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error
                                    );
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
