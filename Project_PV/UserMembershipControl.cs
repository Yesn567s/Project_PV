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
    public partial class UserMembershipControl : UserControl
    {
        public UserMembershipControl()
        {
            InitializeComponent();
            PlanControl();
        }

        // Button Click Events
        private void buttonMonthlyMember_Click(object sender, EventArgs e)
        {
            SetPicturebox();
        }

        private void buttonYearlyMember_Click(object sender, EventArgs e)
        {
            SetPicturebox();
        }

        private void buttonPermanentMember_Click(object sender, EventArgs e)
        {
            SetPicturebox();
            buttonMonthlyMember.Enabled = false;
            buttonYearlyMember.Enabled = false;
            buttonPermanentMember.Enabled = false;
        }

        // Interface Methods
        private void PlanControl()
        {
            if (GlobalData.IsMember)
            {
                SetPicturebox();
            }
            currentPlanIndicatorLabel.Text = GlobalData.GetMembershipDaysLeft();
            //MessageBox.Show(GlobalData.GetMembershipDaysLeft());
        }

        private void SetPicturebox()
        {
            string imagePath = System.IO.Path.Combine(
                Application.StartupPath,
                "Images",
                "checkmark.png"
            );

            pictureBox1.Image = Image.FromFile(imagePath);
            pictureBox2.Image = Image.FromFile(imagePath);
            pictureBox3.Image = Image.FromFile(imagePath);
        }

    }
}
