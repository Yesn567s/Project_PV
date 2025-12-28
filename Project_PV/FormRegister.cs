using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Net.Mail;

namespace Project_PV
{
    public partial class FormRegister : Form
    {
        string connectionString = "Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;";
        public FormRegister()
        {
            InitializeComponent();
        }

        private void signInLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (e.Link != null)
            {
                FormLogin loginForm = new FormLogin();
                loginForm.Show();
                this.Hide();
            }
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            string username = nameTextBox.Text;
            string email = emailTextBox.Text;
            string password = passwordTextBox.Text;
            string confirmPassword = confirmPasswordTextBox.Text;
            DateTime tanggal_lahir = dateTimePicker1.Value;
            bool termsAccepted = termsCheckBox.Checked;

            // Validation
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Passwords do not match. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsValidEmail(email) == false)
            {
                MessageBox.Show("Invalid email format. Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (termsAccepted == false)
            {
                MessageBox.Show("Please Accept the terms and condition first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Insert into database
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"
                INSERT INTO MEMBER (Nama, Email, Password, Tanggal_Lahir, Is_Member)
                VALUES (@Nama, @Email, @Password, @Tanggal_Lahir, @Is_Member)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nama", username);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Password", password); 
                        cmd.Parameters.AddWithValue("@Tanggal_Lahir", tanggal_lahir);
                        cmd.Parameters.AddWithValue("@Is_Member", false); // default non-member

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Redirect to login
                FormLogin loginForm = new FormLogin();
                loginForm.Show();
                this.Hide();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Database error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValidEmail(string email) // Helper method to validate email format
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}