using System.Drawing;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    partial class FrmXacThuc
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
            lblCauHoi = new Label();
            txtTraLoi = new TextBox();
            lblHuongDan = new Label();
            lblThongBao = new Label();
            btnTiepTuc = new Button();
            btnHuy = new Button();
            SuspendLayout();
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Text = "Xác thực sinh viên";
            lblTieuDe.Location = new Point(30, 24);
            lblTieuDe.Size = new Size(550, 44);
            lblTieuDe.Font = new Font("Segoe UI", 22F);
            lblCauHoi.Name = "lblCauHoi";
            lblCauHoi.Text = "Câu hỏi xác thực";
            lblCauHoi.Location = new Point(30, 90);
            lblCauHoi.Size = new Size(550, 32);
            lblCauHoi.Font = new Font("Segoe UI", 12F);
            txtTraLoi.Name = "txtTraLoi";
            txtTraLoi.Location = new Point(30, 134);
            txtTraLoi.Size = new Size(550, 38);
            txtTraLoi.Font = new Font("Segoe UI", 14F);
            txtTraLoi.TabIndex = 0;
            lblHuongDan.Name = "lblHuongDan";
            lblHuongDan.Text = "";
            lblHuongDan.Location = new Point(30, 185);
            lblHuongDan.Size = new Size(550, 60);
            lblHuongDan.Font = new Font("Segoe UI", 10F);
            lblThongBao.Name = "lblThongBao";
            lblThongBao.Text = "";
            lblThongBao.Location = new Point(30, 259);
            lblThongBao.Size = new Size(550, 75);
            lblThongBao.Font = new Font("Segoe UI", 10F);
            lblThongBao.ForeColor = Color.Firebrick;
            btnTiepTuc.Name = "btnTiepTuc";
            btnTiepTuc.Text = "Tiếp tục nhập OTP";
            btnTiepTuc.Location = new Point(30, 350);
            btnTiepTuc.Size = new Size(330, 46);
            btnTiepTuc.FlatStyle = FlatStyle.Flat;
            btnTiepTuc.Click += btnTiepTuc_Click;
            btnTiepTuc.BackColor = Color.FromArgb(24, 94, 177);
            btnTiepTuc.ForeColor = Color.White;
            btnHuy.Name = "btnHuy";
            btnHuy.Text = "Hủy";
            btnHuy.Location = new Point(420, 350);
            btnHuy.Size = new Size(160, 46);
            btnHuy.FlatStyle = FlatStyle.Flat;
            btnHuy.Click += btnHuy_Click;
            btnTiepTuc.TabIndex = 1;
            btnHuy.TabIndex = 2;
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Segoe UI", 10F);
            BackColor = Color.White;
            ForeColor = Color.FromArgb(20, 49, 83);
            ClientSize = new Size(610, 425);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            Name = "FrmXacThuc";
            Text = "Bước 1 • Xác thực sinh viên";
            AcceptButton = btnTiepTuc;
            CancelButton = btnHuy;
            FormClosing += FrmXacThuc_FormClosing;
            Controls.Add(lblTieuDe);
            Controls.Add(lblCauHoi);
            Controls.Add(txtTraLoi);
            Controls.Add(lblHuongDan);
            Controls.Add(lblThongBao);
            Controls.Add(btnTiepTuc);
            Controls.Add(btnHuy);
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTieuDe;
        private Label lblCauHoi;
        private TextBox txtTraLoi;
        private Label lblHuongDan;
        private Label lblThongBao;
        private Button btnTiepTuc;
        private Button btnHuy;
    }
}
