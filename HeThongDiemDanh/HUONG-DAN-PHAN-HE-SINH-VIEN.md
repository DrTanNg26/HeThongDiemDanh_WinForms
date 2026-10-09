# Phân hệ sinh viên — xác thực và điểm danh

## Thử ngay khi chưa có form giảng viên

1. Chạy dự án trong Visual Studio, đăng nhập bằng tài khoản sinh viên đã được duyệt.
2. Trong tab **Điểm danh buổi học**, chọn **Dùng buổi học mẫu để xem thử (không lưu SQL)**.
3. Chọn một trong hai dòng rồi bấm **Vào điểm danh**.
4. Trả lời theo câu hỏi hiện trên màn hình: ngày sinh **15/05/2005** hoặc 4 số cuối CCCD **1234**.
5. Nhập OTP **UTT234**, bấm **Xác nhận điểm danh**.

Buổi **Mẫu • Đúng giờ** trả về **Có mặt**; buổi **Mẫu • Đi muộn** trả về **Đi muộn**. Màn hình cập nhật kết quả sau khi đóng popup. Làm mới vẫn giữ kết quả mẫu; đăng xuất sẽ xóa dữ liệu mẫu. Toàn bộ nhánh xem thử chỉ dùng bộ nhớ, không đọc/ghi SQL.

Sai thông tin xác thực 3 lần sẽ khóa buổi tương ứng trong phiên đăng nhập hiện tại. Đóng rồi mở popup hoặc làm mới không đặt lại số lần sai. Lỗi kết nối không tính là trả lời sai. Bộ đếm này nằm trong ứng dụng theo phạm vi đề bài, không phải khóa tài khoản trên máy chủ.

## Các file cần đọc

| File | Công việc |
| --- | --- |
| `FrmStudentMain.cs` | Hiện danh sách, chọn buổi, mở hai popup, giữ số lần sai và kết quả mẫu. |
| `TrangThaiXacThuc.cs` | Chọn câu hỏi và đếm số lần trả lời sai theo từng buổi. |
| `FrmXacThuc.cs` | Nhận ngày sinh hoặc 4 số cuối CCCD; đúng thì chuyển sang nhập OTP. |
| `FrmNhapOTP.cs` | Nhận OTP, chặn bấm nhiều lần, hiện kết quả và tự đóng sau 3 giây. |
| `SinhVienDAL.cs` | Lấy buổi học đang mở của các lớp mà sinh viên tham gia. |
| `DiemDanhDAL.cs` | Đọc hồ sơ xác thực, lấy mạng, gọi thủ tục ghi nhận điểm danh. |
| `NetworkHelper.cs` | Liệt kê IPv4 của Wi-Fi/Ethernet và so sánh subnet. |
| `../SQL script/04_GhiNhanDiemDanh.sql` | Tạo hàm IPv4 và thủ tục kiểm tra, ghi nhận kết quả trên SQL Server. |

Code xử lý dùng các hàm ngắn, kiểu dữ liệu viết rõ, `if/else` và chú thích tiếng Việt. Truy vấn dùng tham số SQL; các thao tác chờ cơ sở dữ liệu dùng `async/await` để cửa sổ vẫn phản hồi. Hai popup có file `.Designer.cs` riêng để mở bằng Visual Studio Designer. Khu vực điểm danh hiện nằm trong `FrmStudentMain`, chưa tách thành `UcDiemDanh`.

## Ghép với phần giảng viên

Chạy `04_GhiNhanDiemDanh.sql` trên cơ sở dữ liệu đã có các bảng của dự án. Script có thể chạy lại; chỉ tạo/cập nhật hàm và thủ tục, không xóa bảng hay dữ liệu. Không cần chạy lại toàn bộ script tạo cơ sở dữ liệu.

Khi mở buổi, phần giảng viên cần ghi đầy đủ vào `BuoiHoc`:

