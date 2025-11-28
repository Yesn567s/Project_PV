namespace Project_PV
{
    partial class AdminManagementControl
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
            this.bookManagementPanel = new System.Windows.Forms.Panel();
            this.AddBookButton = new System.Windows.Forms.Button();
            this.dataGridViewItems = new System.Windows.Forms.DataGridView();
            this.panel8 = new System.Windows.Forms.Panel();
            this.sortByComboBox = new System.Windows.Forms.ComboBox();
            this.filterByComboBox = new System.Windows.Forms.ComboBox();
            this.label32 = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.labelItemCount = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.bookManagementPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).BeginInit();
            this.panel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // bookManagementPanel
            // 
            this.bookManagementPanel.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.bookManagementPanel.Controls.Add(this.AddBookButton);
            this.bookManagementPanel.Controls.Add(this.dataGridViewItems);
            this.bookManagementPanel.Controls.Add(this.panel8);
            this.bookManagementPanel.Controls.Add(this.labelItemCount);
            this.bookManagementPanel.Controls.Add(this.label34);
            this.bookManagementPanel.Controls.Add(this.label35);
            this.bookManagementPanel.Location = new System.Drawing.Point(0, 0);
            this.bookManagementPanel.Name = "bookManagementPanel";
            this.bookManagementPanel.Size = new System.Drawing.Size(1043, 1302);
            this.bookManagementPanel.TabIndex = 28;
            // 
            // AddBookButton
            // 
            this.AddBookButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(40)))));
            this.AddBookButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AddBookButton.FlatAppearance.BorderSize = 0;
            this.AddBookButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddBookButton.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.AddBookButton.ForeColor = System.Drawing.Color.White;
            this.AddBookButton.Location = new System.Drawing.Point(847, 33);
            this.AddBookButton.Name = "AddBookButton";
            this.AddBookButton.Size = new System.Drawing.Size(113, 30);
            this.AddBookButton.TabIndex = 10;
            this.AddBookButton.Text = "+ Add New Item";
            this.AddBookButton.UseVisualStyleBackColor = false;
            this.AddBookButton.Click += new System.EventHandler(this.AddBookButton_Click);
            // 
            // dataGridViewItems
            // 
            this.dataGridViewItems.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridViewItems.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewItems.Location = new System.Drawing.Point(18, 165);
            this.dataGridViewItems.Name = "dataGridViewItems";
            this.dataGridViewItems.Size = new System.Drawing.Size(1012, 1124);
            this.dataGridViewItems.TabIndex = 9;
            // 
            // panel8
            // 
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.sortByComboBox);
            this.panel8.Controls.Add(this.filterByComboBox);
            this.panel8.Controls.Add(this.label32);
            this.panel8.Controls.Add(this.searchBox);
            this.panel8.Location = new System.Drawing.Point(18, 84);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(1012, 50);
            this.panel8.TabIndex = 8;
            // 
            // sortByComboBox
            // 
            this.sortByComboBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.sortByComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortByComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.sortByComboBox.FormattingEnabled = true;
            this.sortByComboBox.Location = new System.Drawing.Point(828, 15);
            this.sortByComboBox.Name = "sortByComboBox";
            this.sortByComboBox.Size = new System.Drawing.Size(175, 21);
            this.sortByComboBox.TabIndex = 6;
            // 
            // filterByComboBox
            // 
            this.filterByComboBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.filterByComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterByComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.filterByComboBox.FormattingEnabled = true;
            this.filterByComboBox.Location = new System.Drawing.Point(623, 16);
            this.filterByComboBox.Name = "filterByComboBox";
            this.filterByComboBox.Size = new System.Drawing.Size(175, 21);
            this.filterByComboBox.TabIndex = 5;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(350, 15);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(26, 17);
            this.label32.TabIndex = 4;
            this.label32.Text = "🔎";
            // 
            // searchBox
            // 
            this.searchBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.searchBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchBox.Location = new System.Drawing.Point(13, 15);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(331, 20);
            this.searchBox.TabIndex = 0;
            // 
            // labelItemCount
            // 
            this.labelItemCount.AutoSize = true;
            this.labelItemCount.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemCount.Location = new System.Drawing.Point(13, 137);
            this.labelItemCount.Name = "labelItemCount";
            this.labelItemCount.Size = new System.Drawing.Size(152, 25);
            this.labelItemCount.TabIndex = 7;
            this.labelItemCount.Text = "Showing ... Items";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.Location = new System.Drawing.Point(14, 60);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(137, 21);
            this.label34.TabIndex = 6;
            this.label34.Text = "Manage Your Item";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.label35.Location = new System.Drawing.Point(11, 23);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(255, 37);
            this.label35.TabIndex = 5;
            this.label35.Text = "Item Management";
            // 
            // AdminManagementControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bookManagementPanel);
            this.Name = "AdminManagementControl";
            this.Size = new System.Drawing.Size(1045, 1315);
            this.bookManagementPanel.ResumeLayout(false);
            this.bookManagementPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel bookManagementPanel;
        private System.Windows.Forms.DataGridView dataGridViewItems;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.ComboBox sortByComboBox;
        private System.Windows.Forms.ComboBox filterByComboBox;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Label labelItemCount;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Button AddBookButton;
    }
}
