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
    public partial class UserDashboardControl : UserControl
    {
        // Events to notify parent form
        // These events will be handled in UserDashboard to load respective controls when buttons are clicked in this UserControl
        // For example, when Explore Membership button is clicked, UserDashboard will load UserMembershipControl
        public event EventHandler ExploreMembershipClicked;
        public event EventHandler BrowseCatalogClicked;
        public UserDashboardControl()
        {
            InitializeComponent();
        }

        // Button Click Handlers
        // Membership Section
        private void exploreMembershipBtn_Click(object sender, EventArgs e)
        {
            ExploreMembershipClicked?.Invoke(this, EventArgs.Empty); // Raise event to notify parent form
        }

        private void joinMembershipButton_Click(object sender, EventArgs e)
        {
            ExploreMembershipClicked?.Invoke(this, EventArgs.Empty);
        }

        // Catalog Section
        private void browseCatalogButton_Click(object sender, EventArgs e)
        {
            BrowseCatalogClicked?.Invoke(this, EventArgs.Empty);
        }

        private void viewAllButton_Click(object sender, EventArgs e)
        {
            BrowseCatalogClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
