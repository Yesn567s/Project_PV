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
        public AdminDashboard()
        {
            InitializeComponent();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            bookManagementPanel.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bookManagementPanel.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bookManagementPanel.Show();
        }
    }
}
