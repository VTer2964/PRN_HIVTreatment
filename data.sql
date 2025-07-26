Use HIV
-------xóa
-- Tắt kiểm tra khóa ngoại tạm thời
EXEC sp_MSforeachtable 'ALTER TABLE ? NOCHECK CONSTRAINT ALL'

-- Xóa dữ liệu từ tất cả các bảng (theo thứ tự để tránh lỗi khóa ngoại)
DELETE FROM [Notification]
DELETE FROM [CustomizedARV_Protocol_Detail]
DELETE FROM [CustomizedARV_Protocol]
DELETE FROM [ARV_Protocol_Detail]
DELETE FROM [ARV_Protocol]
DELETE FROM [ARV]
DELETE FROM [Comment]
DELETE FROM [Blog]
DELETE FROM [EducationalResources]
DELETE FROM [Examination]
DELETE FROM [Appointment]
DELETE FROM [Schedule]
DELETE FROM [DoctorInfo]
DELETE FROM [User]
DELETE FROM [Account]

-- Reset IDENTITY về 1 cho tất cả các bảng có IDENTITY
DBCC CHECKIDENT ('[Account]', RESEED, 0)
DBCC CHECKIDENT ('[User]', RESEED, 0)
DBCC CHECKIDENT ('[Schedule]', RESEED, 0)
DBCC CHECKIDENT ('[Appointment]', RESEED, 0)
DBCC CHECKIDENT ('[Examination]', RESEED, 0)
DBCC CHECKIDENT ('[Blog]', RESEED, 0)
DBCC CHECKIDENT ('[Comment]', RESEED, 0)
DBCC CHECKIDENT ('[ARV]', RESEED, 0)
DBCC CHECKIDENT ('[ARV_Protocol]', RESEED, 0)
DBCC CHECKIDENT ('[CustomizedARV_Protocol]', RESEED, 0)
DBCC CHECKIDENT ('[EducationalResources]', RESEED, 0)
DBCC CHECKIDENT ('[Notification]', RESEED, 0)
-------

-- Bật lại kiểm tra khóa ngoại
EXEC sp_MSforeachtable 'ALTER TABLE ? WITH CHECK CHECK CONSTRAINT ALL'


-- Thêm dữ liệu vào bảng Account (với các role khác nhau)
INSERT INTO Account (Username, PasswordHash, Email, CreatedAt, Status)
VALUES
(N'admin', 'hashed_password_1', N'admin@example.com', GETDATE(), 'Active'),
(N'bacsi1', 'hashed_password_2', N'bacsi1@example.com', GETDATE(), 'Active'),
(N'bacsi2', 'hashed_password_3', N'bacsi2@example.com', GETDATE(), 'Active'),
(N'nhanvien1', 'hashed_password_4', N'nhanvien1@example.com', GETDATE(), 'Active'),
(N'benhnhan1', 'hashed_password_5', N'benhnhan1@example.com', GETDATE(), 'Active'),
(N'benhnhan2', 'hashed_password_6', N'benhnhan2@example.com', GETDATE(), 'Active'),
(N'benhnhan3', 'hashed_password_7', N'benhnhan3@example.com', GETDATE(), 'Active');

-- Thêm dữ liệu vào bảng User (tương ứng với các Account)
INSERT INTO [User] (AccountId, FullName, Phone, UserAvatar, Address, Gender, Birthdate, Role, Status)
VALUES
(1, N'Quản trị viên hệ thống', '0987654321', NULL, N'Hà Nội', 'Male', '1980-01-01', 'Admin', 'Active'),
(2, N'Bác sĩ Nguyễn Văn A', '0912345678', NULL, N'TP.HCM', 'Male', '1975-05-15', 'Doctor', 'Active'),
(3, N'Bác sĩ Trần Thị B', '0923456789', NULL, N'Đà Nẵng', 'Female', '1982-08-20', 'Doctor', 'Active'),
(4, N'Nhân viên Lê Văn C', '0934567890', NULL, N'Hải Phòng', 'Male', '1990-03-10', 'Staff', 'Active'),
(5, N'Bệnh nhân Phạm Thị D', '0945678901', NULL, N'Cần Thơ', 'Female', '1995-11-25', 'Patient', 'Active'),
(6, N'Bệnh nhân Hoàng Văn E', '0956789012', NULL, N'Bình Dương', 'Male', '1988-07-30', 'Patient', 'Active'),
(7, N'Bệnh nhân Võ Thị F', '0967890123', NULL, N'Đồng Nai', 'Female', '1979-09-05', 'Patient', 'Active');

