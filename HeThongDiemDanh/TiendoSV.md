# TIẾN ĐỘ THỰC HIỆN PHÂN HỆ SINH VIÊN

**Vai trò:** Thành viên 3 – phụ trách phân hệ sinh viên.  
**Dự án:** Hệ thống quản lý điểm danh sinh viên – C# WinForms, Visual Studio, SQL Server.  
**Cập nhật:** 21/10/2026.  
**Phạm vi:** Tổng hợp quá trình làm việc đến bản hiện tại, đối chiếu với mã nguồn trong dự án chung. Các giai đoạn được ghi theo trình tự công việc, không gán ngày hoàn thành khi chưa có ghi nhận cụ thể.

## 1. Tổng quan hiện tại

Luồng chính đã được triển khai:

```text
Đăng nhập bằng tài khoản sinh viên
    → Mở màn hình sinh viên
    → Xem các buổi đang mở trong lớp mình
    → Chọn buổi và bấm Vào điểm danh
    → Xác thực ngày sinh hoặc 4 số cuối CCCD
    → Nhập OTP và kiểm tra cùng mạng với giảng viên
    → SQL kiểm tra điều kiện và lưu kết quả
    → Làm mới danh sách / xem lịch sử điểm danh
    → Đăng xuất về màn hình đăng nhập
```

| Hạng mục | Tiến độ hiện tại |
| --- | --- |
| Nối đăng nhập với màn hình sinh viên | Đã triển khai trong dự án chung |
| Giao diện và thông tin sinh viên | Đã triển khai |
| Danh sách buổi đang mở theo lớp của sinh viên | Đọc dữ liệu thật từ SQL |
| Xác thực thông tin cá nhân | Đã triển khai, giới hạn 3 lần sai theo buổi trong phiên đăng nhập |
| Nhập OTP và ghi nhận điểm danh | Đã triển khai, có kiểm tra lại ở SQL |
| Kiểm tra cùng mạng | Bắt buộc, so sánh subnet IPv4 |
| Lịch sử, lọc kết quả và đếm số buổi | Đã triển khai |
| Bỏ chế độ demo | Đã bỏ khỏi luồng sử dụng hiện tại |
| Đồng bộ thời hạn với phần giảng viên | Đã chặn điểm danh sau thời gian đã chọn |
| Kiểm thử trên hai máy thật của lớp học | Cần thực hiện thêm |

## 2. Quá trình thực hiện từ đầu đến hiện tại

### Giai đoạn 1 – Xác định phần việc và chuẩn bị giao diện

- Tiếp nhận yêu cầu đảm nhiệm phân hệ sinh viên, làm bằng C# WinForms trong Visual Studio.
- Bắt đầu từ yêu cầu form đăng nhập cơ bản, sau đó chuyển sang tích hợp với dự án chung do các thành viên cùng cập nhật.
- Xác định các phần sinh viên cần sử dụng: thông tin cá nhân, danh sách buổi mở, xác thực, OTP và lịch sử điểm danh.
- Giai đoạn đầu có dữ liệu demo để xem và thử luồng; chế độ này đã được loại bỏ ở giai đoạn sau.

**Kết quả:** Có hướng triển khai màn hình sinh viên và luồng thao tác ban đầu. Form đăng nhập/đăng ký là phần dùng chung; tài liệu này tập trung vào phần nối và chức năng sinh viên, không nhận toàn bộ chức năng tài khoản là phần riêng của thành viên 3.

### Giai đoạn 2 – Tạo màn hình sinh viên và nối từ đăng nhập

- Tạo `FrmStudentMain` cùng file Designer để chỉnh giao diện bằng Visual Studio.
- Nối tài khoản có vai trò `SinhVien` từ `Form1` vào màn hình sinh viên.
- Dùng `UserSession` để lấy tài khoản và hồ sơ đang đăng nhập.
- Hiển thị họ tên, mã sinh viên, lớp hành chính, khoa và email; thông báo khi chưa có email.
- Hiển thị đồng hồ trên giao diện; đồng hồ này chỉ để xem, không quyết định thời hạn điểm danh.
- Kiểm tra vai trò trước khi sử dụng form; đóng form nếu chưa đăng nhập đúng tài khoản sinh viên.
- Khi đăng xuất hoặc đóng màn hình sinh viên, luồng đăng nhập xóa phiên và hiện lại form đăng nhập.

**Kết quả:** Sinh viên vào đúng màn hình của mình sau đăng nhập, không phải mở form riêng để thử.

### Giai đoạn 3 – Chỉnh cách viết code cho dễ đọc

