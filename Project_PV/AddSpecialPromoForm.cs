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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project_PV
{
    public partial class AddSpecialPromoForm : Form
    {
        string connectionString = "Server=localhost;Database=db_proyek_pv;UserID=root;Password=;";
        public AddSpecialPromoForm()
        {
            InitializeComponent();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            int hour1, hour2, minute1, minute2;
            hour1 = (int)numericUpDown9.Value;
            hour2 = (int)numericUpDown10.Value;
            minute1 = (int)numericUpDown11.Value;
            minute2 = (int)numericUpDown12.Value;
            if (textBox1.Text == "" || comboBox1.Text == "")
            {
                MessageBox.Show("Lengkapi data promo terlebih dahulu");
                return;
            }
            else if (dateTimePicker2.Value <= dateTimePicker1.Value)
            {
                MessageBox.Show("Tanggal akhir harus setelah tanggal mulai");
                return;
            }
            else if (hour1 == 24 && minute1 > 0 || hour2 == 24 && minute2 > 0)
            {
                MessageBox.Show("Tidak ada jam lebih dari 24:00");
                return;
            }
            try
            {
                DateTime date1 = dateTimePicker1.Value.Date.Add(new TimeSpan(hour1, minute1, 0));
                DateTime date2 = dateTimePicker2.Value.Date.Add(new TimeSpan(hour2, minute2, 0));
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "insert into promo_special (Nama_Promo, Kategori, Keterangan, START, END) Values (@namaPromo, @kategori, @keterangan, @start, @end)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@namaPromo", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@kategori", comboBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@keterangan", textBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@start", date1);
                    cmd.Parameters.AddWithValue("@end", date2);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Special Promo berhasil ditambahkan!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }
    }
}
