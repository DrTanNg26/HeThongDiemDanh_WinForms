namespace HeThongDiemDanh
{
    partial class FrmAdmin
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
            headerPanel = new Panel();
            btnLogout = new Button();
            lblAdminName = new Label();
            lblTitle = new Label();
            tabControlAdmin = new TabControl();
            tabTaiKhoan = new TabPage();
            txtTkSearch = new TextBox();
            cmbRoleFilter = new ComboBox();
            cmbStatusFilter = new ComboBox();
            btnTkSearch = new Button();
            dgvTaiKhoan = new DataGridView();
            btnTaiKhoanAdd = new Button();
            btnTaiKhoanEdit = new Button();
            btnTaiKhoanLockUnlock = new Button();
            btnTaiKhoanResetPwd = new Button();
            tabDanhMuc = new TabPage();
            dgvKhoa = new DataGridView();
            pnlKhoaRight = new Panel();
            lblMaLop = new Label();
            txtMaLop = new TextBox();
            cmbDanhMucSelector = new ComboBox();
            lblTenLop = new Label();
            txtTenLop = new TextBox();
            lblKhoaForLop = new Label();
            cboKhoaForLop = new ComboBox();
            btnLopThem = new Button();
            btnLopLuu = new Button();
            btnLopXoa = new Button();
            lblMaKhoa = new Label();
            txtMaKhoa = new TextBox();
            lblTenKhoa = new Label();
            txtTenKhoa = new TextBox();
            btnKhoaThem = new Button();
            btnKhoaLuu = new Button();
            btnKhoaXoa = new Button();
            lblMaMon = new Label();
            txtMaMon = new TextBox();
            lblTenMon = new Label();
            txtTenMon = new TextBox();
            lblSoTinChi = new Label();
            txtSoTinChi = new TextBox();
            btnMonThem = new Button();
            btnMonLuu = new Button();
            btnMonXoa = new Button();
            dgvLopHanhChinh = new DataGridView();
            dgvMonHoc = new DataGridView();
            tabLopHP = new TabPage();
            dgvLopHP = new DataGridView();
            pnlLopHPRight = new Panel();
            lblMaLopHP = new Label();
            txtMaLopHP = new TextBox();
            lblTenLopHP = new Label();
            txtTenLopHP = new TextBox();
            lblLopHPMon = new Label();
            cboLopHPMon = new ComboBox();
            lblLopHPGV = new Label();
            cboLopHPGV = new ComboBox();
            lblPhongHoc = new Label();
            txtPhongHoc = new TextBox();
            lblNamHoc = new Label();
            txtNamHoc = new TextBox();
            btnLopHPThem = new Button();
            btnLopHPLuu = new Button();
            btnLopHPXoa = new Button();
            tabPhanLop = new TabPage();
            lblChonLopHP = new Label();
            cboPhanLopHP = new ComboBox();
            lblSVTrongLop = new Label();
            dgvSVTrongLop = new DataGridView();
            btnGoiSV = new Button();
            lblSVNgoaiLop = new Label();
            txtTimSVNgoaiLop = new TextBox();
            btnTimSVNgoaiLop = new Button();
            dgvSVNgoaiLop = new DataGridView();
            btnThemSVVaoLop = new Button();
            headerPanel.SuspendLayout();
            tabControlAdmin.SuspendLayout();
            tabTaiKhoan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTaiKhoan).BeginInit();
            tabDanhMuc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvKhoa).BeginInit();
            pnlKhoaRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLopHanhChinh).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMonHoc).BeginInit();
            tabLopHP.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLopHP).BeginInit();
            pnlLopHPRight.SuspendLayout();
            tabPhanLop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSVTrongLop).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSVNgoaiLop).BeginInit();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.Controls.Add(btnLogout);
            headerPanel.Controls.Add(lblAdminName);
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(1005, 72);
            headerPanel.TabIndex = 0;
            headerPanel.Paint += headerPanel_Paint;
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.Location = new Point(863, 18);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(120, 36);
            btnLogout.TabIndex = 2;
            btnLogout.Text = "Đăng xuất";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // lblAdminName
            // 
            lblAdminName.AutoSize = true;
            lblAdminName.Location = new Point(16, 22);
            lblAdminName.Name = "lblAdminName";
            lblAdminName.Size = new Size(155, 32);
            lblAdminName.TabIndex = 1;
            lblAdminName.Text = "Admin Name";
            lblAdminName.Click += lblAdminName_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Times New Roman", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(226, 18);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(564, 49);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Hệ thống Quản lý Điểm danh";
            // 
            // tabControlAdmin
            // 
            tabControlAdmin.Controls.Add(tabTaiKhoan);
            tabControlAdmin.Controls.Add(tabDanhMuc);
            tabControlAdmin.Controls.Add(tabLopHP);
            tabControlAdmin.Controls.Add(tabPhanLop);
            tabControlAdmin.Dock = DockStyle.Fill;
            tabControlAdmin.Location = new Point(0, 72);
            tabControlAdmin.Name = "tabControlAdmin";
            tabControlAdmin.SelectedIndex = 0;
            tabControlAdmin.Size = new Size(1005, 528);
            tabControlAdmin.TabIndex = 1;
            // 
            // tabTaiKhoan
            // 
            tabTaiKhoan.Controls.Add(txtTkSearch);
            tabTaiKhoan.Controls.Add(cmbRoleFilter);
            tabTaiKhoan.Controls.Add(cmbStatusFilter);
            tabTaiKhoan.Controls.Add(btnTkSearch);
            tabTaiKhoan.Controls.Add(dgvTaiKhoan);
            tabTaiKhoan.Controls.Add(btnTaiKhoanAdd);
            tabTaiKhoan.Controls.Add(btnTaiKhoanEdit);
            tabTaiKhoan.Controls.Add(btnTaiKhoanLockUnlock);
            tabTaiKhoan.Controls.Add(btnTaiKhoanResetPwd);
            tabTaiKhoan.Location = new Point(8, 46);
            tabTaiKhoan.Name = "tabTaiKhoan";
            tabTaiKhoan.Padding = new Padding(3);
            tabTaiKhoan.Size = new Size(989, 474);
            tabTaiKhoan.TabIndex = 0;
            tabTaiKhoan.Text = "Tài khoản";
            tabTaiKhoan.UseVisualStyleBackColor = true;
            // 
            // txtTkSearch
            // 
            txtTkSearch.Location = new Point(8, 8);
            txtTkSearch.Name = "txtTkSearch";
            txtTkSearch.Size = new Size(320, 39);
            txtTkSearch.TabIndex = 0;
            // 
            // cmbRoleFilter
            // 
            cmbRoleFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRoleFilter.Items.AddRange(new object[] { "Tất cả", "Admin", "GiangVien", "SinhVien" });
            cmbRoleFilter.Location = new Point(336, 8);
            cmbRoleFilter.Name = "cmbRoleFilter";
            cmbRoleFilter.Size = new Size(160, 40);
            cmbRoleFilter.TabIndex = 1;
            cmbRoleFilter.SelectedIndexChanged += cmbRoleFilter_SelectedIndexChanged;
            // 
            // cmbStatusFilter
            // 
            cmbStatusFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatusFilter.Items.AddRange(new object[] { "Tất cả", "Hoạt động", "Bị khóa", "Chờ" });
            cmbStatusFilter.Location = new Point(504, 8);
            cmbStatusFilter.Name = "cmbStatusFilter";
            cmbStatusFilter.Size = new Size(160, 40);
            cmbStatusFilter.TabIndex = 2;
            // 
            // btnTkSearch
            // 
            btnTkSearch.Location = new Point(672, 8);
            btnTkSearch.Name = "btnTkSearch";
            btnTkSearch.Size = new Size(100, 40);
            btnTkSearch.TabIndex = 3;
            btnTkSearch.Text = "Tìm";
            btnTkSearch.UseVisualStyleBackColor = true;
            btnTkSearch.Click += btnTkSearch_Click;
            // 
            // dgvTaiKhoan
            // 
            dgvTaiKhoan.AllowUserToAddRows = false;
            dgvTaiKhoan.AllowUserToDeleteRows = false;
            dgvTaiKhoan.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvTaiKhoan.ColumnHeadersHeight = 46;
            dgvTaiKhoan.Location = new Point(8, 56);
            dgvTaiKhoan.Name = "dgvTaiKhoan";
            dgvTaiKhoan.ReadOnly = true;
            dgvTaiKhoan.RowHeadersWidth = 82;
            dgvTaiKhoan.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTaiKhoan.Size = new Size(775, 410);
            dgvTaiKhoan.TabIndex = 4;
            dgvTaiKhoan.SelectionChanged += dgvTaiKhoan_SelectionChanged;
            // 
            // btnTaiKhoanAdd
            // 
            btnTaiKhoanAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTaiKhoanAdd.Location = new Point(793, 56);
            btnTaiKhoanAdd.Name = "btnTaiKhoanAdd";
            btnTaiKhoanAdd.Size = new Size(120, 40);
            btnTaiKhoanAdd.TabIndex = 5;
            btnTaiKhoanAdd.Text = "Thêm mới";
            btnTaiKhoanAdd.UseVisualStyleBackColor = true;
            btnTaiKhoanAdd.Click += btnTaiKhoanAdd_Click;
            // 
            // btnTaiKhoanEdit
            // 
            btnTaiKhoanEdit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTaiKhoanEdit.Enabled = false;
            btnTaiKhoanEdit.Location = new Point(793, 106);
            btnTaiKhoanEdit.Name = "btnTaiKhoanEdit";
            btnTaiKhoanEdit.Size = new Size(120, 40);
            btnTaiKhoanEdit.TabIndex = 6;
            btnTaiKhoanEdit.Text = "Sửa";
            btnTaiKhoanEdit.UseVisualStyleBackColor = true;
            btnTaiKhoanEdit.Click += btnTaiKhoanEdit_Click;
            // 
            // btnTaiKhoanLockUnlock
            // 
            btnTaiKhoanLockUnlock.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTaiKhoanLockUnlock.Enabled = false;
            btnTaiKhoanLockUnlock.Location = new Point(793, 156);
            btnTaiKhoanLockUnlock.Name = "btnTaiKhoanLockUnlock";
            btnTaiKhoanLockUnlock.Size = new Size(120, 40);
            btnTaiKhoanLockUnlock.TabIndex = 7;
            btnTaiKhoanLockUnlock.Text = "Khóa/Mở khóa";
            btnTaiKhoanLockUnlock.UseVisualStyleBackColor = true;
            btnTaiKhoanLockUnlock.Click += btnTaiKhoanLockUnlock_Click;
            // 
            // btnTaiKhoanResetPwd
            // 
            btnTaiKhoanResetPwd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnTaiKhoanResetPwd.Enabled = false;
            btnTaiKhoanResetPwd.Location = new Point(793, 206);
            btnTaiKhoanResetPwd.Name = "btnTaiKhoanResetPwd";
            btnTaiKhoanResetPwd.Size = new Size(120, 40);
            btnTaiKhoanResetPwd.TabIndex = 8;
            btnTaiKhoanResetPwd.Text = "Đặt lại MK";
            btnTaiKhoanResetPwd.UseVisualStyleBackColor = true;
            btnTaiKhoanResetPwd.Click += btnTaiKhoanResetPwd_Click;
            // 
            // tabDanhMuc
            // 
            tabDanhMuc.Controls.Add(dgvKhoa);
            tabDanhMuc.Controls.Add(pnlKhoaRight);
            tabDanhMuc.Controls.Add(dgvLopHanhChinh);
            tabDanhMuc.Controls.Add(dgvMonHoc);
            tabDanhMuc.Location = new Point(8, 46);
            tabDanhMuc.Name = "tabDanhMuc";
            tabDanhMuc.Padding = new Padding(3);
            tabDanhMuc.Size = new Size(989, 474);
            tabDanhMuc.TabIndex = 1;
            tabDanhMuc.Text = "Danh mục";
            tabDanhMuc.UseVisualStyleBackColor = true;
            // 
            // dgvKhoa
            // 
            dgvKhoa.AllowUserToAddRows = false;
            dgvKhoa.AllowUserToDeleteRows = false;
            dgvKhoa.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvKhoa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvKhoa.ColumnHeadersHeight = 46;
            dgvKhoa.Location = new Point(16, 8);
            dgvKhoa.Name = "dgvKhoa";
            dgvKhoa.ReadOnly = true;
            dgvKhoa.RowHeadersWidth = 82;
            dgvKhoa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvKhoa.Size = new Size(498, 454);
            dgvKhoa.TabIndex = 0;
            dgvKhoa.CellClick += dgvKhoa_CellClick;
            // 
            // pnlKhoaRight
            // 
            pnlKhoaRight.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            pnlKhoaRight.Controls.Add(lblMaLop);
            pnlKhoaRight.Controls.Add(txtMaLop);
            pnlKhoaRight.Controls.Add(cmbDanhMucSelector);
            pnlKhoaRight.Controls.Add(lblTenLop);
            pnlKhoaRight.Controls.Add(txtTenLop);
            pnlKhoaRight.Controls.Add(lblKhoaForLop);
            pnlKhoaRight.Controls.Add(cboKhoaForLop);
            pnlKhoaRight.Controls.Add(btnLopThem);
            pnlKhoaRight.Controls.Add(btnLopLuu);
            pnlKhoaRight.Controls.Add(btnLopXoa);
            pnlKhoaRight.Controls.Add(lblMaKhoa);
            pnlKhoaRight.Controls.Add(txtMaKhoa);
            pnlKhoaRight.Controls.Add(lblTenKhoa);
            pnlKhoaRight.Controls.Add(txtTenKhoa);
            pnlKhoaRight.Controls.Add(btnKhoaThem);
            pnlKhoaRight.Controls.Add(btnKhoaLuu);
            pnlKhoaRight.Controls.Add(btnKhoaXoa);
            pnlKhoaRight.Controls.Add(lblMaMon);
            pnlKhoaRight.Controls.Add(txtMaMon);
            pnlKhoaRight.Controls.Add(lblTenMon);
            pnlKhoaRight.Controls.Add(txtTenMon);
            pnlKhoaRight.Controls.Add(lblSoTinChi);
            pnlKhoaRight.Controls.Add(txtSoTinChi);
            pnlKhoaRight.Controls.Add(btnMonThem);
            pnlKhoaRight.Controls.Add(btnMonLuu);
            pnlKhoaRight.Controls.Add(btnMonXoa);
            pnlKhoaRight.Location = new Point(565, 8);
            pnlKhoaRight.Name = "pnlKhoaRight";
            pnlKhoaRight.Size = new Size(412, 454);
            pnlKhoaRight.TabIndex = 1;
            // 
            // lblMaLop
            // 
            lblMaLop.AutoSize = true;
            lblMaLop.Location = new Point(164, 57);
            lblMaLop.Name = "lblMaLop";
            lblMaLop.Size = new Size(94, 32);
            lblMaLop.TabIndex = 0;
            lblMaLop.Text = "Mã lớp:";
            lblMaLop.Visible = false;
            // 
            // txtMaLop
            // 
            txtMaLop.Location = new Point(22, 92);
            txtMaLop.Name = "txtMaLop";
            txtMaLop.Size = new Size(372, 39);
            txtMaLop.TabIndex = 1;
            txtMaLop.Visible = false;
            // 
            // cmbDanhMucSelector
            // 
            cmbDanhMucSelector.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDanhMucSelector.Items.AddRange(new object[] { "Khoa", "Lớp hành chính", "Môn học" });
            cmbDanhMucSelector.Location = new Point(18, 14);
            cmbDanhMucSelector.Name = "cmbDanhMucSelector";
            cmbDanhMucSelector.Size = new Size(200, 40);
            cmbDanhMucSelector.TabIndex = 2;
            cmbDanhMucSelector.SelectedIndexChanged += cmbDanhMucSelector_SelectedIndexChanged;
            // 
            // lblTenLop
            // 
            lblTenLop.AutoSize = true;
            lblTenLop.Location = new Point(176, 144);
            lblTenLop.Name = "lblTenLop";
            lblTenLop.Size = new Size(98, 32);
            lblTenLop.TabIndex = 2;
            lblTenLop.Text = "Tên lớp:";
            lblTenLop.Visible = false;
            // 
            // txtTenLop
            // 
            txtTenLop.Location = new Point(22, 179);
            txtTenLop.Name = "txtTenLop";
            txtTenLop.Size = new Size(372, 39);
            txtTenLop.TabIndex = 3;
            txtTenLop.Visible = false;
            // 
            // lblKhoaForLop
            // 
            lblKhoaForLop.AutoSize = true;
            lblKhoaForLop.Location = new Point(189, 229);
            lblKhoaForLop.Name = "lblKhoaForLop";
            lblKhoaForLop.Size = new Size(73, 32);
            lblKhoaForLop.TabIndex = 4;
            lblKhoaForLop.Text = "Khoa:";
            lblKhoaForLop.Visible = false;
            // 
            // cboKhoaForLop
            // 
            cboKhoaForLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cboKhoaForLop.Location = new Point(18, 365);
            cboKhoaForLop.Name = "cboKhoaForLop";
            cboKhoaForLop.Size = new Size(372, 40);
            cboKhoaForLop.TabIndex = 5;
            cboKhoaForLop.Visible = false;
            cboKhoaForLop.SelectedIndexChanged += cboKhoaForLop_SelectedIndexChanged;
            // 
            // btnLopThem
            // 
            btnLopThem.Location = new Point(16, 411);
            btnLopThem.Name = "btnLopThem";
            btnLopThem.Size = new Size(110, 40);
            btnLopThem.TabIndex = 6;
            btnLopThem.Text = "Thêm";
            btnLopThem.UseVisualStyleBackColor = true;
            btnLopThem.Visible = false;
            btnLopThem.Click += btnLopThem_Click;
            // 
            // btnLopLuu
            // 
            btnLopLuu.Location = new Point(142, 411);
            btnLopLuu.Name = "btnLopLuu";
            btnLopLuu.Size = new Size(110, 40);
            btnLopLuu.TabIndex = 7;
            btnLopLuu.Text = "Lưu";
            btnLopLuu.UseVisualStyleBackColor = true;
            btnLopLuu.Visible = false;
            btnLopLuu.Click += btnLopLuu_Click;
            // 
            // btnLopXoa
            // 
            btnLopXoa.Location = new Point(274, 411);
            btnLopXoa.Name = "btnLopXoa";
            btnLopXoa.Size = new Size(116, 40);
            btnLopXoa.TabIndex = 8;
            btnLopXoa.Text = "Xóa";
            btnLopXoa.UseVisualStyleBackColor = true;
            btnLopXoa.Visible = false;
            btnLopXoa.Click += btnLopXoa_Click;
            // 
            // lblMaKhoa
            // 
            lblMaKhoa.AutoSize = true;
            lblMaKhoa.Location = new Point(18, 57);
            lblMaKhoa.Name = "lblMaKhoa";
            lblMaKhoa.Size = new Size(112, 32);
            lblMaKhoa.TabIndex = 0;
            lblMaKhoa.Text = "Mã khoa:";
            // 
            // txtMaKhoa
            // 
            txtMaKhoa.Location = new Point(22, 91);
            txtMaKhoa.Name = "txtMaKhoa";
            txtMaKhoa.Size = new Size(372, 39);
            txtMaKhoa.TabIndex = 1;
            // 
            // lblTenKhoa
            // 
            lblTenKhoa.AutoSize = true;
            lblTenKhoa.Location = new Point(18, 145);
            lblTenKhoa.Name = "lblTenKhoa";
            lblTenKhoa.Size = new Size(116, 32);
            lblTenKhoa.TabIndex = 2;
            lblTenKhoa.Text = "Tên khoa:";
            // 
            // txtTenKhoa
            // 
            txtTenKhoa.Location = new Point(22, 179);
            txtTenKhoa.Name = "txtTenKhoa";
            txtTenKhoa.Size = new Size(372, 39);
            txtTenKhoa.TabIndex = 3;
            // 
            // btnKhoaThem
            // 
            btnKhoaThem.Location = new Point(21, 274);
            btnKhoaThem.Name = "btnKhoaThem";
            btnKhoaThem.Size = new Size(110, 40);
            btnKhoaThem.TabIndex = 4;
            btnKhoaThem.Text = "Thêm";
            btnKhoaThem.UseVisualStyleBackColor = true;
            btnKhoaThem.Click += btnKhoaThem_Click;
            // 
            // btnKhoaLuu
            // 
            btnKhoaLuu.Location = new Point(142, 274);
            btnKhoaLuu.Name = "btnKhoaLuu";
            btnKhoaLuu.Size = new Size(110, 40);
            btnKhoaLuu.TabIndex = 5;
            btnKhoaLuu.Text = "Lưu";
            btnKhoaLuu.UseVisualStyleBackColor = true;
            btnKhoaLuu.Click += btnKhoaLuu_Click;
            // 
            // btnKhoaXoa
            // 
            btnKhoaXoa.Location = new Point(278, 274);
            btnKhoaXoa.Name = "btnKhoaXoa";
            btnKhoaXoa.Size = new Size(116, 40);
            btnKhoaXoa.TabIndex = 6;
            btnKhoaXoa.Text = "Xóa";
            btnKhoaXoa.UseVisualStyleBackColor = true;
            btnKhoaXoa.Click += btnKhoaXoa_Click;
            // 
            // lblMaMon
            // 
            lblMaMon.AutoSize = true;
            lblMaMon.Location = new Point(18, 57);
            lblMaMon.Name = "lblMaMon";
            lblMaMon.Size = new Size(109, 32);
            lblMaMon.TabIndex = 0;
            lblMaMon.Text = "Mã môn:";
            lblMaMon.Visible = false;
            // 
            // txtMaMon
            // 
            txtMaMon.Location = new Point(22, 91);
            txtMaMon.Name = "txtMaMon";
            txtMaMon.Size = new Size(372, 39);
            txtMaMon.TabIndex = 1;
            txtMaMon.Visible = false;
            // 
            // lblTenMon
            // 
            lblTenMon.AutoSize = true;
            lblTenMon.Location = new Point(18, 145);
            lblTenMon.Name = "lblTenMon";
            lblTenMon.Size = new Size(113, 32);
            lblTenMon.TabIndex = 2;
            lblTenMon.Text = "Tên môn:";
            lblTenMon.Visible = false;
            // 
            // txtTenMon
            // 
            txtTenMon.Location = new Point(22, 179);
            txtTenMon.Name = "txtTenMon";
            txtTenMon.Size = new Size(372, 39);
            txtTenMon.TabIndex = 3;
            txtTenMon.Visible = false;
            // 
            // lblSoTinChi
            // 
            lblSoTinChi.AutoSize = true;
            lblSoTinChi.Location = new Point(18, 229);
            lblSoTinChi.Name = "lblSoTinChi";
            lblSoTinChi.Size = new Size(119, 32);
            lblSoTinChi.TabIndex = 4;
            lblSoTinChi.Text = "Số tín chỉ:";
            lblSoTinChi.Visible = false;
            // 
            // txtSoTinChi
            // 
            txtSoTinChi.Location = new Point(22, 265);
            txtSoTinChi.Name = "txtSoTinChi";
            txtSoTinChi.Size = new Size(120, 39);
            txtSoTinChi.TabIndex = 5;
            txtSoTinChi.Visible = false;
            // 
            // btnMonThem
            // 
            btnMonThem.Location = new Point(21, 330);
            btnMonThem.Name = "btnMonThem";
            btnMonThem.Size = new Size(110, 40);
            btnMonThem.TabIndex = 6;
            btnMonThem.Text = "Thêm";
            btnMonThem.UseVisualStyleBackColor = true;
            btnMonThem.Visible = false;
            btnMonThem.Click += btnMonThem_Click;
            // 
            // btnMonLuu
            // 
            btnMonLuu.Location = new Point(142, 330);
            btnMonLuu.Name = "btnMonLuu";
            btnMonLuu.Size = new Size(110, 40);
            btnMonLuu.TabIndex = 7;
            btnMonLuu.Text = "Lưu";
            btnMonLuu.UseVisualStyleBackColor = true;
            btnMonLuu.Visible = false;
            btnMonLuu.Click += btnMonLuu_Click;
            // 
            // btnMonXoa
            // 
            btnMonXoa.Location = new Point(278, 330);
            btnMonXoa.Name = "btnMonXoa";
            btnMonXoa.Size = new Size(116, 40);
            btnMonXoa.TabIndex = 8;
            btnMonXoa.Text = "Xóa";
            btnMonXoa.UseVisualStyleBackColor = true;
            btnMonXoa.Visible = false;
            btnMonXoa.Click += btnMonXoa_Click;
            // 
            // dgvLopHanhChinh
            // 
            dgvLopHanhChinh.AllowUserToAddRows = false;
            dgvLopHanhChinh.AllowUserToDeleteRows = false;
            dgvLopHanhChinh.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvLopHanhChinh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLopHanhChinh.ColumnHeadersHeight = 46;
            dgvLopHanhChinh.Location = new Point(8, 8);
            dgvLopHanhChinh.Name = "dgvLopHanhChinh";
            dgvLopHanhChinh.ReadOnly = true;
            dgvLopHanhChinh.RowHeadersWidth = 82;
            dgvLopHanhChinh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLopHanhChinh.Size = new Size(506, 454);
            dgvLopHanhChinh.TabIndex = 3;
            dgvLopHanhChinh.Visible = false;
            dgvLopHanhChinh.CellClick += dgvLopHanhChinh_CellClick;
            // 
            // dgvMonHoc
            // 
            dgvMonHoc.AllowUserToAddRows = false;
            dgvMonHoc.AllowUserToDeleteRows = false;
            dgvMonHoc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvMonHoc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMonHoc.ColumnHeadersHeight = 46;
            dgvMonHoc.Location = new Point(8, 8);
            dgvMonHoc.Name = "dgvMonHoc";
            dgvMonHoc.ReadOnly = true;
            dgvMonHoc.RowHeadersWidth = 82;
            dgvMonHoc.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMonHoc.Size = new Size(510, 454);
            dgvMonHoc.TabIndex = 9;
            dgvMonHoc.Visible = false;
            dgvMonHoc.CellClick += dgvMonHoc_CellClick;
            // 
            // tabLopHP
            // 
            tabLopHP.Controls.Add(dgvLopHP);
            tabLopHP.Controls.Add(pnlLopHPRight);
            tabLopHP.Location = new Point(8, 46);
            tabLopHP.Name = "tabLopHP";
            tabLopHP.Padding = new Padding(3);
            tabLopHP.Size = new Size(989, 474);
            tabLopHP.TabIndex = 2;
            tabLopHP.Text = "Lớp học phần";
            tabLopHP.UseVisualStyleBackColor = true;
            // 
            // dgvLopHP
            // 
            dgvLopHP.AllowUserToAddRows = false;
            dgvLopHP.AllowUserToDeleteRows = false;
            dgvLopHP.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvLopHP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLopHP.ColumnHeadersHeight = 46;
            dgvLopHP.Location = new Point(8, 6);
            dgvLopHP.Name = "dgvLopHP";
            dgvLopHP.ReadOnly = true;
            dgvLopHP.RowHeadersWidth = 82;
            dgvLopHP.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvLopHP.Size = new Size(550, 458);
            dgvLopHP.TabIndex = 0;
            dgvLopHP.CellClick += dgvLopHP_CellClick;
            // 
            // pnlLopHPRight
            // 
            pnlLopHPRight.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            pnlLopHPRight.Controls.Add(lblMaLopHP);
            pnlLopHPRight.Controls.Add(txtMaLopHP);
            pnlLopHPRight.Controls.Add(lblTenLopHP);
            pnlLopHPRight.Controls.Add(txtTenLopHP);
            pnlLopHPRight.Controls.Add(lblLopHPMon);
            pnlLopHPRight.Controls.Add(cboLopHPMon);
            pnlLopHPRight.Controls.Add(lblLopHPGV);
            pnlLopHPRight.Controls.Add(cboLopHPGV);
            pnlLopHPRight.Controls.Add(lblPhongHoc);
            pnlLopHPRight.Controls.Add(txtPhongHoc);
            pnlLopHPRight.Controls.Add(lblNamHoc);
            pnlLopHPRight.Controls.Add(txtNamHoc);
            pnlLopHPRight.Controls.Add(btnLopHPThem);
            pnlLopHPRight.Controls.Add(btnLopHPLuu);
            pnlLopHPRight.Controls.Add(btnLopHPXoa);
            pnlLopHPRight.Location = new Point(565, 6);
            pnlLopHPRight.Name = "pnlLopHPRight";
            pnlLopHPRight.Size = new Size(415, 458);
            pnlLopHPRight.TabIndex = 1;
            // 
            // lblMaLopHP
            // 
            lblMaLopHP.AutoSize = true;
            lblMaLopHP.Location = new Point(12, 10);
            lblMaLopHP.Name = "lblMaLopHP";
            lblMaLopHP.Size = new Size(136, 32);
            lblMaLopHP.TabIndex = 0;
            lblMaLopHP.Text = "Mã Lớp HP:";
            // 
            // txtMaLopHP
            // 
            txtMaLopHP.Location = new Point(12, 35);
            txtMaLopHP.Name = "txtMaLopHP";
            txtMaLopHP.Size = new Size(390, 39);
            txtMaLopHP.TabIndex = 1;
            // 
            // lblTenLopHP
            // 
            lblTenLopHP.AutoSize = true;
            lblTenLopHP.Location = new Point(12, 70);
            lblTenLopHP.Name = "lblTenLopHP";
            lblTenLopHP.Size = new Size(140, 32);
            lblTenLopHP.TabIndex = 2;
            lblTenLopHP.Text = "Tên Lớp HP:";
            // 
            // txtTenLopHP
            // 
            txtTenLopHP.Location = new Point(12, 95);
            txtTenLopHP.Name = "txtTenLopHP";
            txtTenLopHP.Size = new Size(390, 39);
            txtTenLopHP.TabIndex = 3;
            // 
            // lblLopHPMon
            // 
            lblLopHPMon.AutoSize = true;
            lblLopHPMon.Location = new Point(12, 130);
            lblLopHPMon.Name = "lblLopHPMon";
            lblLopHPMon.Size = new Size(115, 32);
            lblLopHPMon.TabIndex = 4;
            lblLopHPMon.Text = "Môn học:";
            // 
            // cboLopHPMon
            // 
            cboLopHPMon.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLopHPMon.Location = new Point(12, 155);
            cboLopHPMon.Name = "cboLopHPMon";
            cboLopHPMon.Size = new Size(390, 40);
            cboLopHPMon.TabIndex = 5;
            // 
            // lblLopHPGV
            // 
            lblLopHPGV.AutoSize = true;
            lblLopHPGV.Location = new Point(12, 195);
            lblLopHPGV.Name = "lblLopHPGV";
            lblLopHPGV.Size = new Size(133, 32);
            lblLopHPGV.TabIndex = 6;
            lblLopHPGV.Text = "Giảng viên:";
            // 
            // cboLopHPGV
            // 
            cboLopHPGV.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLopHPGV.Location = new Point(12, 220);
            cboLopHPGV.Name = "cboLopHPGV";
            cboLopHPGV.Size = new Size(390, 40);
            cboLopHPGV.TabIndex = 7;
            // 
            // lblPhongHoc
            // 
            lblPhongHoc.AutoSize = true;
            lblPhongHoc.Location = new Point(12, 260);
            lblPhongHoc.Name = "lblPhongHoc";
            lblPhongHoc.Size = new Size(134, 32);
            lblPhongHoc.TabIndex = 8;
            lblPhongHoc.Text = "Phòng học:";
            // 
            // txtPhongHoc
            // 
            txtPhongHoc.Location = new Point(12, 285);
            txtPhongHoc.Name = "txtPhongHoc";
            txtPhongHoc.Size = new Size(180, 39);
            txtPhongHoc.TabIndex = 9;
            // 
            // lblNamHoc
            // 
            lblNamHoc.AutoSize = true;
            lblNamHoc.Location = new Point(210, 260);
            lblNamHoc.Name = "lblNamHoc";
            lblNamHoc.Size = new Size(213, 32);
            lblNamHoc.TabIndex = 10;
            lblNamHoc.Text = "Năm học - Học kỳ:";
            // 
            // txtNamHoc
            // 
            txtNamHoc.Location = new Point(210, 285);
            txtNamHoc.Name = "txtNamHoc";
            txtNamHoc.Size = new Size(192, 39);
            txtNamHoc.TabIndex = 11;
            // 
            // btnLopHPThem
            // 
            btnLopHPThem.Location = new Point(12, 380);
            btnLopHPThem.Name = "btnLopHPThem";
            btnLopHPThem.Size = new Size(110, 40);
            btnLopHPThem.TabIndex = 12;
            btnLopHPThem.Text = "Thêm";
            btnLopHPThem.UseVisualStyleBackColor = true;
            btnLopHPThem.Click += btnLopHPThem_Click;
            // 
            // btnLopHPLuu
            // 
            btnLopHPLuu.Location = new Point(142, 380);
            btnLopHPLuu.Name = "btnLopHPLuu";
            btnLopHPLuu.Size = new Size(110, 40);
            btnLopHPLuu.TabIndex = 13;
            btnLopHPLuu.Text = "Lưu";
            btnLopHPLuu.UseVisualStyleBackColor = true;
            btnLopHPLuu.Click += btnLopHPLuu_Click;
            // 
            // btnLopHPXoa
            // 
            btnLopHPXoa.Location = new Point(272, 380);
            btnLopHPXoa.Name = "btnLopHPXoa";
            btnLopHPXoa.Size = new Size(120, 40);
            btnLopHPXoa.TabIndex = 14;
            btnLopHPXoa.Text = "Xóa";
            btnLopHPXoa.UseVisualStyleBackColor = true;
            btnLopHPXoa.Click += btnLopHPXoa_Click;
            // 
            // tabPhanLop
            // 
            tabPhanLop.Controls.Add(lblChonLopHP);
            tabPhanLop.Controls.Add(cboPhanLopHP);
            tabPhanLop.Controls.Add(lblSVTrongLop);
            tabPhanLop.Controls.Add(dgvSVTrongLop);
            tabPhanLop.Controls.Add(btnGoiSV);
            tabPhanLop.Controls.Add(lblSVNgoaiLop);
            tabPhanLop.Controls.Add(txtTimSVNgoaiLop);
            tabPhanLop.Controls.Add(btnTimSVNgoaiLop);
            tabPhanLop.Controls.Add(dgvSVNgoaiLop);
            tabPhanLop.Controls.Add(btnThemSVVaoLop);
            tabPhanLop.Location = new Point(8, 46);
            tabPhanLop.Name = "tabPhanLop";
            tabPhanLop.Padding = new Padding(3);
            tabPhanLop.Size = new Size(989, 474);
            tabPhanLop.TabIndex = 3;
            tabPhanLop.Text = "Phân lớp";
            tabPhanLop.UseVisualStyleBackColor = true;
            // 
            // lblChonLopHP
            // 
            lblChonLopHP.AutoSize = true;
            lblChonLopHP.Location = new Point(12, 12);
            lblChonLopHP.Name = "lblChonLopHP";
            lblChonLopHP.Size = new Size(229, 32);
            lblChonLopHP.TabIndex = 0;
            lblChonLopHP.Text = "Chọn Lớp học phần:";
            // 
            // cboPhanLopHP
            // 
            cboPhanLopHP.DropDownStyle = ComboBoxStyle.DropDownList;
            cboPhanLopHP.Location = new Point(247, 4);
            cboPhanLopHP.Name = "cboPhanLopHP";
            cboPhanLopHP.Size = new Size(400, 40);
            cboPhanLopHP.TabIndex = 1;
            cboPhanLopHP.SelectedIndexChanged += cboPhanLopHP_SelectedIndexChanged;
            // 
            // lblSVTrongLop
            // 
            lblSVTrongLop.AutoSize = true;
            lblSVTrongLop.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSVTrongLop.Location = new Point(12, 45);
            lblSVTrongLop.Name = "lblSVTrongLop";
            lblSVTrongLop.Size = new Size(321, 32);
            lblSVTrongLop.TabIndex = 2;
            lblSVTrongLop.Text = "Sinh viên đã thuộc Lớp HP:";
            // 
            // dgvSVTrongLop
            // 
            dgvSVTrongLop.AllowUserToAddRows = false;
            dgvSVTrongLop.AllowUserToDeleteRows = false;
            dgvSVTrongLop.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            dgvSVTrongLop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSVTrongLop.ColumnHeadersHeight = 40;
            dgvSVTrongLop.Location = new Point(12, 100);
            dgvSVTrongLop.Name = "dgvSVTrongLop";
            dgvSVTrongLop.ReadOnly = true;
            dgvSVTrongLop.RowHeadersWidth = 51;
            dgvSVTrongLop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSVTrongLop.Size = new Size(440, 320);
            dgvSVTrongLop.TabIndex = 3;
            // 
            // btnGoiSV
            // 
            btnGoiSV.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnGoiSV.Location = new Point(12, 426);
            btnGoiSV.Name = "btnGoiSV";
            btnGoiSV.Size = new Size(160, 40);
            btnGoiSV.TabIndex = 4;
            btnGoiSV.Text = "Loại khỏi lớp <<";
            btnGoiSV.UseVisualStyleBackColor = true;
            btnGoiSV.Click += btnGoiSV_Click;
            // 
            // lblSVNgoaiLop
            // 
            lblSVNgoaiLop.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            lblSVNgoaiLop.AutoSize = true;
            lblSVNgoaiLop.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblSVNgoaiLop.Location = new Point(500, 50);
            lblSVNgoaiLop.Name = "lblSVNgoaiLop";
            lblSVNgoaiLop.Size = new Size(286, 32);
            lblSVNgoaiLop.TabIndex = 5;
            lblSVNgoaiLop.Text = "Danh sách SV chưa gán:";
            // 
            // txtTimSVNgoaiLop
            // 
            txtTimSVNgoaiLop.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            txtTimSVNgoaiLop.Location = new Point(500, 78);
            txtTimSVNgoaiLop.Name = "txtTimSVNgoaiLop";
            txtTimSVNgoaiLop.Size = new Size(350, 39);
            txtTimSVNgoaiLop.TabIndex = 6;
            // 
            // btnTimSVNgoaiLop
            // 
            btnTimSVNgoaiLop.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnTimSVNgoaiLop.Location = new Point(858, 76);
            btnTimSVNgoaiLop.Name = "btnTimSVNgoaiLop";
            btnTimSVNgoaiLop.Size = new Size(110, 35);
            btnTimSVNgoaiLop.TabIndex = 7;
            btnTimSVNgoaiLop.Text = "Tìm";
            btnTimSVNgoaiLop.UseVisualStyleBackColor = true;
            btnTimSVNgoaiLop.Click += btnTimSVNgoaiLop_Click;
            // 
            // dgvSVNgoaiLop
            // 
            dgvSVNgoaiLop.AllowUserToAddRows = false;
            dgvSVNgoaiLop.AllowUserToDeleteRows = false;
            dgvSVNgoaiLop.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            dgvSVNgoaiLop.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvSVNgoaiLop.ColumnHeadersHeight = 40;
            dgvSVNgoaiLop.Location = new Point(500, 118);
            dgvSVNgoaiLop.Name = "dgvSVNgoaiLop";
            dgvSVNgoaiLop.ReadOnly = true;
            dgvSVNgoaiLop.RowHeadersWidth = 51;
            dgvSVNgoaiLop.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSVNgoaiLop.Size = new Size(468, 302);
            dgvSVNgoaiLop.TabIndex = 8;
            // 
            // btnThemSVVaoLop
            // 
            btnThemSVVaoLop.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnThemSVVaoLop.Location = new Point(500, 426);
            btnThemSVVaoLop.Name = "btnThemSVVaoLop";
            btnThemSVVaoLop.Size = new Size(180, 40);
            btnThemSVVaoLop.TabIndex = 9;
            btnThemSVVaoLop.Text = ">> Thêm vào lớp";
            btnThemSVVaoLop.UseVisualStyleBackColor = true;
            btnThemSVVaoLop.Click += btnThemSVVaoLop_Click;
            // 
            // FrmAdmin
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1005, 600);
            Controls.Add(tabControlAdmin);
            Controls.Add(headerPanel);
            Name = "FrmAdmin";
            Text = "FrmAdmin";
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            tabControlAdmin.ResumeLayout(false);
            tabTaiKhoan.ResumeLayout(false);
            tabTaiKhoan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTaiKhoan).EndInit();
            tabDanhMuc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvKhoa).EndInit();
            pnlKhoaRight.ResumeLayout(false);
            pnlKhoaRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvLopHanhChinh).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMonHoc).EndInit();
            tabLopHP.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvLopHP).EndInit();
            pnlLopHPRight.ResumeLayout(false);
            pnlLopHPRight.PerformLayout();
            tabPhanLop.ResumeLayout(false);
            tabPhanLop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSVTrongLop).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSVNgoaiLop).EndInit();
            ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblAdminName;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.TabControl tabControlAdmin;
        private System.Windows.Forms.TabPage tabTaiKhoan;
        private System.Windows.Forms.TabPage tabDanhMuc;
        private System.Windows.Forms.TabPage tabLopHP;
        private System.Windows.Forms.TabPage tabPhanLop;
        // Khoa controls
        private System.Windows.Forms.DataGridView dgvKhoa;
        private System.Windows.Forms.Panel pnlKhoaRight;
        private System.Windows.Forms.Label lblMaKhoa;
        private System.Windows.Forms.TextBox txtMaKhoa;
        private System.Windows.Forms.Label lblTenKhoa;
        private System.Windows.Forms.TextBox txtTenKhoa;
        private System.Windows.Forms.Button btnKhoaThem;
        private System.Windows.Forms.Button btnKhoaLuu;
        private System.Windows.Forms.Button btnKhoaXoa;
        private System.Windows.Forms.ComboBox cmbDanhMucSelector;
        private System.Windows.Forms.DataGridView dgvLopHanhChinh;
        private System.Windows.Forms.Label lblMaLop;
        private System.Windows.Forms.TextBox txtMaLop;
        private System.Windows.Forms.Label lblTenLop;
        private System.Windows.Forms.TextBox txtTenLop;
        private System.Windows.Forms.Label lblKhoaForLop;
        private System.Windows.Forms.ComboBox cboKhoaForLop;
        private System.Windows.Forms.Button btnLopThem;
        private System.Windows.Forms.Button btnLopLuu;
        private System.Windows.Forms.Button btnLopXoa;
        private System.Windows.Forms.DataGridView dgvMonHoc;
        private System.Windows.Forms.Label lblMaMon;
        private System.Windows.Forms.TextBox txtMaMon;
        private System.Windows.Forms.Label lblTenMon;
        private System.Windows.Forms.TextBox txtTenMon;
        private System.Windows.Forms.Label lblSoTinChi;
        private System.Windows.Forms.TextBox txtSoTinChi;
        private System.Windows.Forms.Button btnMonThem;
        private System.Windows.Forms.Button btnMonLuu;
        private System.Windows.Forms.Button btnMonXoa;
        private System.Windows.Forms.TextBox txtTkSearch;
        private System.Windows.Forms.ComboBox cmbRoleFilter;
        private System.Windows.Forms.ComboBox cmbStatusFilter;
        private System.Windows.Forms.Button btnTkSearch;
        private System.Windows.Forms.DataGridView dgvTaiKhoan;
        private System.Windows.Forms.Button btnTaiKhoanAdd;
        private System.Windows.Forms.Button btnTaiKhoanEdit;
        private System.Windows.Forms.Button btnTaiKhoanLockUnlock;
        private System.Windows.Forms.Button btnTaiKhoanResetPwd;

        // Phân lớp controls
        private System.Windows.Forms.Label lblChonLopHP;
        private System.Windows.Forms.ComboBox cboPhanLopHP;
        private System.Windows.Forms.Label lblSVTrongLop;
        private System.Windows.Forms.DataGridView dgvSVTrongLop;
        private System.Windows.Forms.Button btnGoiSV;
        private System.Windows.Forms.Label lblSVNgoaiLop;
        private System.Windows.Forms.TextBox txtTimSVNgoaiLop;
        private System.Windows.Forms.Button btnTimSVNgoaiLop;
        private System.Windows.Forms.DataGridView dgvSVNgoaiLop;
        private System.Windows.Forms.Button btnThemSVVaoLop;

        // Lớp học phần controls
        private System.Windows.Forms.DataGridView dgvLopHP;
        private System.Windows.Forms.Panel pnlLopHPRight;
        private System.Windows.Forms.Label lblMaLopHP;
        private System.Windows.Forms.TextBox txtMaLopHP;
        private System.Windows.Forms.Label lblTenLopHP;
        private System.Windows.Forms.TextBox txtTenLopHP;
        private System.Windows.Forms.Label lblLopHPMon;
        private System.Windows.Forms.ComboBox cboLopHPMon;
        private System.Windows.Forms.Label lblLopHPGV;
        private System.Windows.Forms.ComboBox cboLopHPGV;
        private System.Windows.Forms.Label lblPhongHoc;
        private System.Windows.Forms.TextBox txtPhongHoc;
        private System.Windows.Forms.Label lblNamHoc;
        private System.Windows.Forms.TextBox txtNamHoc;
        private System.Windows.Forms.Button btnLopHPThem;
        private System.Windows.Forms.Button btnLopHPLuu;
        private System.Windows.Forms.Button btnLopHPXoa;
    }
}
