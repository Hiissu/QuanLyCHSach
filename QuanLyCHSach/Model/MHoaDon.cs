using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCHSach.Model
{
    class MHoaDon
    {
        public int Id { get; set; }
        public string Ngaylap { get; set; }
        public int Id_nhanvien { get; set; }
        public string Tennhanvien { get; set; }
        public int Tongtien { get; set; }
    }
}
