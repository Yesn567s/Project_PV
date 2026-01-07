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
            int hour1 = (int)numericUpDown9.Value;
            int hour2 = (int)numericUpDown10.Value;
            int minute1 = (int)numericUpDown11.Value;
            int minute2 = (int)numericUpDown12.Value;

            // BASIC VALIDATION
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Kalau tidak ada nama taruh default 'Promo Biasa'");
                return;
            }

            if (comboBox2.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("Lengkapi data promo terlebih dahulu");
                return;
            }

            if (dateTimePicker2.Value <= dateTimePicker1.Value)
            {
                MessageBox.Show("Tanggal akhir harus setelah tanggal mulai");
                return;
            }

            if ((hour1 == 24 && minute1 > 0) || (hour2 == 24 && minute2 > 0))
            {
                MessageBox.Show("Tidak ada jam lebih dari 24:00");
                return;
            }

            DateTime startDate = dateTimePicker1.Value.Date.Add(new TimeSpan(hour1, minute1, 0));
            DateTime endDate = dateTimePicker2.Value.Date.Add(new TimeSpan(hour2, minute2, 0));

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = @"INSERT INTO promo
            (Nama_Promo, Target_Type, Target_Value, Jenis_Promo,
             Nilai_Potongan, Harga_Baru, Min_Qty, Bonus_Produk_ID, Gratis_Qty, START, END)
            VALUES
            (@namaPromo, @targetType, @targetValue, @jenisPromo,
             @nilaiPotongan, @hargaBaru, @minQty, @bonusProdukID, @gratisQty, @start, @end)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    // REQUIRED
                    cmd.Parameters.AddWithValue("@namaPromo", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@targetType", comboBox2.Text);
                    cmd.Parameters.AddWithValue("@targetValue",
                        comboBox2.Text == "Tag" ? textBox2.Text.Trim() : comboBox1.Text);
                    cmd.Parameters.AddWithValue("@jenisPromo", comboBox3.Text);
                    cmd.Parameters.AddWithValue("@start", startDate);
                    cmd.Parameters.AddWithValue("@end", endDate);

                    // DEFAULTS (IMPORTANT)
                    cmd.Parameters.AddWithValue("@nilaiPotongan", 0);
                    cmd.Parameters.AddWithValue("@hargaBaru", DBNull.Value);
                    cmd.Parameters.AddWithValue("@minQty", 1);
                    cmd.Parameters.AddWithValue("@bonusProdukID", DBNull.Value);
                    cmd.Parameters.AddWithValue("@gratisQty", 0);

                    // PROMO LOGIC
                    switch (comboBox3.Text)
                    {
                        case "Persen":
                            cmd.Parameters["@nilaiPotongan"].Value = (float)numericUpDown5.Value;
                            cmd.Parameters["@minQty"].Value = (int)numericUpDown8.Value;
                            break;

                        case "Harga_Jadi":
                            cmd.Parameters["@hargaBaru"].Value = (int)numericUpDown7.Value;
                            cmd.Parameters["@minQty"].Value = (int)numericUpDown6.Value;
                            break;

                        case "Grosir":
                            cmd.Parameters["@hargaBaru"].Value = (int)numericUpDown3.Value;
                            cmd.Parameters["@minQty"].Value = (int)numericUpDown4.Value;
                            break;

                        case "Bonus":
                            cmd.Parameters["@minQty"].Value = (int)numericUpDown1.Value;
                            cmd.Parameters["@bonusProdukID"].Value = comboBox4.SelectedValue;
                            cmd.Parameters["@gratisQty"].Value = (int)numericUpDown2.Value;
                            break;
                    }

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Promo berhasil ditambahkan!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
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
            numericUpDown1.Value = 1;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 1;
            numericUpDown5.Value = 0;
            numericUpDown6.Value = 1;
            numericUpDown7.Value = 0;
            numericUpDown8.Value = 1;
            comboBox4.Text = "";
            if (comboBox3.Text == "Bonus")
            {
                panel1.Enabled = true;
                panel2.Enabled = false;
                panel3.Enabled = false;
                panel4.Enabled = false;
            }
            else if (comboBox3.Text == "Grosir")
            {
                panel1.Enabled = false;
                panel2.Enabled = true;
                panel3.Enabled = false;
                panel4.Enabled = false;
            }
            else if (comboBox3.Text == "Harga_Jadi")
            {
                panel1.Enabled = false;
                panel2.Enabled = false;
                panel3.Enabled = true;
                panel4.Enabled = false;
            }
            else if (comboBox3.Text == "Persen")
            {
                panel1.Enabled = false;
                panel2.Enabled = false;
                panel3.Enabled = false;
                panel4.Enabled = true;
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
            panel1.Enabled = false;
            panel2.Enabled = false;
            panel3.Enabled = false;
            panel4.Enabled = false;
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
