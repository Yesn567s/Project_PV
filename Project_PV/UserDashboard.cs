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
    public partial class UserDashboard : Form
    {
        // User Controls
        private UserCartControl user_cart_control;
        private UserCatalogControl user_catalog_control;
        private UserAccessoriesControl user_accessories_control;
        private UserDashboardControl user_dashboard_control;
        private UserMembershipControl user_membership_control;
        public UserDashboard()
        {
            InitializeComponent();
            LoadDashboard();
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            btnControl_design(homeButton);
            LoadDashboard();
        }

        private void catalogButton_Click(object sender, EventArgs e)
        {
            btnControl_design(catalogButton);
            LoadCatalog();
        }

        private void accessoriesButton_Click(object sender, EventArgs e)
        {
            btnControl_design(accessoriesButton);
            LoadAccessories();
        }

        private void membershipButton_Click(object sender, EventArgs e)
        {
            btnControl_design(membershipButton);
            LoadMembership();
        }

        private void cartButton_Click(object sender, EventArgs e)
        {
            btnControl_design(cartButton);
            LoadCart();
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin loginForm = new FormLogin();
            loginForm.Show();
        }

        // Load User Controls on Button Click
        private void LoadUserControl(UserControl control)
        {
            contentPanel.Controls.Clear();
            control.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(control);
        }

        private void LoadDashboard()
        {
            if (user_dashboard_control == null)
            {
                user_dashboard_control = new UserDashboardControl();
            }
            LoadUserControl(user_dashboard_control);
        }

        private void LoadCatalog()
        {
            if (user_catalog_control == null)
            {
                user_catalog_control = new UserCatalogControl();
            }
            LoadUserControl(user_catalog_control);
        }

        private void LoadAccessories()
        {
            if (user_accessories_control == null)
            {
                user_accessories_control = new UserAccessoriesControl();
            }
            LoadUserControl(user_accessories_control);
        }

        private void LoadCart()
        {
            if (user_cart_control == null)
            {
                user_cart_control = new UserCartControl();
            }
            LoadUserControl(user_cart_control);
        }

        private void LoadMembership()
        {
            if (user_membership_control == null)
            {
                user_membership_control = new UserMembershipControl();
            }
            LoadUserControl(user_membership_control);
        }

        private void btnControl_design(Button button_name)
        {
            // Reset all button colors
            homeButton.BackColor = Color.Transparent;
            catalogButton.BackColor = Color.Transparent;
            accessoriesButton.BackColor = Color.Transparent;
            cartButton.BackColor = Color.Transparent;
            membershipButton.BackColor = Color.Transparent;

            homeButton.ForeColor = Color.FromArgb(60, 60, 60);
            catalogButton.ForeColor = Color.FromArgb(60, 60, 60);
            accessoriesButton.ForeColor = Color.FromArgb(60, 60, 60);
            cartButton.ForeColor = Color.FromArgb(60, 60, 60);
            membershipButton.ForeColor = Color.FromArgb(60, 60, 60);

            // Set the selected button color
            button_name.BackColor = Color.FromArgb(20, 20, 40);
            button_name.ForeColor = Color.White;
        }
    }
}