-- Thêm thông tin bác sĩ
INSERT INTO DoctorInfo (DoctorId, Degree, Specialization, ExperienceYears, DoctorAvatar, Status)
VALUES
(2, N'Tiến sĩ', N'Chuyên khoa HIV/AIDS', 15, NULL, 'Active'),
(3, N'Thạc sĩ', N'Chuyên khoa Truyền nhiễm', 10, NULL, 'Active');

-- Thêm dữ liệu vào bảng ARV (thuốc ARV)
INSERT INTO ARV (Name, Description, Status)
VALUES
(N'Tenofovir', N'Thuốc ARV nhóm NRTI', 'Active'),
(N'Lamivudine', N'Thuốc ARV nhóm NRTI', 'Active'),
(N'Efavirenz', N'Thuốc ARV nhóm NNRTI', 'Active'),
(N'Dolutegravir', N'Thuốc ARV nhóm INSTI', 'Active'),
(N'Ritonavir', N'Thuốc ARV nhóm PI', 'Active'),
(N'Atazanavir', N'Thuốc ARV nhóm PI', 'Active'),
(N'Lopinavir', N'Thuốc ARV nhóm PI', 'Active');

-- Thêm dữ liệu vào bảng ARV_Protocol (phác đồ điều trị chuẩn)
INSERT INTO ARV_Protocol (Name, Description, Status)
VALUES
(N'Phác đồ 1a', N'Phác đồ đầu tay cho bệnh nhân mới', 'Active'),
(N'Phác đồ 1b', N'Phác đồ thay thế cho phác đồ 1a', 'Active'),
(N'Phác đồ 2a', N'Phác đồ khi thất bại với phác đồ 1', 'Active'),
(N'Phác đồ 2b', N'Phác đồ thay thế cho phác đồ 2a', 'Active'),
(N'Phác đồ đặc biệt 1', N'Cho phụ nữ có thai', 'Active'),
(N'Phác đồ đặc biệt 2', N'Cho trẻ em', 'Active'),
(N'Phác đồ dự phòng', N'Dự phòng sau phơi nhiễm', 'Active');

-- Thêm chi tiết phác đồ chuẩn
INSERT INTO ARV_Protocol_Detail (ProtocolId, ArvId, Dosage, UsageInstruction, Status)
VALUES
(1, 1, N'300mg', N'1 viên/ngày', 'Active'),
(1, 2, N'300mg', N'1 viên/ngày', 'Active'),
(1, 3, N'600mg', N'1 viên/ngày', 'Active'),
(2, 1, N'300mg', N'1 viên/ngày', 'Active'),
(2, 2, N'300mg', N'1 viên/ngày', 'Active'),
(2, 4, N'50mg', N'1 viên/ngày', 'Active'),
(3, 5, N'100mg', N'2 viên/ngày', 'Active');

-- Thêm lịch khám của bác sĩ
INSERT INTO Schedule (DoctorId, ScheduledTime, Room, Status)
VALUES
(2, '2023-06-01 08:00:00', N'Phòng 101', 'Active'),
(2, '2023-06-01 09:00:00', N'Phòng 101', 'Active'),
(2, '2023-06-01 10:00:00', N'Phòng 101', 'Active'),
(3, '2023-06-02 08:00:00', N'Phòng 201', 'Active'),
(3, '2023-06-02 09:00:00', N'Phòng 201', 'Active'),
(3, '2023-06-02 10:00:00', N'Phòng 201', 'Active'),
(3, '2023-06-02 11:00:00', N'Phòng 201', 'Active');

