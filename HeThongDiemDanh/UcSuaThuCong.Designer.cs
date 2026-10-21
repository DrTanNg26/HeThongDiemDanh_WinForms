namespace HeThongDiemDanh
{
    partial class UcSuaThuCong
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
            lblBuoi = new Label();
            cboBuoi = new ComboBox();
            btnTaiLai = new Button();
            dgv = new DataGridView();
            pnlEdit = new Panel();
            pnlRow = new FlowLayoutPanel();
            lblTT = new Label();
            cbTT = new ComboBox();
            lblLyDo = new Label();
            txtLyDo = new TextBox();
            btnLuu = new Button();
            lblChon = new Label();
            pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            pnlEdit.SuspendLayout();
            pnlRow.SuspendLayout();
            SuspendLayout();
            // 
            // pnlFilter
            // 
            pnlFilter.BackColor = SystemColors.Control;
            pnlFilter.Controls.Add(lblLop);
            pnlFilter.Controls.Add(cboLop);
            pnlFilter.Controls.Add(lblBuoi);
            pnlFilter.Controls.Add(cboBuoi);
            pnlFilter.Controls.Add(btnTaiLai);
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
            cboLop.Size = new Size(230, 28);
            cboLop.TabIndex = 1;
            // 
            // lblBuoi
            // 
            lblBuoi.AutoSize = true;
            lblBuoi.Location = new Point(308, 11);
            lblBuoi.Margin = new Padding(0, 5, 4, 0);
            lblBuoi.Name = "lblBuoi";
            lblBuoi.Size = new Size(120, 20);
            lblBuoi.TabIndex = 2;
            lblBuoi.Text = "  Buổi (đã đóng):";
            // 
            // cboBuoi
            // 
            cboBuoi.DropDownStyle = ComboBoxStyle.DropDownList;
            cboBuoi.Location = new Point(432, 9);
            cboBuoi.Margin = new Padding(0, 3, 8, 0);
            cboBuoi.Name = "cboBuoi";
            cboBuoi.Size = new Size(220, 28);
            cboBuoi.TabIndex = 3;
            // 
            // btnTaiLai
            // 
            btnTaiLai.Location = new Point(664, 9);
            btnTaiLai.Margin = new Padding(4, 3, 0, 0);
            btnTaiLai.Name = "btnTaiLai";
            btnTaiLai.Size = new Size(75, 26);
            btnTaiLai.TabIndex = 4;
            btnTaiLai.Text = "Tải lại";
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = SystemColors.Window;
            dgv.BorderStyle = BorderStyle.None;
            dgv.ColumnHeadersHeight = 29;
            dgv.Dock = DockStyle.Fill;
            dgv.Location = new Point(0, 40);
            dgv.Name = "dgv";
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.RowHeadersWidth = 51;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new Size(1025, 607);
            dgv.TabIndex = 0;
            // 
            // pnlEdit
            // 
            pnlEdit.BackColor = SystemColors.Control;
            pnlEdit.Controls.Add(pnlRow);
            pnlEdit.Controls.Add(lblChon);
            pnlEdit.Dock = DockStyle.Bottom;
            pnlEdit.Location = new Point(0, 647);
            pnlEdit.Name = "pnlEdit";
            pnlEdit.Padding = new Padding(8);
            pnlEdit.Size = new Size(1025, 80);
            pnlEdit.TabIndex = 1;
            // 
            // pnlRow
            // 
            pnlRow.BackColor = SystemColors.Control;
            pnlRow.Controls.Add(lblTT);
            pnlRow.Controls.Add(cbTT);
            pnlRow.Controls.Add(lblLyDo);
            pnlRow.Controls.Add(txtLyDo);
            pnlRow.Controls.Add(btnLuu);
            pnlRow.Dock = DockStyle.Top;
            pnlRow.Location = new Point(8, 28);
            pnlRow.Name = "pnlRow";
            pnlRow.Padding = new Padding(0, 4, 0, 0);
            pnlRow.Size = new Size(1009, 36);
            pnlRow.TabIndex = 0;
            // 
            // lblTT
            // 
            lblTT.AutoSize = true;
            lblTT.Location = new Point(0, 9);
            lblTT.Margin = new Padding(0, 5, 4, 0);
            lblTT.Name = "lblTT";
            lblTT.Size = new Size(78, 20);
            lblTT.TabIndex = 0;
            lblTT.Text = "Trạng thái:";
            // 
            // cbTT
            // 
            cbTT.DropDownStyle = ComboBoxStyle.DropDownList;
            cbTT.Items.AddRange(new object[] { "Có mặt", "Đi muộn", "Vắng", "Nghỉ có phép" });
            cbTT.Location = new Point(82, 7);
            cbTT.Margin = new Padding(0, 3, 8, 0);
            cbTT.Name = "cbTT";
            cbTT.Size = new Size(140, 28);
            cbTT.TabIndex = 1;
            // 
            // lblLyDo
            // 
            lblLyDo.AutoSize = true;
            lblLyDo.Location = new Point(230, 9);
            lblLyDo.Margin = new Padding(0, 5, 4, 0);
            lblLyDo.Name = "lblLyDo";
            lblLyDo.Size = new Size(128, 20);
            lblLyDo.TabIndex = 2;
            lblLyDo.Text = "  Lý do (bắt buộc):";
            // 
            // txtLyDo
            // 
            txtLyDo.Location = new Point(368, 7);
            txtLyDo.Margin = new Padding(6, 3, 6, 0);
            txtLyDo.Name = "txtLyDo";
            txtLyDo.Size = new Size(360, 27);
            txtLyDo.TabIndex = 3;
            // 
            // btnLuu
            // 
            btnLuu.Location = new Point(738, 7);
            btnLuu.Margin = new Padding(4, 3, 0, 0);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(75, 26);
            btnLuu.TabIndex = 4;
            btnLuu.Text = "Lưu";
            // 
            // lblChon
            // 
            lblChon.Dock = DockStyle.Top;
            lblChon.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblChon.ForeColor = Color.Gray;
            lblChon.Location = new Point(8, 8);
            lblChon.Name = "lblChon";
            lblChon.Size = new Size(1009, 20);
            lblChon.TabIndex = 1;
            lblChon.Text = "→ Chọn sinh viên cần sửa";
            // 
            // UcSuaThuCong
            // 
            Controls.Add(dgv);
            Controls.Add(pnlEdit);
            Controls.Add(pnlFilter);
            Font = new Font("Segoe UI", 9F);
            Name = "UcSuaThuCong";
            Size = new Size(1025, 727);
            pnlFilter.ResumeLayout(false);
            pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            pnlEdit.ResumeLayout(false);
            pnlRow.ResumeLayout(false);
            pnlRow.PerformLayout();
            ResumeLayout(false);
        }

        // ── Khai báo fields ───────────────────────────────────────────────
        private System.Windows.Forms.FlowLayoutPanel pnlFilter, pnlRow;
        private System.Windows.Forms.Panel pnlEdit;
        private System.Windows.Forms.Label  lblLop, lblBuoi, lblChon, lblTT, lblLyDo;
        private System.Windows.Forms.ComboBox cboLop, cboBuoi, cbTT;
        private System.Windows.Forms.TextBox txtLyDo;
        private System.Windows.Forms.Button btnTaiLai, btnLuu;
        private System.Windows.Forms.DataGridView dgv;
    }
}
