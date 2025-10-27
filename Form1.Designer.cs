namespace DocGhiFileCSV
{
    partial class Form1
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
            this.btnread = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnread
            // 
            this.btnread.BackColor = System.Drawing.Color.Silver;
            this.btnread.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnread.Font = new System.Drawing.Font("Times New Roman", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnread.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnread.Location = new System.Drawing.Point(421, 12);
            this.btnread.Name = "btnread";
            this.btnread.Size = new System.Drawing.Size(349, 91);
            this.btnread.TabIndex = 0;
            this.btnread.Text = "Đọc File CSV";
            this.btnread.UseVisualStyleBackColor = false;
            this.btnread.Click += new System.EventHandler(this.btnread_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 150);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 82;
            this.dataGridView2.RowTemplate.Height = 33;
            this.dataGridView2.Size = new System.Drawing.Size(1157, 371);
            this.dataGridView2.TabIndex = 1;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(1181, 533);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.btnread);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnread;
        private System.Windows.Forms.DataGridView dataGridView2;
    }
}
