using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeThongDiemDanh
{
    public partial class FrmTrinhChieu : Form
    {
        private readonly int _maBuoiHoc;
        private readonly string _tenLop;
        private readonly string _tenBuoi;
        private readonly string _otp;

        private ThongTinBuoiHoc? _thongTinBuoi;
        private DateTime _thoiGianBatDauServer;
        private int _thoiGianDemNguocPhut = 10;
        private TimeSpan _serverTimeOffset = TimeSpan.Zero; // Chênh lệch giữa giờ Server và giờ Client

        private System.Windows.Forms.Timer? _countdownTimer;
        private System.Windows.Forms.Timer? _realtimeTimer;
        private bool _dangCapNhatRealtime = false;
        private bool _daHetGioDungGio = false;
        private bool _dangXuLyHetGio = false;
        private bool _daDongBuoi = false;

        // Controls giao diện
        private Label lblTitle = null!;
        private Label lblOtpTitle = null!;
        private Label lblOtp = null!;
        private Label lblCountdown = null!;
        private Label lblTrangThai = null!;
        private Label lblSiSo = null!;
        private Label lblCoMat = null!;
        private Label lblChuaDiemDanh = null!;
        private DataGridView dgvRealtime = null!;
        private Button btnHuyPhien = null!;
        private Button btnDongBuoiHoc = null!;

        public FrmTrinhChieu(int maBuoiHoc, string tenLop, string tenBuoi, string otp)
        {
            _maBuoiHoc = maBuoiHoc;
            _tenLop = tenLop;
            _tenBuoi = tenBuoi;
            _otp = otp;

            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Text = $"Màn hình Trình chiếu Điểm danh - {_tenLop} ({_tenBuoi})";
            this.Size = new Size(1000, 680);
            this.MinimumSize = new Size(850, 550);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(245, 247, 250);

            // 1. Panel Header (Mã OTP cỡ lớn + Đồng hồ đếm ngược + Trạng thái)
            Panel pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 175,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            lblTitle = new Label
            {
                Text = $"{_tenLop}  •  {_tenBuoi}",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(20, 12),
                AutoSize = true
            };

            lblOtpTitle = new Label
            {
                Text = "MÃ OTP ĐIỂM DANH:",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                ForeColor = Color.Gray,
                Location = new Point(22, 50),
                AutoSize = true
            };

            lblOtp = new Label
            {
                Text = _otp,
                Font = new Font("Segoe UI", 48F, FontStyle.Bold),
                ForeColor = Color.FromArgb(10, 80, 180),
                Location = new Point(16, 70),
                AutoSize = true
            };

            lblCountdown = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Font = new Font("Segoe UI", 48F, FontStyle.Bold),
                ForeColor = Color.ForestGreen,
                Location = new Point(640, 40),
                Size = new Size(330, 85),
                TextAlign = ContentAlignment.MiddleRight,
                Text = "--:--"
            };

            lblTrangThai = new Label
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Font = new Font("Segoe UI", 11F, FontStyle.Italic),
                ForeColor = Color.DarkSlateGray,
                Location = new Point(400, 130),
                Size = new Size(570, 28),
                TextAlign = ContentAlignment.MiddleRight,
                Text = "Đang đồng bộ giờ máy chủ..."
            };

            pnlHeader.Controls.AddRange(new Control[] { lblTitle, lblOtpTitle, lblOtp, lblCountdown, lblTrangThai });

            // 2. Panel Thống kê (Badge/Cards: Sĩ số | Đã có mặt | Chưa điểm danh)
            Panel pnlStats = new Panel
            {
                Dock = DockStyle.Top,
                Height = 48,
                BackColor = Color.FromArgb(238, 242, 246),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblSiSo = new Label
            {
                Text = "Sĩ số lớp: 0",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59),
                Location = new Point(20, 12),
                AutoSize = true
            };

            lblCoMat = new Label
            {
                Text = "Đã có mặt: 0",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.DarkGreen,
                Location = new Point(220, 12),
                AutoSize = true
            };

            lblChuaDiemDanh = new Label
            {
                Text = "Chưa điểm danh: 0",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.Firebrick,
                Location = new Point(440, 12),
                AutoSize = true
            };

            pnlStats.Controls.AddRange(new Control[] { lblSiSo, lblCoMat, lblChuaDiemDanh });

            // 3. DataGridView realtime
            dgvRealtime = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                EnableHeadersVisualStyles = false
            };
            dgvRealtime.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 243, 246);
            dgvRealtime.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            dgvRealtime.ColumnHeadersHeight = 36;
            dgvRealtime.RowTemplate.Height = 32;
            dgvRealtime.DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Regular);

            // 4. Panel Footer (Nút Hủy phiên & Nút Kết thúc buổi học)
            Panel pnlFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 60,
                BackColor = Color.FromArgb(240, 244, 248),
                BorderStyle = BorderStyle.FixedSingle
            };

            btnHuyPhien = new Button
            {
                Text = "Hủy phiên này",
                Font = new Font("Segoe UI", 10F, FontStyle.Regular),
                ForeColor = Color.FromArgb(80, 80, 80),
                Location = new Point(20, 10),
                Size = new Size(150, 38),
                UseVisualStyleBackColor = true
            };
            btnHuyPhien.Click += btnHuyPhien_Click;

            btnDongBuoiHoc = new Button
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Text = "KẾT THÚC BUỔI HỌC",
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(217, 48, 37),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(740, 10),
                Size = new Size(220, 38),
                UseVisualStyleBackColor = false
            };
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
        }

        private async void FrmTrinhChieu_Load(object? sender, EventArgs e)
        {
            await KhoiTaoDongBoServerAsync();

            // Khởi động Timer đếm ngược (chu kỳ 1 giây)
            _countdownTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _countdownTimer.Tick += CountdownTimer_Tick;
            _countdownTimer.Start();
            CountdownTimer_Tick(null, EventArgs.Empty);

            // Khởi động Timer nạp dữ liệu Realtime (chu kỳ 2.5 giây)
            _realtimeTimer = new System.Windows.Forms.Timer { Interval = 2500 };
            _realtimeTimer.Tick += async (s, args) => await CapNhatDuLieuRealtimeAsync();
            _realtimeTimer.Start();

            // Gọi nạp dữ liệu ngay lần đầu
            await CapNhatDuLieuRealtimeAsync();
        }

        // =========================================================================================
        // Task 3.2: Cơ chế đếm ngược đồng bộ giờ Server
        // =========================================================================================
        private async Task KhoiTaoDongBoServerAsync()
        {
            try
            {
                await Task.Run(() =>
                {
                    _thongTinBuoi = BuoiHocDAL.LayThongTinBuoiHoc(_maBuoiHoc);
                    DateTime gioServer = BuoiHocDAL.LayGioServer();
                    _serverTimeOffset = gioServer - DateTime.Now;

                    if (_thongTinBuoi != null)
                    {
                        _thoiGianBatDauServer = _thongTinBuoi.ThoiGianBatDau;
                        _thoiGianDemNguocPhut = _thongTinBuoi.ThoiGianDemNguoc > 0 ? _thongTinBuoi.ThoiGianDemNguoc : 10;
                    }
                    else
                    {
                        _thoiGianBatDauServer = gioServer;
                    }
                });

                if (_thongTinBuoi != null)
                {
                    lblSiSo.Text = $"Sĩ số lớp: {_thongTinBuoi.TongSiSo}";
                }
            }
            catch (Exception ex)
            {
                lblTrangThai.Text = "Lỗi đồng bộ giờ server: " + ex.Message;
            }
        }

        private async void CountdownTimer_Tick(object? sender, EventArgs e)
        {
            // Giờ hiện tại được ánh xạ theo giờ Server
            DateTime hienTaiTheoServer = DateTime.Now + _serverTimeOffset;
            DateTime thoiDiemHetHan = _thoiGianBatDauServer.AddMinutes(_thoiGianDemNguocPhut);
            TimeSpan conLai = thoiDiemHetHan - hienTaiTheoServer;

            if (conLai.TotalSeconds > 0)
            {
                _daHetGioDungGio = false;
                lblCountdown.Text = $"{Math.Floor(conLai.TotalMinutes):00}:{conLai.Seconds:00}";

                // Dưới 2 phút đổi sang màu đỏ cảnh báo
                if (conLai.TotalMinutes < 2)
                {
                    lblCountdown.ForeColor = Color.Red;
                    lblTrangThai.ForeColor = Color.Red;
                    lblTrangThai.Text = "Sắp hết thời gian điểm danh (< 2 phút)!";
                }
                else
                {
                    lblCountdown.ForeColor = Color.ForestGreen;
                    lblTrangThai.ForeColor = Color.DarkSlateGray;
                    lblTrangThai.Text = "Đang nhận điểm danh...";
                }
            }
            else
            {
                // Khi quá giờ điểm danh -> TỰ ĐỘNG ĐÓNG PHIÊN VÀ CHỐT DỮ LIỆU
                if (_dangXuLyHetGio || _daDongBuoi) return;
                _dangXuLyHetGio = true;
                _daHetGioDungGio = true;

                DungTimers();
                lblCountdown.Text = "00:00";
                lblCountdown.ForeColor = Color.Red;
                lblTrangThai.ForeColor = Color.Red;
                lblTrangThai.Text = "Đã hết thời gian điểm danh. Đang tự động kết thúc buổi học và chốt dữ liệu...";

                await TuDongDongBuoiHocKhiHetGioAsync();
            }
        }

        private async Task TuDongDongBuoiHocKhiHetGioAsync()
        {
            if (_daDongBuoi) return;

            try
            {
                KetQuaDongBuoiHoc? kq = null;
                await Task.Run(() =>
                {
                    kq = BuoiHocDAL.DongBuoiHoc(_maBuoiHoc);
                });

                _daDongBuoi = true;

                if (this.IsDisposed || !this.IsHandleCreated) return;

                if (kq != null)
                {
                    lblSiSo.Text = $"Sĩ số lớp: {kq.TongSiSo}";
                    lblCoMat.Text = $"Đã có mặt: {kq.SoCoMat}";
                    lblChuaDiemDanh.Text = $"Vắng: {kq.SoVang}";

                    string thongBao = "========================================\n" +
                                      "    HẾT GIỜ - TỰ ĐỘNG ĐÓNG PHIÊN\n" +
                                      "========================================\n\n" +
                                     $"Đã hết thời gian điểm danh ({_thoiGianDemNguocPhut} phút).\n" +
                                      "Hệ thống đã tự động chốt danh sách và đóng phiên điểm danh:\n\n" +
                                     $"- Lớp học phần: {_tenLop}\n" +
                                     $"- Buổi học: {_tenBuoi}\n\n" +
                                     $"• Tổng sĩ số lớp:      {kq.TongSiSo} sinh viên\n" +
                                     $"• Số SV có mặt:       {kq.SoCoMat} sinh viên\n" +
                                     $"• Số SV đi muộn:      {kq.SoDiMuon} sinh viên\n" +
                                     $"• Số SV vắng mặt:     {kq.SoVang} sinh viên\n\n" +
                                     $"Tổng số đã điểm danh: {kq.TongDaDiemDanh}/{kq.TongSiSo}\n" +
                                      "========================================";

                    MessageBox.Show(this, thongBao, "Hết giờ điểm danh", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.Close();
            }
            catch (Exception ex)
            {
                if (!this.IsDisposed)
                {
                    MessageBox.Show(this, "Lỗi khi tự động kết thúc buổi học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // =========================================================================================
        // Task 3.3: Cập nhật dữ liệu Realtime không làm đơ giao diện (async/await)
        // =========================================================================================
        private async Task CapNhatDuLieuRealtimeAsync()
        {
            if (_dangCapNhatRealtime || _daDongBuoi) return;
            _dangCapNhatRealtime = true;

            try
            {
                DataTable? dtList = null;
                (int daCoMat, int chuaDiemDanh, int tongSiSo) thongKe = (0, 0, 0);
                string maLop = _thongTinBuoi?.MaLopHP ?? "";

                await Task.Run(() =>
                {
                    dtList = BuoiHocDAL.LayDanhSachDiemDanhRealtime(_maBuoiHoc);
                    if (!string.IsNullOrEmpty(maLop))
                    {
                        thongKe = BuoiHocDAL.LayThongKeRealtime(_maBuoiHoc, maLop);
                    }
                });

                if (this.IsDisposed || !this.IsHandleCreated) return;

                // Cập nhật thẻ số liệu thống kê
                if (thongKe.tongSiSo > 0)
                {
                    lblSiSo.Text = $"Sĩ số lớp: {thongKe.tongSiSo}";
                    lblCoMat.Text = $"Đã có mặt: {thongKe.daCoMat}";
                    lblChuaDiemDanh.Text = $"Chưa điểm danh: {thongKe.chuaDiemDanh}";
                }
                else if (dtList != null)
                {
                    lblCoMat.Text = $"Đã có mặt: {dtList.Rows.Count}";
                }

                // Cập nhật DataGridView
                if (dtList != null)
                {
                    // Lưu lại dòng đang chọn để không bị nhảy selection
                    int selectedIndex = dgvRealtime.SelectedRows.Count > 0 ? dgvRealtime.SelectedRows[0].Index : -1;
                    dgvRealtime.DataSource = dtList;

                    if (dgvRealtime.Columns["STT"] != null) dgvRealtime.Columns["STT"]!.FillWeight = 40;
                    if (dgvRealtime.Columns["MaSV"] != null) dgvRealtime.Columns["MaSV"]!.HeaderText = "MSSV";
                    if (dgvRealtime.Columns["HoTen"] != null) dgvRealtime.Columns["HoTen"]!.HeaderText = "Họ và tên";
                    if (dgvRealtime.Columns["GioCheckIn"] != null) dgvRealtime.Columns["GioCheckIn"]!.HeaderText = "Giờ check-in";
                    if (dgvRealtime.Columns["TrangThai"] != null) dgvRealtime.Columns["TrangThai"]!.HeaderText = "Trạng thái";

                    if (selectedIndex >= 0 && selectedIndex < dgvRealtime.Rows.Count)
                    {
                        dgvRealtime.Rows[selectedIndex].Selected = true;
                    }

                    // Luôn cho phép nút Hủy phiên hoạt động (kể cả khi đã có sinh viên điểm danh)
                    if (dtList.Rows.Count > 0)
                    {
                        btnHuyPhien.Text = $"Hủy phiên ({dtList.Rows.Count} SV)";
                    }
                    else
                    {
                        btnHuyPhien.Text = "Hủy phiên này";
                    }
                    btnHuyPhien.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Lỗi cập nhật realtime: " + ex.Message);
            }
            finally
            {
                _dangCapNhatRealtime = false;
            }
        }

        // =========================================================================================
        // Nút Hủy phiên (Cho phép hủy phiên kể cả khi đã có sinh viên điểm danh)
        // =========================================================================================
        private void btnHuyPhien_Click(object? sender, EventArgs e)
        {
            if (_daDongBuoi) return;

            bool daCoSV = BuoiHocDAL.DaCoSinhVienDiemDanh(_maBuoiHoc);
            int soSV = daCoSV ? BuoiHocDAL.LaySoLuongSVDaDiemDanh(_maBuoiHoc) : 0;

            string thongDiepXacNhan;
            if (daCoSV)
            {
                thongDiepXacNhan = $"CẢNH BÁO: Buổi học này hiện đã có {soSV} sinh viên thực hiện điểm danh!\n\n" +
                                   "Nếu bạn hủy phiên, TOÀN BỘ dữ liệu điểm danh của buổi học này sẽ bị XÓA HOÀN TOÀN khỏi hệ thống.\n\n" +
                                   "Bạn có chắc chắn muốn hủy phiên điểm danh này không?";
            }
            else
            {
                thongDiepXacNhan = "Bạn có chắc chắn muốn hủy phiên điểm danh này không?\n" +
                                   "Bản ghi buổi học chưa có sinh viên điểm danh sẽ được xóa khỏi hệ thống.";
            }

            DialogResult confirm = MessageBox.Show(
                this,
                thongDiepXacNhan,
                "Xác nhận hủy phiên điểm danh",
                MessageBoxButtons.YesNo,
                daCoSV ? MessageBoxIcon.Warning : MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes) return;

            DungTimers();
            try
            {
                bool thanhCong = BuoiHocDAL.HuyPhienDiemDanh(_maBuoiHoc);
                if (thanhCong)
                {
                    _daDongBuoi = true;
                    MessageBox.Show(this, "Đã hủy phiên điểm danh và xóa dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show(this, "Không thể hủy phiên điểm danh (phiên có thể đã bị đóng hoặc xóa trước đó).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    _countdownTimer?.Start();
                    _realtimeTimer?.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Lỗi khi hủy phiên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _countdownTimer?.Start();
                _realtimeTimer?.Start();
            }
        }

        // =========================================================================================
        // MODULE 4: Nghiệp vụ Đóng & Tổng kết Buổi học
        // =========================================================================================
        private void btnDongBuoiHoc_Click(object? sender, EventArgs e)
        {
            // Task 4.1: Bật hộp thoại MessageBox yêu cầu giảng viên xác nhận
            DialogResult confirm = MessageBox.Show(
                $"Bạn có chắc chắn muốn kết thúc buổi điểm danh '{_tenLop} - {_tenBuoi}' không?\n\n" +
                "Hệ thống sẽ:\n" +
                "1. Chốt danh sách điểm danh hiện tại.\n" +
                "2. Tự động đánh 'Vắng' cho các sinh viên chưa điểm danh.\n" +
                "3. Đóng phiên điểm danh vĩnh viễn.",
                "Xác nhận kết thúc buổi học",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes) return;

            // Dừng ngay các Timer realtime và đồng hồ
            DungTimers();

            try
            {
                // Task 4.2: Gọi xử lý đóng và thông báo tổng kết qua Stored Procedure sp_DongBuoiHoc
                KetQuaDongBuoiHoc kq = BuoiHocDAL.DongBuoiHoc(_maBuoiHoc);
                _daDongBuoi = true;

                string thongBao = "========================================\n" +
                                  "    TỔNG KẾT KẾT THÚC BUỔI ĐIỂM DANH\n" +
                                  "========================================\n\n" +
                                 $"- Lớp học phần: {_tenLop}\n" +
                                 $"- Buổi học: {_tenBuoi}\n\n" +
                                 $"• Tổng sĩ số lớp:      {kq.TongSiSo} sinh viên\n" +
                                 $"• Số SV có mặt:       {kq.SoCoMat} sinh viên\n" +
                                 $"• Số SV đi muộn:      {kq.SoDiMuon} sinh viên\n" +
                                 $"• Số SV vắng mặt:     {kq.SoVang} sinh viên\n\n" +
                                 $"Tổng số đã điểm danh: {kq.TongDaDiemDanh}/{kq.TongSiSo}\n" +
                                  "========================================";

                MessageBox.Show(thongBao, "Tổng kết buổi điểm danh", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Đóng form trình chiếu
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi kết thúc buổi học: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _countdownTimer?.Start();
                _realtimeTimer?.Start();
            }
        }

        private void DungTimers()
        {
            _countdownTimer?.Stop();
            _realtimeTimer?.Stop();
        }

        private void FrmTrinhChieu_FormClosing(object? sender, FormClosingEventArgs e)
        {
            // Bắt sự kiện FormClosing khi phiên đang mở
            if (!_daDongBuoi)
            {
                DateTime hienTaiTheoServer = DateTime.Now + _serverTimeOffset;
                DateTime thoiDiemHetHan = _thoiGianBatDauServer.AddMinutes(_thoiGianDemNguocPhut);
                if (hienTaiTheoServer >= thoiDiemHetHan)
                {
                    // Nếu đã quá giờ điểm danh thì tự động chốt và đóng phiên
                    try
                    {
                        BuoiHocDAL.DongBuoiHoc(_maBuoiHoc);
                        _daDongBuoi = true;
                    }
                    catch { }
                }
                else
                {
                    DialogResult confirm = MessageBox.Show(
                        "Buổi điểm danh đang mở, bạn có chắc chắn muốn thoát màn hình trình chiếu?",
                        "Cảnh báo đóng màn hình",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (confirm != DialogResult.Yes)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }

            DungTimers();
        }
    }
}
