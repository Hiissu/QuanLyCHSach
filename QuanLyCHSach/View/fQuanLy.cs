﻿using QuanLyCHSach.Controller;
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
    public partial class fQuanLy : Form
    {
        public fQuanLy()
        {
            InitializeComponent();
        
        }
        public string tenDangNhap { get; set; }
        public string tenNhanVien { get; set; }
        public bool loaiTaiKhoan { get; set; }


        private void fQuanLy_Load(object sender, EventArgs e)
        {
            tbTenDangNhap.Text = tenDangNhap;
            tbTenNhanVien.Text = tenNhanVien;
            if (!loaiTaiKhoan)
            {
                tbLoaiTaiKhoan.Text = "Nhân viên";
                btQLCHSach.Visible = false;
            }
            else
            {
                tbLoaiTaiKhoan.Text = "Quản lý";
                btQLCHSach.Visible = true;

            }

            tbNgayLapHoaDon.Text = DateTime.Now.ToString("dd-MM-yyyy");
            LoadDuLieuSach();
            this.ActiveControl = tbTimKiem;

            Dictionary<string, string> dicTimKiem = new Dictionary<string, string>();
            dicTimKiem.Add("s.ten", "Tên sách");
            dicTimKiem.Add("tl.ten", "Thể loại");
            dicTimKiem.Add("s.tacgia", "Tác giả");
            dicTimKiem.Add("nxb.ten", "Nhà xuất bản");
            cbTimKiem.DataSource = new BindingSource(dicTimKiem, null);
            cbTimKiem.DisplayMember = "Value";
            cbTimKiem.ValueMember = "Key";


            tb.Columns.Add("TT", typeof(int));
            tb.Columns.Add("Mã sách", typeof(String));
            tb.Columns.Add("Tên sách", typeof(String));
            tb.Columns.Add("Số lượng", typeof(int));
            tb.Columns.Add("Đơn giá", typeof(int));
            tb.Columns.Add("Thành tiền", typeof(int));
        }
        DataTable tb = new DataTable();
        int tt = 0;

        private void btQLCHSach_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.ShowDialog();
        }

        CSach cs = new CSach();
        CHoaDon chd = new CHoaDon();
        CCTHD ccthd = new CCTHD();

        private void btReload_Click(object sender, EventArgs e)
        {
            LoadDuLieuSach();
        }

        private void LoadDuLieuSach()
        {
            List<MSach> ls = new List<MSach>();
            DataTable dts = cs.HienThiTatCaSach();
            foreach (DataRow r in dts.Rows)
            {
                MSach m = new MSach();
                m.Id = int.Parse(r["id"].ToString());
                m.Ten = r["ten"].ToString();
                m.Tacgia = r["tacgia"].ToString();
                m.Tentheloai = r["tentheloai"].ToString();
                m.Ngayxuatban = Convert.ToDateTime(r["ngayxuatban"].ToString());
                m.Tennhaxuatban = r["tennhaxuatban"].ToString();
                m.Soluong = int.Parse(r["soluong"].ToString());
                m.Dongia = double.Parse(r["dongia"].ToString());
                ls.Add(m);
            }

            if (ls.Count != 0)
            {
                DataTable tb = new DataTable();
                tb.Columns.Add("Id", typeof(String));
                tb.Columns.Add("TT", typeof(int));
                tb.Columns.Add("Tên sách", typeof(String));
                tb.Columns.Add("Tác giả", typeof(String));
                tb.Columns.Add("Thể loại", typeof(String));
                tb.Columns.Add("Ngày xuất bản", typeof(String));
                tb.Columns.Add("Nhà xuất bản", typeof(String));
                tb.Columns.Add("Số lượng", typeof(int));
                tb.Columns.Add("Đơn giá", typeof(double));
                int tt = 0;
                foreach (MSach m in ls)
                {
                    tt++;
                    DataRow r = tb.NewRow();
                    r["Id"] = m.Id.ToString();
                    r["TT"] = tt.ToString();
                    r["Tên sách"] = m.Ten;
                    r["Tác giả"] = m.Tacgia;
                    r["Thể loại"] = m.Tentheloai;
                    r["Ngày xuất bản"] = m.Ngayxuatban.ToString("dd-MM-yyyy");
                    r["Nhà xuất bản"] = m.Tennhaxuatban;
                    r["Số lượng"] = m.Soluong;
                    r["Đơn giá"] = m.Dongia;

                    tb.Rows.Add(r);
                }
                dtgvTimKiemSach.DataSource = tb;
                dtgvTimKiemSach.Columns["Id"].Visible = false;
            }
        }
        private void btTimKiem_Click(object sender, EventArgs e)
        {
            DataTable dts = cs.TimKiem(cbTimKiem.SelectedValue, tbTimKiem.Text);

            List<MSach> ls = new List<MSach>();
            foreach (DataRow r in dts.Rows)
            {
                MSach m = new MSach();
                m.Id = int.Parse(r["id"].ToString());
                m.Ten = r["ten"].ToString();
                m.Tacgia = r["tacgia"].ToString();
                m.Tentheloai = r["tentheloai"].ToString();
                m.Ngayxuatban = Convert.ToDateTime(r["ngayxuatban"].ToString());
                m.Tennhaxuatban = r["tennhaxuatban"].ToString();
                m.Soluong = int.Parse(r["soluong"].ToString());
                m.Dongia = double.Parse(r["dongia"].ToString());
                ls.Add(m);
            }

            if (ls.Count != 0)
            {
                DataTable tb = new DataTable();
                tb.Columns.Add("Id", typeof(String));
                tb.Columns.Add("TT", typeof(int));
                tb.Columns.Add("Tên sách", typeof(String));
                tb.Columns.Add("Tác giả", typeof(String));
                tb.Columns.Add("Thể loại", typeof(String));
                tb.Columns.Add("Ngày xuất bản", typeof(String));
                tb.Columns.Add("Nhà xuất bản", typeof(String));
                tb.Columns.Add("Số lượng", typeof(int));
                tb.Columns.Add("Đơn giá", typeof(double));
                int tt = 0;
                foreach (MSach m in ls)
                {
                    tt++;
                    DataRow r = tb.NewRow();
                    r["Id"] = m.Id.ToString();
                    r["TT"] = tt.ToString();
                    r["Tên sách"] = m.Ten;
                    r["Tác giả"] = m.Tacgia;
                    r["Thể loại"] = m.Tentheloai;
                    r["Ngày xuất bản"] = m.Ngayxuatban.ToString("dd-MM-yyyy");
                    r["Nhà xuất bản"] = m.Tennhaxuatban;
                    r["Số lượng"] = m.Soluong;
                    r["Đơn giá"] = m.Dongia;

                    tb.Rows.Add(r);
                }
                dtgvTimKiemSach.DataSource = tb;
                dtgvTimKiemSach.Columns["Id"].Visible = false;
            }

            
        }

        private void dtgvTimKiemSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgvTimKiemSach.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dtgvTimKiemSach.CurrentRow.Selected = true;
                    tbIdSach.Text = dtgvTimKiemSach.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString();
                    tbTenSach.Text = dtgvTimKiemSach.Rows[e.RowIndex].Cells["Tên sách"].FormattedValue.ToString();
                    
                    donGia = int.Parse(dtgvTimKiemSach.Rows[e.RowIndex].Cells["Đơn giá"].FormattedValue.ToString());
                    thanhTien = donGia * nudSoLuong.Value;
                    
                    tbDonGia.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", donGia);
                    tbThanhTien.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", thanhTien);
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        int donGia;
        decimal thanhTien;
        
        private void nudSoLuong_ValueChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbDonGia.Text))
            {
                thanhTien = donGia * nudSoLuong.Value;
                tbThanhTien.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", thanhTien); 
            }

        }

        
        private void clearTB()
        {
            tbIdSach.Text = "";
            tbTenSach.Text = "";
            tbDonGia.Text = "";
            tbThanhTien.Text = "";
            nudSoLuong.Value = 1;
        }
        private void btThem_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbTenSach.Text))
            {
                tt++;
                DataRow r = tb.NewRow();
                r["TT"] = tt.ToString();
                r["Mã sách"] = tbIdSach.Text;
                r["Tên sách"] = tbTenSach.Text;
                r["Số lượng"] = nudSoLuong.Value;
                r["Đơn giá"] = donGia;
                r["Thành tiền"] = thanhTien;
                tb.Rows.Add(r);

                dtgvDanhSachVatPham.DataSource = tb;
                //dtgvDanhSachVatPham.Columns["Mã sách"].Visible = false;
                clearTB();
                tinhTien();
            }
        }
        int tongTien;
        private void tinhTien()
        {
            tongTien = 0;
            foreach (DataRow r in tb.Rows)
            {
                tongTien += int.Parse(r["Thành tiền"].ToString());
            }
            tbTongTien.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", tongTien);
        }

        int soThuTuSelected;
        private void btSua_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbIdSach.Text) && editMode)
            {
                foreach (DataRow r in tb.Rows)
                {
                    if (int.Parse(r["TT"].ToString()) == soThuTuSelected)
                    {
                        r["Số lượng"] = nudSoLuong.Value;
                        r["Thành tiền"] = thanhTien;

                        dtgvDanhSachVatPham.DataSource = tb;
                        editMode = false;
                        clearTB();
                        tinhTien();
                        return;
                    }
                }
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbIdSach.Text) && editMode)
            {
                foreach (DataRow r in tb.Rows)
                {
                    if (int.Parse(r["TT"].ToString()) == soThuTuSelected)
                    {
                        r.Delete();
                        dtgvDanhSachVatPham.DataSource = tb;
                        editMode = false;
                        clearTB();
                        tinhTien();
                        return;
                    }
                }
            }
        }

        bool editMode;
        private void dtgvDanhSachVatPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgvDanhSachVatPham.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dtgvDanhSachVatPham.CurrentRow.Selected = true;
                    tbIdSach.Text = dtgvDanhSachVatPham.Rows[e.RowIndex].Cells["Mã sách"].FormattedValue.ToString();
                    tbTenSach.Text = dtgvDanhSachVatPham.Rows[e.RowIndex].Cells["Tên sách"].FormattedValue.ToString();
                    donGia = int.Parse(dtgvDanhSachVatPham.Rows[e.RowIndex].Cells["Đơn giá"].FormattedValue.ToString());
                    thanhTien = donGia * nudSoLuong.Value;
                    tbDonGia.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", donGia);
                    tbThanhTien.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", thanhTien);
                    nudSoLuong.Value = int.Parse(dtgvDanhSachVatPham.Rows[e.RowIndex].Cells["Số lượng"].FormattedValue.ToString());

                    soThuTuSelected = int.Parse(dtgvDanhSachVatPham.Rows[e.RowIndex].Cells["TT"].FormattedValue.ToString());
                    editMode = true;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

      
        private void btLuu_Click(object sender, EventArgs e)
        {
            DataTable data = (DataTable)(dtgvDanhSachVatPham.DataSource);
            if (data.Rows.Count > 1)
            {
                try
                {
                    MHoaDon mhd = new MHoaDon();
                    mhd.Ngaylap = Convert.ToDateTime(tbNgayLapHoaDon.Text);
                    //mhd.Id_nhanvien = Convert.ToInt32(cbTheLoai.SelectedValue.ToString());
                    mhd.Id_nhanvien = 1;
                    mhd.Tongtien = int.Parse(tbTongTien.Text);
                    chd.ThemHoaDon(mhd);


                    //List<MCTHD> lcthd = new List<MCTHD>();
                    foreach (DataRow r in data.Rows)
                    {
                        MCTHD m = new MCTHD();
                        //m.Id = int.Parse(r["id"].ToString());
                        //m.Id_hoadon = int.Parse(r["id_hoadon"].ToString());
                        m.Id_sach = int.Parse(r["Mã sách"].ToString());
                        m.Soluong = int.Parse(r["Số lượng"].ToString());
                        
                        ccthd.ThemCTHD(m);
                        //lcthd.Add(m);
                    }

                    
                    MessageBox.Show("Đã lưu hóa đơn!");
                    this.dtgvDanhSachVatPham.DataSource = null;
                    this.dtgvDanhSachVatPham.Rows.Clear();
                }
                catch (Exception)
                {
                    MessageBox.Show("Dữ liệu đã tồn tại hoặc không hợp lệ.");
                    return;
                }
            }
        }


        private void thôngTinTàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fThongTinTaiKhoan f = new fThongTinTaiKhoan();
            f.tenDangNhap = tbTenDangNhap.Text;
            f.ShowDialog();
        }

        private void btDangXuat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
