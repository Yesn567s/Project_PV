namespace Project_PV
{
    partial class UserCartControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.shoppingCartLabel = new System.Windows.Forms.Label();
            this.itemCountLabel = new System.Windows.Forms.Label();
            this.booksLabel = new System.Windows.Forms.Label();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.itemsFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.itemPanel = new System.Windows.Forms.Panel();
            this.removeButton = new System.Windows.Forms.Button();
            this.quantityNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.priceEachLabel = new System.Windows.Forms.Label();
            this.itemPriceLabel = new System.Windows.Forms.Label();
            this.authorLabel = new System.Windows.Forms.Label();
            this.itemTitleLabel = new System.Windows.Forms.Label();
            this.categoryLabel = new System.Windows.Forms.Label();
            this.itemPictureBox = new System.Windows.Forms.PictureBox();
            this.orderSummaryPanel = new System.Windows.Forms.Panel();
            this.secureCheckoutLabel = new System.Windows.Forms.Label();
            this.proceedButton = new System.Windows.Forms.Button();
            this.totalAmountLabel = new System.Windows.Forms.Label();
            this.totalLabel = new System.Windows.Forms.Label();
            this.taxAmountLabel = new System.Windows.Forms.Label();
            this.taxLabel = new System.Windows.Forms.Label();
            this.discountAmountLabel = new System.Windows.Forms.Label();
            this.membershipDiscountLabel = new System.Windows.Forms.Label();
            this.subtotalAmountLabel = new System.Windows.Forms.Label();
            this.subtotalLabel = new System.Windows.Forms.Label();
            this.discountPanel = new System.Windows.Forms.Panel();
            this.discountAppliedLabel = new System.Windows.Forms.Label();
            this.memberLabel = new System.Windows.Forms.Label();
            this.orderSummaryLabel = new System.Windows.Forms.Label();
            this.doNotSellLabel = new System.Windows.Forms.Label();
            this.helpButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.mainPanel.SuspendLayout();
            this.itemsFlowLayoutPanel.SuspendLayout();
            this.itemPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quantityNumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemPictureBox)).BeginInit();
            this.orderSummaryPanel.SuspendLayout();
            this.discountPanel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // shoppingCartLabel
            // 
            this.shoppingCartLabel.AutoSize = true;
            this.shoppingCartLabel.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shoppingCartLabel.Location = new System.Drawing.Point(40, 40);
            this.shoppingCartLabel.Name = "shoppingCartLabel";
            this.shoppingCartLabel.Size = new System.Drawing.Size(294, 54);
            this.shoppingCartLabel.TabIndex = 0;
            this.shoppingCartLabel.Text = "Shopping Cart";
            // 
            // itemCountLabel
            // 
            this.itemCountLabel.AutoSize = true;
            this.itemCountLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemCountLabel.ForeColor = System.Drawing.Color.Gray;
            this.itemCountLabel.Location = new System.Drawing.Point(45, 94);
            this.itemCountLabel.Name = "itemCountLabel";
            this.itemCountLabel.Size = new System.Drawing.Size(150, 23);
            this.itemCountLabel.TabIndex = 1;
            this.itemCountLabel.Text = "1 item in your cart";
            // 
            // booksLabel
            // 
            this.booksLabel.AutoSize = true;
            this.booksLabel.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.booksLabel.Location = new System.Drawing.Point(43, 150);
            this.booksLabel.Name = "booksLabel";
            this.booksLabel.Size = new System.Drawing.Size(80, 31);
            this.booksLabel.TabIndex = 2;
            this.booksLabel.Text = "Books";
            // 
            // mainPanel
            // 
            this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainPanel.Controls.Add(this.itemsFlowLayoutPanel);
            this.mainPanel.Controls.Add(this.orderSummaryPanel);
            this.mainPanel.Location = new System.Drawing.Point(49, 200);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(1182, 456);
            this.mainPanel.TabIndex = 3;
            // 
            // itemsFlowLayoutPanel
            // 
            this.itemsFlowLayoutPanel.AutoScroll = true;
            this.itemsFlowLayoutPanel.Controls.Add(this.itemPanel);
            this.itemsFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.itemsFlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.itemsFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.itemsFlowLayoutPanel.Name = "itemsFlowLayoutPanel";
            this.itemsFlowLayoutPanel.Size = new System.Drawing.Size(782, 456);
            this.itemsFlowLayoutPanel.TabIndex = 1;
            // 
            // itemPanel
            // 
            this.itemPanel.BackColor = System.Drawing.Color.White;
            this.itemPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.itemPanel.Controls.Add(this.removeButton);
            this.itemPanel.Controls.Add(this.quantityNumericUpDown);
            this.itemPanel.Controls.Add(this.priceEachLabel);
            this.itemPanel.Controls.Add(this.itemPriceLabel);
            this.itemPanel.Controls.Add(this.authorLabel);
            this.itemPanel.Controls.Add(this.itemTitleLabel);
            this.itemPanel.Controls.Add(this.categoryLabel);
            this.itemPanel.Controls.Add(this.itemPictureBox);
            this.itemPanel.Location = new System.Drawing.Point(3, 3);
            this.itemPanel.Name = "itemPanel";
            this.itemPanel.Size = new System.Drawing.Size(750, 200);
            this.itemPanel.TabIndex = 0;
            // 
            // removeButton
            // 
            this.removeButton.FlatAppearance.BorderSize = 0;
            this.removeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.removeButton.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.removeButton.ForeColor = System.Drawing.Color.Red;
            this.removeButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.removeButton.Location = new System.Drawing.Point(200, 150);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(120, 35);
            this.removeButton.TabIndex = 7;
            this.removeButton.Text = "Remove";
            this.removeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.removeButton.UseVisualStyleBackColor = true;
            // 
            // quantityNumericUpDown
            // 
            this.quantityNumericUpDown.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quantityNumericUpDown.Location = new System.Drawing.Point(200, 100);
            this.quantityNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.quantityNumericUpDown.Name = "quantityNumericUpDown";
            this.quantityNumericUpDown.Size = new System.Drawing.Size(120, 34);
            this.quantityNumericUpDown.TabIndex = 6;
            this.quantityNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // priceEachLabel
            // 
            this.priceEachLabel.AutoSize = true;
            this.priceEachLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.priceEachLabel.ForeColor = System.Drawing.Color.Gray;
            this.priceEachLabel.Location = new System.Drawing.Point(620, 111);
            this.priceEachLabel.Name = "priceEachLabel";
            this.priceEachLabel.Size = new System.Drawing.Size(100, 23);
            this.priceEachLabel.TabIndex = 5;
            this.priceEachLabel.Text = "$27.99 each";
            this.priceEachLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // itemPriceLabel
            // 
            this.itemPriceLabel.AutoSize = true;
            this.itemPriceLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemPriceLabel.Location = new System.Drawing.Point(658, 83);
            this.itemPriceLabel.Name = "itemPriceLabel";
            this.itemPriceLabel.Size = new System.Drawing.Size(72, 28);
            this.itemPriceLabel.TabIndex = 4;
            this.itemPriceLabel.Text = "$27.99";
            this.itemPriceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // authorLabel
            // 
            this.authorLabel.AutoSize = true;
            this.authorLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.authorLabel.ForeColor = System.Drawing.Color.Gray;
            this.authorLabel.Location = new System.Drawing.Point(196, 68);
            this.authorLabel.Name = "authorLabel";
            this.authorLabel.Size = new System.Drawing.Size(123, 23);
            this.authorLabel.TabIndex = 3;
            this.authorLabel.Text = "by James Clear";
            // 
            // itemTitleLabel
            // 
            this.itemTitleLabel.AutoSize = true;
            this.itemTitleLabel.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.itemTitleLabel.Location = new System.Drawing.Point(194, 37);
            this.itemTitleLabel.Name = "itemTitleLabel";
            this.itemTitleLabel.Size = new System.Drawing.Size(159, 31);
            this.itemTitleLabel.TabIndex = 2;
            this.itemTitleLabel.Text = "Atomic Habits";
            // 
            // categoryLabel
            // 
            this.categoryLabel.AutoSize = true;
            this.categoryLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.categoryLabel.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.categoryLabel.Location = new System.Drawing.Point(197, 10);
            this.categoryLabel.Name = "categoryLabel";
            this.categoryLabel.Padding = new System.Windows.Forms.Padding(5);
            this.categoryLabel.Size = new System.Drawing.Size(74, 27);
            this.categoryLabel.TabIndex = 1;
            this.categoryLabel.Text = "Self-Help";
            // 
            // itemPictureBox
            // 
            this.itemPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.itemPictureBox.Location = new System.Drawing.Point(20, 20);
            this.itemPictureBox.Name = "itemPictureBox";
            this.itemPictureBox.Size = new System.Drawing.Size(150, 150);
            this.itemPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.itemPictureBox.TabIndex = 0;
            this.itemPictureBox.TabStop = false;
            // 
            // orderSummaryPanel
            // 
            this.orderSummaryPanel.BackColor = System.Drawing.SystemColors.Control;
            this.orderSummaryPanel.Controls.Add(this.secureCheckoutLabel);
            this.orderSummaryPanel.Controls.Add(this.proceedButton);
            this.orderSummaryPanel.Controls.Add(this.totalAmountLabel);
            this.orderSummaryPanel.Controls.Add(this.totalLabel);
            this.orderSummaryPanel.Controls.Add(this.taxAmountLabel);
            this.orderSummaryPanel.Controls.Add(this.taxLabel);
            this.orderSummaryPanel.Controls.Add(this.discountAmountLabel);
            this.orderSummaryPanel.Controls.Add(this.membershipDiscountLabel);
            this.orderSummaryPanel.Controls.Add(this.subtotalAmountLabel);
            this.orderSummaryPanel.Controls.Add(this.subtotalLabel);
            this.orderSummaryPanel.Controls.Add(this.discountPanel);
            this.orderSummaryPanel.Controls.Add(this.orderSummaryLabel);
            this.orderSummaryPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.orderSummaryPanel.Location = new System.Drawing.Point(782, 0);
            this.orderSummaryPanel.Name = "orderSummaryPanel";
            this.orderSummaryPanel.Size = new System.Drawing.Size(400, 456);
            this.orderSummaryPanel.TabIndex = 0;
            // 
            // secureCheckoutLabel
            // 
            this.secureCheckoutLabel.AutoSize = true;
            this.secureCheckoutLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secureCheckoutLabel.ForeColor = System.Drawing.Color.Gray;
            this.secureCheckoutLabel.Location = new System.Drawing.Point(80, 420);
            this.secureCheckoutLabel.Name = "secureCheckoutLabel";
            this.secureCheckoutLabel.Size = new System.Drawing.Size(277, 23);
            this.secureCheckoutLabel.TabIndex = 11;
            this.secureCheckoutLabel.Text = "Secure checkout powered by Stripe";
            // 
            // proceedButton
            // 
            this.proceedButton.BackColor = System.Drawing.Color.Black;
            this.proceedButton.FlatAppearance.BorderSize = 0;
            this.proceedButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.proceedButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.proceedButton.ForeColor = System.Drawing.Color.White;
            this.proceedButton.Location = new System.Drawing.Point(30, 360);
            this.proceedButton.Name = "proceedButton";
            this.proceedButton.Size = new System.Drawing.Size(340, 50);
            this.proceedButton.TabIndex = 10;
            this.proceedButton.Text = "Proceed to Checkout";
            this.proceedButton.UseVisualStyleBackColor = false;
            // 
            // totalAmountLabel
            // 
            this.totalAmountLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalAmountLabel.Location = new System.Drawing.Point(240, 310);
            this.totalAmountLabel.Name = "totalAmountLabel";
            this.totalAmountLabel.Size = new System.Drawing.Size(130, 28);
            this.totalAmountLabel.TabIndex = 9;
            this.totalAmountLabel.Text = "$28.72";
            this.totalAmountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // totalLabel
            // 
            this.totalLabel.AutoSize = true;
            this.totalLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.totalLabel.Location = new System.Drawing.Point(25, 310);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(55, 28);
            this.totalLabel.TabIndex = 8;
            this.totalLabel.Text = "Total";
            // 
            // taxAmountLabel
            // 
            this.taxAmountLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taxAmountLabel.Location = new System.Drawing.Point(270, 270);
            this.taxAmountLabel.Name = "taxAmountLabel";
            this.taxAmountLabel.Size = new System.Drawing.Size(100, 23);
            this.taxAmountLabel.TabIndex = 7;
            this.taxAmountLabel.Text = "$2.13";
            this.taxAmountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // taxLabel
            // 
            this.taxLabel.AutoSize = true;
            this.taxLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taxLabel.Location = new System.Drawing.Point(26, 270);
            this.taxLabel.Name = "taxLabel";
            this.taxLabel.Size = new System.Drawing.Size(72, 23);
            this.taxLabel.TabIndex = 6;
            this.taxLabel.Text = "Tax (8%)";
            // 
            // discountAmountLabel
            // 
            this.discountAmountLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.discountAmountLabel.ForeColor = System.Drawing.Color.Green;
            this.discountAmountLabel.Location = new System.Drawing.Point(270, 230);
            this.discountAmountLabel.Name = "discountAmountLabel";
            this.discountAmountLabel.Size = new System.Drawing.Size(100, 23);
            this.discountAmountLabel.TabIndex = 5;
            this.discountAmountLabel.Text = "-$1.40";
            this.discountAmountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // membershipDiscountLabel
            // 
            this.membershipDiscountLabel.AutoSize = true;
            this.membershipDiscountLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.membershipDiscountLabel.ForeColor = System.Drawing.Color.Green;
            this.membershipDiscountLabel.Location = new System.Drawing.Point(26, 230);
            this.membershipDiscountLabel.Name = "membershipDiscountLabel";
            this.membershipDiscountLabel.Size = new System.Drawing.Size(177, 23);
            this.membershipDiscountLabel.TabIndex = 4;
            this.membershipDiscountLabel.Text = "Membership Discount";
            // 
            // subtotalAmountLabel
            // 
            this.subtotalAmountLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subtotalAmountLabel.Location = new System.Drawing.Point(270, 190);
            this.subtotalAmountLabel.Name = "subtotalAmountLabel";
            this.subtotalAmountLabel.Size = new System.Drawing.Size(100, 23);
            this.subtotalAmountLabel.TabIndex = 3;
            this.subtotalAmountLabel.Text = "$27.99";
            this.subtotalAmountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // subtotalLabel
            // 
            this.subtotalLabel.AutoSize = true;
            this.subtotalLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.subtotalLabel.Location = new System.Drawing.Point(26, 190);
            this.subtotalLabel.Name = "subtotalLabel";
            this.subtotalLabel.Size = new System.Drawing.Size(74, 23);
            this.subtotalLabel.TabIndex = 2;
            this.subtotalLabel.Text = "Subtotal";
            // 
            // discountPanel
            // 
            this.discountPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(235)))));
            this.discountPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.discountPanel.Controls.Add(this.discountAppliedLabel);
            this.discountPanel.Controls.Add(this.memberLabel);
            this.discountPanel.Location = new System.Drawing.Point(30, 80);
            this.discountPanel.Name = "discountPanel";
            this.discountPanel.Size = new System.Drawing.Size(340, 80);
            this.discountPanel.TabIndex = 1;
            // 
            // discountAppliedLabel
            // 
            this.discountAppliedLabel.AutoSize = true;
            this.discountAppliedLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.discountAppliedLabel.ForeColor = System.Drawing.Color.Goldenrod;
            this.discountAppliedLabel.Location = new System.Drawing.Point(12, 39);
            this.discountAppliedLabel.Name = "discountAppliedLabel";
            this.discountAppliedLabel.Size = new System.Drawing.Size(164, 23);
            this.discountAppliedLabel.TabIndex = 1;
            this.discountAppliedLabel.Text = "5% discount applied";
            // 
            // memberLabel
            // 
            this.memberLabel.AutoSize = true;
            this.memberLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.memberLabel.ForeColor = System.Drawing.Color.Goldenrod;
            this.memberLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.memberLabel.Location = new System.Drawing.Point(12, 9);
            this.memberLabel.Name = "memberLabel";
            this.memberLabel.Size = new System.Drawing.Size(134, 25);
            this.memberLabel.TabIndex = 0;
            this.memberLabel.Text = "Silver Member";
            // 
            // orderSummaryLabel
            // 
            this.orderSummaryLabel.AutoSize = true;
            this.orderSummaryLabel.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.orderSummaryLabel.Location = new System.Drawing.Point(24, 24);
            this.orderSummaryLabel.Name = "orderSummaryLabel";
            this.orderSummaryLabel.Size = new System.Drawing.Size(184, 31);
            this.orderSummaryLabel.TabIndex = 0;
            this.orderSummaryLabel.Text = "Order Summary";
            // 
            // doNotSellLabel
            // 
            this.doNotSellLabel.AutoSize = true;
            this.doNotSellLabel.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.doNotSellLabel.ForeColor = System.Drawing.Color.Gray;
            this.doNotSellLabel.Location = new System.Drawing.Point(45, 680);
            this.doNotSellLabel.Name = "doNotSellLabel";
            this.doNotSellLabel.Size = new System.Drawing.Size(291, 23);
            this.doNotSellLabel.TabIndex = 4;
            this.doNotSellLabel.Text = "Do not sell or share my personal info";
            // 
            // helpButton
            // 
            this.helpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.helpButton.BackColor = System.Drawing.Color.Black;
            this.helpButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.helpButton.ForeColor = System.Drawing.Color.White;
            this.helpButton.Location = new System.Drawing.Point(1191, 680);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(40, 40);
            this.helpButton.TabIndex = 5;
            this.helpButton.Text = "?";
            this.helpButton.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel1);
            this.panel1.Location = new System.Drawing.Point(1237, 669);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1262, 733);
            this.panel1.TabIndex = 6;
            this.panel1.Visible = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label2.Location = new System.Drawing.Point(3, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(594, 38);
            this.label2.TabIndex = 1;
            this.label2.Text = "Start adding books to your cart to see them here.";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 15.8F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(594, 47);
            this.label1.TabIndex = 0;
            this.label1.Text = "Your Cart is Empty";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(518, 306);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(597, 154);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // UserCartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.helpButton);
            this.Controls.Add(this.doNotSellLabel);
            this.Controls.Add(this.mainPanel);
            this.Controls.Add(this.booksLabel);
            this.Controls.Add(this.itemCountLabel);
            this.Controls.Add(this.shoppingCartLabel);
            this.Name = "UserCartControl";
            this.Size = new System.Drawing.Size(1262, 733);
            this.mainPanel.ResumeLayout(false);
            this.itemsFlowLayoutPanel.ResumeLayout(false);
            this.itemPanel.ResumeLayout(false);
            this.itemPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.quantityNumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.itemPictureBox)).EndInit();
            this.orderSummaryPanel.ResumeLayout(false);
            this.orderSummaryPanel.PerformLayout();
            this.discountPanel.ResumeLayout(false);
            this.discountPanel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label shoppingCartLabel;
        private System.Windows.Forms.Label itemCountLabel;
        private System.Windows.Forms.Label booksLabel;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.Panel orderSummaryPanel;
        private System.Windows.Forms.FlowLayoutPanel itemsFlowLayoutPanel;
        private System.Windows.Forms.Panel itemPanel;
        private System.Windows.Forms.PictureBox itemPictureBox;
        private System.Windows.Forms.Label itemTitleLabel;
        private System.Windows.Forms.Label categoryLabel;
        private System.Windows.Forms.Label authorLabel;
        private System.Windows.Forms.Label itemPriceLabel;
        private System.Windows.Forms.Label priceEachLabel;
        private System.Windows.Forms.NumericUpDown quantityNumericUpDown;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Label orderSummaryLabel;
        private System.Windows.Forms.Panel discountPanel;
        private System.Windows.Forms.Label memberLabel;
        private System.Windows.Forms.Label discountAppliedLabel;
        private System.Windows.Forms.Label subtotalLabel;
        private System.Windows.Forms.Label subtotalAmountLabel;
        private System.Windows.Forms.Label membershipDiscountLabel;
        private System.Windows.Forms.Label discountAmountLabel;
        private System.Windows.Forms.Label taxAmountLabel;
        private System.Windows.Forms.Label taxLabel;
        private System.Windows.Forms.Label totalAmountLabel;
        private System.Windows.Forms.Label totalLabel;
        private System.Windows.Forms.Button proceedButton;
        private System.Windows.Forms.Label secureCheckoutLabel;
        private System.Windows.Forms.Label doNotSellLabel;
        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }

}
