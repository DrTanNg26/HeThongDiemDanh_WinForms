namespace HeThongDiemDanh
{
    partial class UcMoPhien
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

        private void InitializeComponent()
        {
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.lblLopHocPhan = new System.Windows.Forms.Label();
            this.cboLopHocPhan = new System.Windows.Forms.ComboBox();
            this.lblSiSoTitle = new System.Windows.Forms.Label();
            this.lblSiSo = new System.Windows.Forms.Label();
            this.lblBuoiHoc = new System.Windows.Forms.Label();
            this.nudBuoiHoc = new System.Windows.Forms.NumericUpDown();
            this.lblThoiGian = new System.Windows.Forms.Label();
            this.rdo5Phut = new System.Windows.Forms.RadioButton();
            this.rdo10Phut = new System.Windows.Forms.RadioButton();
            this.rdo15Phut = new System.Windows.Forms.RadioButton();
            this.grpMang = new System.Windows.Forms.GroupBox();
            this.lblIPTitle = new System.Windows.Forms.Label();
            this.lblIP = new System.Windows.Forms.Label();
            this.lblSubnetTitle = new System.Windows.Forms.Label();
            this.lblSubnet = new System.Windows.Forms.Label();
            this.btnLamMoiMang = new System.Windows.Forms.Button();
            this.btnBatDauDiemDanh = new System.Windows.Forms.Button();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBuoiHoc)).BeginInit();
            this.grpMang.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpThongTin
            // 
            this.grpThongTin.Controls.Add(this.rdo15Phut);
            this.grpThongTin.Controls.Add(this.rdo10Phut);
            this.grpThongTin.Controls.Add(this.rdo5Phut);
            this.grpThongTin.Controls.Add(this.lblThoiGian);
            this.grpThongTin.Controls.Add(this.nudBuoiHoc);
            this.grpThongTin.Controls.Add(this.lblBuoiHoc);
            this.grpThongTin.Controls.Add(this.lblSiSo);
            this.grpThongTin.Controls.Add(this.lblSiSoTitle);
            this.grpThongTin.Controls.Add(this.cboLopHocPhan);
            this.grpThongTin.Controls.Add(this.lblLopHocPhan);
            this.grpThongTin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.grpThongTin.Location = new System.Drawing.Point(30, 20);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(650, 210);
            this.grpThongTin.TabIndex = 0;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Cấu hình buổi điểm danh";
            // 
            // lblLopHocPhan
            // 
            this.lblLopHocPhan.AutoSize = true;
            this.lblLopHocPhan.Location = new System.Drawing.Point(20, 35);
            this.lblLopHocPhan.Name = "lblLopHocPhan";
            this.lblLopHocPhan.Size = new System.Drawing.Size(98, 19);
            this.lblLopHocPhan.TabIndex = 0;
            this.lblLopHocPhan.Text = "Lớp học phần:";
            // 
            // cboLopHocPhan
            // 
            this.cboLopHocPhan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLopHocPhan.FormattingEnabled = true;
            this.cboLopHocPhan.Location = new System.Drawing.Point(140, 32);
            this.cboLopHocPhan.Name = "cboLopHocPhan";
            this.cboLopHocPhan.Size = new System.Drawing.Size(480, 25);
            this.cboLopHocPhan.TabIndex = 1;
            this.cboLopHocPhan.SelectedIndexChanged += new System.EventHandler(this.cboLopHocPhan_SelectedIndexChanged);
            // 
            // lblSiSoTitle
            // 
            this.lblSiSoTitle.AutoSize = true;
            this.lblSiSoTitle.Location = new System.Drawing.Point(20, 80);
            this.lblSiSoTitle.Name = "lblSiSoTitle";
            this.lblSiSoTitle.Size = new System.Drawing.Size(73, 19);
            this.lblSiSoTitle.TabIndex = 2;
            this.lblSiSoTitle.Text = "Sĩ số lớp:";
            // 
            // lblSiSo
            // 
            this.lblSiSo.AutoSize = true;
            this.lblSiSo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSiSo.ForeColor = System.Drawing.Color.Navy;
            this.lblSiSo.Location = new System.Drawing.Point(140, 80);
            this.lblSiSo.Name = "lblSiSo";
            this.lblSiSo.Size = new System.Drawing.Size(17, 19);
            this.lblSiSo.TabIndex = 3;
            this.lblSiSo.Text = "0";
            // 
            // lblBuoiHoc
            // 
            this.lblBuoiHoc.AutoSize = true;
            this.lblBuoiHoc.Location = new System.Drawing.Point(20, 120);
            this.lblBuoiHoc.Name = "lblBuoiHoc";
            this.lblBuoiHoc.Size = new System.Drawing.Size(89, 19);
            this.lblBuoiHoc.TabIndex = 4;
            this.lblBuoiHoc.Text = "Buổi học số:";
            // 
            // nudBuoiHoc
            // 
            this.nudBuoiHoc.Location = new System.Drawing.Point(140, 118);
            this.nudBuoiHoc.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.nudBuoiHoc.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudBuoiHoc.Name = "nudBuoiHoc";
            this.nudBuoiHoc.Size = new System.Drawing.Size(80, 25);
            this.nudBuoiHoc.TabIndex = 5;
            this.nudBuoiHoc.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblThoiGian
            // 
            this.lblThoiGian.AutoSize = true;
            this.lblThoiGian.Location = new System.Drawing.Point(20, 165);
            this.lblThoiGian.Name = "lblThoiGian";
            this.lblThoiGian.Size = new System.Drawing.Size(107, 19);
            this.lblThoiGian.TabIndex = 6;
            this.lblThoiGian.Text = "Thời gian đếm:";
            // 
            // rdo5Phut
            // 
            this.rdo5Phut.AutoSize = true;
            this.rdo5Phut.Location = new System.Drawing.Point(140, 163);
            this.rdo5Phut.Name = "rdo5Phut";
            this.rdo5Phut.Size = new System.Drawing.Size(68, 23);
            this.rdo5Phut.TabIndex = 7;
            this.rdo5Phut.Text = "5 phút";
            this.rdo5Phut.UseVisualStyleBackColor = true;
            // 
            // rdo10Phut
            // 
            this.rdo10Phut.AutoSize = true;
            this.rdo10Phut.Checked = true;
            this.rdo10Phut.Location = new System.Drawing.Point(230, 163);
            this.rdo10Phut.Name = "rdo10Phut";
            this.rdo10Phut.Size = new System.Drawing.Size(76, 23);
            this.rdo10Phut.TabIndex = 8;
            this.rdo10Phut.TabStop = true;
            this.rdo10Phut.Text = "10 phút";
            this.rdo10Phut.UseVisualStyleBackColor = true;
            // 
            // rdo15Phut
            // 
            this.rdo15Phut.AutoSize = true;
            this.rdo15Phut.Location = new System.Drawing.Point(330, 163);
            this.rdo15Phut.Name = "rdo15Phut";
            this.rdo15Phut.Size = new System.Drawing.Size(76, 23);
            this.rdo15Phut.TabIndex = 9;
            this.rdo15Phut.Text = "15 phút";
            this.rdo15Phut.UseVisualStyleBackColor = true;
            // 
            // grpMang
            // 
            this.grpMang.Controls.Add(this.btnLamMoiMang);
            this.grpMang.Controls.Add(this.lblSubnet);
            this.grpMang.Controls.Add(this.lblSubnetTitle);
            this.grpMang.Controls.Add(this.lblIP);
            this.grpMang.Controls.Add(this.lblIPTitle);
            this.grpMang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.grpMang.Location = new System.Drawing.Point(30, 245);
            this.grpMang.Name = "grpMang";
            this.grpMang.Size = new System.Drawing.Size(650, 110);
            this.grpMang.TabIndex = 1;
            this.grpMang.TabStop = false;
            this.grpMang.Text = "Thông tin kết nối mạng (Wi-Fi / LAN)";
            // 
            // lblIPTitle
            // 
            this.lblIPTitle.AutoSize = true;
            this.lblIPTitle.Location = new System.Drawing.Point(20, 35);
            this.lblIPTitle.Name = "lblIPTitle";
            this.lblIPTitle.Size = new System.Drawing.Size(95, 19);
            this.lblIPTitle.TabIndex = 0;
            this.lblIPTitle.Text = "Địa chỉ IP GV:";
            // 
            // lblIP
            // 
            this.lblIP.AutoSize = true;
            this.lblIP.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblIP.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblIP.Location = new System.Drawing.Point(140, 35);
            this.lblIP.Name = "lblIP";
            this.lblIP.Size = new System.Drawing.Size(107, 19);
            this.lblIP.TabIndex = 1;
            this.lblIP.Text = "Đang kiểm tra...";
            // 
            // lblSubnetTitle
            // 
            this.lblSubnetTitle.AutoSize = true;
            this.lblSubnetTitle.Location = new System.Drawing.Point(20, 70);
            this.lblSubnetTitle.Name = "lblSubnetTitle";
            this.lblSubnetTitle.Size = new System.Drawing.Size(91, 19);
            this.lblSubnetTitle.TabIndex = 2;
            this.lblSubnetTitle.Text = "Subnet Mask:";
            // 
            // lblSubnet
            // 
            this.lblSubnet.AutoSize = true;
            this.lblSubnet.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSubnet.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.lblSubnet.Location = new System.Drawing.Point(140, 70);
            this.lblSubnet.Name = "lblSubnet";
            this.lblSubnet.Size = new System.Drawing.Size(107, 19);
            this.lblSubnet.TabIndex = 3;
            this.lblSubnet.Text = "Đang kiểm tra...";
            // 
            // btnLamMoiMang
            // 
            this.btnLamMoiMang.Location = new System.Drawing.Point(490, 40);
            this.btnLamMoiMang.Name = "btnLamMoiMang";
            this.btnLamMoiMang.Size = new System.Drawing.Size(130, 35);
            this.btnLamMoiMang.TabIndex = 4;
            this.btnLamMoiMang.Text = "Kiểm tra lại";
            this.btnLamMoiMang.UseVisualStyleBackColor = true;
            this.btnLamMoiMang.Click += new System.EventHandler(this.btnLamMoiMang_Click);
            // 
            // btnBatDauDiemDanh
            // 
            this.btnBatDauDiemDanh.BackColor = System.Drawing.SystemColors.Control;
            this.btnBatDauDiemDanh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBatDauDiemDanh.ForeColor = System.Drawing.Color.Black;
            this.btnBatDauDiemDanh.Location = new System.Drawing.Point(210, 375);
            this.btnBatDauDiemDanh.Name = "btnBatDauDiemDanh";
            this.btnBatDauDiemDanh.Size = new System.Drawing.Size(280, 50);
            this.btnBatDauDiemDanh.TabIndex = 2;
            this.btnBatDauDiemDanh.Text = "BẮT ĐẦU ĐIỂM DANH";
            this.btnBatDauDiemDanh.UseVisualStyleBackColor = false;
            this.btnBatDauDiemDanh.Click += new System.EventHandler(this.btnBatDauDiemDanh_Click);
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblTrangThai.ForeColor = System.Drawing.Color.DarkRed;
            this.lblTrangThai.Location = new System.Drawing.Point(30, 440);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(0, 17);
            this.lblTrangThai.TabIndex = 3;
            // 
            // UcMoPhien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.btnBatDauDiemDanh);
            this.Controls.Add(this.grpMang);
            this.Controls.Add(this.grpThongTin);
            this.Name = "UcMoPhien";
            this.Size = new System.Drawing.Size(720, 480);
            this.Load += new System.EventHandler(this.UcMoPhien_Load);
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBuoiHoc)).EndInit();
            this.grpMang.ResumeLayout(false);
            this.grpMang.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblLopHocPhan;
        private System.Windows.Forms.ComboBox cboLopHocPhan;
        private System.Windows.Forms.Label lblSiSoTitle;
        private System.Windows.Forms.Label lblSiSo;
        private System.Windows.Forms.Label lblBuoiHoc;
        private System.Windows.Forms.NumericUpDown nudBuoiHoc;
        private System.Windows.Forms.Label lblThoiGian;
        private System.Windows.Forms.RadioButton rdo5Phut;
        private System.Windows.Forms.RadioButton rdo10Phut;
        private System.Windows.Forms.RadioButton rdo15Phut;
        private System.Windows.Forms.GroupBox grpMang;
        private System.Windows.Forms.Label lblIPTitle;
        private System.Windows.Forms.Label lblIP;
        private System.Windows.Forms.Label lblSubnetTitle;
        private System.Windows.Forms.Label lblSubnet;
        private System.Windows.Forms.Button btnLamMoiMang;
        private System.Windows.Forms.Button btnBatDauDiemDanh;
        private System.Windows.Forms.Label lblTrangThai;
    }
}