- Theo yêu cầu tham khảo phong cách dự án `Khenthuong`, tổ chức các hàm thành từng công việc nhỏ, tên dễ hiểu.
- Dùng các hàm như `LoadThongTinSinhVien`, `LoadBuoiHoc`, `CapNhatNutDiemDanh`, `LoadLichSu`, `LocLichSu`.
- Thêm chú thích tiếng Việt tại các bước cần giải thích.
- Tách giao diện, xử lý thao tác và truy vấn dữ liệu để dễ tìm phần cần sửa.
- Giữ tham số SQL khi truyền dữ liệu và `async/await` khi tải dữ liệu; không nối trực tiếp thông tin nhập của người dùng vào câu lệnh SQL.

**Kết quả:** Có thể lần theo từng nút và từng hàm khi đọc hoặc trình bày code.

### Giai đoạn 4 – Tải buổi học và xác thực sinh viên

- Viết `SinhVienDAL.LayBuoiHocDangMoAsync` để lấy buổi học của các lớp sinh viên đã được phân vào.
- Chỉ hiển thị buổi đang mở, đã đến giờ bắt đầu và còn trong thời gian điểm danh.
- Hiển thị môn học, lớp học phần, giảng viên, phòng, giờ mở và kết quả hiện có.
- Thêm nút Làm mới; có thông báo khi chưa có buổi mở hoặc không kết nối được máy chủ.
- Không cho vào điểm danh lại nếu buổi đã có kết quả.
- Tạo `FrmXacThuc`: chọn ngẫu nhiên câu hỏi ngày sinh hoặc 4 số cuối CCCD, đối chiếu hồ sơ thật.
- Dùng `TrangThaiXacThuc` giữ câu hỏi và số lần trả lời sai theo từng buổi.
- Sai 3 lần thì khóa xác thực buổi đó trong phiên đăng nhập hiện tại. Đóng popup hoặc làm mới không xóa bộ đếm.

**Kết quả:** Sinh viên chọn được buổi hợp lệ và xác thực hồ sơ trước khi nhập OTP.

### Giai đoạn 5 – Nhập OTP và ghi nhận vào cơ sở dữ liệu

- Tạo `FrmNhapOTP` để nhận mã giảng viên cung cấp.
- Kiểm tra mã nhập, hiển thị trạng thái đang xử lý và kết quả trả về.
- Gọi `DiemDanhDAL.GhiNhanDiemDanhAsync` và thủ tục `sp_GhiNhanDiemDanh`.
- SQL kiểm tra lại tài khoản, quyền tham gia lớp, trạng thái/thời hạn buổi, OTP và câu trả lời xác thực trước khi lưu.
- Dùng thời gian SQL Server để ghi nhận, không dựa vào đồng hồ máy sinh viên.
- Chống ghi trùng và không ghi đè kết quả đã tồn tại.
- Tạm khóa thao tác gửi khi đang xử lý; sau khi đóng popup thì tải lại danh sách để cập nhật trạng thái.
- Chuyển lỗi truy vấn thường gặp thành thông báo dễ hiểu để sinh viên biết cần làm mới hoặc thử lại.

**Kết quả:** Luồng điểm danh lưu dữ liệu thật và phản hồi kết quả cho người dùng.

### Giai đoạn 6 – Bỏ demo và bắt buộc kiểm tra cùng mạng

- Bỏ lựa chọn buổi mẫu, dữ liệu mẫu trong bộ nhớ, câu trả lời/OTP mẫu và cấu hình `TestMode`.
- Phần mở phiên giảng viên sử dụng địa chỉ mạng thật, không lấy loopback làm dữ liệu thay thế.
- Viết `NetworkHelper` lấy IPv4 và subnet mask của các kết nối Wi-Fi/Ethernet phù hợp.
- Thêm nút **Kiểm tra mạng** trong form OTP. Nút này chỉ kiểm tra, không ghi điểm danh.
- So sánh subnet của máy sinh viên với thông tin mạng giảng viên lưu trong buổi học.
- Kiểm tra lại kết nối ngay khi bấm xác nhận, không dùng kết quả kiểm tra cũ.
- SQL kiểm tra lại subnet; không cho phép bật tham số bỏ qua kiểm tra IP.

**Kết quả:** Điểm danh yêu cầu mạng hợp lệ cùng subnet IPv4. Cùng tên Wi-Fi chưa chắc cùng subnet; đây không phải cơ chế xác minh vị trí vật lý tuyệt đối.

### Giai đoạn 7 – Hoàn thiện xem lịch sử điểm danh

