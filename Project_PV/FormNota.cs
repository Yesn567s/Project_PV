using CrystalDecisions.CrystalReports.Engine;
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
    public partial class FormNota : Form
    {
        private DataSet DataSetNota;

        public FormNota(DataSet dataSet)
        {
            InitializeComponent();
            this.DataSetNota = dataSet;
            LoadReceipt();
        }

        private void LoadReceipt()
        {
            try
            {
                // Create instance of your Crystal Report
                ReportDocument reportDoc = new ReportDocument();

                // Load the report file (you'll create this in Crystal Reports designer)
                // Make sure the path is correct - adjust based on your project structure
                string reportPath = Application.StartupPath + @"\Reports\ReceiptReport.rpt";
                reportDoc.Load(reportPath);

                // Set the dataset as data source
                reportDoc.SetDataSource(DataSetNota);

                // Assign report to viewer
                crystalReportViewer1.ReportSource = reportDoc;
                crystalReportViewer1.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error loading receipt:\n{ex.Message}\n\nMake sure ReceiptReport.rpt exists in the Reports folder.",
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
                string reportPath = Application.StartupPath + @"\Reports\ReceiptReport.rpt";
                reportDoc.Load(reportPath);
                reportDoc.SetDataSource(DataSetNota);

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

