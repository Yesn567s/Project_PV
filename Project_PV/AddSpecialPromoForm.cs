using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Project_PV
{
    public partial class AddSpecialPromoForm : Form
    {
        string connectionString = "Server=localhost;Database=db_proyek_pv;UserID=root;Password=;";
        public AddSpecialPromoForm()
        {
            InitializeComponent();
            comboBox1.Items.Add("YesNo");
            comboBox1.Items.Add("Input");
            comboBox1.SelectedIndex = 0;
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            int hour1, hour2, minute1, minute2;
            hour1 = (int)numericUpDown9.Value;
            hour2 = (int)numericUpDown10.Value;
            minute1 = (int)numericUpDown11.Value;
            minute2 = (int)numericUpDown12.Value;
            if (textBox1.Text == "" || richTextBox1.Text=="" || comboBox1.Text == "" || numericUpDown1.Value==0)
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Kalau tidak ada nama taruh default 'Promo Biasa'");
                    return;
                }
                if(numericUpDown1.Value == 0)
                {
                    MessageBox.Show("Kalau berlaku ke semua isi '-1'");
                    return;
                }
                else
                {
                    MessageBox.Show("Lengkapi data promo terlebih dahulu");
                    return;
                }
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
                    string query = "insert into promo_special (Nama_Promo, Kategori, Keterangan,Berlaku, START, END) Values (@namaPromo, @kategori, @keterangan,@berlaku, @start, @end)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@namaPromo", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@kategori", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@keterangan", richTextBox1.Text);
                    cmd.Parameters.AddWithValue("@berlaku", (int)numericUpDown1.Value);
                    cmd.Parameters.AddWithValue("@start", date1);
                    cmd.Parameters.AddWithValue("@end", date2);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Promo Special berhasil ditambahkan!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown9_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown11_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown12_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown10_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