-- Thêm cuộc hẹn khám
INSERT INTO Appointment (PatientId, ScheduleId, DoctorId, Note, AppoinmentType, IsAnonymous, Status, AppointmentDate, CreatedAt)
VALUES
(5, 1, 2, N'Khám định kỳ', N'Định kỳ', 0, 'Confirmed', '2023-06-01 08:00:00', GETDATE()),
(6, 2, 2, N'Có triệu chứng sốt', N'Cấp cứu', 0, 'Confirmed', '2023-06-01 09:00:00', GETDATE()),
(7, 3, 2, N'Kiểm tra CD4', N'Định kỳ', 0, 'Confirmed', '2023-06-01 10:00:00', GETDATE()),
(5, 4, 3, N'Tái khám sau điều trị', N'Định kỳ', 0, 'Confirmed', '2023-06-02 08:00:00', GETDATE()),
(6, 5, 3, N'Kiểm tra tải lượng virus', N'Xét nghiệm', 0, 'Confirmed', '2023-06-02 09:00:00', GETDATE()),
(7, 6, 3, N'Đổi phác đồ điều trị', N'Điều trị', 0, 'Confirmed', '2023-06-02 10:00:00', GETDATE()),
(5, 7, 3, N'Khám tổng quát', N'Định kỳ', 0, 'Pending', '2023-06-02 11:00:00', GETDATE());

-- Thêm kết quả khám
INSERT INTO Examination (PatientId, DoctorId, Result, Cd4Count, HivLoad, ExamDate, Status, CreatedAt)
VALUES
(5, 2, N'Ổn định, tiếp tục phác đồ hiện tại', 450, 200, '2023-05-15', 'Completed', GETDATE()),
(6, 2, N'Cần theo dõi thêm', 350, 1500, '2023-05-16', 'Completed', GETDATE()),
(7, 2, N'Cần đổi phác đồ', 280, 5000, '2023-05-17', 'Completed', GETDATE()),
(5, 3, N'Kết quả tốt', 500, 100, '2023-05-18', 'Completed', GETDATE()),
(6, 3, N'Cần tăng liều', 400, 800, '2023-05-19', 'Completed', GETDATE()),
(7, 3, N'Ổn định sau đổi phác đồ', 320, 300, '2023-05-20', 'Completed', GETDATE()),
(5, 2, N'Kiểm tra định kỳ', 480, 150, '2023-06-01', 'Pending', GETDATE());

-- Thêm phác đồ điều trị tùy chỉnh
INSERT INTO CustomizedARV_Protocol (DoctorId, PatientId, BaseProtocolId, Name, Description, Status)
VALUES
(2, 5, 1, N'Phác đồ riêng BN001', N'Điều chỉnh liều cho bệnh nhân 001', 'Active'),
(2, 6, 2, N'Phác đồ riêng BN002', N'Điều chỉnh liều cho bệnh nhân 002', 'Active'),
(3, 7, 3, N'Phác đồ riêng BN003', N'Điều chỉnh liều cho bệnh nhân 003', 'Active'),
(3, 5, 1, N'Phác đồ riêng BN001-2', N'Điều chỉnh sau 6 tháng', 'Active'),
(2, 6, 2, N'Phác đồ riêng BN002-2', N'Điều chỉnh sau phản ứng phụ', 'Active'),
(3, 7, 3, N'Phác đồ riêng BN003-2', N'Điều chỉnh sau tái khám', 'Active'),
(2, 5, 4, N'Phác đồ dự phòng BN001', N'Dự phòng sau phơi nhiễm', 'Active');

-- Thêm chi tiết phác đồ tùy chỉnh
INSERT INTO CustomizedARV_Protocol_Detail (CustomProtocolId, ArvId, Dosage, UsageInstruction, Status)
VALUES
(1, 1, N'250mg', N'1 viên/ngày (giảm liều)', 'Active'),
(1, 2, N'300mg', N'1 viên/ngày', 'Active'),
(1, 3, N'500mg', N'1 viên/ngày (giảm liều)', 'Active'),
(2, 1, N'300mg', N'1 viên/ngày', 'Active'),
(2, 2, N'300mg', N'1 viên/ngày', 'Active'),
(2, 4, N'40mg', N'1 viên/ngày (giảm liều)', 'Active'),
(3, 5, N'80mg', N'2 viên/ngày (giảm liều)', 'Active');