- `MaLopHP` của lớp học phần; sinh viên phải có trong `ChiTietLopHP`.
- `MaOTP` gồm **5–6 chữ in hoa và số**, theo đề bài tránh `0/O`, `1/I`; kiểm tra không trùng phiên đang mở khác trước khi lưu.
- `IP_GV`, `SubnetMask_GV` lấy từ đúng card mạng phòng học. Có thể gọi `NetworkHelper.LayDanhSachMang()` để chọn card.
- `ThoiGianBatDau` lấy từ giờ SQL Server, `ThoiGianDemNguoc` là số phút đúng giờ, `TrangThai = N'Mo'`.

Sinh viên bỏ chọn **Dùng buổi học mẫu** rồi bấm **Làm mới**. Phần sinh viên đã nối sẵn với các bảng này; không cần gọi trực tiếp form giảng viên.

Thủ tục `sp_GhiNhanDiemDanh` kiểm tra lại tài khoản hoạt động, tư cách tham gia lớp, phiên còn mở/chưa hết hạn, thông tin xác thực, OTP, mạng và kết quả trùng. Thời điểm điểm danh do SQL Server xác định: đến hết hạn đếm ngược là **Có mặt**, sau đó là **Đi muộn**. Phiên quá giới hạn cấu hình (mặc định 240 phút) bị từ chối. Bản ghi đã tồn tại không bị ghi đè.

## Phân biệt hai chế độ thử

| Chế độ | Nguồn buổi học | Ghi kết quả vào SQL? |
| --- | --- | --- |
| Checkbox **Dùng buổi học mẫu** | Hai dòng tạo trong bộ nhớ | **Không** |
| `TestMode=true` trong App.config, checkbox không chọn | Buổi thật trong SQL | **Có**; chỉ bỏ kiểm tra cùng mạng |
| `TestMode=false`, checkbox không chọn | Buổi thật trong SQL | **Có**, kiểm tra cả cùng mạng |

Không dùng `TestMode=true` khi điểm danh chính thức. Cấu hình kết nối hiện có được giữ nguyên.

Kiểm tra cùng subnet dùng IPv4 và subnet mask thực tế, không giả định luôn là `/24`. Cơ chế này kiểm tra thông tin mạng theo thiết kế bài tập; nó không tự chứng minh vị trí vật lý của thiết bị.

Tab **Lịch sử điểm danh** vẫn là phần chờ ghép thống kê/lịch sử của thành viên phụ trách. Phần sinh viên không tự ghi **Vắng** cho người khác; thao tác kết thúc buổi của giảng viên sẽ xử lý việc đó.

## Kiểm tra đã thực hiện (09/10/2026)

- Dự án biên dịch thành công; vẫn còn các cảnh báo có sẵn ở phần khác của dự án.
- 25 kiểm tra giao diện: hai buổi mẫu, đúng/sai OTP, giữ kết quả khi làm mới, ba lần xác thực sai, đóng/mở lại popup và lỗi kết nối.
- 8 kiểm tra đăng nhập/đăng xuất và quay lại form đăng nhập.
- 14 kiểm tra so sánh mạng; 22 tình huống SQL; 6 kiểm tra gọi trực tiếp `DiemDanhDAL` đến thủ tục SQL.
- Dữ liệu SQL dùng để kiểm tra đều được tạo trong giao dịch và hoàn tác; không còn bản ghi thử trong cơ sở dữ liệu.

Giới hạn kiểm thử: `Microsoft.Data.SqlClient` trong môi trường kiểm thử gặp lỗi thương lượng mã hóa với cấu hình kết nối hiện tại. Các kiểm tra DAL dùng riêng `Encrypt=Optional` và `TestMode=true` trong cấu hình tạm của công cụ kiểm thử; cấu hình tạm đã được xóa. `App.config` của dự án không bị thay đổi. Vì vậy cần xác nhận luồng điểm danh thật khi chạy ứng dụng bằng Visual Studio với cấu hình hiện có; các kiểm tra trên chưa xác nhận kết nối mã hóa và mạng lớp học thật hoạt động cùng nhau.
