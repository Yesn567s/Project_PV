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
        public AddPromoForm()
        {
            InitializeComponent();
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            string namaPromo = textBox1.Text.Trim();
            string produk = textBox2.Text;          // misal memilih nama produk
            string jenis = comboBox1.Text;            // misal: Diskon / Cashback / Buy 1 Get 1
            float promo = (float)numericUpDown1.Value;      // nilai promo (misal 20% / 10000 / dll)
            string tag = textBox3.Text; // gabung tag jadi 1 kolom

            if (namaPromo == "")
            {
                MessageBox.Show("Nama promo wajib diisi");
                return;
            }

            using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;"))
            {
                conn.Open();

                string query = "INSERT INTO Promo (Nama_Promo, Produk, Jenis, Promo, Tag) VALUES (@namaPromo, @produk, @jenis, @promo, @tag)";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("@namaPromo", namaPromo);
                cmd.Parameters.AddWithValue("@produk", produk);
                cmd.Parameters.AddWithValue("@jenis", jenis);
                cmd.Parameters.AddWithValue("@promo", promo);
                cmd.Parameters.AddWithValue("@tag", tag);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Promo berhasil ditambahkan!");
                this.Close();
            }

        }
    }
}
