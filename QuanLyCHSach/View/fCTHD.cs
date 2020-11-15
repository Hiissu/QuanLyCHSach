using QuanLyCHSach.Controller;
using QuanLyCHSach.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCHSach.View
{
    public partial class fCTHD : Form
    {
        public fCTHD(int idHoaDon)
        {
            InitializeComponent();
            XemCTHD(idHoaDon);
        }

        CCTHD ccthd = new CCTHD();

        private void XemCTHD(int id_hoadon)
        {
            DataTable dt = ccthd.HienThiCTHD(id_hoadon);

            List<MCTHD> lcthd = new List<MCTHD>();
            foreach (DataRow r in dt.Rows)
            {
                MCTHD m = new MCTHD();
                m.Id = int.Parse(r["id"].ToString());
                m.Id_sach = int.Parse(r["id_sach"].ToString());
                m.Ten_sach = r["tensach"].ToString();
                m.Soluong = int.Parse(r["soluong"].ToString());
                m.Dongia = int.Parse(r["dongia"].ToString());
                m.Thanhtien = int.Parse(r["thanhtien"].ToString());
                lcthd.Add(m);
            }

            if (lcthd.Count != 0)
            {
                DataTable tb = new DataTable();
                tb.Columns.Add("Id", typeof(int));
                tb.Columns.Add("TT", typeof(int));
                tb.Columns.Add("Mã sách", typeof(String));
                tb.Columns.Add("Tên sách", typeof(String));
                tb.Columns.Add("Số lượng", typeof(int));
                tb.Columns.Add("Đơn giá", typeof(string));
                tb.Columns.Add("Thành tiền", typeof(string));
                int tt = 0;
                foreach (MCTHD m in lcthd)
                {
                    tt++;
                    DataRow r = tb.NewRow();
                    r["Id"] = m.Id.ToString();
                    r["TT"] = tt.ToString();
                    r["Mã sách"] = m.Ten_sach;
                    r["Tên sách"] = m.Ten_sach;
                    r["Số lượng"] = m.Soluong;
                    r["Đơn giá"] = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", m.Dongia);
                    r["Thành tiền"] = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", m.Thanhtien);

                    tb.Rows.Add(r);
                }
                dtgvChiTietHoaDon.DataSource = tb;
                dtgvChiTietHoaDon.Columns["Id"].Visible = false;
            }

        }

    }
}
