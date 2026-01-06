using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

namespace Project_PV
{
    public partial class FormLaporanPenjualanBulananPreview : Form
    {
        private DataSet reportDataSet;
        private string reportType;
        private int month;
        private int year;

        public FormLaporanPenjualanBulananPreview(DataSet dataSet, string type, int m, int y)
        {
            InitializeComponent();
            this.reportDataSet = dataSet;
            this.reportType = type;
            this.month = m;
            this.year = y;

            LoadReport();
        }

        private void LoadReport()
        {
            try
            {
                ReportDocument reportDoc = new ReportDocument();
                string reportPath = GetReportPath();

                if (!File.Exists(reportPath))
                {
                    MessageBox.Show(
                        $"Report file not found: {reportPath}\n\n" +
                        "Please ensure the Crystal Report file exists in the Laporan folder.",
                        "Report Not Found",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                    this.Close();
                    return;
                }

                reportDoc.Load(reportPath);
                reportDoc.SetDataSource(reportDataSet);

                // Set report parameters
                reportDoc.SetParameterValue("Month", new DateTime(year, month, 1).ToString("MMMM yyyy"));
                reportDoc.SetParameterValue("GeneratedDate", DateTime.Now.ToString("dd/MM/yyyy HH:mm"));

                // Assign to viewer
                crystalReportViewer1.ReportSource = reportDoc;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error loading report: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                this.Close();
            }
        }

        private string GetReportPath()
        {
            string laporanFolder = Path.Combine(Application.StartupPath, "Laporan");
            string fileName = "";

            switch (reportType)
            {
                case "Summary Report":
                    
                    fileName = "LaporanPenjualanBulananSummary.rpt";
                    break;
                case "Detailed Report":
                    fileName = "LaporanPenjualanBulananDetailed.rpt";
                    break;
                case "Product Analysis":
                    fileName = "LaporanPenjualanBulananProduct.rpt";
                    break;
                case "Category Analysis":
                    fileName = "LaporanPenjualanBulananCategory.rpt";
                    break;
                default:
                    fileName = "LaporanPenjualanBulananSummary.rpt";
                    break;
            }

            return Path.Combine(laporanFolder, fileName);
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (crystalReportViewer1.ReportSource != null)
                {
                    ReportDocument report = (ReportDocument)crystalReportViewer1.ReportSource;
                    report.PrintToPrinter(1, false, 0, 0);

                    MessageBox.Show(
                        "Report sent to printer",
                        "Print",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Print error: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void buttonExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF Files (*.pdf)|*.pdf",
                    FileName = $"Sales_Report_{year}_{month:D2}.pdf"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    ReportDocument report = (ReportDocument)crystalReportViewer1.ReportSource;
                    report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat,
                        saveDialog.FileName);

                    MessageBox.Show(
                        "Report exported successfully!",
                        "Export",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Export error: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Clean up report resources
                if (crystalReportViewer1.ReportSource != null)
                {
                    ((ReportDocument)crystalReportViewer1.ReportSource).Close();
                    ((ReportDocument)crystalReportViewer1.ReportSource).Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}