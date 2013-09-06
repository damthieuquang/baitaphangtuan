namespace TienXyLyDuLieu
{
    partial class Form_ThongBao
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
            this.dataGridView_ThongBao = new System.Windows.Forms.DataGridView();
            this.cl_bin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cl_min = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cl_max = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cl_giatri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.test = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ThongBao)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView_ThongBao
            // 
            this.dataGridView_ThongBao.AllowUserToAddRows = false;
            this.dataGridView_ThongBao.AllowUserToDeleteRows = false;
            this.dataGridView_ThongBao.AllowUserToResizeColumns = false;
            this.dataGridView_ThongBao.AllowUserToResizeRows = false;
            this.dataGridView_ThongBao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_ThongBao.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.cl_bin,
            this.cl_min,
            this.cl_max,
            this.cl_giatri,
            this.test});
            this.dataGridView_ThongBao.Location = new System.Drawing.Point(12, 12);
            this.dataGridView_ThongBao.Name = "dataGridView_ThongBao";
            this.dataGridView_ThongBao.Size = new System.Drawing.Size(308, 302);
            this.dataGridView_ThongBao.TabIndex = 0;
            // 
            // cl_bin
            // 
            this.cl_bin.HeaderText = "Bin";
            this.cl_bin.Name = "cl_bin";
            this.cl_bin.ReadOnly = true;
            this.cl_bin.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.cl_bin.Width = 30;
            // 
            // cl_min
            // 
            this.cl_min.HeaderText = "Min";
            this.cl_min.Name = "cl_min";
            this.cl_min.ReadOnly = true;
            this.cl_min.Width = 50;
            // 
            // cl_max
            // 
            this.cl_max.HeaderText = "Max";
            this.cl_max.Name = "cl_max";
            this.cl_max.Width = 50;
            // 
            // cl_giatri
            // 
            this.cl_giatri.HeaderText = "Giá trị giỏ";
            this.cl_giatri.Name = "cl_giatri";
            this.cl_giatri.ReadOnly = true;
            this.cl_giatri.Width = 90;
            // 
            // test
            // 
            this.test.HeaderText = "Count";
            this.test.Name = "test";
            this.test.Width = 50;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(25, 320);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(98, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "Điền vào bảng";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(187, 320);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(75, 23);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // Form_ThongBao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(332, 355);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.dataGridView_ThongBao);
            this.Name = "Form_ThongBao";
            this.Text = "Thông Báo";
            this.Load += new System.EventHandler(this.ThongBao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ThongBao)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView_ThongBao;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.DataGridViewTextBoxColumn cl_bin;
        private System.Windows.Forms.DataGridViewTextBoxColumn cl_min;
        private System.Windows.Forms.DataGridViewTextBoxColumn cl_max;
        private System.Windows.Forms.DataGridViewTextBoxColumn cl_giatri;
        private System.Windows.Forms.DataGridViewTextBoxColumn test;
    }
}