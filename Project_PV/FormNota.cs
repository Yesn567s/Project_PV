using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Project_PV
{
    public partial class FormNota : Form
    {
        private DataSet notaDataSet;
        private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;

        public FormNota(DataSet dataSet)
        {
            InitializeComponent();
            this.notaDataSet = dataSet;
            LoadNota();
        }

        private void LoadNota()
        {
            try
            {
                // Create instance of your Crystal Report
                ReportDocument reportDoc = new ReportDocument();

                // Load the report file (you'll create this in Crystal Reports designer)
                // Make sure the path is correct - adjust based on your project structure
                string reportPath = Application.StartupPath + @"\Laporan\Nota.rpt";
                reportDoc.Load(reportPath);

                // Set the dataset as data source   
                reportDoc.SetDataSource(notaDataSet);

                // Assign report to viewer
                crystalReportViewer1.ReportSource = reportDoc;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error loading receipt:\n{ex.Message}\n\nMake sure Nota.rpt exists in the Laporan folder.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // Method to print directly without preview
        public void PrintReceipt()
        {
            try
            {
                ReportDocument reportDoc = new ReportDocument();
                string reportPath = Application.StartupPath + @"\Laporan\Nota.rpt";
                reportDoc.Load(reportPath);
                reportDoc.SetDataSource(notaDataSet);

                // Print to default printer
                reportDoc.PrintToPrinter(1, false, 0, 0);

                reportDoc.Close();
                reportDoc.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing receipt: {ex.Message}",
                    "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}