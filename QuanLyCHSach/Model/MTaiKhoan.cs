using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCHSach.Model
{
    class MTaiKhoan
    {
        public int Id { get; set; }
        public string Tendangnhap { get; set; }
        public string Tenhienthi { get; set; }
        public string Matkhau { get; set; }
        public bool Loaitaikhoan { get; set; } // 0 : staff         1 : admin
        public int Id_nhanvien { get; set; }
        public string Tennhanvien { get; set; }
    }
}
