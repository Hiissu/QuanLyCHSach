﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCHSach.Controller;
using QuanLyCHSach.Model;
namespace QuanLyCHSach.View
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();
        }


        CSach cs = new CSach();
        CTheLoai ctl = new CTheLoai();
        CNhaXuatBan cnxb = new CNhaXuatBan();
        CTaiKhoan ctk = new CTaiKhoan();
        CNhanVien cnv = new CNhanVien();


        private void fAdmin_Load(object sender, EventArgs e)
        {
            dtpNgayXuatBan.Value = DateTime.Today;
            LoadDuLieuSach();
            LoadDuLieuTheLoai();
            LoadDuLieuNXB();
            LoadDuLieuTaiKhoan();
            LoadDuLieuNhanVien();

            Dictionary<int, string> cbSourceChucVu = new Dictionary<int, string>();
            cbSourceChucVu.Add(1, "Nhân viên");
            cbSourceChucVu.Add(2, "Quản lý");
            cbChucVu.DataSource = new BindingSource(cbSourceChucVu, null);
            cbChucVu.DisplayMember = "Value";
            cbChucVu.ValueMember = "Key";

            Dictionary<bool, string> cbSource = new Dictionary<bool, string>();
            cbSource.Add(false, "Nhân viên");
            cbSource.Add(true, "Quản lý");
            cbLoaiTaiKhoan.DataSource = new BindingSource(cbSource, null);
            cbLoaiTaiKhoan.DisplayMember = "Value";
            cbLoaiTaiKhoan.ValueMember = "Key";


        }




        #region Sach

    
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
                m.Ngayxuatban = r["ngayxuatban"].ToString();
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
                tb.Columns.Add("Tên", typeof(String));
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
                    r["Tên"] = m.Ten;
                    r["Tác giả"] = m.Tacgia;
                    r["Thể loại"] = m.Tentheloai;
                    r["Ngày xuất bản"] = m.Ngayxuatban;
                    r["Nhà xuất bản"] = m.Tennhaxuatban;
                    r["Số lượng"] = m.Soluong;
                    r["Đơn giá"] = m.Dongia;

                    tb.Rows.Add(r);
                }
                dtgvSach.DataSource = tb;
                dtgvSach.Columns["Id"].Visible = false;
            }
            DataTable dttl = ctl.HienThiTatCaTheLoai();
            cbTheLoai.DataSource = dttl;
            cbTheLoai.DisplayMember = "ten";
            cbTheLoai.ValueMember = "id";

            DataTable dtnxb = cnxb.HienThiTatCaNhaXuatBan();
            cbNhaXuatBan.DataSource = dtnxb;
            cbNhaXuatBan.DisplayMember = "ten";
            cbNhaXuatBan.ValueMember = "id";
        }
        private void btThem_Click(object sender, EventArgs e)
        {
            MSach ms = new MSach();
            ms.Ten = tbTenSach.Text;
            ms.Id_theloai = Convert.ToInt32(cbTheLoai.SelectedValue.ToString());
            ms.Tacgia = tbTacGia.Text;
            ms.Ngayxuatban = dtpNgayXuatBan.Value.ToString("yyyy-MM-dd");
            ms.Id_nhaxuatban = Convert.ToInt32(cbNhaXuatBan.SelectedValue.ToString());
            ms.Dongia = Convert.ToDouble(nudDonGia.Value);
            ms.Soluong = Convert.ToInt32(nudSoLuong.Value);
            cs.ThemSach(ms);
            XoaDuLieuTabPageSach();

            LoadDuLieuSach();

            if (!String.IsNullOrEmpty(tbTenSach.Text))
            {
                try
                {
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("Dữ liệu đã tồn tại hoặc không hợp lệ.");
                    return;
                    throw;
                }
            }
        }

        private void btSua_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbIdSach.Text))
            {
                try
                {
                    MSach mss = new MSach();
                    mss.Id = int.Parse(tbIdSach.Text);
                    mss.Ten = tbTenSach.Text;
                    mss.Id_theloai = Convert.ToInt32(cbTheLoai.SelectedValue.ToString());
                    mss.Tacgia = tbTacGia.Text;
                    mss.Ngayxuatban = dtpNgayXuatBan.Value.ToString("yyyy-MM-dd");
                    mss.Id_nhaxuatban = Convert.ToInt32(cbNhaXuatBan.SelectedValue.ToString());
                    mss.Dongia = Convert.ToDouble(nudDonGia.Value);
                    mss.Soluong = Convert.ToInt32(nudSoLuong.Value);
                    cs.CapNhatSach(mss, mss.Id);
                    LoadDuLieuSach();
                    XoaDuLieuTabPageSach();
                }
                catch (Exception)
                {
                    MessageBox.Show("Dữ liệu đã tồn tại hoặc không hợp lệ.");
                    return;
                    throw;
                }
               

            }
            else
            {
                MessageBox.Show("Hãy chọn sách cần sửa.");
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbIdSach.Text))
            {
                cs.XoaSach(tbIdSach.Text);
                LoadDuLieuSach();
                XoaDuLieuTabPageSach();

            }
            else
            {
                MessageBox.Show("Hãy chọn sách cần xóa.");
            }
        }

        private void XoaDuLieuTabPageSach()
        {
            tbIdSach.Text = "";
            tbTenSach.Text = "";
            tbTacGia.Text = "";
            nudDonGia.Value = 0;
            nudSoLuong.Value = 0;
            dtpNgayXuatBan.Value = DateTime.Today;
        }

        

        private void dtgvSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgvSach.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dtgvSach.CurrentRow.Selected = true;
                    tbIdSach.Text = dtgvSach.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString();
                    tbTenSach.Text = dtgvSach.Rows[e.RowIndex].Cells["Tên"].FormattedValue.ToString();

                    //cbTheLoai.Items.Add(dtgvSach.Rows[e.RowIndex].Cells["Thể loại"].FormattedValue.ToString());
                    //cbTheLoai.SelectedItem = cbTheLoai.Items[0];

                    DataTable dt = cs.HienThiTheoIdSach(tbIdSach.Text);
                    foreach (DataRow r in dt.Rows)
                    {
                        cbTheLoai.SelectedValue = int.Parse(r["id_theloai"].ToString());
                        cbNhaXuatBan.SelectedValue = int.Parse(r["id_nhaxuatban"].ToString());
                    }

                    tbTacGia.Text = dtgvSach.Rows[e.RowIndex].Cells["Tác giả"].FormattedValue.ToString();

                    dtpNgayXuatBan.Value = Convert.ToDateTime(dtgvSach.Rows[e.RowIndex].Cells["Ngày xuất bản"].FormattedValue.ToString());

                    nudDonGia.Value = Convert.ToInt32(dtgvSach.Rows[e.RowIndex].Cells["Đơn giá"].FormattedValue.ToString());
                    nudSoLuong.Value = Convert.ToInt32(dtgvSach.Rows[e.RowIndex].Cells["Số lượng"].FormattedValue.ToString());
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }


        #endregion

        #region TheLoai

        private void XoaDuLieuTabPageTheLoai()
        {
            tbIdTheLoai.Text = "";
            tbTenTheLoai.Text = "";
        }

        private void LoadDuLieuTheLoai()
        {
            List<MTheLoai> ls = new List<MTheLoai>();
            DataTable dttl = ctl.HienThiTatCaTheLoai();
            foreach (DataRow r in dttl.Rows)
            {
                MTheLoai m = new MTheLoai();
                m.Id = int.Parse(r["id"].ToString());
                m.Ten = r["ten"].ToString();
                ls.Add(m);
            }

            if (ls.Count != 0)
            {
                DataTable tb = new DataTable();
                tb.Columns.Add("Id", typeof(String));
                tb.Columns.Add("TT", typeof(int));
                tb.Columns.Add("Tên", typeof(String));
                int tt = 0;
                foreach (MTheLoai m in ls)
                {
                    tt++;
                    DataRow r = tb.NewRow();
                    r["Id"] = m.Id.ToString();
                    r["TT"] = tt.ToString();
                    r["Tên"] = m.Ten;

                    tb.Rows.Add(r);
                }
                dtgvTheLoai.DataSource = tb;
                dtgvTheLoai.Columns["Id"].Visible = false;
            }

        }
        private void btThemTheLoai_Click(object sender, EventArgs e)
        {
            
            if (!String.IsNullOrEmpty(tbTenTheLoai.Text))
            {
                try
                {
                    ctl.ThemTheLoai(tbTenTheLoai.Text);
                    XoaDuLieuTabPageTheLoai();
                    LoadDuLieuTheLoai();

                    DataTable dttl = ctl.HienThiTatCaTheLoai();
                    cbTheLoai.DataSource = dttl;
                    cbTheLoai.DisplayMember = "ten";
                    cbTheLoai.ValueMember = "id";
                   

                }
                catch (Exception)
                {
                    MessageBox.Show("Dữ liệu đã tồn tại hoặc không hợp lệ.");
                    return;
                    throw;
                }
            }
        }

        private void btSuaTheLoai_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbIdTheLoai.Text))
            {
                ctl.CapNhatTheLoai(tbTenTheLoai.Text, int.Parse(tbIdTheLoai.Text));
                XoaDuLieuTabPageTheLoai();
                LoadDuLieuTheLoai();


                DataTable dttl = ctl.HienThiTatCaTheLoai();
                cbTheLoai.DataSource = dttl;
                cbTheLoai.DisplayMember = "ten";
                cbTheLoai.ValueMember = "id";
            }
            else
            {
                MessageBox.Show("Hãy chọn thể loại cần sửa.");
            }
        }

        private void btXoaTheLoai_Click(object sender, EventArgs e)
        {

            if (!String.IsNullOrEmpty(tbIdTheLoai.Text))
            {
                ctl.XoaTheLoai(int.Parse(tbIdTheLoai.Text));
                XoaDuLieuTabPageTheLoai();
                LoadDuLieuTheLoai();

                DataTable dttl = ctl.HienThiTatCaTheLoai();
                cbTheLoai.DataSource = dttl;
                cbTheLoai.DisplayMember = "ten";
                cbTheLoai.ValueMember = "id";

            }
            else
            {
                MessageBox.Show("Hãy chọn thể loại cần xóa.");
            }
        }

        private void dtgvTheLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgvTheLoai.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dtgvTheLoai.CurrentRow.Selected = true;
                    tbIdTheLoai.Text = dtgvTheLoai.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
                    tbTenTheLoai.Text = dtgvTheLoai.Rows[e.RowIndex].Cells["ten"].FormattedValue.ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        #endregion

        #region NhaXuatBan

        private void XoaDuLieuTabPageNXB()
        {
            tbIdNhaXuatBan.Text = "";
            tbTenNhaXuatBan.Text = "";
            tbDiaChiNxb.Text = "";
        }
        private void LoadDuLieuNXB()
        {
            List<MNhaXuatBan> ls = new List<MNhaXuatBan>();
            DataTable dtnxb = cnxb.HienThiTatCaNhaXuatBan();
            foreach (DataRow r in dtnxb.Rows)
            {
                MNhaXuatBan m = new MNhaXuatBan();
                m.Id = int.Parse(r["id"].ToString());
                m.Ten = r["ten"].ToString();
                m.Diachi = r["diachi"].ToString();
                ls.Add(m);
            }

            if (ls.Count != 0)
            {
                DataTable tb = new DataTable();
                tb.Columns.Add("Id", typeof(String));
                tb.Columns.Add("TT", typeof(int));
                tb.Columns.Add("Tên", typeof(String));
                tb.Columns.Add("Địa Chỉ", typeof(String));
                int tt = 0;
                foreach (MNhaXuatBan m in ls)
                {
                    tt++;
                    DataRow r = tb.NewRow();
                    r["Id"] = m.Id.ToString();
                    r["TT"] = tt.ToString();
                    r["Tên"] = m.Ten;
                    r["Địa Chỉ"] = m.Diachi;

                    tb.Rows.Add(r);
                }
                dtgvNhaXuatBan.DataSource = tb;
                dtgvNhaXuatBan.Columns["Id"].Visible = false;
            }

        }
        private void btThemNhaXuatBan_Click(object sender, EventArgs e)
        {
            MNhaXuatBan mnxb = new MNhaXuatBan();

            if (!String.IsNullOrEmpty(tbTenNhaXuatBan.Text))
            {
                try
                {
                    mnxb.Ten = tbTenNhaXuatBan.Text;
                    mnxb.Diachi = tbDiaChiNxb.Text;
                    cnxb.ThemNhaXuatBan(mnxb);
                    XoaDuLieuTabPageNXB();
                    LoadDuLieuNXB();

                    DataTable dtnxb = cnxb.HienThiTatCaNhaXuatBan();
                    cbNhaXuatBan.DataSource = dtnxb;
                    cbNhaXuatBan.DisplayMember = "ten";
                    cbNhaXuatBan.ValueMember = "id";
                }
                catch (Exception)
                {
                    MessageBox.Show("Dữ liệu đã tồn tại hoặc không hợp lệ.");
                    return;
                    throw;
                }
            }
        }

        private void btSuaNhaXuatBan_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbIdNhaXuatBan.Text))
            {
                MNhaXuatBan mnxbb = new MNhaXuatBan();
                mnxbb.Ten = tbTenTheLoai.Text;
                cnxb.CapNhatNhaXuatBan(mnxbb, tbIdNhaXuatBan.Text);
                XoaDuLieuTabPageTheLoai();
                LoadDuLieuNXB();

                DataTable dtnxb = cnxb.HienThiTatCaNhaXuatBan();
                cbNhaXuatBan.DataSource = dtnxb;
                cbNhaXuatBan.DisplayMember = "ten";
                cbNhaXuatBan.ValueMember = "id";
            }
            else
            {
                MessageBox.Show("Hãy chọn nhà xuất bản cần sửa.");
            }
        }
        

        private void btXoaNhaXuatBan_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbIdNhaXuatBan.Text))
            {
                cnxb.XoaNhaXuatBan(tbIdNhaXuatBan.Text);
                XoaDuLieuTabPageNXB();
                LoadDuLieuNXB();

                DataTable dtnxb = cnxb.HienThiTatCaNhaXuatBan();
                cbNhaXuatBan.DataSource = dtnxb;
                cbNhaXuatBan.DisplayMember = "ten";
                cbNhaXuatBan.ValueMember = "id";

            }
            else
            {
                MessageBox.Show("Hãy chọn nhà xuất bản cần xóa.");
            }
        }
        private void dtgvNhaXuatBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgvTheLoai.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dtgvNhaXuatBan.CurrentRow.Selected = true;
                    tbIdNhaXuatBan.Text = dtgvNhaXuatBan.Rows[e.RowIndex].Cells["id"].FormattedValue.ToString();
                    tbTenNhaXuatBan.Text = dtgvNhaXuatBan.Rows[e.RowIndex].Cells["ten"].FormattedValue.ToString();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region TaiKhoan

        private void XoaDuLieuTabPageTaiKhoan()
        {
            tbTenDangNhap.Text = "";
            tbMatKhau.Text = "";
            tbTenHienThi.Text = "";
            cbNhanVien.SelectedItem = null;
        }
        
        private void LoadDuLieuTaiKhoan()
        {
            List<MTaiKhoan> ls = new List<MTaiKhoan>();
            DataTable dttk = ctk.HienThiTatCaTaiKhoan();
            foreach (DataRow r in dttk.Rows)
            {
                MTaiKhoan m = new MTaiKhoan();
                m.Id = int.Parse(r["id"].ToString());
                m.Tendangnhap = r["tendangnhap"].ToString();
                m.Tenhienthi = r["tenhienthi"].ToString();
                m.Loaitaikhoan = Convert.ToBoolean(r["loaitaikhoan"].ToString());
                m.Matkhau = r["matkhau"].ToString();
                m.Tennhanvien = r["tennhanvien"].ToString();
                ls.Add(m);
            }

            if (ls.Count != 0)
            {
                DataTable tb = new DataTable();
                tb.Columns.Add("Id", typeof(String));
                tb.Columns.Add("TT", typeof(int));
                tb.Columns.Add("Tên đăng nhập", typeof(String));
                tb.Columns.Add("Mật khẩu", typeof(String));
                tb.Columns.Add("Tên hiển thị", typeof(String));
                tb.Columns.Add("Loại tài khoản", typeof(String));
                tb.Columns.Add("Nhân viên", typeof(String));
                int tt = 0;
                foreach (MTaiKhoan m in ls)
                {
                    tt++;
                    DataRow r = tb.NewRow();
                    r["Id"] = m.Id.ToString();
                    r["TT"] = tt.ToString();
                    r["Tên đăng nhập"] = m.Tendangnhap;
                    r["Mật khẩu"] = m.Matkhau;
                    r["Tên hiển thị"] = m.Tenhienthi;
                    bool o = m.Loaitaikhoan;
                    if (o)
                        r["Loại tài khoản"] = "Quản lý";
                    else
                        r["Loại tài khoản"] = "Nhân viên";


                    r["Nhân viên"] = m.Tennhanvien;

                    tb.Rows.Add(r);
                }
                dtgvTaiKhoan.DataSource = tb;
                dtgvTaiKhoan.Columns["Id"].Visible = false;

                
            }
        }
        
        private void LoadDuLieuComboboxNhanVien()
        {
            //cbNhanVien.Items.Clear();
            DataTable dtnv = cnv.HienThiTatCaNhanVien();
            Dictionary<int, string> dicNhanVien = new Dictionary<int, string>();

            foreach (DataRow r in dtnv.Rows)
            {
                int id = int.Parse(r["id"].ToString());
                string ten = r["ten"].ToString();
                int o = int.Parse(r["id_chucvu"].ToString());
                string chucvu = string.Empty;
                if (o == 1)
                    chucvu = "Nhân viên";
                else
                    chucvu = "Quản lý";
                string ngaysinh = r["ngaysinh"].ToString();
                string item = $"{ten} \t {chucvu} \t {ngaysinh}";
                dicNhanVien.Add(id, item);
            }

            cbNhanVien.DataSource = new BindingSource(dicNhanVien, null); ;
            cbNhanVien.DisplayMember = "Value";
            cbNhanVien.ValueMember = "Key";
        }


        private void btThemTaiKhoan_Click(object sender, EventArgs e)
        {
            MTaiKhoan mtk = new MTaiKhoan();
            
            if (!String.IsNullOrEmpty(tbTenDangNhap.Text) && !String.IsNullOrEmpty(tbMatKhau.Text))
            {
                try
                {
                    if (cbNhanVien.SelectedItem == null)
                    {
                        MessageBox.Show("Hãy chọn nhân viên!");
                    }

                    mtk.Tendangnhap = tbTenDangNhap.Text;
                    mtk.Matkhau = tbMatKhau.Text;
                    mtk.Tenhienthi = tbTenHienThi.Text;
                    mtk.Loaitaikhoan = Convert.ToBoolean(cbLoaiTaiKhoan.SelectedValue.ToString());
                    mtk.Id_nhanvien = Convert.ToInt32(cbNhanVien.SelectedValue.ToString());

                    ctk.ThemTaiKhoan(mtk);
                    XoaDuLieuTabPageTaiKhoan();
                    LoadDuLieuTaiKhoan();
                }
                catch (Exception)
                {
                    MessageBox.Show("Dữ liệu đã tồn tại hoặc không hợp lệ.");
                    return;
                    throw;
                }
            }
        }


        private void btSuaTaiKhoan_Click(object sender, EventArgs e)
        {
            if (cbNhanVien.SelectedItem == null)
            {
                MessageBox.Show("Hãy chọn nhân viên!");
            }

            if (!String.IsNullOrEmpty(tbTenDangNhap.Text) && !String.IsNullOrEmpty(tbMatKhau.Text))
            {
                MTaiKhoan mtk = new MTaiKhoan();
                mtk.Tendangnhap = tbTenDangNhap.Text;
                mtk.Matkhau = tbMatKhau.Text;
                mtk.Tenhienthi = tbTenHienThi.Text;
                mtk.Loaitaikhoan = Convert.ToBoolean(cbLoaiTaiKhoan.SelectedValue.ToString());
                mtk.Id_nhanvien = Convert.ToInt32(cbNhanVien.SelectedValue.ToString());
                ctk.CapNhatTaiKhoan(mtk, tbIdTaiKhoan.Text);
                XoaDuLieuTabPageTaiKhoan();
                LoadDuLieuTaiKhoan();
            }
            else
            {
                MessageBox.Show("Hãy chọn tài khoản cần sửa.");
            }
        }

        private void btXoaTaiKhoan_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbIdNhaXuatBan.Text))
            {
                ctk.XoaTaiKhoan(int.Parse(tbIdTaiKhoan.Text));
                XoaDuLieuTabPageTaiKhoan();
                LoadDuLieuTaiKhoan();
            }
            else
            {
                MessageBox.Show("Hãy chọn tài khoản cần xóa.");
            }
        }


        private void dtgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgvTaiKhoan.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dtgvTaiKhoan.CurrentRow.Selected = true;
                    tbIdTaiKhoan.Text = dtgvTaiKhoan.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString();
                    tbTenDangNhap.Text = dtgvTaiKhoan.Rows[e.RowIndex].Cells["Tên đăng nhập"].FormattedValue.ToString();
                    tbTenHienThi.Text = dtgvTaiKhoan.Rows[e.RowIndex].Cells["Tên hiển thị"].FormattedValue.ToString();
                    tbMatKhau.Text = dtgvTaiKhoan.Rows[e.RowIndex].Cells["Mật khẩu"].FormattedValue.ToString();
                    //cbLoaiTaiKhoan.SelectedValue = dtgvTaiKhoan.Rows[e.RowIndex].Cells["Loại tài khoản"].FormattedValue.ToString();
                    //cbNhanVien.SelectedValue = dtgvTaiKhoan.Rows[e.RowIndex].Cells["Nhân viên"].FormattedValue.ToString();
                    if (dtgvTaiKhoan.Rows[e.RowIndex].Cells["Loại tài khoản"].FormattedValue.ToString() == "Nhân viên")
                        cbChucVu.SelectedValue = false;
                    else
                        cbChucVu.SelectedValue = true;

                    DataTable dt = ctk.HienThiTheoIdTaiKhoan(tbIdTaiKhoan.Text);
                    foreach (DataRow r in dt.Rows)
                    {
                        cbNhanVien.SelectedValue = int.Parse(r["id_nhanvien"].ToString());
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion

        #region NhanVien
        private void XoaDuLieuTabPageNhanVien()
        {
            tbIdNhanVien.Text = "";
            tbTenNhanVien.Text = "";
            tbDiaChiNhanVien.Text = "";
            tbSdt.Text = "";
        }

        private void LoadDuLieuNhanVien()
        {
            List<MNhanVien> ls = new List<MNhanVien>();
            DataTable dtnv = cnv.HienThiTatCaNhanVien();
            foreach (DataRow r in dtnv.Rows)
            {
                MNhanVien m = new MNhanVien();
                m.Id = int.Parse(r["id"].ToString());
                m.Ten = r["ten"].ToString();
                m.Diachi = r["diachi"].ToString();
                m.Ngaysinh = r["ngaysinh"].ToString();
                m.Sdt = r["sdt"].ToString();
                m.Id_chucvu = int.Parse(r["id_chucvu"].ToString());
                ls.Add(m);
            }

            if (ls.Count != 0)
            {
                DataTable tb = new DataTable();
                tb.Columns.Add("Id", typeof(String));
                tb.Columns.Add("TT", typeof(int));
                tb.Columns.Add("Tên", typeof(String));
                tb.Columns.Add("Địa Chỉ", typeof(String));
                tb.Columns.Add("Ngày sinh", typeof(String));
                tb.Columns.Add("Sđt", typeof(String));
                tb.Columns.Add("Chức vụ", typeof(String));
                int tt = 0;
                foreach (MNhanVien m in ls)
                {
                    tt++;
                    DataRow r = tb.NewRow();
                    r["Id"] = m.Id.ToString();
                    r["TT"] = tt.ToString();
                    r["Tên"] = m.Ten;
                    r["Địa Chỉ"] = m.Diachi;
                    r["Ngày sinh"] = m.Ngaysinh;
                    r["Sđt"] = m.Sdt;
                    int o = m.Id_chucvu;
                    if (o == 1)
                        r["Chức vụ"] = "Nhân viên";
                    else
                        r["Chức vụ"] = "Quản lý";

                    tb.Rows.Add(r);
                }
                dtgvNhanVien.DataSource = tb;
                dtgvNhanVien.Columns["Id"].Visible = false;
            }
            LoadDuLieuComboboxNhanVien();
        }

        private void btThemNhanVien_Click(object sender, EventArgs e)
        {
            MNhanVien mnv = new MNhanVien();

            if (!String.IsNullOrEmpty(tbTenNhanVien.Text))
            {
                try
                {
                    mnv.Ten = tbTenNhanVien.Text;
                    mnv.Diachi = tbDiaChiNhanVien.Text;
                    mnv.Sdt = tbSdt.Text;
                    mnv.Ngaysinh = dtpNgaySinh.Value.ToString("yyyy-MM-dd");
                    mnv.Id_chucvu = Convert.ToInt32(cbChucVu.SelectedValue.ToString());
                    cnv.ThemNhanVien(mnv);
                    XoaDuLieuTabPageNhanVien();
                    LoadDuLieuNhanVien();

                    
                }
                catch (Exception)
                {
                    MessageBox.Show("Dữ liệu đã tồn tại hoặc không hợp lệ.");
                    return;
                    throw;
                }
            }
        }

        private void btSuaNhanVien_Click(object sender, EventArgs e)
        {
            MNhanVien mnv = new MNhanVien();

            if (!String.IsNullOrEmpty(tbIdNhanVien.Text))
            {
                mnv.Ten = tbTenNhanVien.Text;
                mnv.Diachi = tbDiaChiNhanVien.Text;
                mnv.Sdt = tbSdt.Text;
                mnv.Ngaysinh = dtpNgaySinh.Value.ToString("yyyy-MM-dd");
                mnv.Id_chucvu = Convert.ToInt32(cbChucVu.SelectedValue.ToString());
                cnv.CapNhatNhanVien(mnv, tbIdNhanVien.Text);
                XoaDuLieuTabPageNhanVien();
                LoadDuLieuNhanVien();

            }
            else
            {
                MessageBox.Show("Hãy chọn thông tin nhân viên cần sửa.");
            }
        }

        private void btXoaNhanVien_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbIdNhanVien.Text))
            {
                cnv.XoaNhanVien(int.Parse(tbIdNhanVien.Text));
                XoaDuLieuTabPageNhanVien();
                LoadDuLieuNhanVien();
            }
            else
            {
                MessageBox.Show("Hãy chọn nhân viên cần xóa.");
            }
        }

        private void dtgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgvNhanVien.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dtgvNhanVien.CurrentRow.Selected = true;
                    tbIdNhanVien.Text = dtgvNhanVien.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString();
                    tbTenNhanVien.Text = dtgvNhanVien.Rows[e.RowIndex].Cells["Tên"].FormattedValue.ToString();
                    tbSdt.Text = dtgvNhanVien.Rows[e.RowIndex].Cells["Sđt"].FormattedValue.ToString();
                    tbDiaChiNhanVien.Text = dtgvNhanVien.Rows[e.RowIndex].Cells["Địa chỉ"].FormattedValue.ToString();
                    dtpNgaySinh.Value = Convert.ToDateTime(dtgvNhanVien.Rows[e.RowIndex].Cells["Ngày sinh"].FormattedValue.ToString());

                    if (dtgvNhanVien.Rows[e.RowIndex].Cells["Chức vụ"].FormattedValue.ToString() == "Nhân viên")
                        cbChucVu.SelectedValue = 1;
                    else
                        cbChucVu.SelectedValue = 2;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion

        
    }
}