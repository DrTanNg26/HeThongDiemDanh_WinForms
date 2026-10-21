# Phân hệ sinh viên — cập nhật 20/10/2026

## Điểm danh

1. Giảng viên kết nối Wi-Fi/LAN phòng học, đăng nhập và mở buổi điểm danh.
2. Sinh viên kết nối cùng mạng, đăng nhập tài khoản đã được duyệt và bấm **Làm mới**.
3. Chọn buổi học, bấm **Vào điểm danh**, trả lời ngày sinh hoặc 4 số cuối CCCD đã đăng ký.
4. Nhập OTP giảng viên cung cấp. Có thể bấm **Kiểm tra mạng** để xem kết nối trước khi gửi.
5. Bấm **Xác nhận điểm danh**. Ứng dụng kiểm tra lại mạng ngay lúc gửi; SQL kiểm tra lại trước khi lưu.

Trong thời gian đã chọn ghi **Có mặt**. Theo yêu cầu mới, khi hết thời gian phiên sẽ đóng và không nhận điểm danh muộn; lịch sử vẫn giữ các kết quả **Đi muộn** đã có trước đây. Thời gian ghi nhận lấy từ SQL Server. Không ghi đè kết quả đã tồn tại. Sai xác thực 3 lần khóa buổi đó trong phiên đăng nhập hiện tại; đóng popup hoặc làm mới không đặt lại số lần sai.

Đã xóa checkbox buổi mẫu, dữ liệu mẫu trong bộ nhớ, câu trả lời/OTP mẫu và cấu hình `TestMode`. Giảng viên cũng không còn dùng địa chỉ `127.0.0.1` thay cho mạng thật. Buổi cũ đã mở bằng địa chỉ loopback cần được giảng viên xử lý và mở lại bằng IP hợp lệ.

## Kiểm tra cùng mạng

- `NetworkHelper.LayDanhSachMang()` lấy IPv4 và subnet mask của Wi-Fi/Ethernet đang hoạt động; bỏ loopback, link-local và các card ảo/VPN nhận diện được theo tên/loại.
- `DiemDanhDAL.KiemTraMangAsync()` đọc IP/mask giảng viên đã lưu trong buổi học rồi tìm card mạng sinh viên phù hợp.
- So sánh hai địa chỉ bằng phép AND với subnet mask. Phía C# kiểm tra theo cả mask của giảng viên và sinh viên, không giả định luôn là `/24`.
- Thiếu mask, IP không hợp lệ, không có kết nối hoặc khác subnet đều bị từ chối. Nút kiểm tra không ghi dữ liệu.
- Mỗi lần xác nhận điểm danh đều đọc lại IP/mask và card mạng hiện tại, không dùng kết quả cũ của nút kiểm tra.
- `sp_GhiNhanDiemDanh` luôn kiểm tra lại subnet theo mask giảng viên. Tham số cũ `@BoQuaKiemTraIP` được giữ để tương thích lời gọi nhưng chỉ chấp nhận `0`; truyền `1` sẽ báo lỗi.

“Cùng mạng” ở đây nghĩa là cùng subnet IPv4. Hai thiết bị cùng tên Wi-Fi vẫn có thể ở các subnet khác nhau. Cơ chế này không tự chứng minh vị trí vật lý của người dùng; ứng dụng hiện kết nối SQL trực tiếp và gửi IP máy sinh viên.

## Xem lịch sử điểm danh

- Mở tab **Lịch sử điểm danh**: tự tải kết quả của tài khoản đang đăng nhập.
- Lọc theo **Lớp học phần** và **Kết quả**, hoặc chọn tất cả.
- Bảng hiển thị lớp, môn, buổi, ngày học, thời điểm điểm danh, kết quả và ghi chú; sắp xếp buổi mới nhất trước.
- Dòng dưới bảng đếm Có mặt/Đi muộn/Vắng/Nghỉ có phép theo bộ lọc đang chọn.
- Bấm **Làm mới** để đọc kết quả mới nhất và giữ lại bộ lọc nếu lớp vẫn còn trong danh sách.
- Buổi Vắng/Nghỉ có phép không có thời điểm quét sẽ hiện `—`.
- Chỉ hiển thị kết quả có thật trong bảng `DiemDanh`, kể cả sau khi sinh viên được chuyển khỏi lớp. Không tự suy ra Vắng cho buổi chưa được ghi/chốt kết quả.
- Danh sách chỉ đọc. Sinh viên không sửa kết quả hoặc xem lịch sử tài khoản khác qua giao diện.

## Các file chính

| File | Công việc |
| --- | --- |
| `FrmStudentMain.cs` / `.Designer.cs` | Danh sách buổi mở, xác thực, lịch sử, bộ lọc và thống kê số buổi. |
| `SinhVienDAL.cs` | Truy vấn buổi mở và lịch sử theo mã sinh viên bằng tham số SQL. |
| `FrmXacThuc.cs`, `TrangThaiXacThuc.cs` | Kiểm tra hồ sơ thật và giữ số lần sai theo buổi. |
| `FrmNhapOTP.cs` / `.Designer.cs` | Kiểm tra mạng, nhập OTP, ghi nhận kết quả. |
| `DiemDanhDAL.cs`, `NetworkHelper.cs` | Kiểm tra mạng bắt buộc và gọi thủ tục SQL. |
| `UcMoPhien.cs` | Phần giảng viên: yêu cầu mạng thật khi mở buổi. |
| `../SQL script/04_GhiNhanDiemDanh.sql` | Thủ tục ghi nhận bắt buộc kiểm tra mạng. |

## Cập nhật trên máy thành viên khác

Build lại dự án bằng Visual Studio và chạy riêng script `04_GhiNhanDiemDanh.sql` trên cơ sở dữ liệu của máy đó. Script chỉ tạo/cập nhật hàm IPv4 và thủ tục ghi nhận, không xóa bảng hoặc kết quả đã có. Trên máy đang làm việc, thủ tục đã được cập nhật.

Chuỗi kết nối hiện có được giữ nguyên; chỉ khóa `TestMode` đã bị xóa khỏi App.config. Kết quả thử cũ đã lưu trong SQL không bị xóa tự động.

Kiểm thử cơ sở dữ liệu dùng các bản ghi tạm trong giao dịch rồi hoàn tác. Các kiểm tra DAL trong môi trường công cụ dùng riêng `Encrypt=Optional` do giới hạn thương lượng mã hóa đã gặp trước đó; cấu hình kết nối của dự án không bị đổi. Cần chạy thêm trên hai máy của lớp học để xác nhận mạng thực tế và cấu hình SQL của từng máy.
