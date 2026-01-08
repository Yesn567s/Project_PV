using System;
using System.Data;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Collections.Generic;

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
            // Ensure report files exist before attempting to load
            if (!EnsureReportFilesExist())
            {
                // If reports are missing, do not attempt to load
                return;
            }

            bool member = GlobalData.IsMember;
            //MessageBox.Show("Member status: " + member.ToString());

            try
            {
                // Create instance of your Crystal Report
                ReportDocument reportDoc = new ReportDocument();

                // Load the report file (you'll create this in Crystal Reports designer)
                // Make sure the path is correct - adjust based on your project structure
                string reportPath = Path.Combine(
                    Application.StartupPath,
                    "Laporan",
                    "Nota.rpt"
                );

                if (!File.Exists(reportPath))
                {
                    MessageBox.Show($"Report not found: {reportPath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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

        // Verify required .rpt files exist in Laporan folder
        private bool EnsureReportFilesExist()
        {
            string laporanFolder = Path.Combine(Application.StartupPath, "Laporan");
            string nota = Path.Combine(laporanFolder, "Nota.rpt");

            List<string> missing = new List<string>();
            if (!File.Exists(nota)) missing.Add("Nota.rpt");

            if (missing.Count > 0)
            {
                MessageBox.Show(
                    $"Missing report file(s): {string.Join(", ", missing)}\n\nMake sure the file(s) exist in:\n{laporanFolder}",
                    "Missing Report Files",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
                return false;
            }

            return true;
        }


        // Method to print directly without preview
        public void PrintReceipt()
        {
            try
            {
                ReportDocument reportDoc = new ReportDocument();
                string reportPath = Application.StartupPath + @"\Laporan\Nota.rpt";

                if (!File.Exists(reportPath))
                {
                    MessageBox.Show($"Report not found: {reportPath}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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