# Tiến độ & Lộ trình dự án — HeThongDiemDanh (chi tiết)

Tài liệu này mô tả chi tiết tiến độ hiện tại của dự án, các thay đổi đã thực hiện trong workspace, những file đã thêm/sửa, những giả định, vấn đề còn tồn tại và lộ trình các nhiệm vụ tiếp theo cùng các bước thực hiện cụ thể (không bỏ sót chi tiết nhỏ).

---

## 1) Tổng quan trạng thái hiện tại (tính đến lần cập nhật này)

- Loại dự án: WinForms (.NET 10)
- Repository: HeThongDiemDanh_WinForms (branch: main)
- IDE: Visual Studio Community 2026
- Build hiện tại: Thành công (không có lỗi biên dịch sau các thay đổi vừa thực hiện)

---

## 2) Các thay đổi chính đã thực hiện (chi tiết theo file)

Lưu ý: tất cả tên file là đường dẫn tương đối trong workspace.

- Form & Navigation
  - `HeThongDiemDanh/FrmAdmin.Designer.cs` — thêm form chủ Admin (header, lblAdminName, btnLogout, TabControl 4 tab: "Tài khoản", "Danh mục", "Lớp học phần", "Phân lớp").
  - `HeThongDiemDanh/FrmAdmin.cs` — code-behind FrmAdmin: khởi tạo lblAdminName từ `UserSession`, btnLogout handler (gọi `UserSession.Clear()`, mở lại `Form1` và đóng FrmAdmin), hàm Load để nạp dữ liệu Khoa ban đầu.
  - `HeThongDiemDanh/Form1.cs` — cập nhật luồng đăng nhập: khi login thành công và `UserSession.LaAdmin` = true, mở `FrmAdmin` và ẩn `Form1`.

- CSDL & Hỗ trợ truy cập
  - `HeThongDiemDanh/DatabaseConnection.cs` — đã có sẵn, dùng `ConfigurationManager.ConnectionStrings["DefaultConnection"]`, helper: `GetConnection()`, `ExecuteQuery`, `ExecuteNonQuery`, `ExecuteScalar`.
  - `HeThongDiemDanh/App.config` — chứa chuỗi kết nối `DefaultConnection` (Initial Catalog = QlyDiemDanh). Kiểm tra DB name nếu cần.

- Session
  - `HeThongDiemDanh/UserSession.cs` — đã có sẵn, dùng để lưu `TenDangNhap`, `HoTen`, `VaiTro`, flags `LaSinhVien/LaGiangVien/LaAdmin`, dùng `Clear()` khi logout.

- Module Danh mục (Tab "Danh mục")
  - Designer và code:
	- `FrmAdmin.Designer.cs` — thêm UI cho Danh mục: `dgvKhoa`, panel phải (`txtMaKhoa`, `txtTenKhoa`, `btnKhoaThem`, `btnKhoaLuu`, `btnKhoaXoa`), selector `cmbDanhMucSelector` để chọn Khoa / Lớp hành chính / Môn học.
	- `FrmAdmin.cs` — handlers: load Khoa, CRUD cho Khoa (btn events), chuyển UI khi đổi selector.
  - DAL:
	- `HeThongDiemDanh/KhoaDAL.cs` — `LayDanhSach()`, `Them()`, `Sua()`, `Xoa()`, `HasDependencies()` kiểm tra `LopHanhChinh`/`GiangVien`.
	- `HeThongDiemDanh/LopHanhChinhDAL.cs` — `LayDanhSach()` (JOIN Khoa để lấy TenKhoa), `Them()`, `Sua()`, `Xoa()`, `HasDependencies()` (kiểm tra `ChiTietLopHP`).
	- `HeThongDiemDanh/MonHocDAL.cs` — `LayDanhSach()`, `Them()`, `Sua()`, `Xoa()`, `HasDependencies()` (kiểm tra `LopHocPhan`).

- Module Tài khoản (Tab "Tài khoản") — phần nhìn tổng quan
  - UI (`FrmAdmin.Designer.cs`): `txtTkSearch`, `cmbRoleFilter`, `cmbStatusFilter`, `btnTkSearch`, `dgvTaiKhoan`, action buttons: `btnTaiKhoanAdd`, `btnTaiKhoanEdit`, `btnTaiKhoanLockUnlock`, `btnTaiKhoanResetPwd`.
  - DAL: `HeThongDiemDanh/TaiKhoanDAL.cs` — `LayDanhSach(keyword, vaiTro, trangThai)`, `ThayDoiTrangThai`, `DatLaiMatKhau`, `LayTheoTen`.
  - Service: `HeThongDiemDanh/TaiKhoanService.cs` — transaction-safe methods để thêm tài khoản cho SinhVien/GiangVien/Admin: `AddStudentAccount`, `AddTeacherAccount`, `AddAdminAccount`, `AccountExists`.
  - Dialog tạo/sửa tài khoản: `FrmThemSuaTaiKhoan.Designer.cs` & `FrmThemSuaTaiKhoan.cs` — UI cho các vai trò khác nhau, load combo Khoa/Lop, validate, gọi `TaiKhoanService` để tạo tài khoản (mật khẩu mặc định "123456" đã băm bằng SHA256). Edit mode scaffolded (thêm) — cập nhật trong bước tiếp theo.

