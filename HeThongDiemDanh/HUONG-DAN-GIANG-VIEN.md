# Giảng viên: Designer, phiên điểm danh và trình chiếu

## Designer

- `FrmGiangVienMain.Designer.cs` chứa sẵn `UcMoPhien` trong tab mở phiên. Mở **View Designer** của `UcMoPhien.cs` để chỉnh trực tiếp các nút, lớp và thời gian.
- `FrmTrinhChieu.cs` có constructor không tham số và giao diện riêng trong `FrmTrinhChieu.Designer.cs`; không còn dựng giao diện bằng hàm chạy riêng.
- Trong Designer không truy vấn SQL, không yêu cầu đăng nhập và không chạy bộ đếm.
- Sau khi build, đóng tab Designer cũ và mở lại nếu Visual Studio còn giữ giao diện trước đó.

## Sử dụng

1. Chọn lớp, buổi và thời gian 5/10/15 phút, rồi bấm **Bắt đầu điểm danh**.
2. Màn hình trình chiếu mở với mã OTP. **Tắt trình chiếu** hoặc dấu X chỉ đóng cửa sổ này, phiên vẫn tiếp tục.
3. Bấm **Bật trình chiếu** để mở lại đúng buổi, đúng OTP và đúng thời gian còn lại. Không tạo phiên mới.
4. Có thể bấm **Kết thúc phiên** trên form giảng viên mà không cần mở trình chiếu. Thao tác này giữ kết quả đã có và chốt Vắng cho sinh viên chưa điểm danh.
5. Khi hết số phút đã chọn, hệ thống đóng phiên và chốt Vắng. Không nhận thêm điểm danh muộn sau mốc này.

Bộ đếm trên `UcMoPhien` chạy độc lập với trình chiếu, kiểm tra mỗi giây, kể cả khi thu nhỏ form chính. Thời hạn do SQL Server quyết định; đồng hồ máy cá nhân không quyết định quyền điểm danh. Các lệnh tự đóng và đóng thủ công có thể gọi lặp mà không xóa/ghi đè kết quả đã có.

Nếu mất kết nối, hệ thống báo trạng thái và tự thử lại, không mở hộp thoại lỗi liên tục hoặc chặn đóng cửa sổ. SQL vẫn từ chối mọi lượt điểm danh sau hạn. Nếu toàn bộ ứng dụng bị thoát/tắt máy, không có timer chạy: phiên hết hạn được chốt vào lần mở lại ứng dụng giảng viên. Muốn trạng thái SQL tự chuyển khi tất cả ứng dụng đều tắt cần một dịch vụ chạy nền trên máy chủ, chưa triển khai trong bản WinForms này.

## Cơ sở dữ liệu

Chạy hai script trên các máy thành viên khác:

1. `SQL script/sp_DongBuoiHoc.sql`: đóng phiên có khóa giao dịch, chốt Vắng, hỗ trợ gọi lặp và tham số `@ChiKhiHetHan`.
2. `SQL script/04_GhiNhanDiemDanh.sql`: chặn điểm danh ngay tại hạn số phút đã chọn, kể cả nếu cửa sổ giảng viên đang tắt.

Cả hai đã được cập nhật trên cơ sở dữ liệu của máy đang làm việc. Phiên 29 (Buổi 1, Lập trình trực quan - Nhóm 01) đã được đóng theo yêu cầu: giữ 1 Có mặt và chốt 4 Vắng.

## Kiểm thử

Đã kiểm tra Designer không gọi SQL, bật/tắt/mở lại trình chiếu, đóng cửa sổ trong lúc mất kết nối, đóng đúng hạn 5/10/15 phút, chốt vắng, giữ kết quả cũ, gọi đóng lặp và từ chối điểm danh quá giờ. Các bản ghi kiểm thử được hoàn tác sau khi thử. Công cụ kiểm thử DAL dùng cấu hình kết nối tạm với Encrypt=Optional do giới hạn môi trường; không sửa chuỗi kết nối ứng dụng của người dùng.
