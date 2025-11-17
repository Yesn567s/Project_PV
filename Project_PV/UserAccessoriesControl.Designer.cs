namespace Project_PV
{
    partial class UserAccessoriesControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelItemCount = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.sortByComboBox = new System.Windows.Forms.ComboBox();
            this.filterByComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(50, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(303, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bookstore Accessories";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(53, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(439, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Enhance your reading experience with our curated accessories";
            // 
            // labelItemCount
            // 
            this.labelItemCount.AutoSize = true;
            this.labelItemCount.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelItemCount.Location = new System.Drawing.Point(52, 116);
            this.labelItemCount.Name = "labelItemCount";
            this.labelItemCount.Size = new System.Drawing.Size(152, 25);
            this.labelItemCount.TabIndex = 2;
            this.labelItemCount.Text = "Showing ... Items";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.sortByComboBox);
            this.panel1.Controls.Add(this.filterByComboBox);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.searchBox);
            this.panel1.Location = new System.Drawing.Point(57, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1080, 50);
            this.panel1.TabIndex = 3;
            // 
            // sortByComboBox
            // 
            this.sortByComboBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.sortByComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sortByComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.sortByComboBox.FormattingEnabled = true;
            this.sortByComboBox.Location = new System.Drawing.Point(886, 16);
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
            this.filterByComboBox.Location = new System.Drawing.Point(687, 16);
            this.filterByComboBox.Name = "filterByComboBox";
            this.filterByComboBox.Size = new System.Drawing.Size(175, 21);
            this.filterByComboBox.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(350, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 17);
            this.label4.TabIndex = 4;
            this.label4.Text = "🔎";
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
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(57, 144);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(1080, 434);
            this.dataGridView1.TabIndex = 4;
            // 
            // UserAccessoriesControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.labelItemCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "UserAccessoriesControl";
            this.Size = new System.Drawing.Size(1200, 680);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelItemCount;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.ComboBox sortByComboBox;
        private System.Windows.Forms.ComboBox filterByComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
