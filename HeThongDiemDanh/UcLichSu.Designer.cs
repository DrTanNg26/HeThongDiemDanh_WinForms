namespace HeThongDiemDanh
{
    partial class UcLichSu
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlFilter = new FlowLayoutPanel();
            lblHocKy = new Label();
            cboHocKy = new ComboBox();
            lblMon = new Label();
            cboMonHoc = new ComboBox();
            btnTaiLai = new Button();
            pnlStat = new FlowLayoutPanel();
            lblStat = new Label();
            lblCanhBao = new Label();
            dgv = new DataGridView();
            pnlFilter.SuspendLayout();
            pnlStat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            SuspendLayout();
            // 
            // pnlFilter
            // 
            pnlFilter.BackColor = SystemColors.Control;
            pnlFilter.Controls.Add(lblHocKy);
            pnlFilter.Controls.Add(cboHocKy);
            pnlFilter.Controls.Add(lblMon);
            pnlFilter.Controls.Add(cboMonHoc);
            pnlFilter.Controls.Add(btnTaiLai);
            pnlFilter.Dock = DockStyle.Top;
            pnlFilter.Location = new Point(0, 0);
            pnlFilter.Name = "pnlFilter";
            pnlFilter.Padding = new Padding(6, 6, 0, 0);
            pnlFilter.Size = new Size(1025, 40);
            pnlFilter.TabIndex = 3;
            // 
            // lblHocKy
            // 
            lblHocKy.AutoSize = true;
            lblHocKy.Location = new Point(6, 11);
            lblHocKy.Margin = new Padding(0, 5, 4, 0);
            lblHocKy.Name = "lblHocKy";
            lblHocKy.Size = new Size(57, 20);
            lblHocKy.TabIndex = 0;
            lblHocKy.Text = "Học kỳ:";
            // 
            // cboHocKy
            // 
            cboHocKy.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHocKy.Location = new Point(67, 9);
            cboHocKy.Margin = new Padding(0, 3, 8, 0);
            cboHocKy.Name = "cboHocKy";
            cboHocKy.Size = new Size(130, 28);
            cboHocKy.TabIndex = 1;
            // 
            // lblMon
            // 
            lblMon.AutoSize = true;
            lblMon.Location = new Point(205, 11);
            lblMon.Margin = new Padding(0, 5, 4, 0);
            lblMon.Name = "lblMon";
            lblMon.Size = new Size(50, 20);
            lblMon.TabIndex = 2;
            lblMon.Text = "  Môn:";
            // 
            // cboMonHoc
            // 
            cboMonHoc.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMonHoc.Location = new Point(259, 9);
            cboMonHoc.Margin = new Padding(0, 3, 8, 0);
            cboMonHoc.Name = "cboMonHoc";
            cboMonHoc.Size = new Size(260, 28);
            cboMonHoc.TabIndex = 3;
            // 
            // btnTaiLai
            // 
            btnTaiLai.Location = new Point(531, 9);
            btnTaiLai.Margin = new Padding(4, 3, 0, 0);
            btnTaiLai.Name = "btnTaiLai";
            btnTaiLai.Size = new Size(75, 26);
            btnTaiLai.TabIndex = 4;
            btnTaiLai.Text = "Tải lại";
            // 
            // pnlStat
            // 
            pnlStat.BackColor = SystemColors.Control;
            pnlStat.Controls.Add(lblStat);
            pnlStat.Dock = DockStyle.Top;
            pnlStat.Location = new Point(0, 40);
            pnlStat.Name = "pnlStat";
            pnlStat.Padding = new Padding(6, 3, 0, 0);
            pnlStat.Size = new Size(1025, 26);
            pnlStat.TabIndex = 2;
            // 
            // lblStat
            // 
            lblStat.AutoSize = true;
            lblStat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStat.Location = new Point(9, 3);
            lblStat.Name = "lblStat";
            lblStat.Size = new Size(0, 20);
            lblStat.TabIndex = 0;
            // 
            // lblCanhBao
            // 
            lblCanhBao.BackColor = Color.MistyRose;
            lblCanhBao.Dock = DockStyle.Top;
            lblCanhBao.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCanhBao.ForeColor = Color.DarkRed;
            lblCanhBao.Location = new Point(0, 66);
            lblCanhBao.Name = "lblCanhBao";
            lblCanhBao.Padding = new Padding(6, 2, 0, 0);
            lblCanhBao.Size = new Size(1025, 22);
            lblCanhBao.TabIndex = 1;
            lblCanhBao.Visible = false;
            lblCanhBao.Click += lblCanhBao_Click;
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = SystemColors.Window;
            dgv.BorderStyle = BorderStyle.None;
            dgv.ColumnHeadersHeight = 29;
            dgv.Dock = DockStyle.Fill;
            dgv.Location = new Point(0, 88);
            dgv.Name = "dgv";
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.RowHeadersWidth = 51;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new Size(1025, 639);
            dgv.TabIndex = 0;
            // 
            // UcLichSu
            // 
            Controls.Add(dgv);
            Controls.Add(lblCanhBao);
            Controls.Add(pnlStat);
            Controls.Add(pnlFilter);
            Font = new Font("Segoe UI", 9F);
            Name = "UcLichSu";
            Size = new Size(1025, 727);
            pnlFilter.ResumeLayout(false);
            pnlFilter.PerformLayout();
            pnlStat.ResumeLayout(false);
            pnlStat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            ResumeLayout(false);
        }

        // ── Khai báo fields ───────────────────────────────────────────────
        private System.Windows.Forms.FlowLayoutPanel pnlFilter, pnlStat;
        private System.Windows.Forms.Label  lblHocKy, lblMon, lblStat, lblCanhBao;
        private System.Windows.Forms.ComboBox cboHocKy, cboMonHoc;
        private System.Windows.Forms.Button btnTaiLai;
        private System.Windows.Forms.DataGridView dgv;
    }
}
