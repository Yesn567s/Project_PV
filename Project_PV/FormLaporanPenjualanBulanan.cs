using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.IO;

namespace Project_PV
{
    public partial class FormLaporanPenjualanBulanan : Form
    {
        private string connectionString = "Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;";

        public FormLaporanPenjualanBulanan()
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
            comboBoxReportType.Items.Add("Summary Report");
            comboBoxReportType.Items.Add("Detailed Report");
            comboBoxReportType.Items.Add("Product Analysis");
            comboBoxReportType.Items.Add("Category Analysis");
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
            DataSet ds = new DataSet("MonthlySalesData");

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                switch (reportType)
                {
                    case "Summary Report":
                        LoadSummaryReport(conn, ds, year, month);
                        break;
                    case "Detailed Report":
                        LoadDetailedReport(conn, ds, year, month);
                        break;
                    case "Product Analysis":
                        LoadProductAnalysis(conn, ds, year, month);
                        break;
                    case "Category Analysis":
                        LoadCategoryAnalysis(conn, ds, year, month);
                        break;
                }
            }

            return ds;
        }

        private void LoadSummaryReport(MySqlConnection conn, DataSet ds, int year, int month)
        {
            // Main summary data
            string query = @"
                SELECT 
                    DATE_FORMAT(t.Tanggal, '%Y-%m') as Period,
                    COUNT(DISTINCT t.ID) as TotalTransactions,
                    COUNT(DISTINCT CASE WHEN t.member_id IS NOT NULL THEN t.ID END) as MemberTransactions,
                    COUNT(DISTINCT CASE WHEN t.member_id IS NULL THEN t.ID END) as NonMemberTransactions,
                    SUM(td.Qty) as TotalItemsSold,
                    SUM(td.Subtotal) as TotalRevenue,
                    AVG(t.Total) as AverageTransaction,
                    MAX(t.Total) as HighestTransaction,
                    MIN(t.Total) as LowestTransaction
                FROM Transaksi t
                INNER JOIN Transaksi_Detail td ON t.ID = td.transaksi_id
                WHERE YEAR(t.Tanggal) = @year AND MONTH(t.Tanggal) = @month
                GROUP BY DATE_FORMAT(t.Tanggal, '%Y-%m')";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@year", year);
            adapter.SelectCommand.Parameters.AddWithValue("@month", month);
            adapter.Fill(ds, "Summary");

            // Daily breakdown
            string dailyQuery = @"
                SELECT 
                    DATE(t.Tanggal) as TransactionDate,
                    COUNT(DISTINCT t.ID) as DailyTransactions,
                    SUM(td.Subtotal) as DailyRevenue,
                    SUM(td.Qty) as DailyItemsSold
                FROM Transaksi t
                INNER JOIN Transaksi_Detail td ON t.ID = td.transaksi_id
                WHERE YEAR(t.Tanggal) = @year AND MONTH(t.Tanggal) = @month
                GROUP BY DATE(t.Tanggal)
                ORDER BY DATE(t.Tanggal)";

            MySqlDataAdapter dailyAdapter = new MySqlDataAdapter(dailyQuery, conn);
            dailyAdapter.SelectCommand.Parameters.AddWithValue("@year", year);
            dailyAdapter.SelectCommand.Parameters.AddWithValue("@month", month);
            dailyAdapter.Fill(ds, "DailyBreakdown");
        }

        private void LoadDetailedReport(MySqlConnection conn, DataSet ds, int year, int month)
        {
            string query = @"
                SELECT 
                    t.ID as TransactionID,
                    t.Tanggal as TransactionDate,
                    COALESCE(m.Nama, 'Guest') as CustomerName,
                    COALESCE(m.Email, '-') as CustomerEmail,
                    CASE WHEN m.Is_Member = 1 THEN 'Member' ELSE 'Non-Member' END as CustomerType,
                    p.Nama as ProductName,
                    p.Merk as Brand,
                    k.Nama as Category,
                    td.Qty as Quantity,
                    td.Harga as UnitPrice,
                    td.Subtotal,
                    t.Total as TransactionTotal
                FROM Transaksi t
                LEFT JOIN Member m ON t.member_id = m.ID
                INNER JOIN Transaksi_Detail td ON t.ID = td.transaksi_id
                INNER JOIN Produk p ON td.produk_id = p.ID
                INNER JOIN Kategori k ON p.kategori_id = k.ID
                WHERE YEAR(t.Tanggal) = @year AND MONTH(t.Tanggal) = @month
                ORDER BY t.Tanggal DESC, t.ID";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@year", year);
            adapter.SelectCommand.Parameters.AddWithValue("@month", month);
            adapter.Fill(ds, "DetailedTransactions");
        }

        private void LoadProductAnalysis(MySqlConnection conn, DataSet ds, int year, int month)
        {
            string query = @"
                SELECT 
                    p.Nama as ProductName,
                    p.Merk as Brand,
                    k.Nama as Category,
                    COUNT(DISTINCT t.ID) as TransactionCount,
                    SUM(td.Qty) as TotalQuantitySold,
                    td.Harga as UnitPrice,
                    SUM(td.Subtotal) as TotalRevenue,
                    ROUND(AVG(td.Qty), 2) as AvgQtyPerTransaction,
                    ROUND(SUM(td.Subtotal) * 100.0 / (
                        SELECT SUM(Subtotal) 
                        FROM Transaksi_Detail td2 
                        INNER JOIN Transaksi t2 ON td2.transaksi_id = t2.ID
                        WHERE YEAR(t2.Tanggal) = @year AND MONTH(t2.Tanggal) = @month
                    ), 2) as RevenuePercentage
                FROM Transaksi t
                INNER JOIN Transaksi_Detail td ON t.ID = td.transaksi_id
                INNER JOIN Produk p ON td.produk_id = p.ID
                INNER JOIN Kategori k ON p.kategori_id = k.ID
                WHERE YEAR(t.Tanggal) = @year AND MONTH(t.Tanggal) = @month
                GROUP BY p.ID, p.Nama, p.Merk, k.Nama, td.Harga
                ORDER BY TotalRevenue DESC";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@year", year);
            adapter.SelectCommand.Parameters.AddWithValue("@month", month);
            adapter.Fill(ds, "ProductAnalysis");
        }

        private void LoadCategoryAnalysis(MySqlConnection conn, DataSet ds, int year, int month)
        {
            string query = @"
                SELECT 
                    k.Nama as Category,
                    COUNT(DISTINCT t.ID) as TransactionCount,
                    COUNT(DISTINCT p.ID) as UniqueProducts,
                    SUM(td.Qty) as TotalQuantitySold,
                    SUM(td.Subtotal) as TotalRevenue,
                    ROUND(AVG(td.Subtotal), 2) as AvgRevenuePerItem,
                    ROUND(SUM(td.Subtotal) * 100.0 / (
                        SELECT SUM(Subtotal) 
                        FROM Transaksi_Detail td2 
                        INNER JOIN Transaksi t2 ON td2.transaksi_id = t2.ID
                        WHERE YEAR(t2.Tanggal) = @year AND MONTH(t2.Tanggal) = @month
                    ), 2) as RevenuePercentage
                FROM Transaksi t
                INNER JOIN Transaksi_Detail td ON t.ID = td.transaksi_id
                INNER JOIN Produk p ON td.produk_id = p.ID
                INNER JOIN Kategori k ON p.kategori_id = k.ID
                WHERE YEAR(t.Tanggal) = @year AND MONTH(t.Tanggal) = @month
                GROUP BY k.ID, k.Nama
                ORDER BY TotalRevenue DESC";

            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
            adapter.SelectCommand.Parameters.AddWithValue("@year", year);
            adapter.SelectCommand.Parameters.AddWithValue("@month", month);
            adapter.Fill(ds, "CategoryAnalysis");
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