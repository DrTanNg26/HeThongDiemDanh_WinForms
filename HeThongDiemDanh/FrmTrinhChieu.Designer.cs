using System.Drawing;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    partial class FrmTrinhChieu
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            timerCapNhat = new System.Windows.Forms.Timer(components);
            timerCapNhat.Interval = 1000;
            timerCapNhat.Tick += timerCapNhat_Tick;
            SuspendLayout();
            this.Text = "Trình chiếu điểm danh";
            this.ClientSize = new Size(1000, 680);
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.MinimumSize = new Size(850, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // 1. Panel Header (Mã OTP cỡ lớn + Đồng hồ đếm ngược + Trạng thái)
            pnlHeader = new Panel();
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Size = new Size(1000, 175);
            pnlHeader.BackColor = Color.White;
            pnlHeader.BorderStyle = BorderStyle.FixedSingle;

            lblTitle = new Label();
            lblTitle.Name = "lblTitle";
            lblTitle.Text = "Lớp học phần • Buổi học";
            lblTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 41, 59);
            lblTitle.Location = new Point(20, 12);
            lblTitle.AutoSize = true;

            lblOtpTitle = new Label();
            lblOtpTitle.Name = "lblOtpTitle";
            lblOtpTitle.Text = "MÃ OTP ĐIỂM DANH:";
            lblOtpTitle.Font = new Font("Segoe UI", 11F, FontStyle.Regular);
            lblOtpTitle.ForeColor = Color.Gray;
            lblOtpTitle.Location = new Point(22, 50);
            lblOtpTitle.AutoSize = true;

            lblOtp = new Label();
            lblOtp.Name = "lblOtp";
            lblOtp.Text = "------";
            lblOtp.Font = new Font("Segoe UI", 48F, FontStyle.Bold);
            lblOtp.ForeColor = Color.FromArgb(10, 80, 180);
            lblOtp.Location = new Point(16, 70);
            lblOtp.AutoSize = true;

            lblCountdown = new Label();
            lblCountdown.Name = "lblCountdown";
            lblCountdown.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblCountdown.Font = new Font("Segoe UI", 48F, FontStyle.Bold);
            lblCountdown.ForeColor = Color.ForestGreen;
            lblCountdown.Location = new Point(640, 40);
            lblCountdown.Size = new Size(330, 85);
            lblCountdown.TextAlign = ContentAlignment.MiddleRight;
            lblCountdown.Text = "--:--";

            lblTrangThai = new Label();
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTrangThai.Font = new Font("Segoe UI", 11F, FontStyle.Italic);
            lblTrangThai.ForeColor = Color.DarkSlateGray;
            lblTrangThai.Location = new Point(400, 130);
            lblTrangThai.Size = new Size(570, 28);
            lblTrangThai.TextAlign = ContentAlignment.MiddleRight;
            lblTrangThai.Text = "Đang đồng bộ giờ máy chủ...";

            pnlHeader.Controls.AddRange(new Control[] { lblTitle, lblOtpTitle, lblOtp, lblCountdown, lblTrangThai });

            // 2. Panel Thống kê (Badge/Cards: Sĩ số | Đã có mặt | Chưa điểm danh)
            pnlStats = new Panel();
            pnlStats.Name = "pnlStats";
            pnlStats.Dock = DockStyle.Top;
            pnlStats.Size = new Size(1000, 48);
            pnlStats.BackColor = Color.FromArgb(238, 242, 246);
            pnlStats.BorderStyle = BorderStyle.FixedSingle;

            lblSiSo = new Label();
            lblSiSo.Name = "lblSiSo";
            lblSiSo.Text = "Sĩ số lớp: 0";
            lblSiSo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblSiSo.ForeColor = Color.FromArgb(30, 41, 59);
            lblSiSo.Location = new Point(20, 12);
            lblSiSo.AutoSize = true;

            lblCoMat = new Label();
            lblCoMat.Name = "lblCoMat";
            lblCoMat.Text = "Đã có mặt: 0";
            lblCoMat.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblCoMat.ForeColor = Color.DarkGreen;
            lblCoMat.Location = new Point(220, 12);
            lblCoMat.AutoSize = true;

            lblChuaDiemDanh = new Label();
            lblChuaDiemDanh.Name = "lblChuaDiemDanh";
            lblChuaDiemDanh.Text = "Chưa điểm danh: 0";
            lblChuaDiemDanh.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblChuaDiemDanh.ForeColor = Color.Firebrick;
            lblChuaDiemDanh.Location = new Point(440, 12);
            lblChuaDiemDanh.AutoSize = true;

            pnlStats.Controls.AddRange(new Control[] { lblSiSo, lblCoMat, lblChuaDiemDanh });

            // 3. DataGridView realtime
            dgvRealtime = new DataGridView();
            dgvRealtime.Name = "dgvRealtime";
            dgvRealtime.Dock = DockStyle.Fill;
            dgvRealtime.BackgroundColor = Color.White;
            dgvRealtime.BorderStyle = BorderStyle.None;
            dgvRealtime.AllowUserToAddRows = false;
            dgvRealtime.AllowUserToDeleteRows = false;
            dgvRealtime.ReadOnly = true;
            dgvRealtime.RowHeadersVisible = false;
            dgvRealtime.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRealtime.MultiSelect = false;
            dgvRealtime.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRealtime.EnableHeadersVisualStyles = false;
            dgvRealtime.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 243, 246);
            dgvRealtime.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvRealtime.ColumnHeadersHeight = 36;
            dgvRealtime.RowTemplate.Height = 32;
            dgvRealtime.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            // 4. Panel Footer (Nút Hủy phiên & Nút Kết thúc buổi học)
            pnlFooter = new Panel();
            pnlFooter.Name = "pnlFooter";
            pnlFooter.Dock = DockStyle.Bottom;
            pnlFooter.Size = new Size(1000, 60);
            pnlFooter.BackColor = Color.FromArgb(240, 244, 248);
            pnlFooter.BorderStyle = BorderStyle.FixedSingle;

            btnHuyPhien = new Button();
            btnHuyPhien.Name = "btnHuyPhien";
            btnHuyPhien.Text = "Hủy phiên này";
            btnHuyPhien.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btnHuyPhien.ForeColor = Color.FromArgb(80, 80, 80);
            btnHuyPhien.Location = new Point(20, 10);
            btnHuyPhien.Size = new Size(150, 38);
            btnHuyPhien.UseVisualStyleBackColor = true;
            btnHuyPhien.Click += btnHuyPhien_Click;

            btnDongBuoiHoc = new Button();
            btnDongBuoiHoc.Name = "btnDongBuoiHoc";
            btnDongBuoiHoc.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDongBuoiHoc.Text = "KẾT THÚC BUỔI HỌC";
            btnDongBuoiHoc.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDongBuoiHoc.BackColor = Color.FromArgb(217, 48, 37);
            btnDongBuoiHoc.ForeColor = Color.White;
            btnDongBuoiHoc.FlatStyle = FlatStyle.Flat;
            btnDongBuoiHoc.Location = new Point(740, 10);
            btnDongBuoiHoc.Size = new Size(220, 38);
            btnDongBuoiHoc.UseVisualStyleBackColor = false;
            btnDongBuoiHoc.FlatAppearance.BorderSize = 0;
            btnDongBuoiHoc.Click += btnDongBuoiHoc_Click;

            pnlFooter.Controls.AddRange(new Control[] { btnHuyPhien, btnDongBuoiHoc });

            // Thêm các control vào Form theo thứ tự Dock
            this.Controls.Add(dgvRealtime);
            this.Controls.Add(pnlStats);
            this.Controls.Add(pnlHeader);
            this.Controls.Add(pnlFooter);

            this.Load += FrmTrinhChieu_Load;
            this.FormClosing += FrmTrinhChieu_FormClosing;
            ResumeLayout(false);
        }
        private System.Windows.Forms.Timer timerCapNhat;
        private Panel pnlHeader;
        private Label lblTitle;
        private Label lblOtpTitle;
        private Label lblOtp;
        private Label lblCountdown;
        private Label lblTrangThai;
        private Panel pnlStats;
        private Label lblSiSo;
        private Label lblCoMat;
        private Label lblChuaDiemDanh;
        private DataGridView dgvRealtime;
        private Panel pnlFooter;
        private Button btnHuyPhien;
        private Button btnDongBuoiHoc;
    }
}
