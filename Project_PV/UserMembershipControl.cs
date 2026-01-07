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
            ActivateMembership("Monthly");
        }

        private void buttonYearlyMember_Click(object sender, EventArgs e)
        {
            ActivateMembership("Yearly");
        }

        private void buttonPermanentMember_Click(object sender, EventArgs e)
        {
            ActivateMembership("Permanent");
        }

        // Membership Logic
        private void ActivateMembership(string planType)
        {
            string connectionString = "Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;";

            try
            {
                DateTime today = DateTime.Today;
                DateTime startDate;
                DateTime endDate;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // READ CURRENT MEMBERSHIP
                    string selectQuery = @"
                SELECT Is_Member, membership_start, membership_end
                FROM member
                WHERE ID = @id";

                    DateTime? dbStart = null;
                    DateTime? dbEnd = null;
                    bool isMember;

                    using (MySqlCommand selectCmd = new MySqlCommand(selectQuery, conn))
                    {
                        selectCmd.Parameters.AddWithValue("@id", GlobalData.UserID);

                        using (MySqlDataReader reader = selectCmd.ExecuteReader())
                        {
                            if (!reader.Read())
                                return;

                            isMember = reader.GetBoolean("Is_Member");

                            if (!reader.IsDBNull(reader.GetOrdinal("membership_start")))
                                dbStart = reader.GetDateTime("membership_start");

                            if (!reader.IsDBNull(reader.GetOrdinal("membership_end")))
                                dbEnd = reader.GetDateTime("membership_end");
                        }
                    }

                    // PERMANENT ALREADY → BLOCK
                    if (dbEnd == null && isMember)
                    {
                        MessageBox.Show("You already have a permanent membership.");
                        DisableAllButtons();
                        return;
                    }

                    // START DATE: SET ONLY ONCE
                    startDate = dbStart ?? today;

                    // END DATE LOGIC
                    if (planType == "Permanent")
                    {
                        endDate = DateTime.MinValue;
                    }
                    else
                    {
                        int addDays = planType == "Monthly" ? 30 : 365;

                        // If membership still active → extend from end
                        if (dbEnd != null && dbEnd.Value >= today)
                            endDate = dbEnd.Value.AddDays(addDays);
                        else
                            endDate = today.AddDays(addDays);
                    }

                    // UPDATE DB
                    string updateQuery = @"
                UPDATE member
                SET Is_Member = 1,
                    membership_start = IFNULL(membership_start, @start),
                    membership_end = @end
                WHERE ID = @id";

                    using (MySqlCommand updateCmd = new MySqlCommand(updateQuery, conn))
                    {
                        updateCmd.Parameters.AddWithValue("@id", GlobalData.UserID);
                        updateCmd.Parameters.AddWithValue("@start", startDate);

                        if (endDate == DateTime.MinValue)
                            updateCmd.Parameters.AddWithValue("@end", DBNull.Value);
                        else
                            updateCmd.Parameters.AddWithValue("@end", endDate);

                        updateCmd.ExecuteNonQuery();
                    }
                }

                // UPDATE GLOBAL DATA
                GlobalData.IsMember = true;
                GlobalData.MembershipStart = startDate;
                GlobalData.MembershipEnd = endDate;

                // UPDATE UI
                SetPicturebox();
                currentPlanIndicatorLabel.Text = GlobalData.GetMembershipDaysLeft();

                if (planType == "Permanent")
                    DisableAllButtons();

                MessageBox.Show($"{planType} membership updated successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Membership update failed: " + ex.Message);
            }
        }

        // Interface Methods
        private void PlanControl()
        {
            if (GlobalData.IsMember)
            {
                SetPicturebox();
                currentPlanIndicatorLabel.Text = GlobalData.GetMembershipDaysLeft();
            }
            else
            {
                currentPlanIndicatorLabel.Text = "Plan Duration: None";
            }

            if (GlobalData.MembershipEnd == DateTime.MinValue && GlobalData.IsMember)
            {
                DisableAllButtons();
            }
        }

        private void DisableAllButtons()
        {
            buttonMonthlyMember.BackColor = Color.LightGray;
            buttonYearlyMember.BackColor = Color.LightGray;
            buttonPermanentMember.BackColor = Color.LightGray;

            buttonMonthlyMember.ForeColor = Color.Black;
            buttonYearlyMember.ForeColor = Color.Black;
            buttonPermanentMember.ForeColor = Color.Black;

            buttonMonthlyMember.Enabled = false;
            buttonYearlyMember.Enabled = false;
            buttonPermanentMember.Enabled = false;
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
