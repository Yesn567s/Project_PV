using System.Drawing;
using System.Windows.Forms;

namespace Project_PV
{
    partial class FormRegister
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
            this.leftPanel = new System.Windows.Forms.Panel();
            this.panelLabel = new System.Windows.Forms.Label();
            this.iconLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.subtitleLabel = new System.Windows.Forms.Label();
            this.rightPanel = new System.Windows.Forms.Panel();
            this.headerLabel = new System.Windows.Forms.Label();
            this.descLabel = new System.Windows.Forms.Label();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.emailLabel = new System.Windows.Forms.Label();
            this.emailTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.confirmPasswordLabel = new System.Windows.Forms.Label();
            this.confirmPasswordTextBox = new System.Windows.Forms.TextBox();
            this.termsCheckBox = new System.Windows.Forms.CheckBox();
            this.createButton = new System.Windows.Forms.Button();
            this.signInLabel = new System.Windows.Forms.Label();
            this.signInLink = new System.Windows.Forms.LinkLabel();
            this.leftPanel.SuspendLayout();
            this.rightPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftPanel
            // 
            this.leftPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.leftPanel.Controls.Add(this.panelLabel);
            this.leftPanel.Location = new System.Drawing.Point(50, 150);
            this.leftPanel.Name = "leftPanel";
            this.leftPanel.Size = new System.Drawing.Size(550, 380);
            this.leftPanel.TabIndex = 0;
            // 
            // panelLabel
            // 
            this.panelLabel.AutoSize = true;
            this.panelLabel.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.panelLabel.ForeColor = System.Drawing.Color.White;
            this.panelLabel.Location = new System.Drawing.Point(200, 170);
            this.panelLabel.Name = "panelLabel";
            this.panelLabel.Size = new System.Drawing.Size(131, 30);
            this.panelLabel.TabIndex = 0;
            this.panelLabel.Text = "Image Panel";
            // 
            // iconLabel
            // 
            this.iconLabel.AutoSize = true;
            this.iconLabel.Font = new System.Drawing.Font("Segoe UI", 24F);
            this.iconLabel.Location = new System.Drawing.Point(50, 30);
            this.iconLabel.Name = "iconLabel";
            this.iconLabel.Size = new System.Drawing.Size(64, 45);
            this.iconLabel.TabIndex = 1;
            this.iconLabel.Text = "📖";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Segoe UI", 16F);
            this.titleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(69)))), ((int)(((byte)(19)))));
            this.titleLabel.Location = new System.Drawing.Point(120, 42);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(156, 30);
            this.titleLabel.TabIndex = 2;
            this.titleLabel.Text = "Chapter & Verse";
            // 
            // subtitleLabel
            // 
            this.subtitleLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.subtitleLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.subtitleLabel.Location = new System.Drawing.Point(50, 85);
            this.subtitleLabel.Name = "subtitleLabel";
            this.subtitleLabel.Size = new System.Drawing.Size(500, 45);
            this.subtitleLabel.TabIndex = 3;
            this.subtitleLabel.Text = "Join thousands of book lovers. Create your account and start your reading\njourney" +
    " today.";
            // 
            // rightPanel
            // 
            this.rightPanel.BackColor = System.Drawing.Color.White;
            this.rightPanel.Controls.Add(this.headerLabel);
            this.rightPanel.Controls.Add(this.descLabel);
            this.rightPanel.Controls.Add(this.nameLabel);
            this.rightPanel.Controls.Add(this.nameTextBox);
            this.rightPanel.Controls.Add(this.emailLabel);
            this.rightPanel.Controls.Add(this.emailTextBox);
            this.rightPanel.Controls.Add(this.passwordLabel);
            this.rightPanel.Controls.Add(this.passwordTextBox);
            this.rightPanel.Controls.Add(this.confirmPasswordLabel);
            this.rightPanel.Controls.Add(this.confirmPasswordTextBox);
            this.rightPanel.Controls.Add(this.termsCheckBox);
            this.rightPanel.Controls.Add(this.createButton);
            this.rightPanel.Controls.Add(this.signInLabel);
            this.rightPanel.Controls.Add(this.signInLink);
            this.rightPanel.Location = new System.Drawing.Point(650, 80);
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size(500, 491);
            this.rightPanel.TabIndex = 4;
            // 
            // headerLabel
            // 
            this.headerLabel.AutoSize = true;
            this.headerLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.headerLabel.Location = new System.Drawing.Point(30, 25);
            this.headerLabel.Name = "headerLabel";
            this.headerLabel.Size = new System.Drawing.Size(189, 32);
            this.headerLabel.TabIndex = 0;
            this.headerLabel.Text = "Create Account";
            // 
            // descLabel
            // 
            this.descLabel.AutoSize = true;
            this.descLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.descLabel.ForeColor = System.Drawing.Color.Gray;
            this.descLabel.Location = new System.Drawing.Point(30, 60);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(213, 15);
            this.descLabel.TabIndex = 1;
            this.descLabel.Text = "Sign up to start your reading adventure";
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.nameLabel.Location = new System.Drawing.Point(30, 100);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(62, 15);
            this.nameLabel.TabIndex = 2;
            this.nameLabel.Text = "Full Name";
            // 
            // nameTextBox
            // 
            this.nameTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.nameTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.nameTextBox.ForeColor = System.Drawing.Color.Gray;
            this.nameTextBox.Location = new System.Drawing.Point(30, 125);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(440, 25);
            this.nameTextBox.TabIndex = 3;
            this.nameTextBox.Text = "John Doe";
            // 
            // emailLabel
            // 
            this.emailLabel.AutoSize = true;
            this.emailLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.emailLabel.Location = new System.Drawing.Point(30, 170);
            this.emailLabel.Name = "emailLabel";
            this.emailLabel.Size = new System.Drawing.Size(36, 15);
            this.emailLabel.TabIndex = 4;
            this.emailLabel.Text = "Email";
            // 
            // emailTextBox
            // 
            this.emailTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.emailTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.emailTextBox.ForeColor = System.Drawing.Color.Gray;
            this.emailTextBox.Location = new System.Drawing.Point(30, 195);
            this.emailTextBox.Name = "emailTextBox";
            this.emailTextBox.Size = new System.Drawing.Size(440, 25);
            this.emailTextBox.TabIndex = 5;
            this.emailTextBox.Text = "you@example.com";
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.passwordLabel.Location = new System.Drawing.Point(30, 240);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(59, 15);
            this.passwordLabel.TabIndex = 6;
            this.passwordLabel.Text = "Password";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.passwordTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.passwordTextBox.Location = new System.Drawing.Point(30, 265);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '•';
            this.passwordTextBox.Size = new System.Drawing.Size(440, 25);
            this.passwordTextBox.TabIndex = 7;
            this.passwordTextBox.Text = "******";
            // 
            // confirmPasswordLabel
            // 
            this.confirmPasswordLabel.AutoSize = true;
            this.confirmPasswordLabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.confirmPasswordLabel.Location = new System.Drawing.Point(30, 310);
            this.confirmPasswordLabel.Name = "confirmPasswordLabel";
            this.confirmPasswordLabel.Size = new System.Drawing.Size(107, 15);
            this.confirmPasswordLabel.TabIndex = 8;
            this.confirmPasswordLabel.Text = "Confirm Password";
            // 
            // confirmPasswordTextBox
            // 
            this.confirmPasswordTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
            this.confirmPasswordTextBox.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.confirmPasswordTextBox.Location = new System.Drawing.Point(30, 335);
            this.confirmPasswordTextBox.Name = "confirmPasswordTextBox";
            this.confirmPasswordTextBox.PasswordChar = '•';
            this.confirmPasswordTextBox.Size = new System.Drawing.Size(440, 25);
            this.confirmPasswordTextBox.TabIndex = 9;
            this.confirmPasswordTextBox.Text = "grehsr";
            // 
            // termsCheckBox
            // 
            this.termsCheckBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.termsCheckBox.Location = new System.Drawing.Point(30, 380);
            this.termsCheckBox.Name = "termsCheckBox";
            this.termsCheckBox.Size = new System.Drawing.Size(440, 25);
            this.termsCheckBox.TabIndex = 10;
            this.termsCheckBox.Text = "I agree to the Terms of Service and Privacy Policy";
            // 
            // createButton
            // 
            this.createButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(40)))));
            this.createButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.createButton.FlatAppearance.BorderSize = 0;
            this.createButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.createButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.createButton.ForeColor = System.Drawing.Color.White;
            this.createButton.Location = new System.Drawing.Point(30, 415);
            this.createButton.Name = "createButton";
            this.createButton.Size = new System.Drawing.Size(440, 40);
            this.createButton.TabIndex = 11;
            this.createButton.Text = "Create Account";
            this.createButton.UseVisualStyleBackColor = false;
            // 
            // signInLabel
            // 
            this.signInLabel.AutoSize = true;
            this.signInLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.signInLabel.ForeColor = System.Drawing.Color.Gray;
            this.signInLabel.Location = new System.Drawing.Point(150, 462);
            this.signInLabel.Name = "signInLabel";
            this.signInLabel.Size = new System.Drawing.Size(148, 15);
            this.signInLabel.TabIndex = 12;
            this.signInLabel.Text = "Already have an account?  ";
            // 
            // signInLink
            // 
            this.signInLink.AutoSize = true;
            this.signInLink.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.signInLink.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(40)))));
            this.signInLink.Location = new System.Drawing.Point(310, 462);
            this.signInLink.Name = "signInLink";
            this.signInLink.Size = new System.Drawing.Size(44, 15);
            this.signInLink.TabIndex = 13;
            this.signInLink.TabStop = true;
            this.signInLink.Text = "Sign in";
            // 
            // FormRegister
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(239)))), ((int)(((byte)(231)))));
            this.ClientSize = new System.Drawing.Size(1184, 599);
            this.Controls.Add(this.leftPanel);
            this.Controls.Add(this.iconLabel);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.subtitleLabel);
            this.Controls.Add(this.rightPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormRegister";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chapter & Verse - Register";
            this.leftPanel.ResumeLayout(false);
            this.leftPanel.PerformLayout();
            this.rightPanel.ResumeLayout(false);
            this.rightPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel leftPanel;
        private Label panelLabel;
        private Label iconLabel;
        private Label titleLabel;
        private Label subtitleLabel;
        private Panel rightPanel;
        private Label headerLabel;
        private Label descLabel;
        private Label nameLabel;
        private TextBox nameTextBox;
        private Label emailLabel;
        private TextBox emailTextBox;
        private Label passwordLabel;
        private TextBox passwordTextBox;
        private Label confirmPasswordLabel;
        private TextBox confirmPasswordTextBox;
        private CheckBox termsCheckBox;
        private Button createButton;
        private Label signInLabel;
        private LinkLabel signInLink;
    }
}