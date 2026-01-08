using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace Project_PV
{
    public partial class FormLaporanTransaksi : Form
    {
        private string connectionString = "Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;";

        public FormLaporanTransaksi()
        {
            InitializeComponent();
            LoadMonthYearOptions();
        }

        private void LoadMonthYearOptions()
        {
            // Populate year dropdown (last 5 years)
            int currentYear = DateTime.Now.Year;
            for (int i = 0; i < 5; i++)
            {
                comboBoxYear.Items.Add(currentYear - i);
            }
            comboBoxYear.SelectedIndex = 0;

            // Populate month dropdown
            for (int i = 1; i <= 12; i++)
            {
                comboBoxMonth.Items.Add(new DateTime(currentYear, i, 1).ToString("MMMM"));
            }
            comboBoxMonth.SelectedIndex = DateTime.Now.Month - 1;

            // Report type options
            comboBoxReportType.Items.Add("Transaksi Non-Promo");
            comboBoxReportType.Items.Add("Transaksi Promo Member");
            comboBoxReportType.Items.Add("Transaksi Promo Spesial");
            comboBoxReportType.Items.Add("Transaksi Promo");
            comboBoxReportType.SelectedIndex = 0;
        }

        private void buttonGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                int year = Convert.ToInt32(comboBoxYear.SelectedItem);
                int month = comboBoxMonth.SelectedIndex + 1;
                string reportType = comboBoxReportType.SelectedItem.ToString();

                DataSet ds = GetMonthlySalesData(year, month, reportType);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show(
                        $"No sales data found for {comboBoxMonth.SelectedItem} {year}",
                        "No Data",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                    return;
                }

                // Show preview form
                FormLaporanPenjualanBulananPreview previewForm =
                    new FormLaporanPenjualanBulananPreview(ds, reportType, month, year);
                previewForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error generating report: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private DataSet GetMonthlySalesData(int year, int month, string reportType)
        {
            // Create properly named DataSet for Crystal Reports
            DataSet ds = new DataSet("DataSetLaporanBulanan");

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                switch (reportType)
                {
                    case "Transaksi Non-Promo":
                        LoadNonPromo(conn, ds, year, month);
                        break;
                    case "Transaksi Promo Member":
                        LoadPromoMember(conn, ds, year, month);
                        break;
                    case "Transaksi Promo Spesial":
                        LoadPromoSpesial(conn, ds, year, month);
                        break;
                    case "Transaksi Promo":
                        LoadPromo(conn, ds, year, month);
                        break;
                }
            }

            return ds;
        }

        private void LoadNonPromo(MySqlConnection conn, DataSet ds, int year, int month)
        {
            // Main summary data
            string query = @"
                SELECT 
                    t.ID as TransID,
                    t.Tanggal as TransDate,
                    COALESCE(m.Nama, 'Guest') as CustomerName,
                    p.Nama as ProductName,
                    p.Merk as Brand,
                    k.Nama as Category,
                    td.Qty as Quantity,
                    td.Harga as UnitPrice,
                    td.Subtotal,
                    td.Diskon,
                    td.Diskon_Spesial
                FROM Transaksi t
                LEFT JOIN Member m ON t.member_id = m.ID
                INNER JOIN Transaksi_Detail td ON t.ID = td.transaksi_id
                INNER JOIN Produk p ON td.produk_id = p.ID
                INNER JOIN Kategori k ON p.kategori_id = k.ID
                WHERE YEAR(t.Tanggal) = @year AND MONTH(t.Tanggal) = @month AND td.diskon = 0 AND td.diskon_spesial = 0
                ORDER BY t.Tanggal DESC, t.ID";


            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@year", year);
            adapter.SelectCommand.Parameters.AddWithValue("@month", month);
            adapter.Fill(ds, "Data");
        }

        private void LoadPromoMember(MySqlConnection conn, DataSet ds, int year, int month)
        {
            string query = @"
                SELECT 
                    t.ID as TransID,
                    t.Tanggal as TransDate,
                    COALESCE(m.Nama, 'Guest') as CustomerName,
                    p.Nama as ProductName,
                    p.Merk as Brand,
                    k.Nama as Category,
                    td.Qty as Quantity,
                    td.Harga as UnitPrice,
                    td.Subtotal,
                    td.Diskon,
                    td.Diskon_Spesial
                FROM Transaksi t
                LEFT JOIN Member m ON t.member_id = m.ID
                INNER JOIN Transaksi_Detail td ON t.ID = td.transaksi_id
                INNER JOIN Produk p ON td.produk_id = p.ID
                INNER JOIN Kategori k ON p.kategori_id = k.ID
                WHERE YEAR(t.Tanggal) = @year AND MONTH(t.Tanggal) = @month AND td.diskon > 0 AND td.diskon_spesial = 0
                ORDER BY t.Tanggal DESC, t.ID";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@year", year);
            adapter.SelectCommand.Parameters.AddWithValue("@month", month);
            adapter.Fill(ds, "Data");
        }

        private void LoadPromoSpesial(MySqlConnection conn, DataSet ds, int year, int month)
        {
            string query = @"
                SELECT 
                    t.ID as TransID,
                    t.Tanggal as TransDate,
                    COALESCE(m.Nama, 'Guest') as CustomerName,
                    p.Nama as ProductName,
                    p.Merk as Brand,
                    k.Nama as Category,
                    td.Qty as Quantity,
                    td.Harga as UnitPrice,
                    td.Subtotal,
                    td.Diskon,
                    td.Diskon_Spesial
                FROM Transaksi t
                LEFT JOIN Member m ON t.member_id = m.ID
                INNER JOIN Transaksi_Detail td ON t.ID = td.transaksi_id
                INNER JOIN Produk p ON td.produk_id = p.ID
                INNER JOIN Kategori k ON p.kategori_id = k.ID
                WHERE YEAR(t.Tanggal) = @year AND MONTH(t.Tanggal) = @month AND td.diskon = 0 AND td.diskon_spesial > 0
                ORDER BY t.Tanggal DESC, t.ID";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@year", year);
            adapter.SelectCommand.Parameters.AddWithValue("@month", month);
            adapter.Fill(ds, "Data");
        }

        private void LoadPromo(MySqlConnection conn, DataSet ds, int year, int month)
        {
            string query = @"
                SELECT 
                    t.ID as TransID,
                    t.Tanggal as TransDate,
                    COALESCE(m.Nama, 'Guest') as CustomerName,
                    p.Nama as ProductName,
                    p.Merk as Brand,
                    k.Nama as Category,
                    td.Qty as Quantity,
                    td.Harga as UnitPrice,
                    td.Subtotal,
                    td.Diskon,
                    td.Diskon_Spesial
                FROM Transaksi t
                LEFT JOIN Member m ON t.member_id = m.ID
                INNER JOIN Transaksi_Detail td ON t.ID = td.transaksi_id
                INNER JOIN Produk p ON td.produk_id = p.ID
                INNER JOIN Kategori k ON p.kategori_id = k.ID
                WHERE YEAR(t.Tanggal) = @year AND MONTH(t.Tanggal) = @month AND td.diskon > 0 AND td.diskon_spesial > 0
                ORDER BY t.Tanggal DESC, t.ID";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@year", year);
            adapter.SelectCommand.Parameters.AddWithValue("@month", month);
            adapter.Fill(ds, "Data");
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                int year = Convert.ToInt32(comboBoxYear.SelectedItem);
                int month = comboBoxMonth.SelectedIndex + 1;
                string reportType = comboBoxReportType.SelectedItem.ToString();

                DataSet ds = GetMonthlySalesData(year, month, reportType);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    MessageBox.Show("No data to export", "Information",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "CSV Files (*.csv)|*.csv|Excel Files (*.xlsx)|*.xlsx",
                    FileName = $"Sales_Report_{year}_{month:D2}.csv"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    ExportToCSV(ds.Tables[0], saveDialog.FileName);
                    MessageBox.Show("Export successful!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToCSV(DataTable dt, string filePath)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                // Write headers
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sw.Write(dt.Columns[i].ColumnName);
                    if (i < dt.Columns.Count - 1)
                        sw.Write(",");
                }
                sw.WriteLine();

                // Write rows
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sw.Write(row[i].ToString().Replace(",", ";"));
                        if (i < dt.Columns.Count - 1)
                            sw.Write(",");
                    }
                    sw.WriteLine();
                }
            }
        }
    }
}