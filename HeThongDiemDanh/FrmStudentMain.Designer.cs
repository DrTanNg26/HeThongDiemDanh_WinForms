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
        dgvBuoiHoc = new DataGridView();
        lblStatus = new Label();
        lblNextStep = new Label();
        pnlActions = new Panel();
        lblSection = new Label();
        btnRefresh = new Button();
        btnDiemDanh = new Button();
        tabLichSu = new TabPage();
        lblHistory = new Label();
        pnlLocLichSu = new Panel();
        lblLopLichSu = new Label();
        lblTrangThaiLichSu = new Label();
        cboLopLichSu = new ComboBox();
        cboTrangThaiLichSu = new ComboBox();
        btnLamMoiLichSu = new Button();
        dgvLichSu = new DataGridView();
        clockTimer = new System.Windows.Forms.Timer(components);
        pnlHeader.SuspendLayout();
        pnlProfile.SuspendLayout();
        tabs.SuspendLayout();
        tabDiemDanh.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvBuoiHoc).BeginInit();
        pnlActions.SuspendLayout();
        tabLichSu.SuspendLayout();
        pnlLocLichSu.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvLichSu).BeginInit();
        SuspendLayout();
        // 
        // pnlHeader
        // 
        pnlHeader.BackColor = Color.FromArgb(20, 49, 83);
        pnlHeader.Controls.Add(lblTitle);
        pnlHeader.Controls.Add(lblClock);
        pnlHeader.Controls.Add(btnLogout);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(0, 0);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Size = new Size(1100, 100);
        pnlHeader.TabIndex = 2;
        // 
        // lblTitle
        // 
        lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
        lblTitle.ForeColor = Color.White;
        lblTitle.Location = new Point(24, 18);
        lblTitle.Name = "lblTitle";
        lblTitle.Size = new Size(700, 42);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "UTT  |  CỔNG SINH VIÊN";
        // 
        // lblClock
        // 
        lblClock.ForeColor = Color.FromArgb(211, 227, 244);
        lblClock.Location = new Point(28, 65);
        lblClock.Name = "lblClock";
        lblClock.Size = new Size(500, 28);
        lblClock.TabIndex = 1;
        lblClock.Text = "Ngày / giờ hiện tại";
        // 
        // btnLogout
        // 
        btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnLogout.BackColor = Color.White;
        btnLogout.FlatStyle = FlatStyle.Flat;
        btnLogout.ForeColor = Color.FromArgb(20, 49, 83);
        btnLogout.Location = new Point(932, 28);
        btnLogout.Name = "btnLogout";
        btnLogout.Size = new Size(140, 42);
        btnLogout.TabIndex = 2;
        btnLogout.Text = "Đăng xuất";
        btnLogout.UseVisualStyleBackColor = false;
        btnLogout.Click += btnLogout_Click;
        // 
        // pnlProfile
        // 
        pnlProfile.BackColor = Color.White;
        pnlProfile.Controls.Add(lblHoTen);
        pnlProfile.Controls.Add(lblThongTin);
        pnlProfile.Controls.Add(lblEmail);
        pnlProfile.Dock = DockStyle.Top;
        pnlProfile.Location = new Point(0, 100);
        pnlProfile.Name = "pnlProfile";
        pnlProfile.Size = new Size(1100, 132);
        pnlProfile.TabIndex = 1;
        // 
        // lblHoTen
        // 
        lblHoTen.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblHoTen.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        lblHoTen.Location = new Point(26, 16);
        lblHoTen.Name = "lblHoTen";
        lblHoTen.Size = new Size(1040, 38);
        lblHoTen.TabIndex = 0;
        lblHoTen.Text = "Thông tin sinh viên";
        // 
        // lblThongTin
        // 
        lblThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        lblThongTin.Location = new Point(28, 62);
        lblThongTin.Name = "lblThongTin";
        lblThongTin.Size = new Size(1040, 26);
        lblThongTin.TabIndex = 1;
        lblThongTin.Text = "Mã sinh viên  •  Lớp hành chính  •  Khoa";
        // 
        // lblEmail
        // 
        lblEmail.ForeColor = Color.FromArgb(91, 108, 128);
        lblEmail.Location = new Point(28, 94);
        lblEmail.Name = "lblEmail";
        lblEmail.Size = new Size(1040, 25);
        lblEmail.TabIndex = 2;
        // 
        // tabs
        // 
        tabs.Controls.Add(tabDiemDanh);
        tabs.Controls.Add(tabLichSu);
        tabs.Dock = DockStyle.Fill;
        tabs.Location = new Point(0, 232);
        tabs.Name = "tabs";
        tabs.SelectedIndex = 0;
        tabs.SelectedIndexChanged += tabs_SelectedIndexChanged;
        tabs.Size = new Size(1100, 488);
        tabs.TabIndex = 0;
        // 
        // tabDiemDanh
        // 
        tabDiemDanh.BackColor = Color.FromArgb(243, 246, 250);
        tabDiemDanh.Controls.Add(dgvBuoiHoc);
        tabDiemDanh.Controls.Add(lblStatus);
        tabDiemDanh.Controls.Add(lblNextStep);
        tabDiemDanh.Controls.Add(pnlActions);
        tabDiemDanh.Location = new Point(4, 32);
        tabDiemDanh.Name = "tabDiemDanh";
        tabDiemDanh.Padding = new Padding(18);
        tabDiemDanh.Size = new Size(1092, 452);
        tabDiemDanh.TabIndex = 0;
        tabDiemDanh.Text = "Điểm danh buổi học";
        // 
        // dgvBuoiHoc
        // 
        dgvBuoiHoc.AllowUserToAddRows = false;
        dgvBuoiHoc.AllowUserToDeleteRows = false;
        dgvBuoiHoc.AutoGenerateColumns = true;
        dgvBuoiHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvBuoiHoc.BackgroundColor = Color.White;
        dgvBuoiHoc.BorderStyle = BorderStyle.None;
        dgvBuoiHoc.ColumnHeadersHeight = 42;
        dgvBuoiHoc.Dock = DockStyle.Fill;
        dgvBuoiHoc.Location = new Point(18, 80);
        dgvBuoiHoc.MultiSelect = false;
        dgvBuoiHoc.Name = "dgvBuoiHoc";
        dgvBuoiHoc.ReadOnly = true;
        dgvBuoiHoc.RowHeadersVisible = false;
        dgvBuoiHoc.RowHeadersWidth = 51;
        dgvBuoiHoc.RowTemplate.Height = 38;
        dgvBuoiHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvBuoiHoc.Size = new Size(1056, 254);
        dgvBuoiHoc.TabIndex = 0;
        dgvBuoiHoc.CurrentCellChanged += dgvBuoiHoc_CurrentCellChanged;
        // 
        // lblStatus
        // 
        lblStatus.Dock = DockStyle.Bottom;
        lblStatus.Location = new Point(18, 334);
        lblStatus.Name = "lblStatus";
        lblStatus.Padding = new Padding(0, 12, 0, 0);
        lblStatus.Size = new Size(1056, 54);
        lblStatus.TabIndex = 1;
        lblStatus.Text = "Danh sách sẽ được tải sau khi đăng nhập.";
        // 
        // lblNextStep
        // 
        lblNextStep.Dock = DockStyle.Bottom;
        lblNextStep.ForeColor = Color.FromArgb(91, 108, 128);
        lblNextStep.Location = new Point(18, 388);
        lblNextStep.Name = "lblNextStep";
        lblNextStep.Size = new Size(1056, 46);
        lblNextStep.TabIndex = 2;
        lblNextStep.Text = "Chọn buổi học để xác thực và nhập OTP.";
        // 
        // pnlActions
        // 
        pnlActions.Controls.Add(lblSection);
        pnlActions.Controls.Add(btnRefresh);
        pnlActions.Controls.Add(btnDiemDanh);
        pnlActions.Dock = DockStyle.Top;
        pnlActions.Location = new Point(18, 18);
        pnlActions.Name = "pnlActions";
        pnlActions.Size = new Size(1056, 62);
        pnlActions.TabIndex = 3;
        // 
        // lblSection
        // 
        lblSection.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
        lblSection.Location = new Point(0, 13);
        lblSection.Name = "lblSection";
        lblSection.Size = new Size(600, 34);
        lblSection.TabIndex = 0;
        lblSection.Text = "Buổi học đang mở";
        lblSection.Click += lblSection_Click;
        // 
        // btnRefresh
        // 
        btnRefresh.BackColor = Color.FromArgb(24, 94, 177);
        btnRefresh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnRefresh.FlatStyle = FlatStyle.Flat;
        btnRefresh.ForeColor = Color.White;
        btnRefresh.Location = new Point(926, 6);
        btnRefresh.Name = "btnRefresh";
        btnRefresh.Size = new Size(130, 44);
        btnRefresh.TabIndex = 1;
        btnRefresh.Text = "Làm mới";
        btnRefresh.UseVisualStyleBackColor = false;
        btnRefresh.Click += btnRefresh_Click;
        // Mở xác thực và nhập OTP cho buổi đang chọn.
        btnDiemDanh.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnDiemDanh.BackColor = Color.FromArgb(24, 117, 75);
        btnDiemDanh.ForeColor = Color.White;
        btnDiemDanh.FlatStyle = FlatStyle.Flat;
        btnDiemDanh.Location = new Point(746, 6);
        btnDiemDanh.Size = new Size(170, 44);
        btnDiemDanh.Name = "btnDiemDanh";
        btnDiemDanh.Text = "Vào điểm danh";
        btnDiemDanh.Enabled = false;
        btnDiemDanh.TabIndex = 2;
        btnDiemDanh.Click += btnDiemDanh_Click;
        // 
        // tabLichSu
        // 
        tabLichSu.BackColor = Color.FromArgb(243, 246, 250);
        tabLichSu.Padding = new Padding(18);
        tabLichSu.Controls.Add(dgvLichSu);
        tabLichSu.Controls.Add(lblHistory);
        tabLichSu.Controls.Add(pnlLocLichSu);
        tabLichSu.Location = new Point(4, 32);
        tabLichSu.Name = "tabLichSu";
        tabLichSu.Size = new Size(1092, 452);
        tabLichSu.TabIndex = 1;
        tabLichSu.Text = "Lịch sử điểm danh";
        // Bộ lọc lịch sử.
        pnlLocLichSu.Dock = DockStyle.Top;
        pnlLocLichSu.Size = new Size(1056, 88);
        pnlLocLichSu.Name = "pnlLocLichSu";
        pnlLocLichSu.Controls.Add(lblLopLichSu);
        pnlLocLichSu.Controls.Add(cboLopLichSu);
        pnlLocLichSu.Controls.Add(lblTrangThaiLichSu);
        pnlLocLichSu.Controls.Add(cboTrangThaiLichSu);
        pnlLocLichSu.Controls.Add(btnLamMoiLichSu);
        lblLopLichSu.Text = "Lớp học phần";
        lblLopLichSu.Location = new Point(0, 0);
        lblLopLichSu.Size = new Size(320, 28);
        lblLopLichSu.Name = "lblLopLichSu";
        cboLopLichSu.Name = "cboLopLichSu";
        cboLopLichSu.DropDownStyle = ComboBoxStyle.DropDownList;
        cboLopLichSu.Location = new Point(0, 32);
        cboLopLichSu.Size = new Size(390, 32);
        cboLopLichSu.Enabled = false;
        cboLopLichSu.SelectedIndexChanged += cboLocLichSu_SelectedIndexChanged;
        lblTrangThaiLichSu.Text = "Kết quả";
        lblTrangThaiLichSu.Location = new Point(412, 0);
        lblTrangThaiLichSu.Size = new Size(210, 28);
        lblTrangThaiLichSu.Name = "lblTrangThaiLichSu";
        cboTrangThaiLichSu.Name = "cboTrangThaiLichSu";
        cboTrangThaiLichSu.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTrangThaiLichSu.Location = new Point(412, 32);
        cboTrangThaiLichSu.Size = new Size(220, 32);
        cboTrangThaiLichSu.Items.AddRange(new object[] { "Tất cả kết quả", "Có mặt", "Đi muộn", "Vắng", "Nghỉ có phép" });
        cboTrangThaiLichSu.SelectedIndex = 0;
        cboTrangThaiLichSu.Enabled = false;
        cboTrangThaiLichSu.SelectedIndexChanged += cboLocLichSu_SelectedIndexChanged;
        btnLamMoiLichSu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnLamMoiLichSu.Name = "btnLamMoiLichSu";
        btnLamMoiLichSu.Text = "Làm mới";
        btnLamMoiLichSu.Location = new Point(926, 24);
        btnLamMoiLichSu.Size = new Size(130, 44);
        btnLamMoiLichSu.BackColor = Color.FromArgb(24, 94, 177);
        btnLamMoiLichSu.ForeColor = Color.White;
        btnLamMoiLichSu.FlatStyle = FlatStyle.Flat;
        btnLamMoiLichSu.Click += btnLamMoiLichSu_Click;
        // Danh sách kết quả đã được ghi nhận.
        dgvLichSu.Name = "dgvLichSu";
        dgvLichSu.Dock = DockStyle.Fill;
        dgvLichSu.ReadOnly = true;
        dgvLichSu.AllowUserToAddRows = false;
        dgvLichSu.AllowUserToDeleteRows = false;
        dgvLichSu.AutoGenerateColumns = true;
        dgvLichSu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvLichSu.BackgroundColor = Color.White;
        dgvLichSu.BorderStyle = BorderStyle.None;
        dgvLichSu.RowHeadersVisible = false;
        dgvLichSu.ColumnHeadersHeight = 42;
        dgvLichSu.RowTemplate.Height = 38;
        dgvLichSu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvLichSu.MultiSelect = false;
        lblHistory.Name = "lblHistory";
        lblHistory.Dock = DockStyle.Bottom;
        lblHistory.Height = 70;
        lblHistory.Padding = new Padding(0, 12, 0, 0);
        lblHistory.ForeColor = Color.FromArgb(91, 108, 128);
        lblHistory.Text = "Chọn tab Lịch sử điểm danh để tải kết quả.";
        // 
        // clockTimer
        // 
        clockTimer.Interval = 1000;
        clockTimer.Tick += clockTimer_Tick;
        // 
        // FrmStudentMain
        // 
        AutoScaleDimensions = new SizeF(9F, 23F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(1100, 720);
        Controls.Add(tabs);
        Controls.Add(pnlProfile);
        Controls.Add(pnlHeader);
        Font = new Font("Segoe UI", 10F);
        ForeColor = Color.FromArgb(20, 49, 83);
        MinimumSize = new Size(1000, 650);
        Name = "FrmStudentMain";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Sinh viên | Hệ thống điểm danh UTT";
        Load += FrmStudentMain_Load;
        pnlHeader.ResumeLayout(false);
        pnlProfile.ResumeLayout(false);
        tabs.ResumeLayout(false);
        tabDiemDanh.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvBuoiHoc).EndInit();
        pnlActions.ResumeLayout(false);
        tabLichSu.ResumeLayout(false);
        pnlLocLichSu.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvLichSu).EndInit();
        ResumeLayout(false);
    }

    private Panel pnlHeader, pnlProfile, pnlActions;
    private Label lblTitle, lblClock, lblHoTen, lblThongTin, lblEmail, lblSection, lblStatus, lblNextStep, lblHistory;
    private Button btnLogout, btnRefresh, btnDiemDanh;
    private TabControl tabs;
    private TabPage tabDiemDanh, tabLichSu;
    private DataGridView dgvBuoiHoc, dgvLichSu;
    private Panel pnlLocLichSu;
    private Label lblLopLichSu, lblTrangThaiLichSu;
    private ComboBox cboLopLichSu, cboTrangThaiLichSu;
    private Button btnLamMoiLichSu;
    private System.Windows.Forms.Timer clockTimer;
}

