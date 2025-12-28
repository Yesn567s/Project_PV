using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_PV
{
    internal class GlobalData
    {
        public static int UserID { get; set; }
        public static string Nama { get; set; }
        public static DateTime TanggalLahir { get; set; }
        public static bool IsMember { get; set; }
        public static string Email { get; set; }

        public static void Clear()
        {
            UserID = 0;
            Nama = null;
            Email = null;
            IsMember = false;
            TanggalLahir = DateTime.MinValue;
        }
    }
}
