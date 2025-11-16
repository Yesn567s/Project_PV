using System.Drawing;
using System.Windows.Forms;

namespace Project_PV
{
    partial class UserDashboardControl
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
            this.heroPanel = new System.Windows.Forms.Panel();
            this.leftContent = new System.Windows.Forms.Panel();
            this.badgePanel = new System.Windows.Forms.Panel();
            this.badgeIcon = new System.Windows.Forms.Label();
            this.badgeLabel = new System.Windows.Forms.Label();
            this.headingLabel = new System.Windows.Forms.Label();
            this.descLabel = new System.Windows.Forms.Label();
            this.buttonsPanel = new System.Windows.Forms.Panel();
            this.browseCatalogButton = new System.Windows.Forms.Button();
            this.joinMembershipButton = new System.Windows.Forms.Button();
            this.imagePanel = new System.Windows.Forms.Panel();
            this.imagePlaceholder = new System.Windows.Forms.Label();
            this.featuresPanel = new System.Windows.Forms.Panel();
            this.card1 = new System.Windows.Forms.Panel();
            this.cardTitle1 = new System.Windows.Forms.Label();
            this.cardDesc1 = new System.Windows.Forms.Label();
            this.iconBg1 = new System.Windows.Forms.Panel();
            this.icon1 = new System.Windows.Forms.Label();
            this.card2 = new System.Windows.Forms.Panel();
            this.cardTitle2 = new System.Windows.Forms.Label();
            this.cardDesc2 = new System.Windows.Forms.Label();
            this.iconBg2 = new System.Windows.Forms.Panel();
            this.icon2 = new System.Windows.Forms.Label();
            this.card3 = new System.Windows.Forms.Panel();
            this.cardTitle3 = new System.Windows.Forms.Label();
            this.cardDesc3 = new System.Windows.Forms.Label();
            this.iconBg3 = new System.Windows.Forms.Panel();
            this.icon3 = new System.Windows.Forms.Label();
            this.heroPanel.SuspendLayout();
            this.leftContent.SuspendLayout();
            this.badgePanel.SuspendLayout();
            this.buttonsPanel.SuspendLayout();
            this.imagePanel.SuspendLayout();
            this.featuresPanel.SuspendLayout();
            this.card1.SuspendLayout();
            this.card2.SuspendLayout();
            this.card3.SuspendLayout();
            this.SuspendLayout();
            // 
            // heroPanel
            // 
            this.heroPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(100)))), ((int)(((byte)(20)))));
            this.heroPanel.Controls.Add(this.leftContent);
            this.heroPanel.Controls.Add(this.imagePanel);
            this.heroPanel.Location = new System.Drawing.Point(0, 0);
            this.heroPanel.Name = "heroPanel";
            this.heroPanel.Size = new System.Drawing.Size(1200, 345);
            this.heroPanel.TabIndex = 0;
            // 
            // leftContent
            // 
            this.leftContent.BackColor = System.Drawing.Color.Transparent;
            this.leftContent.Controls.Add(this.badgePanel);
            this.leftContent.Controls.Add(this.headingLabel);
            this.leftContent.Controls.Add(this.descLabel);
            this.leftContent.Controls.Add(this.buttonsPanel);
            this.leftContent.Location = new System.Drawing.Point(80, 50);
            this.leftContent.Name = "leftContent";
            this.leftContent.Size = new System.Drawing.Size(550, 250);
            this.leftContent.TabIndex = 0;
            // 
            // badgePanel
            // 
            this.badgePanel.BackColor = System.Drawing.Color.White;
            this.badgePanel.Controls.Add(this.badgeIcon);
            this.badgePanel.Controls.Add(this.badgeLabel);
            this.badgePanel.Location = new System.Drawing.Point(0, 0);
            this.badgePanel.Name = "badgePanel";
            this.badgePanel.Size = new System.Drawing.Size(180, 30);
            this.badgePanel.TabIndex = 0;
            // 
            // badgeIcon
            // 
            this.badgeIcon.AutoSize = true;
            this.badgeIcon.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.badgeIcon.Location = new System.Drawing.Point(10, 6);
            this.badgeIcon.Name = "badgeIcon";
            this.badgeIcon.Size = new System.Drawing.Size(19, 15);
            this.badgeIcon.TabIndex = 0;
            this.badgeIcon.Text = "🎉";
            // 
            // badgeLabel
            // 
            this.badgeLabel.AutoSize = true;
            this.badgeLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.badgeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(100)))), ((int)(((byte)(20)))));
            this.badgeLabel.Location = new System.Drawing.Point(35, 7);
            this.badgeLabel.Name = "badgeLabel";
            this.badgeLabel.Size = new System.Drawing.Size(138, 15);
            this.badgeLabel.TabIndex = 1;
            this.badgeLabel.Text = "November Special Offers";
            // 
            // headingLabel
            // 
            this.headingLabel.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.headingLabel.ForeColor = System.Drawing.Color.White;
            this.headingLabel.Location = new System.Drawing.Point(0, 60);
            this.headingLabel.Name = "headingLabel";
            this.headingLabel.Size = new System.Drawing.Size(550, 50);
            this.headingLabel.TabIndex = 1;
            this.headingLabel.Text = "Discover Your Next Great Read";
            // 
            // descLabel
            // 
            this.descLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.descLabel.ForeColor = System.Drawing.Color.White;
            this.descLabel.Location = new System.Drawing.Point(0, 125);
            this.descLabel.Name = "descLabel";
            this.descLabel.Size = new System.Drawing.Size(530, 60);
            this.descLabel.TabIndex = 2;
            this.descLabel.Text = "Explore our vast collection of books across all genres. Join our membership progr" +
    "am\nfor exclusive discounts and early access to new releases.";
            // 
            // buttonsPanel
            // 
            this.buttonsPanel.BackColor = System.Drawing.Color.Transparent;
            this.buttonsPanel.Controls.Add(this.browseCatalogButton);
            this.buttonsPanel.Controls.Add(this.joinMembershipButton);
            this.buttonsPanel.Location = new System.Drawing.Point(0, 195);
            this.buttonsPanel.Name = "buttonsPanel";
            this.buttonsPanel.Size = new System.Drawing.Size(400, 45);
            this.buttonsPanel.TabIndex = 3;
            // 
            // browseCatalogButton
            // 
            this.browseCatalogButton.BackColor = System.Drawing.Color.White;
            this.browseCatalogButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.browseCatalogButton.FlatAppearance.BorderSize = 0;
            this.browseCatalogButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.browseCatalogButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.browseCatalogButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(100)))), ((int)(((byte)(20)))));
            this.browseCatalogButton.Location = new System.Drawing.Point(0, 0);
            this.browseCatalogButton.Name = "browseCatalogButton";
            this.browseCatalogButton.Size = new System.Drawing.Size(165, 45);
            this.browseCatalogButton.TabIndex = 0;
            this.browseCatalogButton.Text = "Browse Catalog  →";
            this.browseCatalogButton.UseVisualStyleBackColor = false;
            // 
            // joinMembershipButton
            // 
            this.joinMembershipButton.BackColor = System.Drawing.Color.Transparent;
            this.joinMembershipButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.joinMembershipButton.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.joinMembershipButton.FlatAppearance.BorderSize = 2;
            this.joinMembershipButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.joinMembershipButton.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.joinMembershipButton.ForeColor = System.Drawing.Color.White;
            this.joinMembershipButton.Location = new System.Drawing.Point(185, 0);
            this.joinMembershipButton.Name = "joinMembershipButton";
            this.joinMembershipButton.Size = new System.Drawing.Size(165, 45);
            this.joinMembershipButton.TabIndex = 1;
            this.joinMembershipButton.Text = "Join Membership";
            this.joinMembershipButton.UseVisualStyleBackColor = false;
            // 
            // imagePanel
            // 
            this.imagePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.imagePanel.Controls.Add(this.imagePlaceholder);
            this.imagePanel.Location = new System.Drawing.Point(650, 15);
            this.imagePanel.Name = "imagePanel";
            this.imagePanel.Size = new System.Drawing.Size(520, 315);
            this.imagePanel.TabIndex = 1;
            // 
            // imagePlaceholder
            // 
            this.imagePlaceholder.AutoSize = true;
            this.imagePlaceholder.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.imagePlaceholder.ForeColor = System.Drawing.Color.White;
            this.imagePlaceholder.Location = new System.Drawing.Point(165, 145);
            this.imagePlaceholder.Name = "imagePlaceholder";
            this.imagePlaceholder.Size = new System.Drawing.Size(198, 25);
            this.imagePlaceholder.TabIndex = 0;
            this.imagePlaceholder.Text = "Circular Library Image";
            // 
            // featuresPanel
            // 
            this.featuresPanel.BackColor = System.Drawing.Color.White;
            this.featuresPanel.Controls.Add(this.card1);
            this.featuresPanel.Controls.Add(this.iconBg1);
            this.featuresPanel.Controls.Add(this.card2);
            this.featuresPanel.Controls.Add(this.iconBg2);
            this.featuresPanel.Controls.Add(this.card3);
            this.featuresPanel.Controls.Add(this.iconBg3);
            this.featuresPanel.Location = new System.Drawing.Point(0, 345);
            this.featuresPanel.Name = "featuresPanel";
            this.featuresPanel.Size = new System.Drawing.Size(1200, 295);
            this.featuresPanel.TabIndex = 1;
            // 
            // card1
            // 
            this.card1.BackColor = System.Drawing.Color.White;
            this.card1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.card1.Controls.Add(this.cardTitle1);
            this.card1.Controls.Add(this.cardDesc1);
            this.card1.Controls.Add(this.icon1);
            this.card1.Location = new System.Drawing.Point(100, 50);
            this.card1.Name = "card1";
            this.card1.Size = new System.Drawing.Size(300, 195);
            this.card1.TabIndex = 0;
            // 
            // cardTitle1
            // 
            this.cardTitle1.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.cardTitle1.Location = new System.Drawing.Point(35, 120);
            this.cardTitle1.Name = "cardTitle1";
            this.cardTitle1.Size = new System.Drawing.Size(230, 30);
            this.cardTitle1.TabIndex = 0;
            this.cardTitle1.Text = "Featured Collection";
            this.cardTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cardDesc1
            // 
            this.cardDesc1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cardDesc1.ForeColor = System.Drawing.Color.Gray;
            this.cardDesc1.Location = new System.Drawing.Point(20, 155);
            this.cardDesc1.Name = "cardDesc1";
            this.cardDesc1.Size = new System.Drawing.Size(260, 35);
            this.cardDesc1.TabIndex = 1;
            this.cardDesc1.Text = "Curated selections from bestsellers and hidden\ngems";
            this.cardDesc1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // iconBg1
            // 
            this.iconBg1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.iconBg1.Location = new System.Drawing.Point(115, 30);
            this.iconBg1.Name = "iconBg1";
            this.iconBg1.Size = new System.Drawing.Size(70, 70);
            this.iconBg1.TabIndex = 1;
            // 
            // icon1
            // 
            this.icon1.AutoSize = true;
            this.icon1.Font = new System.Drawing.Font("Segoe UI", 28F);
            this.icon1.Location = new System.Drawing.Point(114, 34);
            this.icon1.Name = "icon1";
            this.icon1.Size = new System.Drawing.Size(72, 51);
            this.icon1.TabIndex = 2;
            this.icon1.Text = "📈";
            // 
            // card2
            // 
            this.card2.BackColor = System.Drawing.Color.White;
            this.card2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.card2.Controls.Add(this.cardTitle2);
            this.card2.Controls.Add(this.cardDesc2);
            this.card2.Controls.Add(this.icon2);
            this.card2.Location = new System.Drawing.Point(450, 50);
            this.card2.Name = "card2";
            this.card2.Size = new System.Drawing.Size(300, 195);
            this.card2.TabIndex = 3;
            // 
            // cardTitle2
            // 
            this.cardTitle2.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.cardTitle2.Location = new System.Drawing.Point(35, 120);
            this.cardTitle2.Name = "cardTitle2";
            this.cardTitle2.Size = new System.Drawing.Size(230, 30);
            this.cardTitle2.TabIndex = 0;
            this.cardTitle2.Text = "Membership Benefits";
            this.cardTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cardDesc2
            // 
            this.cardDesc2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cardDesc2.ForeColor = System.Drawing.Color.Gray;
            this.cardDesc2.Location = new System.Drawing.Point(20, 155);
            this.cardDesc2.Name = "cardDesc2";
            this.cardDesc2.Size = new System.Drawing.Size(260, 35);
            this.cardDesc2.TabIndex = 1;
            this.cardDesc2.Text = "Exclusive discounts and early access to new\nreleases";
            this.cardDesc2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // iconBg2
            // 
            this.iconBg2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.iconBg2.Location = new System.Drawing.Point(464, 30);
            this.iconBg2.Name = "iconBg2";
            this.iconBg2.Size = new System.Drawing.Size(70, 70);
            this.iconBg2.TabIndex = 4;
            // 
            // icon2
            // 
            this.icon2.AutoSize = true;
            this.icon2.BackColor = System.Drawing.Color.White;
            this.icon2.Font = new System.Drawing.Font("Segoe UI", 28F);
            this.icon2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.icon2.Location = new System.Drawing.Point(127, 34);
            this.icon2.Name = "icon2";
            this.icon2.Size = new System.Drawing.Size(61, 51);
            this.icon2.TabIndex = 5;
            this.icon2.Text = "💡";
            // 
            // card3
            // 
            this.card3.BackColor = System.Drawing.Color.White;
            this.card3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.card3.Controls.Add(this.cardTitle3);
            this.card3.Controls.Add(this.cardDesc3);
            this.card3.Controls.Add(this.icon3);
            this.card3.Location = new System.Drawing.Point(800, 50);
            this.card3.Name = "card3";
            this.card3.Size = new System.Drawing.Size(300, 195);
            this.card3.TabIndex = 6;
            // 
            // cardTitle3
            // 
            this.cardTitle3.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.cardTitle3.Location = new System.Drawing.Point(35, 120);
            this.cardTitle3.Name = "cardTitle3";
            this.cardTitle3.Size = new System.Drawing.Size(230, 30);
            this.cardTitle3.TabIndex = 0;
            this.cardTitle3.Text = "Expert Reviews";
            this.cardTitle3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cardDesc3
            // 
            this.cardDesc3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cardDesc3.ForeColor = System.Drawing.Color.Gray;
            this.cardDesc3.Location = new System.Drawing.Point(20, 155);
            this.cardDesc3.Name = "cardDesc3";
            this.cardDesc3.Size = new System.Drawing.Size(260, 35);
            this.cardDesc3.TabIndex = 1;
            this.cardDesc3.Text = "Detailed reviews and ratings from our community";
            this.cardDesc3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // iconBg3
            // 
            this.iconBg3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.iconBg3.Location = new System.Drawing.Point(820, 30);
            this.iconBg3.Name = "iconBg3";
            this.iconBg3.Size = new System.Drawing.Size(70, 70);
            this.iconBg3.TabIndex = 7;
            // 
            // icon3
            // 
            this.icon3.AutoSize = true;
            this.icon3.Font = new System.Drawing.Font("Segoe UI", 28F);
            this.icon3.Location = new System.Drawing.Point(128, 34);
            this.icon3.Name = "icon3";
            this.icon3.Size = new System.Drawing.Size(55, 51);
            this.icon3.TabIndex = 8;
            this.icon3.Text = "⭐";
            // 
            // UserDashboardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.heroPanel);
            this.Controls.Add(this.featuresPanel);
            this.Name = "UserDashboardControl";
            this.Size = new System.Drawing.Size(1200, 640);
            this.heroPanel.ResumeLayout(false);
            this.leftContent.ResumeLayout(false);
            this.badgePanel.ResumeLayout(false);
            this.badgePanel.PerformLayout();
            this.buttonsPanel.ResumeLayout(false);
            this.imagePanel.ResumeLayout(false);
            this.imagePanel.PerformLayout();
            this.featuresPanel.ResumeLayout(false);
            this.card1.ResumeLayout(false);
            this.card1.PerformLayout();
            this.card2.ResumeLayout(false);
            this.card2.PerformLayout();
            this.card3.ResumeLayout(false);
            this.card3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Panel heroPanel;
        private Panel leftContent;
        private Panel badgePanel;
        private Label badgeIcon;
        private Label badgeLabel;
        private Label headingLabel;
        private Label descLabel;
        private Panel buttonsPanel;
        private Button browseCatalogButton;
        private Button joinMembershipButton;
        private Panel imagePanel;
        private Label imagePlaceholder;
        private Panel featuresPanel;
        private Panel card1;
        private Label cardTitle1;
        private Label cardDesc1;
        private Panel iconBg1;
        private Label icon1;
        private Panel card2;
        private Label cardTitle2;
        private Label cardDesc2;
        private Panel iconBg2;
        private Label icon2;
        private Panel card3;
        private Label cardTitle3;
        private Label cardDesc3;
        private Panel iconBg3;
        private Label icon3;
    }
}