namespace HeThongDiemDanh
{
    partial class UcTongKet
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
            lblLop = new Label();
            cboLop = new ComboBox();
            btnTaiLai = new Button();
            btnTK = new Button();
            btnXLS = new Button();
            pnlStat = new FlowLayoutPanel();
            lblTT = new Label();
            lblStat = new Label();
            dgv = new DataGridView();
            pnlFilter.SuspendLayout();
            pnlStat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            SuspendLayout();
            // 
            // pnlFilter
            // 
            pnlFilter.BackColor = SystemColors.Control;
            pnlFilter.Controls.Add(lblLop);
            pnlFilter.Controls.Add(cboLop);
            pnlFilter.Controls.Add(btnTaiLai);
            pnlFilter.Controls.Add(btnTK);
            pnlFilter.Controls.Add(btnXLS);
            pnlFilter.Dock = DockStyle.Top;
            pnlFilter.Location = new Point(0, 0);
            pnlFilter.Name = "pnlFilter";
            pnlFilter.Padding = new Padding(6, 6, 0, 0);
            pnlFilter.Size = new Size(1025, 40);
            pnlFilter.TabIndex = 2;
            // 
            // lblLop
            // 
            lblLop.AutoSize = true;
            lblLop.Location = new Point(6, 11);
            lblLop.Margin = new Padding(0, 5, 4, 0);
            lblLop.Name = "lblLop";
            lblLop.Size = new Size(60, 20);
            lblLop.TabIndex = 0;
            lblLop.Text = "Lớp HP:";
            // 
            // cboLop
            // 
            cboLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLop.Location = new Point(70, 9);
            cboLop.Margin = new Padding(0, 3, 8, 0);
            cboLop.Name = "cboLop";
            cboLop.Size = new Size(260, 28);
            cboLop.TabIndex = 1;
            // 
            // btnTaiLai
            // 
            btnTaiLai.Location = new Point(342, 9);
            btnTaiLai.Margin = new Padding(4, 3, 0, 0);
            btnTaiLai.Name = "btnTaiLai";
            btnTaiLai.Size = new Size(75, 26);
            btnTaiLai.TabIndex = 2;
            btnTaiLai.Text = "Tải lại";
            // 
            // btnTK
            // 
            btnTK.Enabled = false;
            btnTK.Location = new Point(423, 9);
            btnTK.Margin = new Padding(6, 3, 0, 0);
            btnTK.Name = "btnTK";
            btnTK.Size = new Size(130, 26);
            btnTK.TabIndex = 3;
            btnTK.Text = "Tổng kết học kỳ";
            // 
            // btnXLS
            // 
            btnXLS.Location = new Point(559, 9);
            btnXLS.Margin = new Padding(6, 3, 0, 0);
            btnXLS.Name = "btnXLS";
            btnXLS.Size = new Size(130, 26);
            btnXLS.TabIndex = 4;
            btnXLS.Text = "Xuất Excel";
            // 
            // pnlStat
            // 
            pnlStat.BackColor = SystemColors.Control;
            pnlStat.Controls.Add(lblTT);
            pnlStat.Controls.Add(lblStat);
            pnlStat.Dock = DockStyle.Top;
            pnlStat.Location = new Point(0, 40);
            pnlStat.Name = "pnlStat";
            pnlStat.Padding = new Padding(6, 3, 0, 0);
            pnlStat.Size = new Size(1025, 26);
            pnlStat.TabIndex = 1;
            // 
            // lblTT
            // 
            lblTT.AutoSize = true;
            lblTT.Location = new Point(6, 3);
            lblTT.Margin = new Padding(0, 0, 20, 0);
            lblTT.Name = "lblTT";
            lblTT.Size = new Size(0, 20);
            lblTT.TabIndex = 0;
            // 
            // lblStat
            // 
            lblStat.AutoSize = true;
            lblStat.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblStat.Location = new Point(29, 3);
            lblStat.Name = "lblStat";
            lblStat.Size = new Size(0, 20);
            lblStat.TabIndex = 1;
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = SystemColors.Window;
            dgv.BorderStyle = BorderStyle.None;
            dgv.ColumnHeadersHeight = 29;
            dgv.Dock = DockStyle.Fill;
            dgv.Location = new Point(0, 66);
            dgv.Name = "dgv";
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.RowHeadersWidth = 51;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new Size(1025, 661);
            dgv.TabIndex = 0;
            // 
            // UcTongKet
            // 
            Controls.Add(dgv);
            Controls.Add(pnlStat);
            Controls.Add(pnlFilter);
            Font = new Font("Segoe UI", 9F);
            Name = "UcTongKet";
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
        private System.Windows.Forms.Label  lblLop, lblTT, lblStat;
        private System.Windows.Forms.ComboBox cboLop;
        private System.Windows.Forms.Button btnTaiLai, btnTK, btnXLS;
        private System.Windows.Forms.DataGridView dgv;
    }
}