-- Thêm bài viết blog
INSERT INTO Blog (Title, Content, AuthorId, Status, CreatedAt, PublishedAt, IsApproved, ImageUrl)
VALUES
(N'Hiểu đúng về HIV/AIDS', N'Nội dung bài viết về HIV/AIDS...', 2, 'Published', GETDATE(), GETDATE(), 1, NULL),
(N'Cách phòng tránh lây nhiễm HIV', N'Nội dung bài viết về phòng tránh...', 3, 'Published', GETDATE(), GETDATE(), 1, NULL),
(N'Điều trị ARV hiệu quả', N'Nội dung bài viết về điều trị ARV...', 2, 'Published', GETDATE(), GETDATE(), 1, NULL),
(N'Sống khỏe với HIV', N'Nội dung bài viết về lối sống...', 3, 'Draft', GETDATE(), NULL, 0, NULL),
(N'Dinh dưỡng cho người nhiễm HIV', N'Nội dung bài viết về dinh dưỡng...', 2, 'Published', GETDATE(), GETDATE(), 1, NULL),
(N'Tâm lý cho bệnh nhân HIV', N'Nội dung bài viết về tâm lý...', 3, 'Published', GETDATE(), GETDATE(), 1, NULL),
(N'Cập nhật phác đồ mới 2023', N'Nội dung bài viết về phác đồ...', 2, 'Pending', GETDATE(), NULL, 0, NULL);

-- Thêm bình luận blog
INSERT INTO Comment (BlogId, UserId, Content, Status, CreatedAt)
VALUES
(1, 5, N'Bài viết rất hữu ích!', 'Active', GETDATE()),
(1, 6, N'Cảm ơn bác sĩ đã chia sẻ', 'Active', GETDATE()),
(2, 7, N'Tôi đã học được nhiều điều', 'Active', GETDATE()),
(3, 5, N'Thông tin rất chi tiết', 'Active', GETDATE()),
(3, 6, N'Có thể giải thích thêm về phần này không?', 'Active', GETDATE()),
(5, 7, N'Chế độ ăn này rất phù hợp với tôi', 'Active', GETDATE()),
(6, 5, N'Bài viết giúp tôi lạc quan hơn', 'Active', GETDATE());

-- Thêm tài liệu giáo dục
INSERT INTO EducationalResources (Title, Content, CreatedBy, CreatedAt)
VALUES
(N'Hướng dẫn sử dụng thuốc ARV', N'Nội dung hướng dẫn...', 2, GETDATE()),
(N'Cẩm nang chăm sóc bệnh nhân HIV', N'Nội dung cẩm nang...', 3, GETDATE()),
(N'Các xét nghiệm cần thiết', N'Nội dung về xét nghiệm...', 2, GETDATE()),
(N'Quy trình khám bệnh', N'Nội dung quy trình...', 4, GETDATE()),
(N'Dấu hiệu cảnh báo', N'Nội dung về dấu hiệu...', 3, GETDATE()),
(N'Chế độ tập luyện', N'Nội dung về tập luyện...', 2, GETDATE()),
(N'FAQ về HIV/AIDS', N'Các câu hỏi thường gặp...', 4, GETDATE());

-- Thêm thông báo
INSERT INTO Notification (UserId, Type, Message, ScheduledTime, IsRead, CreatedAt, Status, AppointmentId, ProtocolId, ExaminationId)
VALUES
(5, N'Appointment', N'Bạn có lịch hẹn khám vào ngày 01/06/2023', '2023-05-30 08:00:00', 0, GETDATE(), 'Active', 1, NULL, NULL),
(6, N'Appointment', N'Bạn có lịch hẹn khám vào ngày 01/06/2023', '2023-05-30 08:00:00', 0, GETDATE(), 'Active', 2, NULL, NULL),
(7, N'Examination', N'Kết quả xét nghiệm của bạn đã có', '2023-05-18 10:00:00', 1, GETDATE(), 'Active', NULL, NULL, 1),
(5, N'Protocol', N'Phác đồ điều trị mới đã được cập nhật', '2023-05-20 09:00:00', 1, GETDATE(), 'Active', NULL, 1, NULL),
(6, N'Examination', N'Bạn cần đi xét nghiệm lại sau 1 tháng', '2023-05-19 11:00:00', 0, GETDATE(), 'Active', NULL, NULL, 5),
(7, N'Appointment', N'Lịch hẹn khám của bạn đã được xác nhận', '2023-05-31 14:00:00', 0, GETDATE(), 'Active', 6, NULL, NULL),
(5, N'General', N'Hệ thống đã cập nhật tính năng mới', '2023-06-01 00:00:00', 0, GETDATE(), 'Active', NULL, NULL, NULL);