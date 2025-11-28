using System.Drawing;
using System.Windows.Forms;

namespace Project_PV
{
    partial class UserDashboard
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.headerPanel = new System.Windows.Forms.Panel();
            this.logoLabel = new System.Windows.Forms.Label();
            this.LabelBrand = new System.Windows.Forms.Label();
            this.navPanel = new System.Windows.Forms.Panel();
            this.homeButton = new System.Windows.Forms.Button();
            this.catalogButton = new System.Windows.Forms.Button();
            this.membershipButton = new System.Windows.Forms.Button();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.cartButton = new System.Windows.Forms.Button();
            this.userLabel = new System.Windows.Forms.Label();
            this.logoutButton = new System.Windows.Forms.Button();
            this.headerBorder = new System.Windows.Forms.Panel();
            this.contentPanel = new System.Windows.Forms.Panel();
            this.contentLabel = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.navPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.contentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.White;
            this.headerPanel.Controls.Add(this.logoLabel);
            this.headerPanel.Controls.Add(this.LabelBrand);
            this.headerPanel.Controls.Add(this.navPanel);
            this.headerPanel.Controls.Add(this.rightPanel);
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1200, 60);
            this.headerPanel.TabIndex = 0;
            // 
            // logoLabel
            // 
            this.logoLabel.AutoSize = true;
            this.logoLabel.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.logoLabel.Location = new System.Drawing.Point(30, 15);
            this.logoLabel.Name = "logoLabel";
            this.logoLabel.Size = new System.Drawing.Size(47, 32);
            this.logoLabel.TabIndex = 0;
            this.logoLabel.Text = "📖";
            // 
            // LabelBrand
            // 
            this.LabelBrand.AutoSize = true;
            this.LabelBrand.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.LabelBrand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.LabelBrand.Location = new System.Drawing.Point(74, 20);
            this.LabelBrand.Name = "LabelBrand";
            this.LabelBrand.Size = new System.Drawing.Size(56, 21);
            this.LabelBrand.TabIndex = 1;
            this.LabelBrand.Text = "TBMO";
            // 
            // navPanel
            // 
            this.navPanel.BackColor = System.Drawing.Color.Transparent;
            this.navPanel.Controls.Add(this.cartButton);
            this.navPanel.Controls.Add(this.homeButton);
            this.navPanel.Controls.Add(this.catalogButton);
            this.navPanel.Controls.Add(this.membershipButton);
            this.navPanel.Location = new System.Drawing.Point(300, 0);
            this.navPanel.Name = "navPanel";
            this.navPanel.Size = new System.Drawing.Size(550, 60);
            this.navPanel.TabIndex = 2;
            // 
            // homeButton
            // 
            this.homeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(40)))));
            this.homeButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.homeButton.FlatAppearance.BorderSize = 0;
            this.homeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.homeButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.homeButton.ForeColor = System.Drawing.Color.White;
            this.homeButton.Location = new System.Drawing.Point(20, 15);
            this.homeButton.Name = "homeButton";
            this.homeButton.Size = new System.Drawing.Size(100, 30);
            this.homeButton.TabIndex = 0;
            this.homeButton.Text = "🏠  Home";
            this.homeButton.UseVisualStyleBackColor = false;
            this.homeButton.Click += new System.EventHandler(this.homeButton_Click);
            // 
            // catalogButton
            // 
            this.catalogButton.BackColor = System.Drawing.Color.Transparent;
            this.catalogButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.catalogButton.FlatAppearance.BorderSize = 0;
            this.catalogButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.catalogButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.catalogButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.catalogButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.catalogButton.Location = new System.Drawing.Point(140, 15);
            this.catalogButton.Name = "catalogButton";
            this.catalogButton.Size = new System.Drawing.Size(110, 30);
            this.catalogButton.TabIndex = 1;
            this.catalogButton.Text = "📚  Catalog";
            this.catalogButton.UseVisualStyleBackColor = false;
            this.catalogButton.Click += new System.EventHandler(this.catalogButton_Click);
            // 
            // membershipButton
            // 
            this.membershipButton.BackColor = System.Drawing.Color.Transparent;
            this.membershipButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.membershipButton.FlatAppearance.BorderSize = 0;
            this.membershipButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.membershipButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.membershipButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.membershipButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.membershipButton.Location = new System.Drawing.Point(256, 17);
            this.membershipButton.Name = "membershipButton";
            this.membershipButton.Size = new System.Drawing.Size(130, 30);
            this.membershipButton.TabIndex = 3;
            this.membershipButton.Text = "💳  Membership";
            this.membershipButton.UseVisualStyleBackColor = false;
            this.membershipButton.Click += new System.EventHandler(this.membershipButton_Click);
            // 
            // rightPanel
            // 
            this.rightPanel.BackColor = System.Drawing.Color.Transparent;
            this.rightPanel.Controls.Add(this.userLabel);
            this.rightPanel.Controls.Add(this.logoutButton);
            this.rightPanel.Location = new System.Drawing.Point(900, 0);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(280, 60);
            this.rightPanel.TabIndex = 3;
            // 
            // cartButton
            // 
            this.cartButton.BackColor = System.Drawing.Color.Transparent;
            this.cartButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cartButton.FlatAppearance.BorderSize = 0;
            this.cartButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.cartButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cartButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cartButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.cartButton.Location = new System.Drawing.Point(392, 15);
            this.cartButton.Name = "cartButton";
            this.cartButton.Size = new System.Drawing.Size(80, 30);
            this.cartButton.TabIndex = 0;
            this.cartButton.Text = "🛒  Cart";
            this.cartButton.UseVisualStyleBackColor = false;
            this.cartButton.Click += new System.EventHandler(this.cartButton_Click);
            // 
            // userLabel
            // 
            this.userLabel.AutoSize = true;
            this.userLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.userLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.userLabel.Location = new System.Drawing.Point(90, 22);
            this.userLabel.Name = "userLabel";
            this.userLabel.Size = new System.Drawing.Size(69, 15);
            this.userLabel.TabIndex = 1;
            this.userLabel.Text = "👤  test_user";
            // 
            // logoutButton
            // 
            this.logoutButton.BackColor = System.Drawing.Color.Transparent;
            this.logoutButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logoutButton.FlatAppearance.BorderSize = 0;
            this.logoutButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.logoutButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.logoutButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.logoutButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.logoutButton.Location = new System.Drawing.Point(190, 15);
            this.logoutButton.Name = "logoutButton";
            this.logoutButton.Size = new System.Drawing.Size(85, 30);
            this.logoutButton.TabIndex = 2;
            this.logoutButton.Text = "🚪  Logout";
            this.logoutButton.UseVisualStyleBackColor = false;
            this.logoutButton.Click += new System.EventHandler(this.logoutButton_Click);
            // 
            // headerBorder
            // 
            this.headerBorder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.headerBorder.Location = new System.Drawing.Point(0, 59);
            this.headerBorder.Name = "headerBorder";
            this.headerBorder.Size = new System.Drawing.Size(1200, 1);
            this.headerBorder.TabIndex = 1;
            // 
            // contentPanel
            // 
            this.contentPanel.AutoScroll = true;
            this.contentPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.contentPanel.Controls.Add(this.contentLabel);
            this.contentPanel.Location = new System.Drawing.Point(0, 60);
            this.contentPanel.Name = "contentPanel";
            this.contentPanel.Size = new System.Drawing.Size(1200, 640);
            this.contentPanel.TabIndex = 2;
            // 
            // contentLabel
            // 
            this.contentLabel.AutoSize = true;
            this.contentLabel.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.contentLabel.ForeColor = System.Drawing.Color.Gray;
            this.contentLabel.Location = new System.Drawing.Point(50, 50);
            this.contentLabel.Name = "contentLabel";
            this.contentLabel.Size = new System.Drawing.Size(252, 30);
            this.contentLabel.TabIndex = 0;
            this.contentLabel.Text = "Dashboard Content Area";
            // 
            // UserDashboard
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.headerPanel);
            this.Controls.Add(this.headerBorder);
            this.Controls.Add(this.contentPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "UserDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chapter & Verse - Dashboard";
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.navPanel.ResumeLayout(false);
            this.rightPanel.ResumeLayout(false);
            this.rightPanel.PerformLayout();
            this.contentPanel.ResumeLayout(false);
            this.contentPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel headerPanel;
        private Label logoLabel;
        private Label LabelBrand;
        private Panel navPanel;
        private Button homeButton;
        private Button catalogButton;
        private Button membershipButton;
        private Panel rightPanel;
        private Button cartButton;
        private Label userLabel;
        private Button logoutButton;
        private Panel headerBorder;
        private Panel contentPanel;
        private Label contentLabel;
    }
}