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
            this.cmbVaiTro = new System.Windows.Forms.ComboBox();
            this.pnlSinhVien = new System.Windows.Forms.Panel();
            this.txtSVMa = new System.Windows.Forms.TextBox();
            this.txtSVHoTen = new System.Windows.Forms.TextBox();
            this.txtSVEmail = new System.Windows.Forms.TextBox();
            this.dtpSVNgaySinh = new System.Windows.Forms.DateTimePicker();
            this.txtSVCCCD = new System.Windows.Forms.TextBox();
            this.cboSVMaLop = new System.Windows.Forms.ComboBox();
            this.cboSVMaKhoa = new System.Windows.Forms.ComboBox();
            this.pnlGiangVien = new System.Windows.Forms.Panel();
            this.txtGVMa = new System.Windows.Forms.TextBox();
            this.txtGVHoTen = new System.Windows.Forms.TextBox();
            this.txtGVEmail = new System.Windows.Forms.TextBox();
            this.txtGVHocHam = new System.Windows.Forms.TextBox();
            this.cboGVMaKhoa = new System.Windows.Forms.ComboBox();
            this.pnlAdmin = new System.Windows.Forms.Panel();
            this.txtADMa = new System.Windows.Forms.TextBox();
            this.txtADHoTen = new System.Windows.Forms.TextBox();
            this.txtADEmail = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmbVaiTro
            // 
            this.cmbVaiTro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbVaiTro.Location = new System.Drawing.Point(12, 12);
            this.cmbVaiTro.Name = "cmbVaiTro";
            this.cmbVaiTro.Size = new System.Drawing.Size(240, 40);
            this.cmbVaiTro.TabIndex = 0;
            this.cmbVaiTro.Items.AddRange(new object[] { "SinhVien", "GiangVien", "Admin" });
            this.cmbVaiTro.SelectedIndexChanged += new System.EventHandler(this.cmbVaiTro_SelectedIndexChanged);
            // 
            // pnlSinhVien
            // 
            this.pnlSinhVien.Location = new System.Drawing.Point(12, 60);
            this.pnlSinhVien.Name = "pnlSinhVien";
            this.pnlSinhVien.Size = new System.Drawing.Size(760, 300);
            this.pnlSinhVien.TabIndex = 1;
            // add simple controls inside
            this.txtSVMa.Location = new System.Drawing.Point(8, 8);
            this.txtSVMa.PlaceholderText = "Mã SV";
            this.txtSVMa.Width = 360;
            this.pnlSinhVien.Controls.Add(this.txtSVMa);
            this.txtSVHoTen.Location = new System.Drawing.Point(8, 56);
            this.txtSVHoTen.PlaceholderText = "Họ tên";
            this.txtSVHoTen.Width = 360;
            this.pnlSinhVien.Controls.Add(this.txtSVHoTen);
            this.txtSVEmail.Location = new System.Drawing.Point(8, 104);
            this.txtSVEmail.PlaceholderText = "Email";
            this.txtSVEmail.Width = 360;
            this.pnlSinhVien.Controls.Add(this.txtSVEmail);
            this.dtpSVNgaySinh.Location = new System.Drawing.Point(8, 152);
            this.pnlSinhVien.Controls.Add(this.dtpSVNgaySinh);
            this.txtSVCCCD.Location = new System.Drawing.Point(8, 200);
            this.txtSVCCCD.PlaceholderText = "Số CCCD (12 số)";
            this.txtSVCCCD.Width = 360;
            this.pnlSinhVien.Controls.Add(this.txtSVCCCD);
            this.cboSVMaLop.Location = new System.Drawing.Point(380, 8);
            this.cboSVMaLop.Width = 360;
            this.pnlSinhVien.Controls.Add(this.cboSVMaLop);
            this.cboSVMaKhoa.Location = new System.Drawing.Point(380, 56);
            this.cboSVMaKhoa.Width = 360;
            this.pnlSinhVien.Controls.Add(this.cboSVMaKhoa);

            // 
            // pnlGiangVien
            // 
            this.pnlGiangVien.Location = new System.Drawing.Point(12, 60);
            this.pnlGiangVien.Name = "pnlGiangVien";
            this.pnlGiangVien.Size = new System.Drawing.Size(760, 300);
            this.pnlGiangVien.TabIndex = 2;
            this.txtGVMa.PlaceholderText = "Mã GV";
            this.txtGVMa.Location = new System.Drawing.Point(8, 8);
            this.txtGVMa.Width = 360;
            this.pnlGiangVien.Controls.Add(this.txtGVMa);
            this.txtGVHoTen.Location = new System.Drawing.Point(8, 56);
            this.txtGVHoTen.PlaceholderText = "Họ tên";
            this.txtGVHoTen.Width = 360;
            this.pnlGiangVien.Controls.Add(this.txtGVHoTen);
            this.txtGVEmail.Location = new System.Drawing.Point(8, 104);
            this.txtGVEmail.PlaceholderText = "Email";
            this.txtGVEmail.Width = 360;
            this.pnlGiangVien.Controls.Add(this.txtGVEmail);
            this.txtGVHocHam.Location = new System.Drawing.Point(8, 152);
            this.txtGVHocHam.PlaceholderText = "Học hàm học vị";
            this.txtGVHocHam.Width = 360;
            this.pnlGiangVien.Controls.Add(this.txtGVHocHam);
            this.cboGVMaKhoa.Location = new System.Drawing.Point(380, 8);
            this.cboGVMaKhoa.Width = 360;
            this.pnlGiangVien.Controls.Add(this.cboGVMaKhoa);

            // 
            // pnlAdmin
            // 
            this.pnlAdmin.Location = new System.Drawing.Point(12, 60);
            this.pnlAdmin.Name = "pnlAdmin";
            this.pnlAdmin.Size = new System.Drawing.Size(760, 300);
            this.pnlAdmin.TabIndex = 3;
            this.txtADMa.PlaceholderText = "Mã Admin";
            this.txtADMa.Location = new System.Drawing.Point(8, 8);
            this.txtADMa.Width = 360;
            this.pnlAdmin.Controls.Add(this.txtADMa);
            this.txtADHoTen.Location = new System.Drawing.Point(8, 56);
            this.txtADHoTen.PlaceholderText = "Họ tên";
            this.txtADHoTen.Width = 360;
            this.pnlAdmin.Controls.Add(this.txtADHoTen);
            this.txtADEmail.Location = new System.Drawing.Point(8, 104);
            this.txtADEmail.PlaceholderText = "Email";
            this.txtADEmail.Width = 360;
            this.pnlAdmin.Controls.Add(this.txtADEmail);

            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(616, 380);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(120, 40);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(740, 380);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 40);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // Form
            this.ClientSize = new System.Drawing.Size(784, 431);
            this.Controls.Add(this.cmbVaiTro);
            this.Controls.Add(this.pnlSinhVien);
            this.Controls.Add(this.pnlGiangVien);
            this.Controls.Add(this.pnlAdmin);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Name = "FrmThemSuaTaiKhoan";
            this.Text = "Thêm / Sửa Tài khoản";

            this.pnlSinhVien.Visible = false;
            this.pnlGiangVien.Visible = false;
            this.pnlAdmin.Visible = false;

            this.ResumeLayout(false);
            this.PerformLayout();
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
