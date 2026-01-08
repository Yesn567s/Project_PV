using MySql.Data.MySqlClient;
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
        private List<int> promosToConsume = new List<int>();

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
            var baseItems = CartManager.GetCartItems();
            var displayItems = CartManager.GetCartDisplayItems();

            // Raw subtotal (unit price * qty) shown to user
            int rawSubtotal = CartManager.GetSubtotal();

            // Effective subtotal after item-level promos
            int effectiveSubtotal = CartManager.GetEffectiveSubtotalAfterItemPromos();

            // Item-level savings
            int itemSavings = CartManager.GetItemLevelSavings();

            // Member discount (applies to effective subtotal)
            int memberDiscount = isMember ? CartManager.CalculateDiscount(isMember) : 0;

            // Special promo discount applies after member discount on the effective subtotal
            int specialDiscountAmount = (int)Math.Round((effectiveSubtotal - memberDiscount) * (promoSpecialDiscountPercent / 100m));

            // Final total: effective subtotal minus member discount and special discount
            int total = effectiveSubtotal - memberDiscount - specialDiscountAmount;

            // Display summary in labels
            lblSubtotal.Text = $"Rp {rawSubtotal:N0}";

            // update special discount label value
            SpecialDiscountlabelvalue.Text = specialDiscountAmount > 0 ? $"-Rp {specialDiscountAmount:N0}" : "Rp 0";
            SpecialDiscountlabelvalue.Visible = specialDiscountAmount > 0;
            SpecialDiscountLabel.Visible = specialDiscountAmount > 0;

            // Build discount display text matching calculation
            var discountLines = new List<string>();
            if (itemSavings > 0)
                discountLines.Add($"-Rp {itemSavings:N0}");
            if (memberDiscount > 0)
                discountLines.Add($"Member: -Rp {memberDiscount:N0}");
            if (promoSpecialDiscountPercent > 0 && specialDiscountAmount > 0)
                discountLines.Add($"Promo -{promoSpecialDiscountPercent}%: -Rp {specialDiscountAmount:N0}");

            lblDiscount.Text = discountLines.Count > 0 ? string.Join("\n", discountLines) : "Rp 0";

            lblTotal.Text = $"Rp {total:N0}";
            // Show number of displayed lines (including bonus items) and total quantity (including bonus quantities)
            int displayedLines = displayItems.Count;
            int displayedQty = displayItems.Sum(d => d.Item.Quantity);
            lblItemCount.Text = $"{displayedLines} items ({displayedQty} total)";

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
            promosToConsume.Clear();

            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                string sql = @"SELECT ID, Nama_Promo, Kategori, Keterangan, Berlaku FROM promo_special WHERE START <= NOW() AND (END IS NULL OR END >= NOW())";
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32("ID");
                            string name = reader.GetString("Nama_Promo");
                            string kategori = reader.GetString("Kategori");
                            string keterangan = reader.GetString("Keterangan");

                            int? berlaku = null;
                            if (!reader.IsDBNull(reader.GetOrdinal("Berlaku")))
                            {
                                berlaku = reader.GetInt32("Berlaku");
                            }

                            // If berlaku is explicitly 0 then promo is exhausted -> skip
                            if (berlaku.HasValue && berlaku.Value == 0)
                                continue;

                            string key = id + ":" + name;

                            if (string.Equals(kategori, "YesNo", StringComparison.OrdinalIgnoreCase))
                            {
                                var dr = MessageBox.Show(keterangan, name, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                promoSpecialResponses[key] = (dr == DialogResult.Yes) ? "Yes" : "No";
                                promoSpecialTypes[key] = "YesNo";
                                // only consume if it has a positive limit (>0). If berlaku is null or -1 treat as unlimited (no consume)
                                if (dr == DialogResult.Yes && berlaku.HasValue && berlaku.Value > 0)
                                {
                                    promosToConsume.Add(id);
                                }
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
                                    if (!string.IsNullOrWhiteSpace(input) && berlaku.HasValue && berlaku.Value > 0)
                                    {
                                        promosToConsume.Add(id);
                                    }
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

                        // compute per-line discounts using display items (which include item-level promos and bonus items)
                        var displayItemsAll = CartManager.GetCartDisplayItems();
                        var chargeableDisplayItems = displayItemsAll.Where(d => !d.IsBonusItem).ToList();

                        int subtotalRaw = CartManager.GetSubtotal();
                        int effectiveSubtotal = CartManager.GetEffectiveSubtotalAfterItemPromos();

                        decimal memberPercent = isMember ? CartManager.GetMemberDiscountPercent() : 0m;

                        int totalMemberDiscount = 0;
                        int totalSpecialDiscount = 0;

                        var perLineDiscounts = new List<Tuple<int,int,int>>(); // productId, memberDisc, specialDisc
                        foreach (var di in chargeableDisplayItems)
                        {
                            int lineTotal = di.EffectiveSubtotal; // already respects Harga_Jadi/Persen/Grosir
                            int memberDiscLine = (int)Math.Round(lineTotal * (memberPercent / 100m));
                            int specialDiscLine = (int)Math.Round((lineTotal - memberDiscLine) * (promoSpecialDiscountPercent / 100m));
                            totalMemberDiscount += memberDiscLine;
                            totalSpecialDiscount += specialDiscLine;
                            perLineDiscounts.Add(Tuple.Create(di.Item.ProductID, memberDiscLine, specialDiscLine));
                        }

                        // compute final total based on effective subtotal
                        int total = effectiveSubtotal - totalMemberDiscount - totalSpecialDiscount;

                        // hargaTerpotong should represent total discount amount from raw subtotal
                        int hargaTerpotong = subtotalRaw - total;

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

                        // insert using display items so bonus freebies are also recorded
                        // First insert chargeable/displayable items (non-bonus)
                        foreach (var di in displayItemsAll.Where(d => !d.IsBonusItem))
                        {
                            var tuple = perLineDiscounts.FirstOrDefault(t => t.Item1 == di.Item.ProductID);
                            int memberDiscLine = tuple != null ? tuple.Item2 : 0;
                            int specialDiscLine = tuple != null ? tuple.Item3 : 0;

                            MySqlCommand cmdDetail = new MySqlCommand(insertDetail, conn, transaction);
                            cmdDetail.Parameters.AddWithValue("@transaksiID", transactionID);
                            cmdDetail.Parameters.AddWithValue("@produkID", di.Item.ProductID);
                            cmdDetail.Parameters.AddWithValue("@qty", di.Item.Quantity);
                            // store original unit price in Harga column
                            cmdDetail.Parameters.AddWithValue("@harga", di.Item.UnitPrice);
                            cmdDetail.Parameters.AddWithValue("@diskon", memberDiscLine);
                            cmdDetail.Parameters.AddWithValue("@diskon_spesial", specialDiscLine);
                            cmdDetail.ExecuteNonQuery();
                        }

                        // Aggregate bonus items by product id and insert single rows with price 0
                        var bonusGroups = displayItemsAll.Where(d => d.IsBonusItem)
                                                       .GroupBy(d => d.Item.ProductID)
                                                       .Select(g => new { ProductID = g.Key, Qty = g.Sum(x => x.Item.Quantity) });

                        foreach (var bg in bonusGroups)
                        {
                            MySqlCommand cmdDetail = new MySqlCommand(insertDetail, conn, transaction);
                            cmdDetail.Parameters.AddWithValue("@transaksiID", transactionID);
                            cmdDetail.Parameters.AddWithValue("@produkID", bg.ProductID);
                            cmdDetail.Parameters.AddWithValue("@qty", bg.Qty);
                            cmdDetail.Parameters.AddWithValue("@harga", 0);
                            cmdDetail.Parameters.AddWithValue("@diskon", 0);
                            cmdDetail.Parameters.AddWithValue("@diskon_spesial", 0);
                            cmdDetail.ExecuteNonQuery();
                        }

                        // Decrement Berlaku for consumed promos (only when Berlaku > 0)
                        if (promosToConsume != null && promosToConsume.Count > 0)
                        {
                            string updateBerlakuSql = "UPDATE promo_special SET Berlaku = Berlaku - 1 WHERE ID = @id AND Berlaku IS NOT NULL AND Berlaku > 0";
                            foreach (int promoId in promosToConsume.Distinct())
                            {
                                MySqlCommand cmdUpdate = new MySqlCommand(updateBerlakuSql, conn, transaction);
                                cmdUpdate.Parameters.AddWithValue("@id", promoId);
                                cmdUpdate.ExecuteNonQuery();
                            }
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
                    td.Diskon_Spesial,
                    m.is_member as IsMember,
                    t.Harga_Terpotong as DiskonTotal
                FROM Transaksi t
                LEFT JOIN Member m ON t.member_id = m.ID
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
