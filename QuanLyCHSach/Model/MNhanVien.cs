using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCHSach.Model
{
    class MNhanVien
    {
        public int Id { get; set; }
        public string Ten { get; set; }
        public string Diachi { get; set; }
        public DateTime  Ngaysinh { get; set; }
        public string  Sdt { get; set; }
        public int Id_chucvu { get; set; }
        public string Tenchucvu { get; set; }
    }
}
