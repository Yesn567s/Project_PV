namespace Project_PV
{
    partial class FormCheckout
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelSummary = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblDiscountLabel = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblSubtotalLabel = new System.Windows.Forms.Label();
            this.lblItemCount = new System.Windows.Forms.Label();
            this.lblSummaryTitle = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConfirmCheckout = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SpecialDiscountLabel = new System.Windows.Forms.Label();
            this.SpecialDiscountlabelvalue = new System.Windows.Forms.Label();
            this.panelHeader.SuspendLayout();
            this.panelSummary.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(667, 98);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(27, 25);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(309, 46);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Confirm Checkout";
            // 
            // panelSummary
            // 
            this.panelSummary.BackColor = System.Drawing.Color.White;
            this.panelSummary.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSummary.Controls.Add(this.SpecialDiscountlabelvalue);
            this.panelSummary.Controls.Add(this.SpecialDiscountLabel);
            this.panelSummary.Controls.Add(this.lblTotal);
            this.panelSummary.Controls.Add(this.lblTotalLabel);
            this.panelSummary.Controls.Add(this.lblDiscount);
            this.panelSummary.Controls.Add(this.lblDiscountLabel);
            this.panelSummary.Controls.Add(this.lblSubtotal);
            this.panelSummary.Controls.Add(this.lblSubtotalLabel);
            this.panelSummary.Controls.Add(this.lblItemCount);
            this.panelSummary.Controls.Add(this.lblSummaryTitle);
            this.panelSummary.Location = new System.Drawing.Point(40, 135);
            this.panelSummary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelSummary.Name = "panelSummary";
            this.panelSummary.Size = new System.Drawing.Size(586, 307);
            this.panelSummary.TabIndex = 1;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblTotal.Location = new System.Drawing.Point(320, 246);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(240, 37);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "Rp 0";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalLabel
            // 
            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotalLabel.Location = new System.Drawing.Point(27, 246);
            this.lblTotalLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalLabel.Name = "lblTotalLabel";
            this.lblTotalLabel.Size = new System.Drawing.Size(85, 32);
            this.lblTotalLabel.TabIndex = 6;
            this.lblTotalLabel.Text = "TOTAL";
            // 
            // lblDiscount
            // 
            this.lblDiscount.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblDiscount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblDiscount.Location = new System.Drawing.Point(320, 154);
            this.lblDiscount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(240, 31);
            this.lblDiscount.TabIndex = 5;
            this.lblDiscount.Text = "Rp 0";
            this.lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiscountLabel
            // 
            this.lblDiscountLabel.AutoSize = true;
            this.lblDiscountLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblDiscountLabel.Location = new System.Drawing.Point(28, 154);
            this.lblDiscountLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDiscountLabel.Name = "lblDiscountLabel";
            this.lblDiscountLabel.Size = new System.Drawing.Size(162, 25);
            this.lblDiscountLabel.TabIndex = 4;
            this.lblDiscountLabel.Text = "Member Discount";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtotal.Location = new System.Drawing.Point(321, 113);
            this.lblSubtotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(240, 31);
            this.lblSubtotal.TabIndex = 3;
            this.lblSubtotal.Text = "Rp 0";
            this.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblSubtotalLabel
            // 
            this.lblSubtotalLabel.AutoSize = true;
            this.lblSubtotalLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSubtotalLabel.Location = new System.Drawing.Point(26, 113);
            this.lblSubtotalLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubtotalLabel.Name = "lblSubtotalLabel";
            this.lblSubtotalLabel.Size = new System.Drawing.Size(86, 25);
            this.lblSubtotalLabel.TabIndex = 2;
            this.lblSubtotalLabel.Text = "Subtotal:";
            // 
            // lblItemCount
            // 
            this.lblItemCount.AutoSize = true;
            this.lblItemCount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblItemCount.ForeColor = System.Drawing.Color.Gray;
            this.lblItemCount.Location = new System.Drawing.Point(27, 74);
            this.lblItemCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItemCount.Name = "lblItemCount";
            this.lblItemCount.Size = new System.Drawing.Size(148, 23);
            this.lblItemCount.TabIndex = 1;
            this.lblItemCount.Text = "0 items (0 in total)";
            // 
            // lblSummaryTitle
            // 
            this.lblSummaryTitle.AutoSize = true;
            this.lblSummaryTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblSummaryTitle.Location = new System.Drawing.Point(27, 25);
            this.lblSummaryTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSummaryTitle.Name = "lblSummaryTitle";
            this.lblSummaryTitle.Size = new System.Drawing.Size(196, 32);
            this.lblSummaryTitle.TabIndex = 0;
            this.lblSummaryTitle.Text = "Order Summary";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnConfirmCheckout);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 503);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(667, 86);
            this.panelButtons.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(40, 18);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(267, 55);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConfirmCheckout
            // 
            this.btnConfirmCheckout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnConfirmCheckout.FlatAppearance.BorderSize = 0;
            this.btnConfirmCheckout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmCheckout.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnConfirmCheckout.ForeColor = System.Drawing.Color.White;
            this.btnConfirmCheckout.Location = new System.Drawing.Point(360, 18);
            this.btnConfirmCheckout.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnConfirmCheckout.Name = "btnConfirmCheckout";
            this.btnConfirmCheckout.Size = new System.Drawing.Size(267, 55);
            this.btnConfirmCheckout.TabIndex = 0;
            this.btnConfirmCheckout.Text = "Confirm && Print Receipt";
            this.btnConfirmCheckout.UseVisualStyleBackColor = false;
            this.btnConfirmCheckout.Click += new System.EventHandler(this.btnConfirmCheckout_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(198, 450);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(267, 55);
            this.button1.TabIndex = 2;
            this.button1.Text = "Promo Special Check";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // SpecialDiscountLabel
            // 
            this.SpecialDiscountLabel.AutoSize = true;
            this.SpecialDiscountLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.SpecialDiscountLabel.Location = new System.Drawing.Point(28, 196);
            this.SpecialDiscountLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SpecialDiscountLabel.Name = "SpecialDiscountLabel";
            this.SpecialDiscountLabel.Size = new System.Drawing.Size(151, 25);
            this.SpecialDiscountLabel.TabIndex = 8;
            this.SpecialDiscountLabel.Text = "Special Discount";
            // 
            // SpecialDiscountlabelvalue
            // 
            this.SpecialDiscountlabelvalue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.SpecialDiscountlabelvalue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.SpecialDiscountlabelvalue.Location = new System.Drawing.Point(320, 196);
            this.SpecialDiscountlabelvalue.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SpecialDiscountlabelvalue.Name = "SpecialDiscountlabelvalue";
            this.SpecialDiscountlabelvalue.Size = new System.Drawing.Size(240, 31);
            this.SpecialDiscountlabelvalue.TabIndex = 9;
            this.SpecialDiscountlabelvalue.Text = "Rp 0";
            this.SpecialDiscountlabelvalue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // FormCheckout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(667, 589);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelSummary);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCheckout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Checkout - TBMO Store";
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelSummary.ResumeLayout(false);
            this.panelSummary.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel panelSummary;
        private System.Windows.Forms.Label lblSummaryTitle;
        private System.Windows.Forms.Label lblItemCount;
        private System.Windows.Forms.Label lblSubtotalLabel;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblDiscountLabel;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnConfirmCheckout;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label SpecialDiscountlabelvalue;
        private System.Windows.Forms.Label SpecialDiscountLabel;
    }
}