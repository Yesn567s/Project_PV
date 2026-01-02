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
    public partial class AddPromoForm : Form
    {
        string connectionString = "Server=localhost;Database=db_proyek_pv;UserID=root;Password=;";
        public AddPromoForm()
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
            if (textBox1.Text=="" || comboBox2.Text=="" || comboBox3.Text == "" || comboBox1.Text=="" )
            {
                if(textBox1.Text == "")
                {
                    MessageBox.Show("Kalau tidak ada nama taruh default 'Promo Biasa'");
                    return;
                }
                else
                {
                    MessageBox.Show("Lengkapi data promo terlebih dahulu");
                    return;
                }
            }
            else if (comboBox3.Text == "Bonus")
            {
                if (comboBox4.Text == "" || numericUpDown1.Value <= 0 || numericUpDown2.Value == 0)
                {
                    MessageBox.Show("Lengkapi data promo terlebih dahulu");
                    return;
                }
            }
            else if (comboBox3.Text == "Grosir")
            {
                if (numericUpDown3.Value == 0 || numericUpDown4.Value <= 0)
                {
                    MessageBox.Show("Lengkapi data promo terlebih dahulu");
                    return;
                }
            }
            else if (comboBox3.Text == "Harga_Jadi")
            {
                if (numericUpDown5.Value == 0 || numericUpDown6.Value <= 0)
                {
                    MessageBox.Show("Lengkapi data promo terlebih dahulu");
                    return;
                }
            }
            else if (comboBox3.Text == "Persen")
            {
                if (numericUpDown7.Value == 0 || numericUpDown8.Value <= 0)
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
            //string temp=dateTimePicker1.Value.ToString("yyyy-MM-dd") + " " + hour1.ToString("D2") + ":" + minute1.ToString("D2") + ":00";
            //DateTime date1=DateTime.Parse(temp);
            //temp=dateTimePicker2.Value.ToString("yyyy-MM-dd") + " " + hour2.ToString("D2") + ":" + minute2.ToString("D2") + ":00";
            //DateTime date2=DateTime.Parse(temp);
            DateTime date1 = dateTimePicker1.Value.Date.Add(new TimeSpan(hour1, minute1, 0));
            DateTime date2 = dateTimePicker2.Value.Date.Add(new TimeSpan(hour2, minute2, 0));
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "insert into promo (Nama_Promo, Target_Type, Target_Value, Jenis_Promo, Nilai_Potongan, Harga_Baru, Min_Qty, Bonus_Produk_ID, Gratis_Qty, START, END) Values (@namaPromo, @targetType, @targetValue, @jenisPromo, @nilaiPotongan, @hargaBaru, @minQty, @bonusProdukID, @gratisQty, @start, @end)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@namaPromo", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@targetType", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@targetValue", comboBox2.Text == "Tag" ? textBox2.Text.Trim() : comboBox1.Text);
                    cmd.Parameters.AddWithValue("@jenisPromo", comboBox3.Text);
                    cmd.Parameters.AddWithValue("@nilaiPotongan", comboBox3.Text == "Persen" ? (float)numericUpDown7.Value : 0);
                    cmd.Parameters.AddWithValue("@hargaBaru", comboBox3.Text == "Harga_Jadi" ? (float)numericUpDown6.Value : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@minQty", numericUpDown4.Value==0 ? 0:0);
                    cmd.Parameters.AddWithValue("@bonusProdukID", comboBox3.Text == "Bonus" ? (int)comboBox4.SelectedValue : (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@gratisQty", comboBox3.Text == "Bonus" ? (int)numericUpDown2.Value : 0);
                    cmd.Parameters.AddWithValue("@start", date1);
                    cmd.Parameters.AddWithValue("@end", date2);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Promo berhasil ditambahkan!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

            //string namaPromo = textBox1.Text.Trim();
            //string produk = textBox2.Text;          // misal memilih nama produk
            //string jenis = comboBox1.Text;            // misal: Diskon / Cashback / Buy 1 Get 1
            //float promo = (float)numericUpDown1.Value;      // nilai promo (misal 20% / 10000 / dll)
            //string tag = textBox3.Text; // gabung tag jadi 1 kolom

            //if (namaPromo == "")
            //{
            //    MessageBox.Show("Nama promo wajib diisi");
            //    return;
            //}

            //using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;"))
            //{
            //    conn.Open();

            //    string query = "INSERT INTO Promo (Nama_Promo, Produk, Jenis, Promo, Tag) VALUES (@namaPromo, @produk, @jenis, @promo, @tag)";
            //    MySqlCommand cmd = new MySqlCommand(query, conn);

            //    cmd.Parameters.AddWithValue("@namaPromo", namaPromo);
            //    cmd.Parameters.AddWithValue("@produk", produk);
            //    cmd.Parameters.AddWithValue("@jenis", jenis);
            //    cmd.Parameters.AddWithValue("@promo", promo);
            //    cmd.Parameters.AddWithValue("@tag", tag);

            //    cmd.ExecuteNonQuery();

            //    MessageBox.Show("Promo berhasil ditambahkan!");
            //    this.Close();
            //}

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
            if (comboBox1.Items != null)
            {
                comboBox1.Items.Clear();
            }
            comboBox1.Show();
            textBox2.Hide();
            if (comboBox2.SelectedItem.ToString() == "Global")
            {
                comboBox1.Items.Add("Semua Item");
            }
            else if (comboBox2.SelectedItem.ToString() == "Kategori")
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT Nama FROM kategori";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox1.Items.Add(reader.GetString("Nama"));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else if (comboBox2.SelectedItem.ToString() == "Merk")
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT distinct Merk FROM produk";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox1.Items.Add(reader.GetString("Merk"));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else if (comboBox2.SelectedItem.ToString() == "Produk")
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "SELECT distinct Nama FROM produk";
                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                comboBox1.Items.Add(reader.GetString("Nama"));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            else if (comboBox2.SelectedItem.ToString() == "Tag")
            {
                comboBox1.Hide();
                textBox2.Show();
            }
            comboBox1.Text = "";
            textBox2.Text = "";
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
            numericUpDown6.Value = 0;
            numericUpDown7.Value = 0;
            comboBox4.Text = "";
            if (comboBox3.Text == "Bonus")
            {
                panel1.Show();
                panel2.Hide();
                panel3.Hide();
                panel4.Hide();
            }
            else if (comboBox3.Text == "Grosir")
            {
                panel1.Hide();
                panel2.Show();
                panel3.Hide();
                panel4.Hide();
            }
            else if (comboBox3.Text == "Harga_Jadi")
            {
                panel1.Hide();
                panel2.Hide();
                panel3.Show();
                panel4.Hide();
            }
            else if (comboBox3.Text == "Persen")
            {
                panel1.Hide();
                panel2.Hide();
                panel3.Hide();
                panel4.Show();
            }
        }

        private void AddPromoForm_Load(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox2.Items.Add("Global");
            comboBox2.Items.Add("Kategori");
            comboBox2.Items.Add("Merk");
            comboBox2.Items.Add("Produk");
            comboBox2.Items.Add("Tag");
            comboBox3.Items.Add("Bonus");
            comboBox3.Items.Add("Grosir");
            comboBox3.Items.Add("Harga_Jadi");
            comboBox3.Items.Add("Persen");
            textBox2.Hide();
            panel1.Show();
            panel2.Hide();
            panel3.Hide();
            panel4.Hide();
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            textBox1.Text = "Promo Biasa";
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT ID,Nama FROM produk";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    comboBox4.DataSource = dt;
                    comboBox4.DisplayMember = "Nama";
                    comboBox4.ValueMember = "ID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