- Thêm tab **Lịch sử điểm danh**, tải dữ liệu theo tài khoản đang đăng nhập.
- Viết `SinhVienDAL.LayLichSuDiemDanhAsync` để lấy kết quả đã lưu.
- Hiển thị lớp học phần, môn, buổi, ngày học, thời điểm điểm danh, kết quả và ghi chú.
- Sắp xếp buổi mới nhất trước; hiển thị `—` khi không có thời điểm quét.
- Thêm bộ lọc lớp học phần và kết quả, có lựa chọn xem tất cả.
- Đếm Có mặt, Đi muộn, Vắng và Nghỉ có phép theo bộ lọc đang chọn.
- Có nút Làm mới, giữ lớp đã lọc nếu lớp đó vẫn còn trong dữ liệu.
- Bảng chỉ đọc; sinh viên không sửa kết quả hoặc chọn xem tài khoản khác qua giao diện.
- Chỉ hiển thị bản ghi đã có trong `DiemDanh`, không tự suy ra Vắng khi buổi chưa được chốt.
- Giữ lịch sử đã ghi kể cả khi sinh viên được chuyển khỏi lớp.

**Kết quả:** Sinh viên theo dõi được kết quả từng buổi và tổng số buổi theo trạng thái.

### Giai đoạn 8 – Đồng bộ với phiên giảng viên và kiểm tra tích hợp

- Khi tích hợp bản cập nhật chung, khôi phục hàm đọc lịch sử còn được form sinh viên sử dụng.
- Đồng bộ quy tắc mới: hết thời gian giảng viên chọn 5/10/15 phút thì không nhận thêm điểm danh.
- Kết quả Đi muộn cũ vẫn giữ trong lịch sử; luồng hiện tại không nhận lượt mới sau hạn đã chọn.
- Hỗ trợ phần liên quan ở giảng viên: sửa cấu trúc Designer, thêm bật/tắt trình chiếu và kết thúc phiên.
- Tách việc tự đóng phiên khỏi cửa sổ trình chiếu; khi hết hạn, chốt Vắng cho người chưa điểm danh và giữ kết quả đã có.
- Kiểm tra việc đóng lặp không tạo thêm kết quả trùng.

**Kết quả:** Danh sách buổi mở, quyền gửi điểm danh và lịch sử sinh viên thống nhất với thời hạn phiên giảng viên.

## 3. Các file cần nhớ khi đọc lại code

| File | Vai trò |
| --- | --- |
| `Form1.cs` | Đăng nhập, điều hướng sinh viên và trở lại khi đăng xuất |
| `UserSession.cs` | Thông tin tài khoản đang đăng nhập |
| `FrmStudentMain.cs` | Thông tin sinh viên, buổi mở, mở xác thực/OTP, lịch sử và bộ lọc |
| `FrmStudentMain.Designer.cs` | Bố cục, các bảng, nút và tab của màn hình sinh viên |
| `SinhVienDAL.cs` | Truy vấn buổi đang mở và lịch sử theo mã sinh viên |
| `FrmXacThuc.cs` / `.Designer.cs` | Câu hỏi xác thực hồ sơ và xử lý câu trả lời |
| `TrangThaiXacThuc.cs` | Loại câu hỏi và số lần trả lời sai theo buổi |
| `FrmNhapOTP.cs` / `.Designer.cs` | Nhập OTP, nút kiểm tra mạng, gửi và hiện kết quả |
| `DiemDanhDAL.cs` | Kiểm tra hồ sơ, mạng và gọi thủ tục ghi nhận |
| `NetworkHelper.cs` | Đọc card mạng và so sánh subnet IPv4 |
| `AppConfig.cs` | Đọc cấu hình ứng dụng; không còn chế độ demo |
| `SQL script/04_GhiNhanDiemDanh.sql` | Kiểm tra điều kiện và lưu điểm danh phía SQL |
| `SQL script/sp_DongBuoiHoc.sql` | Đóng buổi và chốt Vắng để sinh viên xem lịch sử |

Hai file SQL nằm trong thư mục `SQL script` ở gốc kho mã nguồn. Các file C# nằm trong thư mục dự án `HeThongDiemDanh`.

## 4. Những gì đã kiểm tra

Các kết quả dưới đây được ghi nhận trong các lượt triển khai trước; lượt tạo tài liệu này chỉ đối chiếu mã nguồn, không chạy lại toàn bộ kiểm thử.

