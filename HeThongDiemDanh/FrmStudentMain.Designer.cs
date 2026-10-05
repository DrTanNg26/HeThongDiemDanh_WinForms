namespace HeThongDiemDanh;

partial class FrmStudentMain
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            lifetime.Cancel();
            components?.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        pnlHeader = new Panel();
        lblTitle = new Label();
        lblClock = new Label();
        btnLogout = new Button();
        pnlProfile = new Panel();
        lblHoTen = new Label();
        lblThongTin = new Label();
        lblEmail = new Label();
        tabs = new TabControl();
        tabDiemDanh = new TabPage();
        tabLichSu = new TabPage();
        pnlActions = new Panel();
        lblSection = new Label();
        btnRefresh = new Button();
        lblStatus = new Label();
        lblNextStep = new Label();
        lblHistory = new Label();
        dgvBuoiHoc = new DataGridView();
        colMon = new DataGridViewTextBoxColumn();
        colLop = new DataGridViewTextBoxColumn();
        colGiangVien = new DataGridViewTextBoxColumn();
        colPhong = new DataGridViewTextBoxColumn();
        colBatDau = new DataGridViewTextBoxColumn();
        colKetQua = new DataGridViewTextBoxColumn();
        clockTimer = new System.Windows.Forms.Timer(components);
        pnlHeader.SuspendLayout();
        pnlProfile.SuspendLayout();
        tabs.SuspendLayout();
        tabDiemDanh.SuspendLayout();
        tabLichSu.SuspendLayout();
        pnlActions.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvBuoiHoc).BeginInit();
        SuspendLayout();
        // Header
        pnlHeader.BackColor = Color.FromArgb(20, 49, 83);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Size = new Size(1100, 100);
        pnlHeader.Controls.Add(lblTitle);
        pnlHeader.Controls.Add(lblClock);
        pnlHeader.Controls.Add(btnLogout);
        pnlHeader.Name = "pnlHeader";
        lblTitle.Text = "UTT  |  CỔNG SINH VIÊN";
        lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        lblTitle.ForeColor = Color.White;
        lblTitle.Location = new Point(24, 18);
        lblTitle.Size = new Size(700, 42);
        lblTitle.Name = "lblTitle";
        lblClock.Text = "Ngày / giờ hiện tại";
        lblClock.ForeColor = Color.FromArgb(211, 227, 244);
        lblClock.Location = new Point(28, 65);
        lblClock.Size = new Size(500, 28);
        lblClock.Name = "lblClock";
        btnLogout.Text = "Đăng xuất";
        btnLogout.Location = new Point(932, 28);
        btnLogout.Size = new Size(140, 42);
        btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnLogout.BackColor = Color.White;
        btnLogout.ForeColor = Color.FromArgb(20, 49, 83);
        btnLogout.FlatStyle = FlatStyle.Flat;
        btnLogout.Name = "btnLogout";
        btnLogout.Click += btnLogout_Click;
        // Profile
        pnlProfile.Dock = DockStyle.Top;
        pnlProfile.Size = new Size(1100, 132);
        pnlProfile.BackColor = Color.White;
        pnlProfile.Controls.Add(lblHoTen);
        pnlProfile.Controls.Add(lblThongTin);
        pnlProfile.Controls.Add(lblEmail);
        lblHoTen.Text = "Thông tin sinh viên";
        lblHoTen.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        lblHoTen.Location = new Point(26, 16);
        lblHoTen.Size = new Size(1040, 38);
        lblHoTen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblHoTen.Name = "lblHoTen";
        lblThongTin.Text = "Mã sinh viên  •  Lớp hành chính  •  Khoa";
        lblThongTin.Location = new Point(28, 62);
        lblThongTin.Size = new Size(1040, 26);
        lblThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblThongTin.Name = "lblThongTin";
        lblEmail.Location = new Point(28, 94);
        lblEmail.Size = new Size(1040, 25);
        lblEmail.ForeColor = Color.FromArgb(91, 108, 128);
        lblEmail.Name = "lblEmail";
        // Tabs
        tabs.Dock = DockStyle.Fill;
        tabs.Name = "tabs";
        tabs.Controls.Add(tabDiemDanh);
        tabs.Controls.Add(tabLichSu);
        tabDiemDanh.Text = "Điểm danh buổi học";
        tabDiemDanh.Name = "tabDiemDanh";
        tabDiemDanh.Padding = new Padding(18);
        tabDiemDanh.BackColor = Color.FromArgb(243, 246, 250);
        tabDiemDanh.Controls.Add(dgvBuoiHoc);
        tabDiemDanh.Controls.Add(lblStatus);
        tabDiemDanh.Controls.Add(lblNextStep);
        tabDiemDanh.Controls.Add(pnlActions);
        pnlActions.Dock = DockStyle.Top;
        pnlActions.Height = 62;
        pnlActions.Controls.Add(lblSection);
        pnlActions.Controls.Add(btnRefresh);
        lblSection.Text = "Buổi học đang mở";
        lblSection.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblSection.Location = new Point(0, 13);
        lblSection.Size = new Size(600, 34);
        btnRefresh.Text = "Làm mới";
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(130, 40);
        btnRefresh.Dock = DockStyle.Right;
        btnRefresh.BackColor = Color.FromArgb(24, 94, 177);
        btnRefresh.ForeColor = Color.White;
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.Click += btnRefresh_Click;
        lblStatus.Dock = DockStyle.Bottom;
        lblStatus.Height = 54;
        lblStatus.Padding = new Padding(0, 12, 0, 0);
        lblStatus.Text = "Danh sách sẽ được tải sau khi đăng nhập.";
        lblStatus.Name = "lblStatus";
        lblNextStep.Dock = DockStyle.Bottom;
        lblNextStep.Height = 46;
        lblNextStep.Text = "Chức năng xác thực và nhập OTP sẽ được bổ sung ở bước tiếp theo.";
        lblNextStep.ForeColor = Color.FromArgb(91, 108, 128);
        // Read-only lesson list
        dgvBuoiHoc.Name = "dgvBuoiHoc";
        dgvBuoiHoc.Dock = DockStyle.Fill;
        dgvBuoiHoc.AutoGenerateColumns = false;
        dgvBuoiHoc.AllowUserToAddRows = false;
        dgvBuoiHoc.AllowUserToDeleteRows = false;
        dgvBuoiHoc.ReadOnly = true;
        dgvBuoiHoc.RowHeadersVisible = false;
        dgvBuoiHoc.MultiSelect = false;
        dgvBuoiHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvBuoiHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvBuoiHoc.BackgroundColor = Color.White;
        dgvBuoiHoc.BorderStyle = BorderStyle.None;
        dgvBuoiHoc.ColumnHeadersHeight = 42;
        dgvBuoiHoc.RowTemplate.Height = 38;
        dgvBuoiHoc.Columns.AddRange(new DataGridViewColumn[] { colMon, colLop, colGiangVien, colPhong, colBatDau, colKetQua });
        colMon.HeaderText = "Môn học"; colMon.DataPropertyName = "TenMon"; colMon.FillWeight = 130;
        colLop.HeaderText = "Lớp học phần"; colLop.DataPropertyName = "TenLopHP"; colLop.FillWeight = 140;
        colGiangVien.HeaderText = "Giảng viên"; colGiangVien.DataPropertyName = "GiangVien";
        colPhong.HeaderText = "Phòng"; colPhong.DataPropertyName = "PhongHoc"; colPhong.FillWeight = 60;
        colBatDau.HeaderText = "Giờ mở"; colBatDau.DataPropertyName = "ThoiGianBatDau"; colBatDau.FillWeight = 120;
        colBatDau.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        colKetQua.HeaderText = "Điểm danh"; colKetQua.DataPropertyName = "KetQua";
        // Reserved integration point for member 4
        tabLichSu.Text = "Lịch sử điểm danh";
        tabLichSu.Name = "tabLichSu";
        tabLichSu.BackColor = Color.White;
        tabLichSu.Controls.Add(lblHistory);
        lblHistory.Dock = DockStyle.Fill;
        lblHistory.TextAlign = ContentAlignment.MiddleCenter;
        lblHistory.Text = "Lịch sử điểm danh và thống kê cá nhân\r\nChức năng đang được phát triển.";
        lblHistory.Font = new Font("Segoe UI", 14F);
        lblHistory.ForeColor = Color.FromArgb(91, 108, 128);
        clockTimer.Interval = 1000;
        clockTimer.Tick += clockTimer_Tick;
        // Form
        AutoScaleDimensions = new SizeF(8F, 23F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 720);
        MinimumSize = new Size(1000, 650);
        Font = new Font("Segoe UI", 10F);
        ForeColor = Color.FromArgb(20, 49, 83);
        Controls.Add(tabs);
        Controls.Add(pnlProfile);
        Controls.Add(pnlHeader);
        Name = "FrmStudentMain";
        Text = "Sinh viên | Hệ thống điểm danh UTT";
        StartPosition = FormStartPosition.CenterScreen;
        Load += FrmStudentMain_Load;
        pnlHeader.ResumeLayout(false);
        pnlProfile.ResumeLayout(false);
        tabs.ResumeLayout(false);
        tabDiemDanh.ResumeLayout(false);
        tabLichSu.ResumeLayout(false);
        pnlActions.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvBuoiHoc).EndInit();
        ResumeLayout(false);
    }

    private Panel pnlHeader, pnlProfile, pnlActions;
    private Label lblTitle, lblClock, lblHoTen, lblThongTin, lblEmail, lblSection, lblStatus, lblNextStep, lblHistory;
    private Button btnLogout, btnRefresh;
    private TabControl tabs;
    private TabPage tabDiemDanh, tabLichSu;
    private DataGridView dgvBuoiHoc;
    private DataGridViewTextBoxColumn colMon, colLop, colGiangVien, colPhong, colBatDau, colKetQua;
    private System.Windows.Forms.Timer clockTimer;
}

