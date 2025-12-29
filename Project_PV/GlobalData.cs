using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_PV
{
    internal class GlobalData
    {
        public static int UserID { get; set; }
        public static string Nama { get; set; }
        public static DateTime TanggalLahir { get; set; }
        public static bool IsMember { get; set; }
        public static string Email { get; set; }
        public static DateTime MembershipStart { get; set; }
        public static DateTime MembershipEnd { get; set; }

        public static void Clear()
        {
            UserID = 0;
            Nama = null;
            Email = null;
            IsMember = false;
            TanggalLahir = DateTime.MinValue;
            MembershipStart = DateTime.MinValue;
            MembershipEnd = DateTime.MinValue;
        }

        public static string GetMembershipDaysLeft() // dipake di UserMembershipControl.cs
        {
            // If user is not a member, no time left
            if (!IsMember)
                return "Plan Duration: None";

            // If the membership is permanent
            if (MembershipEnd == DateTime.MinValue)
                return "Plan Duration: Permanent";

            // Calculate remaining days
            TimeSpan remaining = MembershipEnd.Date - DateTime.Today;

            // If already expired
            if (remaining.TotalDays <= 0)
                return "Plan Duration: None";

            int n = (int)Math.Ceiling(remaining.TotalDays);

            return "Plan Duration: " + n.ToString() + " Days";
        }

        public static void CheckAndExpireMembership(string connectionString) // dipake di FormLogin.cs (setelah login sukses)
        {
            // Only check members
            if (!IsMember || MembershipEnd == DateTime.MinValue)
                return;

            // Expired
            if (DateTime.Today > MembershipEnd.Date)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();

                        string query = @"
                    UPDATE MEMBER
                    SET Is_Member = 0,
                        MembershipStart = NULL,
                        MembershipEnd = NULL
                    WHERE ID = @UserID";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserID", UserID);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Update GlobalData
                    IsMember = false;
                    MembershipStart = DateTime.MinValue;
                    MembershipEnd = DateTime.MinValue;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failed to update membership status: " + ex.Message);
                }
            }
        }

    }
}
