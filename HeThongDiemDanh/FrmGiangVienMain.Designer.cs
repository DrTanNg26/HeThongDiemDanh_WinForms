namespace HeThongDiemDanh
{
    partial class FrmGiangVienMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            // Khởi tạo các UserControl của Thành viên 1, 4
            this._ucMoPhien    = new UcMoPhien();
            this._ucSuaThuCong = new UcSuaThuCong();
            this._ucTongKet    = new UcTongKet();

            this.pnlTop       = new System.Windows.Forms.Panel();
            this.lblTitle     = new System.Windows.Forms.Label();
            this.lblGiangVien = new System.Windows.Forms.Label();
            this.btnDangXuat  = new System.Windows.Forms.Button();
            this.tabMain      = new System.Windows.Forms.TabControl();
            this.tabMoPhien   = new System.Windows.Forms.TabPage();
            this.tabSuaThuCong= new System.Windows.Forms.TabPage();
            this.tabTongKet   = new System.Windows.Forms.TabPage();

            this.pnlTop.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.SuspendLayout();

            // pnlTop – thanh tiêu đề
            this.pnlTop.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlTop.Controls.Add(this.btnDangXuat);
            this.pnlTop.Controls.Add(this.lblGiangVien);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1024, 60);
            this.pnlTop.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(15, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "PHÂN HỆ GIẢNG VIÊN";

            // lblGiangVien
            this.lblGiangVien.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.lblGiangVien.AutoSize = true;
            this.lblGiangVien.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGiangVien.Location = new System.Drawing.Point(680, 20);
            this.lblGiangVien.Name = "lblGiangVien";
            this.lblGiangVien.TabIndex = 1;
            this.lblGiangVien.Text = "Xin chào: [Giảng viên]";

            // btnDangXuat
            this.btnDangXuat.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            this.btnDangXuat.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.btnDangXuat.Location = new System.Drawing.Point(898, 12);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Size = new System.Drawing.Size(100, 35);
            this.btnDangXuat.TabIndex = 2;
            this.btnDangXuat.Text = "Đăng xuất";
            this.btnDangXuat.UseVisualStyleBackColor = true;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);

            // tabMain
            this.tabMain.Controls.Add(this.tabMoPhien);
            this.tabMain.Controls.Add(this.tabSuaThuCong);
            this.tabMain.Controls.Add(this.tabTongKet);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabMain.Location = new System.Drawing.Point(0, 60);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1024, 600);
            this.tabMain.TabIndex = 1;
            // Khi chuyển tab: làm mới dữ liệu UC tương ứng
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.TabMain_SelectedIndexChanged);

            // tabMoPhien – Tab 1: Mở phiên điểm danh (Thành viên 2)
            this._ucMoPhien.Name = "_ucMoPhien";
            this._ucMoPhien.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ucMoPhien.AutoScroll = true;
            this.tabMoPhien.Controls.Add(this._ucMoPhien);
            this.tabMoPhien.Location = new System.Drawing.Point(4, 26);
            this.tabMoPhien.Name = "tabMoPhien";
            this.tabMoPhien.Padding = new System.Windows.Forms.Padding(3);
            this.tabMoPhien.Size = new System.Drawing.Size(1016, 570);
            this.tabMoPhien.TabIndex = 0;
            this.tabMoPhien.Text = "Mở phiên điểm danh";
            this.tabMoPhien.UseVisualStyleBackColor = true;

            // tabSuaThuCong – Tab 2: Điều chỉnh thủ công (Thành viên 4)
            this._ucSuaThuCong.Name = "_ucSuaThuCong";
            this._ucSuaThuCong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabSuaThuCong.Controls.Add(this._ucSuaThuCong);
            this.tabSuaThuCong.Location = new System.Drawing.Point(4, 26);
            this.tabSuaThuCong.Name = "tabSuaThuCong";
            this.tabSuaThuCong.Padding = new System.Windows.Forms.Padding(3);
            this.tabSuaThuCong.Size = new System.Drawing.Size(1016, 570);
            this.tabSuaThuCong.TabIndex = 1;
            this.tabSuaThuCong.Text = "Điều chỉnh thủ công";
            this.tabSuaThuCong.UseVisualStyleBackColor = true;

            // tabTongKet – Tab 3: Tổng kết & xuất báo cáo (Thành viên 4)
            this._ucTongKet.Name = "_ucTongKet";
            this._ucTongKet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabTongKet.Controls.Add(this._ucTongKet);
            this.tabTongKet.Location = new System.Drawing.Point(4, 26);
            this.tabTongKet.Name = "tabTongKet";
            this.tabTongKet.Padding = new System.Windows.Forms.Padding(3);
            this.tabTongKet.Size = new System.Drawing.Size(1016, 570);
            this.tabTongKet.TabIndex = 2;
            this.tabTongKet.Text = "Tổng kết & Báo cáo";
            this.tabTongKet.UseVisualStyleBackColor = true;

            // FrmGiangVienMain
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 660);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.pnlTop);
            this.Name = "FrmGiangVienMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Điểm danh - Giảng viên";
            this.Load += new System.EventHandler(this.FrmGiangVienMain_Load);

            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        // Khai báo field
        private UcMoPhien     _ucMoPhien;
        private UcSuaThuCong  _ucSuaThuCong;
        private UcTongKet     _ucTongKet;
        private System.Windows.Forms.Panel     pnlTop;
        private System.Windows.Forms.Label     lblTitle;
        private System.Windows.Forms.Label     lblGiangVien;
        private System.Windows.Forms.Button    btnDangXuat;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage   tabMoPhien;
        private System.Windows.Forms.TabPage   tabSuaThuCong;
        private System.Windows.Forms.TabPage   tabTongKet;
    }
}
