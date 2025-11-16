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
    public partial class UserCartControl : UserControl
    {
        public UserCartControl()
        {
            InitializeComponent();
            if (true) // IF Cart empty
            {
                panel1.Location = new Point(0, 0);
                panel1.Dock = DockStyle.Fill;
                panel1.Visible = true;
            }
        }
    }
}