- Hỗ trợ bảo mật mật khẩu
  - `HeThongDiemDanh/PasswordHelper.cs` — SHA256 hashing và kiểm tra hash. Login ban đầu cần chuyển sang dùng `PasswordHelper.KiemTra` (hiện đã thêm/đề xuất).

---

## 3) Build & Kiểm thử

- Build: đã chạy (sau mỗi thay đổi lớn) — kết quả hiện tại: Build successful.
- Kiểm thử thủ công nên thực hiện:
  1. Đảm bảo App.config chuỗi kết nối trỏ đến DB chứa các bảng: Khoa, LopHanhChinh, MonHoc, TaiKhoan, SinhVien, GiangVien, Admin, LopHocPhan, ChiTietLopHP.
  2. Mở ứng dụng, đăng nhập bằng tài khoản Admin — FrmAdmin hiển thị, header hiện tên Admin.
  3. Tab Danh mục:
	 - Khoa: thử Thêm / Sửa / Xóa (xóa sẽ bị chặn nếu có ràng buộc dữ liệu).
	 - Chuyển sang Lớp hành chính: kiểm tra Load Combo Khoa, Thêm Lớp, Sửa, Xóa (khóa nếu có phụ thuộc).
	 - Môn học: thêm/sửa/xóa (kiểm tra ràng buộc SoTinChi 1-10).
  4. Tab Tài khoản: Search, chọn 1 dòng sẽ bật các nút Sửa/Khóa/Reset; thử Khóa/Mở khóa (không cho phép khóa chính mình), thử Reset mật khẩu — kiểm tra DB cập nhật MatKhau là hash của "123456".

---

## 4) Giả định và điều kiện cần thỏa

- Tên bảng và cột trong DB phải khớp với các truy vấn đã viết:
  - Khoa(MaKhoa, TenKhoa)
  - LopHanhChinh(MaLop, TenLop, MaKhoa)
  - MonHoc(MaMon, TenMon, SoTinChi)
  - TaiKhoan(TenDangNhap, MatKhau, VaiTro, TrangThai)
  - SinhVien(MaSV, HoTen, Email, NgaySinh, SoCCCD, MaLop, MaKhoa)
  - GiangVien(MaGV, HoTen, Email, HocHamHocVi, MaKhoa)
  - Admin(MaAD, HoTen, Email)
  - LopHocPhan (nếu dùng để kiểm dependency MonHoc)
  - ChiTietLopHP (nếu dùng để kiểm dependency Lớp hành chính)

- Nếu DB dùng tên khác (ví dụ: `Lop` thay vì `LopHanhChinh`) cần thông báo để tôi sửa DAL tương ứng.

---

## 5) Vấn đề đã biết / Rủi ro

- Chưa có: chức năng Sửa tài khoản (Form hiện được mở ở chế độ edit nhưng chưa load dữ liệu chi tiết và cập nhật transactional). Cần implement.
- Nếu CSDL bị khác tên bảng/khóa ngoại/kiểu dữ liệu, sẽ có lỗi SQL runtime (invalid object / column). Cần cung cấp schema nếu khác.
- Hiện tại login so sánh mật khẩu đã băm chưa được áp dụng (hoàn thiện nếu cần) — hãy chắc chắn `TaiKhoan.MatKhau` lưu hash SHA256 chứ không lưu plaintext.
- Giao diện designer tạo thủ công nhiều control; nếu dùng Designer của Visual Studio phải đồng bộ tên control (tránh trùng tên). Tôi đã sửa trùng khai báo khi phát sinh.

---

## 6) Nhiệm vụ tiếp theo (chi tiết, có thứ tự, từng bước hành động)

Ưu tiên cao (nên làm ngay):

