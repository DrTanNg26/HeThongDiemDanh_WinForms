using System.ComponentModel;
using System.Data;

namespace HeThongDiemDanh;

public partial class FrmStudentMain : Form
{
    private readonly CancellationTokenSource lifetime = new();
    private bool loading;
    private string maSV = string.Empty;

    public FrmStudentMain()
    {
        InitializeComponent();
    }

    private async void FrmStudentMain_Load(object? sender, EventArgs e)
    {
        if (LicenseManager.UsageMode == LicenseUsageMode.Designtime) return;
        if (!UserSession.DaDangNhap || !UserSession.LaSinhVien)
        {
            MessageBox.Show(this, "Vui lòng đăng nhập bằng tài khoản sinh viên.", "Thông báo");
            Close();
            return;
        }
        maSV = UserSession.TenDangNhap;
        lblHoTen.Text = "Xin chào, " + UserSession.HoTen;
        lblThongTin.Text = $"Mã sinh viên: {maSV}    •    Lớp: {UserSession.MaLopHanhChinh}    •    Khoa: {UserSession.MaKhoa}";
        lblEmail.Text = string.IsNullOrWhiteSpace(UserSession.Email) ? "Chưa cập nhật email" : UserSession.Email;
        UpdateClock();
        clockTimer.Start();
        await LoadLessonsAsync();
    }

    private void UpdateClock() => lblClock.Text = DateTime.Now.ToString("dd/MM/yyyy  HH:mm:ss");
    private void clockTimer_Tick(object? sender, EventArgs e) => UpdateClock();
    private async void btnRefresh_Click(object? sender, EventArgs e) => await LoadLessonsAsync();

    private async Task LoadLessonsAsync()
    {
        if (loading || lifetime.IsCancellationRequested || string.IsNullOrEmpty(maSV)) return;
        loading = true;
        btnRefresh.Enabled = false;
        lblStatus.ForeColor = Color.FromArgb(91, 108, 128);
        lblStatus.Text = "Đang tải danh sách buổi học…";
        try
        {
            var table = await SinhVienDAL.LayBuoiHocDangMoAsync(maSV, lifetime.Token);
            if (IsDisposed || lifetime.IsCancellationRequested) { table.Dispose(); return; }
            var previous = dgvBuoiHoc.DataSource as DataTable;
            dgvBuoiHoc.DataSource = table;
            previous?.Dispose();
            lblStatus.Text = table.Rows.Count == 0
                ? "Hiện tại không có môn học nào đang mở điểm danh. Bấm Làm mới để kiểm tra lại."
                : $"Có {table.Rows.Count} buổi học đang mở trong các lớp của bạn.";
        }
        catch (OperationCanceledException) when (lifetime.IsCancellationRequested) { }
        catch (Exception)
        {
            if (!IsDisposed && !lifetime.IsCancellationRequested)
            {
                var previous = dgvBuoiHoc.DataSource as DataTable;
                dgvBuoiHoc.DataSource = null;
                previous?.Dispose();
                lblStatus.ForeColor = Color.Firebrick;
                lblStatus.Text = "Không tải được buổi học. Vui lòng kiểm tra kết nối máy chủ và bấm Làm mới.";
            }
        }
        finally
        {
            loading = false;
            if (!IsDisposed && !lifetime.IsCancellationRequested) btnRefresh.Enabled = true;
        }
    }

    private void btnLogout_Click(object? sender, EventArgs e) => Close();

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        lifetime.Cancel();
        clockTimer.Stop();
        (dgvBuoiHoc.DataSource as DataTable)?.Dispose();
        // Form đăng nhập gọi lại Show() và xóa phiên trong finally của ShowDialog.
        base.OnFormClosed(e);
    }
}
