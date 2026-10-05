Trong file này là các file chứa script tạo CSDL cho SQL server bao gồm 3 file chia ra các mục đích chính:



\+  **"QlySinhvien.sql"** là file để tạo khung dữ liệu , bao gồm tạo CSDL mới, sử dụng CSDL đó và tạo bảng với các khung sườn để chuẩn bị lưu dữ liệu mẫu. AE chạy từng phần từng lớp một, không nên chạy toàn bộ file code trong 1 lần rất dễ lỗi.



\+  **"DuLieuMau.sql"** là file giữ script để tạo dữ liệu mẫu trên sql dựa trên khung sườn mà file QlySinhvien.sql trước đó đã tạo. AE cũng cần bôi đen từng phần sau đó chạy từng cái một, tránh việc chạy cùng lúc rất dễ lỗi.



\+  **"XoaDuLieu.sql"** là file code script khi chạy sẽ xóa toàn bộ dữ liệu đã lưu trữ trong CSDL, nó xóa bao gồm cả dữ liệu mẫu, xóa các bảng đã tạo, biến cái CSDL này như mới tạo, AE xem nếu code lỗi quá thì chạy toàn bộ file XoaDuLieu.sql này để làm mới CSDL, sau đó lại chạy lại từ đầu từ file "QlySinhvien.sql" để tạo bảng và khung sườn, sau đó hãng chạy "DuLieuMau.sql"


Do file lưu trữ SQL được SQL management quản lý và cần truy cấp, nếu tôi gửi file này rất dễ dính lỗi Accept denied hoặc lỗi phiên bản khi ae dung khác phiên bản với nhau, nên tôi gửi file script sang để ae tự chạy, tạo và test trên chính file sql của ae