- Đã build bản tích hợp gần nhất: **0 lỗi, 135 cảnh báo**; không đồng nghĩa dự án đã sạch mọi cảnh báo.
- Đã kiểm tra SQL từ chối sai OTP, sai xác thực, ghi trùng, buổi đóng/chưa bắt đầu/quá hạn và trường hợp mạng không hợp lệ.
- Đã kiểm tra cùng/khác subnet, các subnet mask khác nhau và việc không cho bỏ qua kiểm tra IP.
- Đã kiểm tra truy vấn lịch sử nhận được kết quả có mặt và vắng sau khi đóng buổi.
- Đã kiểm tra thời hạn 5/10/15 phút, đóng lặp an toàn và giữ kết quả cũ.
- Đã kiểm tra timer giảng viên vẫn chốt khi trình chiếu tắt và form chính thu nhỏ.
- Dữ liệu kiểm thử SQL được tạo trong giao dịch rồi hoàn tác. Cấu hình kết nối tạm của công cụ kiểm thử không thay thế chuỗi kết nối ứng dụng.

## 5. Những giới hạn và việc cần làm tiếp

- [ ] Chạy thử toàn bộ luồng trên hai máy thật: một giảng viên, một sinh viên, dùng dữ liệu thử được nhóm thống nhất.
- [ ] Thử cùng mạng và khác mạng thực tế; kiểm tra thêm máy có nhiều card mạng, Wi-Fi/LAN hoặc VPN.
- [ ] Kiểm tra giao diện trên độ phân giải và mức phóng to chữ của các máy thành viên.
- [ ] Khi đưa sang máy khác, cập nhật cả hai script SQL ghi nhận/đóng buổi và cấu hình kết nối phù hợp.
- [ ] Rà soát các cảnh báo build còn lại cùng nhóm, tránh sửa chồng phần của thành viên khác.

Các giới hạn hiện tại cần nhớ khi trình bày:

- Khóa sau 3 lần sai được giữ trong bộ nhớ theo phiên đăng nhập, chưa phải khóa bền vững lưu ở SQL.
- So sánh mạng là so sánh subnet IPv4. Ứng dụng kết nối SQL trực tiếp và gửi IP máy sinh viên; chưa có máy chủ trung gian xác minh nguồn kết nối.
- Tự chốt phiên đúng giờ cần ứng dụng giảng viên đang chạy và kết nối được SQL. Nếu thoát toàn bộ ứng dụng, SQL vẫn chặn điểm danh quá hạn; việc chốt phiên được thực hiện khi giảng viên mở lại.
- Lịch sử chỉ có kết quả đã ghi/chốt. Việc hiển thị Nghỉ có phép không có nghĩa đã triển khai chức năng sinh viên gửi đơn xin nghỉ.
- Không đánh dấu toàn bộ đề tài hoàn thành chỉ dựa vào luồng sinh viên; các phân hệ và yêu cầu khác cần đối chiếu riêng với nhóm.

## 6. Trình tự tự xem lại hoặc trình bày sản phẩm

1. Mở `Form1.cs`, xem nhánh đăng nhập sinh viên và cách gọi `FrmStudentMain`.
2. Mở Designer của `FrmStudentMain`, xác định khu vực thông tin, danh sách buổi mở và tab lịch sử.
3. Đọc `LoadBuoiHoc` cùng `SinhVienDAL.LayBuoiHocDangMoAsync` để hiểu dữ liệu đến từ đâu.
4. Đọc `btnDiemDanh_Click`, theo luồng qua `FrmXacThuc` rồi `FrmNhapOTP`.
5. Đọc `DiemDanhDAL` và `NetworkHelper` để giải thích điều kiện xác thực, OTP và mạng.
6. Đọc thủ tục SQL ghi nhận để hiểu kiểm tra cuối cùng trước khi lưu kết quả.
7. Trở lại `LoadLichSu` và `LocLichSu` để giải thích lịch sử, bộ lọc và tổng số buổi.
8. Thử đăng xuất để xác nhận ứng dụng trở lại màn hình đăng nhập.

## 7. Mẫu ghi thêm tiến độ lần sau

| Ngày cập nhật | Yêu cầu / vấn đề | Đã làm | File thay đổi | Kết quả kiểm tra | Việc còn lại |
| --- | --- | --- | --- | --- | --- |
| … | … | … | … | … | … |

Tài liệu đi kèm trong cùng thư mục: `HUONG-DAN-PHAN-HE-SINH-VIEN.md` hướng dẫn sử dụng chi tiết; `HUONG-DAN-GIANG-VIEN.md` giải thích phần phiên và trình chiếu có liên quan.
