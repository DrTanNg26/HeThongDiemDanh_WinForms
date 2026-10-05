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

            // lblTitle
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(160, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(270, 32);
            lblTitle.Text = "ĐĂNG KÝ TÀI KHOẢN";

            // lblVaiTro
            lblVaiTro.AutoSize = true;
            lblVaiTro.Location = new Point(30, 65);
            lblVaiTro.Name = "lblVaiTro";
            lblVaiTro.Size = new Size(116, 20);
            lblVaiTro.Text = "Đăng ký vai trò:";

            // cmbVaiTro
            cmbVaiTro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVaiTro.Items.AddRange(new object[] { "SinhVien", "GiangVien" });
            cmbVaiTro.Location = new Point(160, 62);
            cmbVaiTro.Name = "cmbVaiTro";
            cmbVaiTro.Size = new Size(350, 28);
            cmbVaiTro.SelectedIndexChanged += cmbVaiTro_SelectedIndexChanged;

            // pnlSinhVien
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
            pnlSinhVien.Location = new Point(20, 100);
            pnlSinhVien.Name = "pnlSinhVien";
            pnlSinhVien.Size = new Size(500, 280);

            // SinhVien controls setup
            lblSVMa.Location = new Point(10, 10);
            lblSVMa.Text = "Mã sinh viên:";
            txtSVMa.Location = new Point(140, 7);
            txtSVMa.Size = new Size(340, 27);

            lblSVHoTen.Location = new Point(10, 48);
            lblSVHoTen.Text = "Họ và tên:";
            txtSVHoTen.Location = new Point(140, 45);
            txtSVHoTen.Size = new Size(340, 27);

            lblSVEmail.Location = new Point(10, 86);
            lblSVEmail.Text = "Email:";
            txtSVEmail.Location = new Point(140, 83);
            txtSVEmail.Size = new Size(340, 27);

            lblSVNgaySinh.Location = new Point(10, 124);
            lblSVNgaySinh.Text = "Ngày sinh:";
            dtpSVNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpSVNgaySinh.CustomFormat = "dd/MM/yyyy";
            dtpSVNgaySinh.Location = new Point(140, 121);
            dtpSVNgaySinh.Size = new Size(340, 27);

            lblSVCCCD.Location = new Point(10, 162);
            lblSVCCCD.Text = "Số CCCD:";
            txtSVCCCD.Location = new Point(140, 159);
            txtSVCCCD.Size = new Size(340, 27);

            lblSVMaLop.Location = new Point(10, 200);
            lblSVMaLop.Text = "Lớp hành chính:";
            cboSVMaLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSVMaLop.Location = new Point(140, 197);
            cboSVMaLop.Size = new Size(340, 28);

            lblSVMaKhoa.Location = new Point(10, 238);
            lblSVMaKhoa.Text = "Khoa:";
            cboSVMaKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
            cboSVMaKhoa.Location = new Point(140, 235);
            cboSVMaKhoa.Size = new Size(340, 28);

            // pnlGiangVien
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
            pnlGiangVien.Location = new Point(20, 100);
            pnlGiangVien.Name = "pnlGiangVien";
            pnlGiangVien.Size = new Size(500, 280);

            // GiangVien controls setup
            lblGVMa.Location = new Point(10, 10);
            lblGVMa.Text = "Mã giảng viên:";
            txtGVMa.Location = new Point(140, 7);
            txtGVMa.Size = new Size(340, 27);

            lblGVHoTen.Location = new Point(10, 48);
            lblGVHoTen.Text = "Họ và tên:";
            txtGVHoTen.Location = new Point(140, 45);
            txtGVHoTen.Size = new Size(340, 27);

            lblGVEmail.Location = new Point(10, 86);
            lblGVEmail.Text = "Email:";
            txtGVEmail.Location = new Point(140, 83);
            txtGVEmail.Size = new Size(340, 27);

            lblGVHocHam.Location = new Point(10, 124);
            lblGVHocHam.Text = "Học hàm/Học vị:";
            txtGVHocHam.Location = new Point(140, 121);
            txtGVHocHam.Size = new Size(340, 27);

            lblGVMaKhoa.Location = new Point(10, 162);
            lblGVMaKhoa.Text = "Khoa:";
            cboGVMaKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
            cboGVMaKhoa.Location = new Point(140, 159);
            cboGVMaKhoa.Size = new Size(340, 28);

            // Password
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(30, 395);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(73, 20);
            lblPassword.Text = "Mật khẩu:";

            txtPassword.Location = new Point(160, 392);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(350, 27);
            txtPassword.UseSystemPasswordChar = true;

            // Buttons
            btnRegister.Location = new Point(270, 440);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(110, 38);
            btnRegister.Text = "Đăng ký";
            btnRegister.UseVisualStyleBackColor = true;
            btnRegister.Click += btnRegister_Click;

            btnCancel.Location = new Point(400, 440);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(110, 38);
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;

            // FrmDangKy
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(540, 500);
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
