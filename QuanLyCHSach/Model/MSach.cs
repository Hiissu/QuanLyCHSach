using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCHSach.Model
{
    class MSach
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string Tacgia { get; set; }
        public int Id_theloai { get; set; }
        public string Tentheloai { get; set; }
        public string Ngayxuatban { get; set; }
        public int Id_nhaxuatban { get; set; }
        public string Tennhaxuatban { get; set; }
        public int Soluong { get; set; }
        public double Dongia { get; set; }
    }
}
