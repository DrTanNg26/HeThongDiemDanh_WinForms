namespace HeThongDiemDanh
{
    partial class FrmDangKy
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            lblTitle = new Label();
            lblVaiTro = new Label();
            cmbVaiTro = new ComboBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            btnRegister = new Button();
            btnCancel = new Button();
            pnlSinhVien = new Panel();
            lblSVMa = new Label();
            txtSVMa = new TextBox();
            lblSVHoTen = new Label();
            txtSVHoTen = new TextBox();
            lblSVEmail = new Label();
            txtSVEmail = new TextBox();
            lblSVNgaySinh = new Label();
            dtpSVNgaySinh = new DateTimePicker();
            lblSVCCCD = new Label();
            txtSVCCCD = new TextBox();
            lblSVMaLop = new Label();
            cboSVMaLop = new ComboBox();
            lblSVMaKhoa = new Label();
            cboSVMaKhoa = new ComboBox();
            pnlGiangVien = new Panel();
            lblGVMa = new Label();
            txtGVMa = new TextBox();
            lblGVHoTen = new Label();
            txtGVHoTen = new TextBox();
            lblGVEmail = new Label();
            txtGVEmail = new TextBox();
            lblGVHocHam = new Label();
            txtGVHocHam = new TextBox();
            lblGVMaKhoa = new Label();
            cboGVMaKhoa = new ComboBox();
            pnlSinhVien.SuspendLayout();
            pnlGiangVien.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(260, 24);
            lblTitle.Margin = new Padding(5, 0, 5, 0);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(411, 51);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "ĐĂNG KÝ TÀI KHOẢN";
            lblTitle.Click += lblTitle_Click;
            // 
            // lblVaiTro
            // 
            lblVaiTro.AutoSize = true;
            lblVaiTro.Location = new Point(49, 104);
            lblVaiTro.Margin = new Padding(5, 0, 5, 0);
            lblVaiTro.Name = "lblVaiTro";
            lblVaiTro.Size = new Size(181, 32);
            lblVaiTro.TabIndex = 1;
            lblVaiTro.Text = "Đăng ký vai trò:";
            // 
            // cmbVaiTro
            // 
            cmbVaiTro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVaiTro.Items.AddRange(new object[] { "SinhVien", "GiangVien" });
            cmbVaiTro.Location = new Point(260, 99);
            cmbVaiTro.Margin = new Padding(5, 5, 5, 5);
            cmbVaiTro.Name = "cmbVaiTro";
            cmbVaiTro.Size = new Size(566, 40);
            cmbVaiTro.TabIndex = 2;
            cmbVaiTro.SelectedIndexChanged += cmbVaiTro_SelectedIndexChanged;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(49, 632);
            lblPassword.Margin = new Padding(5, 0, 5, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(120, 32);
            lblPassword.TabIndex = 5;
            lblPassword.Text = "Mật khẩu:";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(260, 627);
            txtPassword.Margin = new Padding(5, 5, 5, 5);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(566, 39);
            txtPassword.TabIndex = 6;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(439, 704);
            btnRegister.Margin = new Padding(5, 5, 5, 5);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(179, 61);
            btnRegister.TabIndex = 7;
            btnRegister.Text = "Đăng ký";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(650, 704);
            btnCancel.Margin = new Padding(5, 5, 5, 5);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(179, 61);
            btnCancel.TabIndex = 8;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // pnlSinhVien
            // 
            pnlSinhVien.Controls.Add(lblSVMa);
            pnlSinhVien.Controls.Add(txtSVMa);
            pnlSinhVien.Controls.Add(lblSVHoTen);
            pnlSinhVien.Controls.Add(txtSVHoTen);
            pnlSinhVien.Controls.Add(lblSVEmail);
            pnlSinhVien.Controls.Add(txtSVEmail);
            pnlSinhVien.Controls.Add(lblSVNgaySinh);
            pnlSinhVien.Controls.Add(dtpSVNgaySinh);
            pnlSinhVien.Controls.Add(lblSVCCCD);
            pnlSinhVien.Controls.Add(txtSVCCCD);
            pnlSinhVien.Controls.Add(lblSVMaLop);
            pnlSinhVien.Controls.Add(cboSVMaLop);
            pnlSinhVien.Controls.Add(lblSVMaKhoa);
            pnlSinhVien.Controls.Add(cboSVMaKhoa);
            pnlSinhVien.Location = new Point(32, 160);
            pnlSinhVien.Margin = new Padding(5, 5, 5, 5);
            pnlSinhVien.Name = "pnlSinhVien";
            pnlSinhVien.Size = new Size(812, 448);
            pnlSinhVien.TabIndex = 3;
            // 
            // lblSVMa
            // 
            lblSVMa.Location = new Point(16, 16);
            lblSVMa.Margin = new Padding(5, 0, 5, 0);
            lblSVMa.Name = "lblSVMa";
            lblSVMa.Size = new Size(162, 37);
            lblSVMa.TabIndex = 0;
            lblSVMa.Text = "Mã sinh viên:";
            // 
            // txtSVMa
            // 
            txtSVMa.Location = new Point(228, 11);
            txtSVMa.Margin = new Padding(5, 5, 5, 5);
            txtSVMa.Name = "txtSVMa";
            txtSVMa.Size = new Size(550, 39);
            txtSVMa.TabIndex = 1;
            // 
            // lblSVHoTen
            // 
            lblSVHoTen.Location = new Point(16, 77);
            lblSVHoTen.Margin = new Padding(5, 0, 5, 0);
            lblSVHoTen.Name = "lblSVHoTen";
            lblSVHoTen.Size = new Size(162, 37);
            lblSVHoTen.TabIndex = 2;
            lblSVHoTen.Text = "Họ và tên:";
            // 
            // txtSVHoTen
            // 
            txtSVHoTen.Location = new Point(228, 72);
            txtSVHoTen.Margin = new Padding(5, 5, 5, 5);
            txtSVHoTen.Name = "txtSVHoTen";
            txtSVHoTen.Size = new Size(550, 39);
            txtSVHoTen.TabIndex = 3;
            // 
            // lblSVEmail
            // 
            lblSVEmail.Location = new Point(16, 138);
            lblSVEmail.Margin = new Padding(5, 0, 5, 0);
            lblSVEmail.Name = "lblSVEmail";
            lblSVEmail.Size = new Size(162, 37);
            lblSVEmail.TabIndex = 4;
            lblSVEmail.Text = "Email:";
            // 
            // txtSVEmail
            // 
            txtSVEmail.Location = new Point(228, 133);
            txtSVEmail.Margin = new Padding(5, 5, 5, 5);
            txtSVEmail.Name = "txtSVEmail";
            txtSVEmail.Size = new Size(550, 39);
            txtSVEmail.TabIndex = 5;
            // 
            // lblSVNgaySinh
            // 
            lblSVNgaySinh.Location = new Point(16, 198);
            lblSVNgaySinh.Margin = new Padding(5, 0, 5, 0);
            lblSVNgaySinh.Name = "lblSVNgaySinh";
            lblSVNgaySinh.Size = new Size(162, 37);
            lblSVNgaySinh.TabIndex = 6;
            lblSVNgaySinh.Text = "Ngày sinh:";
            // 
            // dtpSVNgaySinh
            // 
            dtpSVNgaySinh.CustomFormat = "dd/MM/yyyy";
            dtpSVNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpSVNgaySinh.Location = new Point(228, 194);
            dtpSVNgaySinh.Margin = new Padding(5, 5, 5, 5);
            dtpSVNgaySinh.Name = "dtpSVNgaySinh";
            dtpSVNgaySinh.Size = new Size(550, 39);
            dtpSVNgaySinh.TabIndex = 7;
            // 
            // lblSVCCCD
            // 
            lblSVCCCD.Location = new Point(16, 259);
            lblSVCCCD.Margin = new Padding(5, 0, 5, 0);
            lblSVCCCD.Name = "lblSVCCCD";
            lblSVCCCD.Size = new Size(162, 37);
            lblSVCCCD.TabIndex = 8;
            lblSVCCCD.Text = "Số CCCD:";
            // 
            // txtSVCCCD
            // 
            txtSVCCCD.Location = new Point(228, 254);
            txtSVCCCD.Margin = new Padding(5, 5, 5, 5);
            txtSVCCCD.Name = "txtSVCCCD";
            txtSVCCCD.Size = new Size(550, 39);
            txtSVCCCD.TabIndex = 9;
            // 
            // lblSVMaLop
            // 
            lblSVMaLop.Location = new Point(16, 320);
            lblSVMaLop.Margin = new Padding(5, 0, 5, 0);
            lblSVMaLop.Name = "lblSVMaLop";
            lblSVMaLop.Size = new Size(162, 37);
            lblSVMaLop.TabIndex = 10;
            lblSVMaLop.Text = "Lớp hành chính:";
            // 
            // cboSVMaLop
            // 
            cboSVMaLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSVMaLop.Location = new Point(228, 315);
            cboSVMaLop.Margin = new Padding(5, 5, 5, 5);
            cboSVMaLop.Name = "cboSVMaLop";
            cboSVMaLop.Size = new Size(550, 40);
            cboSVMaLop.TabIndex = 11;
            // 
            // lblSVMaKhoa
            // 
            lblSVMaKhoa.Location = new Point(16, 381);
            lblSVMaKhoa.Margin = new Padding(5, 0, 5, 0);
            lblSVMaKhoa.Name = "lblSVMaKhoa";
            lblSVMaKhoa.Size = new Size(162, 37);
            lblSVMaKhoa.TabIndex = 12;
            lblSVMaKhoa.Text = "Khoa:";
            // 
            // cboSVMaKhoa
            // 
            cboSVMaKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSVMaKhoa.Location = new Point(228, 376);
            cboSVMaKhoa.Margin = new Padding(5, 5, 5, 5);
            cboSVMaKhoa.Name = "cboSVMaKhoa";
            cboSVMaKhoa.Size = new Size(550, 40);
            cboSVMaKhoa.TabIndex = 13;
            // 
            // pnlGiangVien
            // 
            pnlGiangVien.Controls.Add(lblGVMa);
            pnlGiangVien.Controls.Add(txtGVMa);
            pnlGiangVien.Controls.Add(lblGVHoTen);
            pnlGiangVien.Controls.Add(txtGVHoTen);
            pnlGiangVien.Controls.Add(lblGVEmail);
            pnlGiangVien.Controls.Add(txtGVEmail);
            pnlGiangVien.Controls.Add(lblGVHocHam);
            pnlGiangVien.Controls.Add(txtGVHocHam);
            pnlGiangVien.Controls.Add(lblGVMaKhoa);
            pnlGiangVien.Controls.Add(cboGVMaKhoa);
            pnlGiangVien.Location = new Point(32, 160);
            pnlGiangVien.Margin = new Padding(5, 5, 5, 5);
            pnlGiangVien.Name = "pnlGiangVien";
            pnlGiangVien.Size = new Size(812, 448);
            pnlGiangVien.TabIndex = 4;
            // 
            // lblGVMa
            // 
            lblGVMa.Location = new Point(16, 16);
            lblGVMa.Margin = new Padding(5, 0, 5, 0);
            lblGVMa.Name = "lblGVMa";
            lblGVMa.Size = new Size(162, 37);
            lblGVMa.TabIndex = 0;
            lblGVMa.Text = "Mã giảng viên:";
            // 
            // txtGVMa
            // 
            txtGVMa.Location = new Point(228, 11);
            txtGVMa.Margin = new Padding(5, 5, 5, 5);
            txtGVMa.Name = "txtGVMa";
            txtGVMa.Size = new Size(550, 39);
            txtGVMa.TabIndex = 1;
            // 
            // lblGVHoTen
            // 
            lblGVHoTen.Location = new Point(16, 77);
            lblGVHoTen.Margin = new Padding(5, 0, 5, 0);
            lblGVHoTen.Name = "lblGVHoTen";
            lblGVHoTen.Size = new Size(162, 37);
            lblGVHoTen.TabIndex = 2;
            lblGVHoTen.Text = "Họ và tên:";
            // 
            // txtGVHoTen
            // 
            txtGVHoTen.Location = new Point(228, 72);
            txtGVHoTen.Margin = new Padding(5, 5, 5, 5);
            txtGVHoTen.Name = "txtGVHoTen";
            txtGVHoTen.Size = new Size(550, 39);
            txtGVHoTen.TabIndex = 3;
            // 
            // lblGVEmail
            // 
            lblGVEmail.Location = new Point(16, 138);
            lblGVEmail.Margin = new Padding(5, 0, 5, 0);
            lblGVEmail.Name = "lblGVEmail";
            lblGVEmail.Size = new Size(162, 37);
            lblGVEmail.TabIndex = 4;
            lblGVEmail.Text = "Email:";
            // 
            // txtGVEmail
            // 
            txtGVEmail.Location = new Point(228, 133);
            txtGVEmail.Margin = new Padding(5, 5, 5, 5);
            txtGVEmail.Name = "txtGVEmail";
            txtGVEmail.Size = new Size(550, 39);
            txtGVEmail.TabIndex = 5;
            // 
            // lblGVHocHam
            // 
            lblGVHocHam.Location = new Point(16, 198);
            lblGVHocHam.Margin = new Padding(5, 0, 5, 0);
            lblGVHocHam.Name = "lblGVHocHam";
            lblGVHocHam.Size = new Size(162, 37);
            lblGVHocHam.TabIndex = 6;
            lblGVHocHam.Text = "Học hàm/Học vị:";
            // 
            // txtGVHocHam
            // 
            txtGVHocHam.Location = new Point(228, 194);
            txtGVHocHam.Margin = new Padding(5, 5, 5, 5);
            txtGVHocHam.Name = "txtGVHocHam";
            txtGVHocHam.Size = new Size(550, 39);
            txtGVHocHam.TabIndex = 7;
            // 
            // lblGVMaKhoa
            // 
            lblGVMaKhoa.Location = new Point(16, 259);
            lblGVMaKhoa.Margin = new Padding(5, 0, 5, 0);
            lblGVMaKhoa.Name = "lblGVMaKhoa";
            lblGVMaKhoa.Size = new Size(162, 37);
            lblGVMaKhoa.TabIndex = 8;
            lblGVMaKhoa.Text = "Khoa:";
            // 
            // cboGVMaKhoa
            // 
            cboGVMaKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGVMaKhoa.Location = new Point(228, 254);
            cboGVMaKhoa.Margin = new Padding(5, 5, 5, 5);
            cboGVMaKhoa.Name = "cboGVMaKhoa";
            cboGVMaKhoa.Size = new Size(550, 40);
            cboGVMaKhoa.TabIndex = 9;
            // 
            // FrmDangKy
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(878, 800);
            Controls.Add(lblTitle);
            Controls.Add(lblVaiTro);
            Controls.Add(cmbVaiTro);
            Controls.Add(pnlSinhVien);
            Controls.Add(pnlGiangVien);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(btnRegister);
            Controls.Add(btnCancel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(5, 5, 5, 5);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmDangKy";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Đăng ký tài khoản";
            pnlSinhVien.ResumeLayout(false);
            pnlSinhVien.PerformLayout();
            pnlGiangVien.ResumeLayout(false);
            pnlGiangVien.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblVaiTro;
        private ComboBox cmbVaiTro;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnRegister;
        private Button btnCancel;
        private Panel pnlSinhVien;
        private Label lblSVMa;
        private TextBox txtSVMa;
        private Label lblSVHoTen;
        private TextBox txtSVHoTen;
        private Label lblSVEmail;
        private TextBox txtSVEmail;
        private Label lblSVNgaySinh;
        private DateTimePicker dtpSVNgaySinh;
        private Label lblSVCCCD;
        private TextBox txtSVCCCD;
        private Label lblSVMaLop;
        private ComboBox cboSVMaLop;
        private Label lblSVMaKhoa;
        private ComboBox cboSVMaKhoa;
        private Panel pnlGiangVien;
        private Label lblGVMa;
        private TextBox txtGVMa;
        private Label lblGVHoTen;
        private TextBox txtGVHoTen;
        private Label lblGVEmail;
        private TextBox txtGVEmail;
        private Label lblGVHocHam;
        private TextBox txtGVHocHam;
        private Label lblGVMaKhoa;
        private ComboBox cboGVMaKhoa;
    }
}
