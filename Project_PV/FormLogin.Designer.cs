using System.Drawing;
using System.Windows.Forms;

namespace Project_PV
{
    partial class FormLogin
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
            this.iconLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.headerLabel = new System.Windows.Forms.Label();
            this.descLabel = new System.Windows.Forms.Label();
            this.emailLabel = new System.Windows.Forms.Label();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.rememberCheckBox = new System.Windows.Forms.CheckBox();
            this.forgotPasswordLink = new System.Windows.Forms.LinkLabel();
            this.signInButton = new System.Windows.Forms.Button();
            this.createAccountLabel = new System.Windows.Forms.Label();
            this.createAccountLink = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.rightPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // iconLabel
            // 
            this.iconLabel.AutoSize = true;
            this.iconLabel.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.iconLabel.Location = new System.Drawing.Point(50, 30);
            this.iconLabel.Name = "iconLabel";
            this.iconLabel.Size = new System.Drawing.Size(64, 45);
            this.iconLabel.TabIndex = 0;
            this.iconLabel.Text = "📖";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.titleLabel.Location = new System.Drawing.Point(109, 42);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(66, 30);
            this.titleLabel.TabIndex = 1;
            this.titleLabel.Text = "Login";
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.subtitleLabel.Location = new System.Drawing.Point(50, 85);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(580, 45);
            this.subtitleLabel.TabIndex = 2;
            this.subtitleLabel.Text = "Welcome to your literary haven. Discover thousands of books, join our\nmembership " +
    "program, and enjoy exclusive benefits.";
            // 
            // rightPanel
            // 
            this.rightPanel.BackColor = System.Drawing.Color.White;
            this.rightPanel.Controls.Add(this.headerLabel);
            this.rightPanel.Controls.Add(this.descLabel);
            this.rightPanel.Controls.Add(this.emailLabel);
            this.rightPanel.Controls.Add(this.emailTextBox);
            this.rightPanel.Controls.Add(this.passwordLabel);
            this.rightPanel.Controls.Add(this.passwordTextBox);
            this.rightPanel.Controls.Add(this.rememberCheckBox);
            this.rightPanel.Controls.Add(this.forgotPasswordLink);
            this.rightPanel.Controls.Add(this.signInButton);
            this.rightPanel.Controls.Add(this.createAccountLabel);
            this.rightPanel.Controls.Add(this.createAccountLink);
            this.rightPanel.Location = new System.Drawing.Point(650, 80);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(510, 470);
            this.rightPanel.TabIndex = 4;
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.headerLabel.Location = new System.Drawing.Point(30, 35);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(203, 37);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Welcome Back";
            // 
            // descLabel
            // 
            this.descLabel.AutoSize = true;
            this.descLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.descLabel.ForeColor = System.Drawing.Color.Gray;
            this.descLabel.Location = new System.Drawing.Point(30, 75);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(194, 15);
            this.descLabel.TabIndex = 1;
            this.descLabel.Text = "Sign in to your account to continue";
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.emailLabel.Location = new System.Drawing.Point(30, 130);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(36, 15);
            this.emailLabel.TabIndex = 2;
            this.emailLabel.Text = "Email";
            // 
            // emailTextBox
            // 
            this.emailTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.emailTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.emailTextBox.ForeColor = System.Drawing.Color.Gray;
            this.emailTextBox.Location = new System.Drawing.Point(30, 155);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(450, 25);
            this.emailTextBox.TabIndex = 3;
            this.emailTextBox.Text = "you@example.com";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.passwordLabel.Location = new System.Drawing.Point(30, 210);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(59, 15);
            this.passwordLabel.TabIndex = 4;
            this.passwordLabel.Text = "Password";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.passwordTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.passwordTextBox.Location = new System.Drawing.Point(30, 235);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '•';
            this.passwordTextBox.Size = new System.Drawing.Size(450, 25);
            this.passwordTextBox.TabIndex = 5;
            this.passwordTextBox.Text = "abcdef";
            // 
            // rememberCheckBox
            // 
            this.rememberCheckBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.rememberCheckBox.Location = new System.Drawing.Point(30, 285);
            this.rememberCheckBox.Name = "rememberCheckBox";
            this.rememberCheckBox.Size = new System.Drawing.Size(150, 25);
            this.rememberCheckBox.TabIndex = 6;
            this.rememberCheckBox.Text = "Remember me";
            // 
            // forgotPasswordLink
            // 
            this.forgotPasswordLink.AutoSize = true;
            this.forgotPasswordLink.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.forgotPasswordLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(40)))));
            this.forgotPasswordLink.Location = new System.Drawing.Point(360, 287);
            this.forgotPasswordLink.Name = "forgotPasswordLink";
            this.forgotPasswordLink.Size = new System.Drawing.Size(100, 15);
            this.forgotPasswordLink.TabIndex = 7;
            this.forgotPasswordLink.TabStop = true;
            this.forgotPasswordLink.Text = "Forgot password?";
            // 
            // signInButton
            // 
            this.signInButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(40)))));
            this.signInButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.signInButton.FlatAppearance.BorderSize = 0;
            this.signInButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.signInButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.signInButton.ForeColor = System.Drawing.Color.White;
            this.signInButton.Location = new System.Drawing.Point(30, 330);
            this.signInButton.Name = "signInButton";
            this.signInButton.Size = new System.Drawing.Size(450, 45);
            this.signInButton.TabIndex = 8;
            this.signInButton.Text = "Sign In";
            this.signInButton.UseVisualStyleBackColor = false;
            this.signInButton.Click += new System.EventHandler(this.signInButton_Click);
            // 
            // createAccountLabel
            // 
            this.createAccountLabel.AutoSize = true;
            this.createAccountLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.createAccountLabel.ForeColor = System.Drawing.Color.Gray;
            this.createAccountLabel.Location = new System.Drawing.Point(150, 400);
            this.createAccountLabel.Name = "createAccountLabel";
            this.createAccountLabel.Size = new System.Drawing.Size(137, 15);
            this.createAccountLabel.TabIndex = 9;
            this.createAccountLabel.Text = "Don\'t have an account?  ";
            // 
            // createAccountLink
            // 
            this.createAccountLink.AutoSize = true;
            this.createAccountLink.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.createAccountLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(40)))));
            this.createAccountLink.Location = new System.Drawing.Point(293, 400);
            this.createAccountLink.Name = "createAccountLink";
            this.createAccountLink.Size = new System.Drawing.Size(68, 15);
            this.createAccountLink.TabIndex = 10;
            this.createAccountLink.TabStop = true;
            this.createAccountLink.Text = "Create one";
            this.createAccountLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.createAccountLink_LinkClicked);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Project_PV.Properties.Resources.placeholders;
            this.pictureBox1.Location = new System.Drawing.Point(50, 150);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(560, 400);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // FormLogin
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(239)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.iconLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.subtitleLabel);
            this.Controls.Add(this.rightPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chapter & Verse - Sign In";
            this.rightPanel.ResumeLayout(false);
            this.rightPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label iconLabel;
        private Label titleLabel;
        private Label subtitleLabel;
        private Panel rightPanel;
        private Label headerLabel;
        private Label descLabel;
        private Label emailLabel;
        private TextBox emailTextBox;
        private Label passwordLabel;
        private TextBox passwordTextBox;
        private CheckBox rememberCheckBox;
        private LinkLabel forgotPasswordLink;
        private Button signInButton;
        private Label createAccountLabel;
        private LinkLabel createAccountLink;
        private PictureBox pictureBox1;
    }
}