1) [ĐÃ HOÀN THÀNH] Hoàn thiện chức năng Sửa tài khoản (FrmThemSuaTaiKhoan - edit)
   - Bước 1: Thêm phương thức `TaiKhoanDAL.LayChiTietSinhVien`, `LayChiTietGiangVien`, `LayChiTietAdmin` để load thông tin cũ.
   - Bước 2: Thêm `TaiKhoanService.UpdateStudentAccount`, `UpdateTeacherAccount`, `UpdateAdminAccount` (transactional) để cập nhật thông tin trong SQL.
   - Bước 3: Cập nhật `FrmThemSuaTaiKhoan` nạp dữ liệu cũ, đổi tiêu đề "Sửa tài khoản", khóa ô Mã và gọi phương thức Update khi Lưu.
   - Bước 4: Cập nhật `FrmAdmin.cs` truyền `TenDangNhap` vào `FrmThemSuaTaiKhoan` khi nhấn nút Edit.
   - Bước 5: Biên dịch dự án thành công (0 Errors).

2) [ĐÃ HOÀN THÀNH] Cập nhật Luồng Đăng nhập để dùng mật khẩu băm thực sự
   - Bước 1: Trong `Form1.cs`, cập nhật logic kiểm tra mật khẩu sử dụng `PasswordHelper.KiemTra(plainPwd, storedHash)`.
   - Bước 2: Hỗ trợ linh hoạt so sánh mật khẩu SHA256 lẫn fallback plaintext để không làm hỏng dữ liệu test ban đầu.
   - Bước 3: Biên dịch dự án thành công (0 Errors).

3) [ĐÃ HOÀN THÀNH] Hoàn thiện Tab "Lớp học phần" (Stage 3)
   - Bước 1: Tạo `LopHocPhanDAL.cs` thực hiện truy vấn JOIN lấy thông tin Tên môn, Tên giảng viên và xử lý Thêm / Sửa / Xóa / HasDependencies.
   - Bước 2: Thiết kế giao diện trong Tab `tabLopHP` (`FrmAdmin.Designer.cs`) gồm DataGridView hiển thị danh sách Lớp HP và Panel nhập thông tin (Mã, Tên, Môn học, Giảng viên, Phòng học, Năm học).
   - Bước 3: Bổ sung code-behind trong `FrmAdmin.cs` xử lý load danh sách, nạp dữ liệu vào ComboBox Môn học & Giảng viên, validate thông tin và thực hiện CRUD.
   - Bước 4: Biên dịch dự án thành công (0 Errors).

4) [ĐÃ HOÀN THÀNH] Hoàn thiện Tab "Phân lớp" (Stage 4)
   - Bước 1: Tạo `ChiTietLopHPDAL.cs` quản lý gán / gỡ sinh viên vào Lớp học phần (`LaySinhVienTrongLop`, `LaySinhVienNgoaiLop`, `ThemSinhVienVaoLop`, `XoaSinhVienKhoiLop`, `DaCoDiemDanh`).
   - Bước 2: Thiết kế giao diện trong Tab `tabPhanLop` (`FrmAdmin.Designer.cs`) dạng 2 cột: Cột bên trái hiển thị danh sách SV đã thuộc Lớp HP, Cột bên phải hiển thị danh sách SV chưa gán kèm ô tìm kiếm.
   - Bước 3: Bổ sung logic xử lý trong `FrmAdmin.cs`: tự động đồng bộ khi đổi Lớp HP trong ComboBox, nút `>> Thêm vào lớp` gán SV được chọn sang bảng trái, nút `Loại khỏi lớp <<` loại SV khỏi lớp (kiểm tra an toàn nếu SV chưa có dữ liệu điểm danh).
   - Bước 4: Biên dịch dự án thành công (0 Errors).

