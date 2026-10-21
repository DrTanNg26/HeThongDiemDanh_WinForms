using System.Drawing;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    partial class FrmNhapOTP
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lblTieuDe = new Label();
            lblMonHoc = new Label();
            txtOTP = new TextBox();
            lblHuongDan = new Label();
            lblThongBao = new Label();
            btnXacNhan = new Button();
            btnDong = new Button();
            btnKiemTraMang = new Button();
            timerDong = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // lblTieuDe
            // 
            lblTieuDe.Font = new Font("Segoe UI", 22F);
            lblTieuDe.Location = new Point(30, 24);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(550, 44);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "Nhập mã điểm danh";
            // 
            // lblMonHoc
            // 
            lblMonHoc.Font = new Font("Segoe UI", 12F);
            lblMonHoc.Location = new Point(30, 82);
            lblMonHoc.Name = "lblMonHoc";
            lblMonHoc.Size = new Size(550, 36);
            lblMonHoc.TabIndex = 1;
            lblMonHoc.Text = "Môn học";
            // 
            // txtOTP
            // 
            txtOTP.CharacterCasing = CharacterCasing.Upper;
            txtOTP.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            txtOTP.Location = new Point(30, 130);
            txtOTP.MaxLength = 6;
            txtOTP.Name = "txtOTP";
            txtOTP.Size = new Size(550, 56);
            txtOTP.TabIndex = 0;
            txtOTP.TextChanged += txtOTP_TextChanged;
            // 
            // lblHuongDan
            // 
            lblHuongDan.Font = new Font("Segoe UI", 10F);
            lblHuongDan.Location = new Point(30, 199);
            lblHuongDan.Name = "lblHuongDan";
            lblHuongDan.Size = new Size(550, 60);
            lblHuongDan.TabIndex = 2;
            // 
            // lblThongBao
            // 
            lblThongBao.Font = new Font("Segoe UI", 10F);
            lblThongBao.Location = new Point(30, 308);
            lblThongBao.Name = "lblThongBao";
            lblThongBao.Size = new Size(550, 90);
            lblThongBao.TabIndex = 3;
            // 
            // btnXacNhan
            // 
            btnXacNhan.BackColor = Color.FromArgb(24, 94, 177);
            btnXacNhan.FlatStyle = FlatStyle.Flat;
            btnXacNhan.ForeColor = Color.White;
            btnXacNhan.Location = new Point(30, 414);
            btnXacNhan.Name = "btnXacNhan";
            btnXacNhan.Size = new Size(330, 46);
            btnXacNhan.TabIndex = 1;
            btnXacNhan.Text = "Xác nhận điểm danh";
            btnXacNhan.UseVisualStyleBackColor = false;
            btnXacNhan.Click += btnXacNhan_Click;
            // 
            // btnDong
            // 
            btnDong.FlatStyle = FlatStyle.Flat;
            btnDong.Location = new Point(420, 414);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(160, 46);
            btnDong.TabIndex = 2;
            btnDong.Text = "Hủy";
            btnDong.Click += btnDong_Click;
            btnKiemTraMang.Name = "btnKiemTraMang";
            btnKiemTraMang.Text = "Kiểm tra mạng";
            btnKiemTraMang.Location = new Point(30, 260);
            btnKiemTraMang.Size = new Size(200, 38);
            btnKiemTraMang.FlatStyle = FlatStyle.Flat;
            btnKiemTraMang.TabIndex = 3;
            btnKiemTraMang.Click += btnKiemTraMang_Click;
            // 
            // timerDong
            // 
            timerDong.Interval = 3000;
            timerDong.Tick += timerDong_Tick;
            // 
            // FrmNhapOTP
            // 
            AcceptButton = btnXacNhan;
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            CancelButton = btnDong;
            ClientSize = new Size(610, 490);
            Controls.Add(lblTieuDe);
            Controls.Add(lblMonHoc);
            Controls.Add(txtOTP);
            Controls.Add(lblHuongDan);
            Controls.Add(lblThongBao);
            Controls.Add(btnXacNhan);
            Controls.Add(btnDong);
            Controls.Add(btnKiemTraMang);
            Font = new Font("Segoe UI", 10F);
            ForeColor = Color.FromArgb(20, 49, 83);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmNhapOTP";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Bước 2 • Điểm danh";
            FormClosing += FrmNhapOTP_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTieuDe;
        private Label lblMonHoc;
        private TextBox txtOTP;
        private Label lblHuongDan;
        private Label lblThongBao;
        private Button btnXacNhan;
        private Button btnDong;
        private Button btnKiemTraMang;
        private System.Windows.Forms.Timer timerDong;
    }
}
