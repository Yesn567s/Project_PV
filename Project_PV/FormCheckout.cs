using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Project_PV
{
    public partial class FormCheckout : Form
    {
        private int memberID;
        private bool isMember;
        private string connectionString = "Server=localhost;Database=db_proyek_pv;Uid=root;Pwd=;";
        private int transactionID = 0;

        public FormCheckout(int memberID, bool isMember)
        {
            InitializeComponent();
            this.memberID = memberID;
            this.isMember = isMember;

            DisplayOrderSummary();
        }

        private void DisplayOrderSummary()
        {
            var cartItems = CartManager.GetCartItems();
            int subtotal = CartManager.GetSubtotal();
            int discount = CartManager.CalculateDiscount(isMember, 5);
            int total = subtotal - discount;

            // Display summary in labels
            lblSubtotal.Text = $"Rp {subtotal:N0}";
            lblDiscount.Text = isMember ? $"-Rp {discount:N0}" : "Rp 0";
            lblTotal.Text = $"Rp {total:N0}";
            lblItemCount.Text = $"{cartItems.Count} items ({CartManager.GetItemCount()} total)";
        }

        private void btnConfirmCheckout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Confirm this transaction?",
                "Checkout Confirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                ProcessCheckout();
            }
        }

        private void ProcessCheckout()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MySqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        // 1. Insert into Transaksi table
                        int total = CartManager.GetTotal(isMember);
                        string insertTransaksi = @"
                            INSERT INTO Transaksi (member_id, Tanggal, Total)
                            VALUES (@memberID, NOW(), @total);
                            SELECT LAST_INSERT_ID();";

                        MySqlCommand cmdTransaksi = new MySqlCommand(insertTransaksi, conn, transaction);
                        cmdTransaksi.Parameters.AddWithValue("@memberID",
                            memberID > 0 ? (object)memberID : DBNull.Value);
                        cmdTransaksi.Parameters.AddWithValue("@total", total);

                        transactionID = Convert.ToInt32(cmdTransaksi.ExecuteScalar());

                        // 2. Insert cart items into Transaksi_Detail
                        var cartItems = CartManager.GetCartItems();
                        string insertDetail = @"
                            INSERT INTO Transaksi_Detail (transaksi_id, produk_id, Qty, Harga)
                            VALUES (@transaksiID, @produkID, @qty, @harga)";

                        foreach (var item in cartItems)
                        {
                            MySqlCommand cmdDetail = new MySqlCommand(insertDetail, conn, transaction);
                            cmdDetail.Parameters.AddWithValue("@transaksiID", transactionID);
                            cmdDetail.Parameters.AddWithValue("@produkID", item.ProductID);
                            cmdDetail.Parameters.AddWithValue("@qty", item.Quantity);
                            cmdDetail.Parameters.AddWithValue("@harga", item.UnitPrice);
                            cmdDetail.ExecuteNonQuery();
                        }

                        transaction.Commit();

                        MessageBox.Show(
                            $"Transaction completed successfully!\nTransaction ID: {transactionID}",
                            "Success",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        CartManager.ClearCart();

                        ShowNota();

                        // Close checkout form
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Transaction failed: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Checkout failed: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void ShowNota()
        {
            try
            {
                DataSet DataSetNota = CreateDataSetNota();

                FormNota fr = new FormNota(DataSetNota);
                fr.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error displaying receipt: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private DataSet CreateDataSetNota()
        {
            DataSet ds = new DataSet("DataSetNota");

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();

                // Query to get receipt data
                string query = @"
                    SELECT 
                        t.ID as TransactionID,
                        t.Tanggal as TransactionDate,
                        t.Total as TransactionTotal,
                        COALESCE(m.Nama, 'Non-Member') as CustomerName,
                        COALESCE(m.Email, '') as CustomerEmail,
                        COALESCE(m.Is_Member, 0) as IsMember,
                        td.ID as DetailID,
                        p.Nama as ProductName,
                        p.Merk as Brand,
                        td.Qty as Quantity,
                        td.Harga as UnitPrice,
                        td.Subtotal,
                        k.Nama as Category
                    FROM Transaksi t
                    LEFT JOIN MEMBER m ON t.member_id = m.ID
                    INNER JOIN Transaksi_Detail td ON t.ID = td.transaksi_id
                    INNER JOIN Produk p ON td.produk_id = p.ID
                    INNER JOIN Kategori k ON p.kategori_id = k.ID
                    WHERE t.ID = @transactionID
                    ORDER BY td.ID";

                MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@transactionID", transactionID);
                adapter.Fill(ds, "Nota");
            }

            return ds;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
