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
    public partial class FormLogin : Form
    {
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
                // Here you would typically validate the user credentials against a database (database is still not build yet)
                UserDashboard userDashboard = new UserDashboard();
                userDashboard.Show();
                this.Hide();
            }
        }
    }
}
