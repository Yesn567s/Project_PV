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
    public partial class AdminDashboard : Form
    {
        // Admin Controls
        private AdminDashboardControl adminDashboardControl = new AdminDashboardControl();
        private AdminManagementControl adminManagementControl = new AdminManagementControl();
        private AdminPromoManagement adminPromoControl = new AdminPromoManagement();
        public AdminDashboard()
        {
            InitializeComponent();
            button1.FlatStyle = FlatStyle.Flat;
            button2.FlatStyle = FlatStyle.Flat;
            buttonPromo.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;
            button2.FlatAppearance.BorderSize = 0;
            buttonPromo.FlatAppearance.BorderSize = 0;
            btnControl_design(button1);
            LoadDashboard();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadDashboard();
            btnControl_design(button1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LoadManagement();
            btnControl_design(button2);
        }

        private void buttonPromo_Click(object sender, EventArgs e)
        {
            LoadPromoManagement();
            btnControl_design(buttonPromo);
        }

        // Load User Controls on Button Click
        private void LoadUserControl(UserControl control)
        {
            ContentPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            ContentPanel.Controls.Add(control);
        }

        private void LoadDashboard()
        {
            if (adminDashboardControl == null)
            {
                adminDashboardControl = new AdminDashboardControl();
            }
            LoadUserControl(adminDashboardControl);
        }

        private void LoadManagement()
        {
            if (adminManagementControl == null)
            {
                adminManagementControl = new AdminManagementControl();
            }
            LoadUserControl(adminManagementControl);
        }

        private void LoadPromoManagement()
        {
            if (adminPromoControl == null)
            {
                adminPromoControl = new AdminPromoManagement();
            }
            LoadUserControl(adminPromoControl);
        }

        private void btnControl_design(Button activeButton)
        {
            // Reset all buttons to default style
            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;
            button2.BackColor = Color.White;
            button2.ForeColor = Color.Black;
            buttonPromo.BackColor = Color.White;
            buttonPromo.ForeColor = Color.Black;

            // Highlight the active button
            activeButton.BackColor = Color.Black;
            activeButton.ForeColor = Color.White;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FormLogin loginForm = new FormLogin();
            loginForm.Show();
            this.Hide();
        }

        
    }
}
