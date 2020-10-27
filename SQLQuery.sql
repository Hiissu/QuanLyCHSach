create database QLyCHSach

create table Chucvu
(
id  int identity(1,1)   primary key,
ten nvarchar(100) 
)

create table NhanVien
(
id int identity(1,1)  primary key,
ten nvarchar(100) ,
diachi nvarchar(100) ,
ngaysinh nvarchar(100) ,
sdt nvarchar(15) ,
id_chucvu int,
foreign key (id_chucvu) references Chucvu(id)
)

create table TaiKhoan
(
id  int identity(1,1)   primary key,
tendangnhap nvarchar(100) ,
tenhienthi nvarchar(100) ,
matkhau nvarchar(100) ,
loaitaikhoan bit , -- 0: nhanvien   1: admin
id_nhanvien int ,
foreign key (id_nhanvien) references Nhanvien(id)
)


create table Sach
(
id  int identity(1,1)  primary key,
ten nvarchar(100) ,
tacgia nvarchar(100) ,
ngayxuatban nvarchar(100) ,
soluong int ,
dongia float ,
id_theloai int ,
id_nhaxuatban int ,
foreign key (id_theloai) references Theloai(id),
foreign key (id_nhaxuatban) references Nhaxuatban(id)
)


create table TheLoai
(
id  int identity(1,1)  primary key,
ten nvarchar(100) 
)


create table NhaXuatBan
(
id int identity(1,1)  primary key,
ten nvarchar(100),
diachi nvarchar(100) 
)


create table HoaDon
(
id int identity(1,1)  primary key,
ngaylap nvarchar(100) ,
id_nhanvien int,
foreign key (id_nhanvien) references Nhanvien(id)
)




create table CTHD
(
id  int identity(1,1)   primary key,
id_hoadon int,
id_sach int,
soluong int ,
foreign key (id_sach) references Sach(id),
foreign key (id_hoadon) references HoaDon(id)
)



SELECT s.id, s.ten, s.tacgia, s.ngayxuatban, s.soluong, s.dongia, tl.ten as tentheloai, nxb.ten as tennxb FROM dbo.Sach as s 
                INNER JOIN dbo.TheLoai as tl ON s.id_theloai = tl.id
                INNER JOIN dbo.NhaXuatBan as nxb ON s.id_nhaxuatban = nxb.id



INSERT INTO dbo.CTHD(id_hoadon, id_sach, soluong)  VALUES (IDENT_CURRENT('HoaDon'), IDENT_CURRENT('Sach'), '12')

