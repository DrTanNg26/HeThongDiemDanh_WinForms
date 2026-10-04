namespace HeThongDiemDanh
{
    partial class FrmThemSuaTaiKhoan
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
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
            cmbVaiTro = new ComboBox();
            pnlSinhVien = new Panel();
            txtSVMa = new TextBox();
            txtSVHoTen = new TextBox();
            txtSVEmail = new TextBox();
            dtpSVNgaySinh = new DateTimePicker();
            txtSVCCCD = new TextBox();
            cboSVMaLop = new ComboBox();
            cboSVMaKhoa = new ComboBox();
            pnlGiangVien = new Panel();
            txtGVMa = new TextBox();
            txtGVHoTen = new TextBox();
            txtGVEmail = new TextBox();
            txtGVHocHam = new TextBox();
            cboGVMaKhoa = new ComboBox();
            pnlAdmin = new Panel();
            txtADMa = new TextBox();
            txtADHoTen = new TextBox();
            txtADEmail = new TextBox();
            btnSave = new Button();
            btnCancel = new Button();
            pnlSinhVien.SuspendLayout();
            pnlGiangVien.SuspendLayout();
            pnlAdmin.SuspendLayout();
            SuspendLayout();
            // 
            // cmbVaiTro
            // 
            cmbVaiTro.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbVaiTro.Items.AddRange(new object[] { "SinhVien", "GiangVien", "Admin" });
            cmbVaiTro.Location = new Point(12, 12);
            cmbVaiTro.Name = "cmbVaiTro";
            cmbVaiTro.Size = new Size(240, 40);
            cmbVaiTro.TabIndex = 0;
            cmbVaiTro.SelectedIndexChanged += cmbVaiTro_SelectedIndexChanged;
            // 
            // pnlSinhVien
            // 
            pnlSinhVien.Controls.Add(txtSVMa);
            pnlSinhVien.Controls.Add(txtSVHoTen);
            pnlSinhVien.Controls.Add(txtSVEmail);
            pnlSinhVien.Controls.Add(dtpSVNgaySinh);
            pnlSinhVien.Controls.Add(txtSVCCCD);
            pnlSinhVien.Controls.Add(cboSVMaLop);
            pnlSinhVien.Controls.Add(cboSVMaKhoa);
            pnlSinhVien.Location = new Point(12, 60);
            pnlSinhVien.Name = "pnlSinhVien";
            pnlSinhVien.Size = new Size(760, 300);
            pnlSinhVien.TabIndex = 1;
            pnlSinhVien.Visible = false;
            // 
            // txtSVMa
            // 
            txtSVMa.Location = new Point(8, 8);
            txtSVMa.Name = "txtSVMa";
            txtSVMa.PlaceholderText = "Mã SV";
            txtSVMa.Size = new Size(360, 39);
            txtSVMa.TabIndex = 0;
            // 
            // txtSVHoTen
            // 
            txtSVHoTen.Location = new Point(8, 56);
            txtSVHoTen.Name = "txtSVHoTen";
            txtSVHoTen.PlaceholderText = "Họ tên";
            txtSVHoTen.Size = new Size(360, 39);
            txtSVHoTen.TabIndex = 1;
            // 
            // txtSVEmail
            // 
            txtSVEmail.Location = new Point(8, 104);
            txtSVEmail.Name = "txtSVEmail";
            txtSVEmail.PlaceholderText = "Email";
            txtSVEmail.Size = new Size(360, 39);
            txtSVEmail.TabIndex = 2;
            // 
            // dtpSVNgaySinh
            // 
            dtpSVNgaySinh.Location = new Point(8, 152);
            dtpSVNgaySinh.Name = "dtpSVNgaySinh";
            dtpSVNgaySinh.Size = new Size(200, 39);
            dtpSVNgaySinh.TabIndex = 3;
            // 
            // txtSVCCCD
            // 
            txtSVCCCD.Location = new Point(8, 200);
            txtSVCCCD.Name = "txtSVCCCD";
            txtSVCCCD.PlaceholderText = "Số CCCD (12 số)";
            txtSVCCCD.Size = new Size(360, 39);
            txtSVCCCD.TabIndex = 4;
            // 
            // cboSVMaLop
            // 
            cboSVMaLop.Location = new Point(380, 8);
            cboSVMaLop.Name = "cboSVMaLop";
            cboSVMaLop.Size = new Size(360, 40);
            cboSVMaLop.TabIndex = 5;
            // 
            // cboSVMaKhoa
            // 
            cboSVMaKhoa.Location = new Point(380, 56);
            cboSVMaKhoa.Name = "cboSVMaKhoa";
            cboSVMaKhoa.Size = new Size(360, 40);
            cboSVMaKhoa.TabIndex = 6;
            // 
            // pnlGiangVien
            // 
            pnlGiangVien.Controls.Add(txtGVMa);
            pnlGiangVien.Controls.Add(txtGVHoTen);
            pnlGiangVien.Controls.Add(txtGVEmail);
            pnlGiangVien.Controls.Add(txtGVHocHam);
            pnlGiangVien.Controls.Add(cboGVMaKhoa);
            pnlGiangVien.Location = new Point(12, 60);
            pnlGiangVien.Name = "pnlGiangVien";
            pnlGiangVien.Size = new Size(760, 300);
            pnlGiangVien.TabIndex = 2;
            pnlGiangVien.Visible = false;
            // 
            // txtGVMa
            // 
            txtGVMa.Location = new Point(8, 8);
            txtGVMa.Name = "txtGVMa";
            txtGVMa.PlaceholderText = "Mã GV";
            txtGVMa.Size = new Size(360, 39);
            txtGVMa.TabIndex = 0;
            // 
            // txtGVHoTen
            // 
            txtGVHoTen.Location = new Point(8, 56);
            txtGVHoTen.Name = "txtGVHoTen";
            txtGVHoTen.PlaceholderText = "Họ tên";
            txtGVHoTen.Size = new Size(360, 39);
            txtGVHoTen.TabIndex = 1;
            // 
            // txtGVEmail
            // 
            txtGVEmail.Location = new Point(8, 104);
            txtGVEmail.Name = "txtGVEmail";
            txtGVEmail.PlaceholderText = "Email";
            txtGVEmail.Size = new Size(360, 39);
            txtGVEmail.TabIndex = 2;
            // 
            // txtGVHocHam
            // 
            txtGVHocHam.Location = new Point(8, 152);
            txtGVHocHam.Name = "txtGVHocHam";
            txtGVHocHam.PlaceholderText = "Học hàm học vị";
            txtGVHocHam.Size = new Size(360, 39);
            txtGVHocHam.TabIndex = 3;
            // 
            // cboGVMaKhoa
            // 
            cboGVMaKhoa.Location = new Point(380, 8);
            cboGVMaKhoa.Name = "cboGVMaKhoa";
            cboGVMaKhoa.Size = new Size(360, 40);
            cboGVMaKhoa.TabIndex = 4;
            // 
            // pnlAdmin
            // 
            pnlAdmin.Controls.Add(txtADMa);
            pnlAdmin.Controls.Add(txtADHoTen);
            pnlAdmin.Controls.Add(txtADEmail);
            pnlAdmin.Location = new Point(12, 60);
            pnlAdmin.Name = "pnlAdmin";
            pnlAdmin.Size = new Size(760, 300);
            pnlAdmin.TabIndex = 3;
            pnlAdmin.Visible = false;
            // 
            // txtADMa
            // 
            txtADMa.Location = new Point(8, 8);
            txtADMa.Name = "txtADMa";
            txtADMa.PlaceholderText = "Mã Admin";
            txtADMa.Size = new Size(360, 39);
            txtADMa.TabIndex = 0;
            // 
            // txtADHoTen
            // 
            txtADHoTen.Location = new Point(8, 56);
            txtADHoTen.Name = "txtADHoTen";
            txtADHoTen.PlaceholderText = "Họ tên";
            txtADHoTen.Size = new Size(360, 39);
            txtADHoTen.TabIndex = 1;
            // 
            // txtADEmail
            // 
            txtADEmail.Location = new Point(8, 104);
            txtADEmail.Name = "txtADEmail";
            txtADEmail.PlaceholderText = "Email";
            txtADEmail.Size = new Size(360, 39);
            txtADEmail.TabIndex = 2;
            // 
            // btnSave
            // 
            btnSave.Location = new Point(616, 380);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 40);
            btnSave.TabIndex = 10;
            btnSave.Text = "Lưu";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(740, 380);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(120, 40);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Hủy";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // FrmThemSuaTaiKhoan
            // 
            ClientSize = new Size(932, 431);
            Controls.Add(cmbVaiTro);
            Controls.Add(pnlSinhVien);
            Controls.Add(pnlGiangVien);
            Controls.Add(pnlAdmin);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Name = "FrmThemSuaTaiKhoan";
            Text = "Thêm / Sửa Tài khoản";
            pnlSinhVien.ResumeLayout(false);
            pnlSinhVien.PerformLayout();
            pnlGiangVien.ResumeLayout(false);
            pnlGiangVien.PerformLayout();
            pnlAdmin.ResumeLayout(false);
            pnlAdmin.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.ComboBox cmbVaiTro;
        private System.Windows.Forms.Panel pnlSinhVien;
        private System.Windows.Forms.TextBox txtSVMa;
        private System.Windows.Forms.TextBox txtSVHoTen;
        private System.Windows.Forms.TextBox txtSVEmail;
        private System.Windows.Forms.DateTimePicker dtpSVNgaySinh;
        private System.Windows.Forms.TextBox txtSVCCCD;
        private System.Windows.Forms.ComboBox cboSVMaLop;
        private System.Windows.Forms.ComboBox cboSVMaKhoa;
        private System.Windows.Forms.Panel pnlGiangVien;
        private System.Windows.Forms.TextBox txtGVMa;
        private System.Windows.Forms.TextBox txtGVHoTen;
        private System.Windows.Forms.TextBox txtGVEmail;
        private System.Windows.Forms.TextBox txtGVHocHam;
        private System.Windows.Forms.ComboBox cboGVMaKhoa;
        private System.Windows.Forms.Panel pnlAdmin;
        private System.Windows.Forms.TextBox txtADMa;
        private System.Windows.Forms.TextBox txtADHoTen;
        private System.Windows.Forms.TextBox txtADEmail;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
