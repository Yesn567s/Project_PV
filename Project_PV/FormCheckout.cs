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
        private Dictionary<string, string> promoSpecialResponses = new Dictionary<string, string>();
        private Dictionary<string, string> promoSpecialTypes = new Dictionary<string, string>();
        private decimal promoSpecialDiscountPercent = 0m;

        public FormCheckout(int memberID, bool isMember)
        {
            InitializeComponent();
            this.memberID = memberID;
            this.isMember = isMember;
            // Don't run runtime UI population in constructor (designer may instantiate this class).
            this.Load += FormCheckout_Load;
        }

        private void FormCheckout_Load(object sender, EventArgs e)
        {
            // Only populate summary at runtime, not at design time
            if (!this.DesignMode)
            {
                DisplayOrderSummary();
            }
        }

        private void DisplayOrderSummary()
        {
            var cartItems = CartManager.GetCartItems();
            int subtotal = CartManager.GetSubtotal();
            int discount = CartManager.CalculateDiscount(isMember, 5);
            // apply special discount after member discount: compute on remaining amount
            int specialDiscountAmount = (int)Math.Round((subtotal - discount) * (promoSpecialDiscountPercent / 100m));
            int total = subtotal - discount - specialDiscountAmount;

            // Display summary in labels
            lblSubtotal.Text = $"Rp {subtotal:N0}";
            // update special discount label value (if present on the form)
            // If the developer added a label named SpecialDiscountlabelvalue, update it
            SpecialDiscountlabelvalue.Text = specialDiscountAmount > 0 ? $"-Rp {specialDiscountAmount:N0}" : "Rp 0";
            SpecialDiscountlabelvalue.Visible = specialDiscountAmount > 0;
            // also hide or show the label caption if present
            SpecialDiscountLabel.Visible = specialDiscountAmount > 0;
            if (isMember && discount > 0)
            {
                if (promoSpecialDiscountPercent > 0)
                    lblDiscount.Text = $"-Rp {discount:N0}\nPromo -{promoSpecialDiscountPercent}%: -Rp {specialDiscountAmount:N0}";
                else
                    lblDiscount.Text = $"-Rp {discount:N0}";
            }
            else
            {
                if (promoSpecialDiscountPercent > 0)
                    lblDiscount.Text = $"Promo -{promoSpecialDiscountPercent}%: -Rp {specialDiscountAmount:N0}";
                else
                    lblDiscount.Text = "Rp 0";
            }

            lblTotal.Text = $"Rp {total:N0}";
            lblItemCount.Text = $"{cartItems.Count} items ({CartManager.GetItemCount()} total)";

            if (GlobalData.IsMember)
            {
                lblDiscountLabel.Visible = true;
                lblDiscount.Visible = true;
            }
            else
            {
                lblDiscount.Visible = false;
                lblDiscountLabel.Visible = false;
        }
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

        // Loads active promo_special rows and prompts user accordingly.
        // Returns false if user cancelled any input prompt.
        private bool AskPromoSpecials()
        {
            promoSpecialResponses.Clear();
            promoSpecialTypes.Clear();
            promoSpecialDiscountPercent = 0m;

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"SELECT ID, Nama_Promo, Kategori, Keterangan FROM promo_special WHERE START <= NOW() AND (END IS NULL OR END >= NOW())";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("ID");
                            string name = reader.GetString("Nama_Promo");
                            string kategori = reader.GetString("Kategori");
                            string keterangan = reader.GetString("Keterangan");

                            string key = id + ":" + name;

                            if (string.Equals(kategori, "YesNo", StringComparison.OrdinalIgnoreCase))
                            {
                                var dr = MessageBox.Show(keterangan, name, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                promoSpecialResponses[key] = (dr == DialogResult.Yes) ? "Yes" : "No";
                                promoSpecialTypes[key] = "YesNo";
                            }
                            else if (string.Equals(kategori, "Input", StringComparison.OrdinalIgnoreCase))
                            {
                                string input = PromptForInput(keterangan, name);
                                if (input == null)
                                {
                                    // user cancelled input prompt -> record empty and continue
                                    promoSpecialResponses[key] = string.Empty;
                                }
                                else
                                {
                                    promoSpecialResponses[key] = input;
                                }
                                promoSpecialTypes[key] = "Input";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load special promos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Don't block checkout if promos failed to load; continue
            }

            return true;
        }

        // Shows a simple input dialog. Returns null if cancelled.
        private string PromptForInput(string prompt, string title)
        {
            Form inputForm = new Form();
            inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputForm.StartPosition = FormStartPosition.CenterParent;
            inputForm.ClientSize = new Size(400, 140);
            inputForm.Text = title;

            Label lbl = new Label() { Left = 12, Top = 12, Text = prompt, AutoSize = false, Width = 370, Height = 40 };
            TextBox txt = new TextBox() { Left = 12, Top = 60, Width = 370 };
            Button btnOk = new Button() { Text = "OK", Left = 220, Width = 80, Top = 95, DialogResult = DialogResult.OK };
            Button btnCancel = new Button() { Text = "Cancel", Left = 305, Width = 80, Top = 95, DialogResult = DialogResult.Cancel };

            inputForm.Controls.Add(lbl);
            inputForm.Controls.Add(txt);
            inputForm.Controls.Add(btnOk);
            inputForm.Controls.Add(btnCancel);
            inputForm.AcceptButton = btnOk;
            inputForm.CancelButton = btnCancel;

            var dr = inputForm.ShowDialog(this);
            if (dr == DialogResult.OK)
            {
                return txt.Text;
            }
            return null;
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
                        // Calculate totals and per-line discounts
                        var cartItems = CartManager.GetCartItems();

                        decimal memberPercent = isMember ? 5m : 0m;

                        int subtotal = CartManager.GetSubtotal();

                        int totalMemberDiscount = 0;
                        int totalSpecialDiscount = 0;

                        // compute per-line discounts where special discount is applied after member discount
                        var perLineDiscounts = new List<Tuple<int,int,int>>(); // productId, memberDisc, specialDisc
                        foreach (var item in cartItems)
                        {
                            int lineTotal = item.UnitPrice * item.Quantity;
                            int memberDiscLine = (int)Math.Round(lineTotal * (memberPercent / 100m));
                            // special discount is applied on (lineTotal - memberDiscLine)
                            int specialDiscLine = (int)Math.Round((lineTotal - memberDiscLine) * (promoSpecialDiscountPercent / 100m));
                            totalMemberDiscount += memberDiscLine;
                            totalSpecialDiscount += specialDiscLine;
                            perLineDiscounts.Add(Tuple.Create(item.ProductID, memberDiscLine, specialDiscLine));
                        }

                        int hargaTerpotong = totalMemberDiscount + totalSpecialDiscount;

                        // compute final total from subtotal minus discounts
                        int total = subtotal - totalMemberDiscount - totalSpecialDiscount;

                        // 1. Insert into Transaksi table (with Harga_Terpotong)
                        string insertTransaksi = @"
                            INSERT INTO Transaksi (member_id, Tanggal, Harga_Terpotong, Total)
                            VALUES (@memberID, NOW(), @hargaTerpotong, @total);
                            SELECT LAST_INSERT_ID();";

                        MySqlCommand cmdTransaksi = new MySqlCommand(insertTransaksi, conn, transaction);
                        cmdTransaksi.Parameters.AddWithValue("@memberID",
                            memberID > 0 ? (object)memberID : DBNull.Value);
                        cmdTransaksi.Parameters.AddWithValue("@hargaTerpotong", hargaTerpotong);
                        cmdTransaksi.Parameters.AddWithValue("@total", total);

                        transactionID = Convert.ToInt32(cmdTransaksi.ExecuteScalar());

                        // 2. Insert cart items into Transaksi_Detail including Diskon and Diskon_Spesial
                        string insertDetail = @"
                            INSERT INTO Transaksi_Detail (transaksi_id, produk_id, Qty, Harga, Diskon, Diskon_Spesial)
                            VALUES (@transaksiID, @produkID, @qty, @harga, @diskon, @diskon_spesial)";

                        foreach (var item in cartItems)
                        {
                            // find computed discounts for this product
                            var tuple = perLineDiscounts.FirstOrDefault(t => t.Item1 == item.ProductID);
                            int memberDiscLine = tuple != null ? tuple.Item2 : 0;
                            int specialDiscLine = tuple != null ? tuple.Item3 : 0;

                            MySqlCommand cmdDetail = new MySqlCommand(insertDetail, conn, transaction);
                            cmdDetail.Parameters.AddWithValue("@transaksiID", transactionID);
                            cmdDetail.Parameters.AddWithValue("@produkID", item.ProductID);
                            cmdDetail.Parameters.AddWithValue("@qty", item.Quantity);
                            cmdDetail.Parameters.AddWithValue("@harga", item.UnitPrice);
                            cmdDetail.Parameters.AddWithValue("@diskon", memberDiscLine);
                            cmdDetail.Parameters.AddWithValue("@diskon_spesial", specialDiscLine);
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
                        COALESCE(td.Diskon, 0) as Diskon,
                        COALESCE(td.Diskon_Spesial, 0) as DiskonSpesial,
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

                // Add promo special responses as a separate datatable so it can be printed on receipt
                DataTable promoTable = new DataTable("PromoSpecial");
                promoTable.Columns.Add("PromoID_Name", typeof(string));
                promoTable.Columns.Add("Response", typeof(string));

                foreach (var kv in promoSpecialResponses)
                {
                    var row = promoTable.NewRow();
                    row["PromoID_Name"] = kv.Key;
                    row["Response"] = kv.Value;
                    promoTable.Rows.Add(row);
                }

                ds.Tables.Add(promoTable);
            }

            return ds;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Trigger the promo special prompts manually and compute discount
            AskPromoSpecials();

            if (promoSpecialResponses.Count == 0)
            {
                MessageBox.Show("No active special promos found.", "Promo Check", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Determine discount percent from collected promos
            // YesNo => fixed 10%
            // Input => parse numeric percentage from input text
            decimal highestPercent = 0m;
            foreach (var kv in promoSpecialResponses)
            {
                string key = kv.Key;
                string resp = kv.Value;
                string type = promoSpecialTypes.ContainsKey(key) ? promoSpecialTypes[key] : string.Empty;

                if (string.Equals(type, "YesNo", StringComparison.OrdinalIgnoreCase))
                {
                    if (resp == "Yes") highestPercent = Math.Max(highestPercent, 10m);
                }
                else if (string.Equals(type, "Input", StringComparison.OrdinalIgnoreCase))
                {
                    // try parse number from response
                    decimal parsed = 0m;
                    if (decimal.TryParse(resp, out parsed))
                    {
                        if (parsed > 0) highestPercent = Math.Max(highestPercent, parsed);
                    }
                    else
                    {
                        // try to extract digits only (e.g., "15%" or "15 percent")
                        string digits = new string(resp.Where(c => char.IsDigit(c) || c == '.' || c == ',').ToArray());
                        digits = digits.Replace(',', '.');
                        if (decimal.TryParse(digits, out parsed))
                        {
                            if (parsed > 0) highestPercent = Math.Max(highestPercent, parsed);
                        }
                    }
                }
            }

            promoSpecialDiscountPercent = highestPercent;

            if (promoSpecialDiscountPercent > 0)
            {
                MessageBox.Show($"Promo applied: {promoSpecialDiscountPercent}% discount added to summary.", "Promo Applied", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Refresh displayed summary to include promo discount
            DisplayOrderSummary();
        }
    }
}