5) [ĐÃ HOÀN THÀNH] Tính năng Đăng ký tài khoản (Trạng thái 'Chờ' & Duyệt tài khoản)
   - Cập nhật CSDL: Thêm giá trị `N'Chờ'` vào ràng buộc `CK_TK_TrangThai` của bảng `TaiKhoan`.
   - Tạo Form Đăng ký ([FrmDangKy.cs](file:///D:/Appdata/GitHub/HeThongDiemDanh/HeThongDiemDanh_WinForms/HeThongDiemDanh/FrmDangKy.cs)): Người dùng (Sinh viên / Giảng viên) có thể chọn vai trò, nhập Họ tên, Email, Ngày sinh, CCCD, Lớp HC, Khoa và Mật khẩu tự chọn ➔ Tài khoản được lưu ở trạng thái **'Chờ'**.
   - Kiểm tra khi Đăng nhập ([Form1.cs](file:///D:/Appdata/GitHub/HeThongDiemDanh/HeThongDiemDanh_WinForms/HeThongDiemDanh/Form1.cs)): Tài khoản ở trạng thái `'Chờ'` sẽ bị từ chối đăng nhập và thông báo người dùng quay lại sau khi Admin duyệt.
   - Duyệt tài khoản ở Admin ([FrmAdmin.cs](file:///D:/Appdata/GitHub/HeThongDiemDanh/HeThongDiemDanh_WinForms/HeThongDiemDanh/FrmAdmin.cs)): Thêm bộ lọc trạng thái `'Chờ'` trên tab Tài khoản. Khi Admin click vào tài khoản Chờ ➔ bấm **Sửa** xem xét/chỉnh sửa ➔ bấm **Cập nhật**, tài khoản sẽ tự động được duyệt và đổi sang trạng thái **'Hoạt động'**. Khi đó người dùng mới có thể đăng nhập được.

Medium / Nice-to-have

- Tách DAL ra thư mục / namespace `Dal` và service `Services` — tăng tính tổ chức.
- Thêm logging server-side (ghi vào file/DB khi có lỗi SQL) để dễ debug runtime.
- Thêm unit tests cho các lớp DAL (cần DB test hoặc mocking).
- UI polish: responsive layout, dùng TableLayout/Anchors đúng cách, thêm icon và tooltip.

---

## 7) Kiểm tra và kịch bản test chi tiết (test cases)

- Test case 1: Login Admin thành công
  - Input: tài khoản admin tồn tại (TenDangNhap, MatKhau đã băm bằng SHA256)
  - Kỳ vọng: Form1 mở FrmAdmin, lblAdminName hiển thị, không có exception.

- Test case 2: Thêm Khoa mới
  - Input: MaKhoa=K001, TenKhoa="Khoa CNTT"
  - Kỳ vọng: KhoaDAL.Them trả về 1, dgvKhoa cập nhật, không có trùng MaKhoa.

- Test case 3: Xóa Khoa có phụ thuộc
  - Input: chọn Khoa đã liên kết với LopHanhChinh/GiangVien
  - Kỳ vọng: KhoaDAL.HasDependencies trả true và UI báo không thể xóa.

- Test case 4: Thêm Lớp hành chính (cần Khoa tồn tại)
  - Kỳ vọng: cboKhoaForLop được load và Lớp được thêm, dgvLopHC refresh.

- Test case 5: Thêm Môn học với SoTinChi invalid
  - Input: SoTinChi = "abc" hoặc 0 hoặc 11
  - Kỳ vọng: UI báo lỗi valid và không lưu.

- Test case 6: Tạo tài khoản Sinh viên (Add)
  - Input: mã SV mới, thông tin hợp lệ
  - Kỳ vọng: transaction commit, có bản ghi trong TaiKhoan và SinhVien; MatKhau trong TaiKhoan = SHA256("123456").

---

## 8) Các file mới / đã sửa (tóm tắt)

- Mới thêm:
  - FrmAdmin.Designer.cs, FrmAdmin.cs
  - KhoaDAL.cs, LopHanhChinhDAL.cs, MonHocDAL.cs
  - TaiKhoanDAL.cs, TaiKhoanService.cs
  - MonHocDAL.cs
  - FrmThemSuaTaiKhoan.Designer.cs, FrmThemSuaTaiKhoan.cs
  - TienDo.md (tệp này)

- Sửa:
  - Form1.cs (login flow)

---

## 9) Ghi chú kỹ thuật & khuyến nghị

- Bảo mật: luôn lưu hash mật khẩu (PasswordHelper.BamSHA256). Không bao giờ lưu mật khẩu plaintext trong DB hoặc log.
- Transaction: mọi thao tác tác động 2 bảng trở lên (ví dụ tạo tài khoản và thêm chi tiết SinhVien/GiangVien) phải dùng SqlTransaction — đã tuân theo trong TaiKhoanService.
- Kiểm tra quyền DB: user trong connection string phải có quyền INSERT/UPDATE/DELETE/SELECT theo yêu cầu.
- Schema drift: nếu bạn restore DB từ file SQL (QlySinhVien vs QlyDiemDanh) hãy xác nhận `Initial Catalog` trong `App.config` khớp với DB đã restore.

---

Nếu bạn muốn tôi tiếp tục thực hiện nhiệm vụ tiếp theo ngay bây giờ, hãy chọn một trong các mục trong phần "Nhiệm vụ tiếp theo" (ví dụ: "Hoàn thiện Edit tài khoản" hoặc "Cập nhật đăng nhập dùng hash"), tôi sẽ tiếp tục và cập nhật TienDo.md khi hoàn tất từng bước.
