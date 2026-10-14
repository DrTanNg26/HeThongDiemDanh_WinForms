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
            timerDong = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Text = "Nhập mã điểm danh";
            lblTieuDe.Location = new Point(30, 24);
            lblTieuDe.Size = new Size(550, 44);
            lblTieuDe.Font = new Font("Segoe UI", 22F);
            lblMonHoc.Name = "lblMonHoc";
            lblMonHoc.Text = "Môn học";
            lblMonHoc.Location = new Point(30, 82);
            lblMonHoc.Size = new Size(550, 36);
            lblMonHoc.Font = new Font("Segoe UI", 12F);
            txtOTP.Name = "txtOTP";
            txtOTP.Location = new Point(30, 130);
            txtOTP.Size = new Size(550, 56);
            txtOTP.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            txtOTP.CharacterCasing = CharacterCasing.Upper;
            txtOTP.MaxLength = 6;
            txtOTP.TabIndex = 0;
            lblHuongDan.Name = "lblHuongDan";
            lblHuongDan.Text = "";
            lblHuongDan.Location = new Point(30, 199);
            lblHuongDan.Size = new Size(550, 60);
            lblHuongDan.Font = new Font("Segoe UI", 10F);
            lblThongBao.Name = "lblThongBao";
            lblThongBao.Text = "";
            lblThongBao.Location = new Point(30, 270);
            lblThongBao.Size = new Size(550, 75);
            lblThongBao.Font = new Font("Segoe UI", 10F);
            btnXacNhan.Name = "btnXacNhan";
            btnXacNhan.Text = "Xác nhận điểm danh";
            btnXacNhan.Location = new Point(30, 366);
            btnXacNhan.Size = new Size(330, 46);
            btnXacNhan.FlatStyle = FlatStyle.Flat;
            btnXacNhan.Click += btnXacNhan_Click;
            btnXacNhan.BackColor = Color.FromArgb(24, 94, 177);
            btnXacNhan.ForeColor = Color.White;
            btnDong.Name = "btnDong";
            btnDong.Text = "Hủy";
            btnDong.Location = new Point(420, 366);
            btnDong.Size = new Size(160, 46);
            btnDong.FlatStyle = FlatStyle.Flat;
            btnDong.Click += btnDong_Click;
            btnXacNhan.TabIndex = 1;
            btnDong.TabIndex = 2;
            timerDong.Interval = 3000;
            timerDong.Tick += timerDong_Tick;
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.White;
            ForeColor = Color.FromArgb(20, 49, 83);
            ClientSize = new Size(610, 442);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Name = "FrmNhapOTP";
            Text = "Bước 2 • Điểm danh";
            AcceptButton = btnXacNhan;
            CancelButton = btnDong;
            FormClosing += FrmNhapOTP_FormClosing;
            Controls.Add(lblTieuDe);
            Controls.Add(lblMonHoc);
            Controls.Add(txtOTP);
            Controls.Add(lblHuongDan);
            Controls.Add(lblThongBao);
            Controls.Add(btnXacNhan);
            Controls.Add(btnDong);
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
        private System.Windows.Forms.Timer timerDong;
    }
}
