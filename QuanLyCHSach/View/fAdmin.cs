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
        CHoaDon chd = new CHoaDon();
        CCTHD ccthd = new CCTHD();

        private void fAdmin_Load(object sender, EventArgs e)
        {
            dtpNgayXuatBan.Value = DateTime.Today;

            DataTable dts = cs.HienThiTatCaSach();
            LoadDuLieuSach(dts);

            DataTable dttl = ctl.HienThiTatCaTheLoai();
            LoadDuLieuTheLoai(dttl);

            DataTable dtnxb = cnxb.HienThiTatCaNhaXuatBan();
            LoadDuLieuNXB(dtnxb);

            DataTable dttk = ctk.HienThiTatCaTaiKhoan();
            LoadDuLieuTaiKhoan(dttk);

            DataTable dtnv = cnv.HienThiTatCaNhanVien();
            LoadDuLieuNhanVien(dtnv);

            DataTable data = chd.HienThiHoaDon(dtpTuNgay.Value, dtpDenNgay.Value);
            LoadDuLieuHoaDon(data);


            Dictionary<string, string> dicTimKiem = new Dictionary<string, string>();
            dicTimKiem.Add("s.ten", "Tên sách");
            dicTimKiem.Add("tl.ten", "Thể loại");
            dicTimKiem.Add("s.tacgia", "Tác giả");
            dicTimKiem.Add("nxb.ten", "Nhà xuất bản");

            cbTimKiem.DataSource = new BindingSource(dicTimKiem, null);
            cbTimKiem.DisplayMember = "Value";
            cbTimKiem.ValueMember = "Key";

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

            dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpDenNgay.Value = dtpTuNgay.Value.AddMonths(1).AddDays(-1);
        }




        #region Sach

        private void btTimkiem_Click(object sender, EventArgs e)
        {
            DataTable dttks = cs.TimKiem(cbTimKiem.SelectedValue, tbTimKiem.Text);
            LoadDuLieuSach(dttks);
        }

        private void btReload_Click(object sender, EventArgs e)
        {
            DataTable dts = cs.HienThiTatCaSach();
            LoadDuLieuSach(dts);
        }
        private void LoadDuLieuSach(DataTable data)   
        {
            List<MSach> ls = new List<MSach>();
            foreach (DataRow r in data.Rows)
            {
                MSach m = new MSach();
                m.Id = int.Parse(r["id"].ToString());
                m.Ten = r["ten"].ToString();
                m.Tacgia = r["tacgia"].ToString();
                m.Tentheloai = r["tentheloai"].ToString();
                m.Ngayxuatban = DateTime.Parse(r["ngayxuatban"].ToString());
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
                tb.Columns.Add("Ngày xuất bản", typeof(string));
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
                    r["Ngày xuất bản"] = m.Ngayxuatban.ToString("dd-MM-yyyy");
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
            
            if (!String.IsNullOrEmpty(tbTenSach.Text))
            {
                try
                {
                    MSach ms = new MSach();
                    ms.Ten = tbTenSach.Text;
                    ms.Id_theloai = Convert.ToInt32(cbTheLoai.SelectedValue.ToString());
                    ms.Tacgia = tbTacGia.Text;
                    ms.Ngayxuatban = Convert.ToDateTime(dtpNgayXuatBan.Value.ToString("dd-MM-yyyy"));
                    ms.Id_nhaxuatban = Convert.ToInt32(cbNhaXuatBan.SelectedValue.ToString());
                    ms.Dongia = Convert.ToDouble(nudDonGia.Value);
                    ms.Soluong = Convert.ToInt32(nudSoLuong.Value);
                    cs.ThemSach(ms);
                    XoaDuLieuTabPageSach();

                    DataTable dts = cs.HienThiTatCaSach();
                    LoadDuLieuSach(dts);

                }
                catch (Exception)
                {
                    MessageBox.Show("Dữ liệu đã tồn tại hoặc không hợp lệ.");
                    return;
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
                    mss.Ngayxuatban = Convert.ToDateTime(dtpNgayXuatBan.Value.ToString("dd-MM-yyyy"));
                    mss.Id_nhaxuatban = Convert.ToInt32(cbNhaXuatBan.SelectedValue.ToString());
                    mss.Dongia = Convert.ToDouble(nudDonGia.Value);
                    mss.Soluong = Convert.ToInt32(nudSoLuong.Value);
                    cs.CapNhatSach(mss, mss.Id);

                    DataTable dts = cs.HienThiTatCaSach();
                    LoadDuLieuSach(dts);
                    XoaDuLieuTabPageSach();
                }
                catch (Exception)
                {
                    MessageBox.Show("Dữ liệu đã tồn tại hoặc không hợp lệ.");
                    return;
                }
               

            }
            else
            {
                MessageBox.Show("Hãy chọn sách cần sửa.");
            }
        }

        private void btXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(tbIdSach.Text))
                {
                    cs.XoaSach(tbIdSach.Text);
                    this.dtgvSach.DataSource = null;
                    this.dtgvSach.Rows.Clear();

                    DataTable dts = cs.HienThiTatCaSach();
                    LoadDuLieuSach(dts);
                    XoaDuLieuTabPageSach();
                }
                else
                {
                    MessageBox.Show("Hãy chọn sách cần xóa.");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xóa vì đang có ràng buộc.");
                return;
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
                return;
            }

        }


        #endregion

        #region TheLoai

        private void XoaDuLieuTabPageTheLoai()
        {
            tbIdTheLoai.Text = "";
            tbTenTheLoai.Text = "";
        }
        private void btTimKiemTheLoai_Click(object sender, EventArgs e)
        {
            DataTable data = ctl.TimKiem(tbTimKiemTenTheLoai.Text);
            LoadDuLieuTheLoai(data);
        }

        private void btReloadTheLoai_Click(object sender, EventArgs e)
        {
            DataTable data = ctl.HienThiTatCaTheLoai();
            LoadDuLieuTheLoai(data);
        }

        private void LoadDuLieuTheLoai(DataTable data)
        {
            List<MTheLoai> ls = new List<MTheLoai>();
            foreach (DataRow r in data.Rows)
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
                    DataTable data = ctl.HienThiTatCaTheLoai();
                    LoadDuLieuTheLoai(data);

                    DataTable dttl = ctl.HienThiTatCaTheLoai();
                    cbTheLoai.DataSource = dttl;
                    cbTheLoai.DisplayMember = "ten";
                    cbTheLoai.ValueMember = "id";
                   

                }
                catch (Exception)
                {
                    MessageBox.Show("Dữ liệu đã tồn tại hoặc không hợp lệ.");
                    return;
                }
            }
        }

        private void btSuaTheLoai_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbIdTheLoai.Text))
            {
                ctl.CapNhatTheLoai(tbTenTheLoai.Text, int.Parse(tbIdTheLoai.Text));
                XoaDuLieuTabPageTheLoai();
                DataTable data = ctl.HienThiTatCaTheLoai();
                LoadDuLieuTheLoai(data);


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
            try
            {
                if (!String.IsNullOrEmpty(tbIdTheLoai.Text))
                {
                    ctl.XoaTheLoai(int.Parse(tbIdTheLoai.Text));
                    XoaDuLieuTabPageTheLoai();
                    this.dtgvTheLoai.DataSource = null;
                    this.dtgvTheLoai.Rows.Clear();

                    DataTable data = ctl.HienThiTatCaTheLoai();
                    LoadDuLieuTheLoai(data);

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
            catch (Exception)
            {
                MessageBox.Show("Không thể xóa vì đang có ràng buộc.");
                return;
            }
            
        }

        private void dtgvTheLoai_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgvTheLoai.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dtgvTheLoai.CurrentRow.Selected = true;
                    tbIdTheLoai.Text = dtgvTheLoai.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString();
                    tbTenTheLoai.Text = dtgvTheLoai.Rows[e.RowIndex].Cells["Tên"].FormattedValue.ToString();
                }
            }
            catch (Exception)
            {
                return;
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

        private void btReloadNxb_Click(object sender, EventArgs e)
        {
            DataTable dtnxb = cnxb.HienThiTatCaNhaXuatBan();
            LoadDuLieuNXB(dtnxb);
        }

        private void btTimKiemNhaXuatBan_Click(object sender, EventArgs e)
        {
            DataTable data = cnxb.TimKiem(tbTimKiemTenNXB.Text);
            LoadDuLieuNXB(data);

        }
        private void LoadDuLieuNXB(DataTable data)
        {
            List<MNhaXuatBan> ls = new List<MNhaXuatBan>();
            foreach (DataRow r in data.Rows)
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
                    DataTable dtnxb = cnxb.HienThiTatCaNhaXuatBan();
                    LoadDuLieuNXB(dtnxb);

                    cbNhaXuatBan.DataSource = dtnxb;
                    cbNhaXuatBan.DisplayMember = "ten";
                    cbNhaXuatBan.ValueMember = "id";
                }
                catch (Exception)
                {
                    MessageBox.Show("Dữ liệu đã tồn tại hoặc không hợp lệ.");
                    return;
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

                DataTable dtnxb = cnxb.HienThiTatCaNhaXuatBan();
                LoadDuLieuNXB(dtnxb);

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
            try
            {
                if (!String.IsNullOrEmpty(tbIdNhaXuatBan.Text))
                {
                    cnxb.XoaNhaXuatBan(tbIdNhaXuatBan.Text);
                    XoaDuLieuTabPageNXB();
                    this.dtgvNhaXuatBan.DataSource = null;
                    this.dtgvNhaXuatBan.Rows.Clear();

                    DataTable dtnxb = cnxb.HienThiTatCaNhaXuatBan();
                    LoadDuLieuNXB(dtnxb);

                    cbNhaXuatBan.DataSource = dtnxb;
                    cbNhaXuatBan.DisplayMember = "ten";
                    cbNhaXuatBan.ValueMember = "id";

                }
                else
                {
                    MessageBox.Show("Hãy chọn nhà xuất bản cần xóa.");
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Không thể xóa vì đang có ràng buộc.");
                return;
            }
        }
        private void dtgvNhaXuatBan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgvTheLoai.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dtgvNhaXuatBan.CurrentRow.Selected = true;
                    tbIdNhaXuatBan.Text = dtgvNhaXuatBan.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString();
                    tbTenNhaXuatBan.Text = dtgvNhaXuatBan.Rows[e.RowIndex].Cells["Tên"].FormattedValue.ToString();
                    tbDiaChiNxb.Text = dtgvNhaXuatBan.Rows[e.RowIndex].Cells["Địa chỉ"].FormattedValue.ToString();
                }
            }
            catch (Exception)
            {
                return;
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

        private void btReloadTaiKhoan_Click(object sender, EventArgs e)
        {
            DataTable dttk = ctk.HienThiTatCaTaiKhoan();
            LoadDuLieuTaiKhoan(dttk);
        }

        private void btTimTenTaiKhoan_Click(object sender, EventArgs e)
        {
            DataTable dttk = ctk.TimKiem(tbTimTenDangNhap.Text);
            LoadDuLieuTaiKhoan(dttk);
        }

     
        private void LoadDuLieuTaiKhoan(DataTable data)
        {
            List<MTaiKhoan> ls = new List<MTaiKhoan>();
            foreach (DataRow r in data.Rows)
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
                string diachi = r["diachi"].ToString();

                string item = $"Tên: {ten}         |Chức vụ: {chucvu}       |Địa chỉ: {diachi}          |Ngày sinh: {ngaysinh}";
                dicNhanVien.Add(id, item);
            }
            if (dicNhanVien.Count > 0)
            {
                cbNhanVien.DataSource = new BindingSource(dicNhanVien, null); ;
                cbNhanVien.DisplayMember = "Value";
                cbNhanVien.ValueMember = "Key";

            }
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
                        return;
                    }

                    mtk.Tendangnhap = tbTenDangNhap.Text;
                    mtk.Matkhau = tbMatKhau.Text;
                    mtk.Tenhienthi = tbTenHienThi.Text;
                    mtk.Loaitaikhoan = Convert.ToBoolean(cbLoaiTaiKhoan.SelectedValue.ToString());
                    mtk.Id_nhanvien = Convert.ToInt32(cbNhanVien.SelectedValue.ToString());

                    if (ctk.ThemTaiKhoan(mtk))
                    {
                        XoaDuLieuTabPageTaiKhoan();
                        DataTable dttk = ctk.HienThiTatCaTaiKhoan();
                        LoadDuLieuTaiKhoan(dttk);
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản đã tồn tại!");

                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Dữ liệu đã tồn tại hoặc không hợp lệ.");
                    return;
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

                if (ctk.CapNhatTaiKhoan(mtk, tbIdTaiKhoan.Text))
                {
                    XoaDuLieuTabPageTaiKhoan();
                    DataTable dttk = ctk.HienThiTatCaTaiKhoan();
                    LoadDuLieuTaiKhoan(dttk);
                }
                else
                {
                    MessageBox.Show("Tài khoản đã tồn tại!");

                }
            }
            else
            {
                MessageBox.Show("Hãy chọn tài khoản cần sửa.");
            }
        }

        private void btXoaTaiKhoan_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(tbIdTaiKhoan.Text))
                {
                    ctk.XoaTaiKhoan(int.Parse(tbIdTaiKhoan.Text));
                    XoaDuLieuTabPageTaiKhoan();
                    this.dtgvTaiKhoan.DataSource = null;
                    this.dtgvTaiKhoan.Rows.Clear();

                    DataTable dttk = ctk.HienThiTatCaTaiKhoan();
                    LoadDuLieuTaiKhoan(dttk);
                }
                else
                {
                    MessageBox.Show("Hãy chọn tài khoản cần xóa.");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xóa vì đang có ràng buộc.");
                return;

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
                return;
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

        private void btReloadNhanVien_Click(object sender, EventArgs e)
        {
            DataTable dtnv = cnv.HienThiTatCaNhanVien();
            LoadDuLieuNhanVien(dtnv);
        }

        private void btTimNhanVien_Click(object sender, EventArgs e)
        {
            DataTable dtnv = cnv.TimKiem(tbTimKiemTenNhanVien.Text);
            LoadDuLieuNhanVien(dtnv);

        }
        private void LoadDuLieuNhanVien(DataTable data)
        {
            List<MNhanVien> ls = new List<MNhanVien>();
            foreach (DataRow r in data.Rows)
            {
                MNhanVien m = new MNhanVien();
                m.Id = int.Parse(r["id"].ToString());
                m.Ten = r["ten"].ToString();
                m.Diachi = r["diachi"].ToString();
                m.Ngaysinh = Convert.ToDateTime( r["ngaysinh"].ToString());
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
                tb.Columns.Add("Ngày sinh", typeof(string));
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
                    r["Ngày sinh"] = m.Ngaysinh.ToString("dd-MM-yyyy");
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
            if (!String.IsNullOrEmpty(tbTenNhanVien.Text))
            {
                try
                {
                    MNhanVien mnv = new MNhanVien();
                    mnv.Ten = tbTenNhanVien.Text;
                    mnv.Diachi = tbDiaChiNhanVien.Text;
                    mnv.Sdt = tbSdt.Text;
                    mnv.Ngaysinh = Convert.ToDateTime(dtpNgaySinh.Value.ToString("dd-MM-yyyy"));
                    mnv.Id_chucvu = Convert.ToInt32(cbChucVu.SelectedValue.ToString());
                    cnv.ThemNhanVien(mnv);
                    XoaDuLieuTabPageNhanVien();
                    DataTable dtnv = cnv.HienThiTatCaNhanVien();
                    LoadDuLieuNhanVien(dtnv);
                }
                catch (Exception)
                {
                    MessageBox.Show("Dữ liệu đã tồn tại hoặc không hợp lệ.");
                    return;
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
                mnv.Ngaysinh = Convert.ToDateTime(dtpNgaySinh.Value.ToString("dd-MM-yyyy"));
                mnv.Id_chucvu = Convert.ToInt32(cbChucVu.SelectedValue.ToString());
                cnv.CapNhatNhanVien(mnv, tbIdNhanVien.Text);
                XoaDuLieuTabPageNhanVien();
                DataTable dtnv = cnv.HienThiTatCaNhanVien();
                LoadDuLieuNhanVien(dtnv);

            }
            else
            {
                MessageBox.Show("Hãy chọn thông tin nhân viên cần sửa.");
            }
        }

        private void btXoaNhanVien_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(tbIdNhanVien.Text))
                {
                    cnv.XoaNhanVien(int.Parse(tbIdNhanVien.Text));
                    XoaDuLieuTabPageNhanVien();
                    this.dtgvNhanVien.DataSource = null;
                    this.dtgvNhanVien.Rows.Clear();

                    DataTable dtnv = cnv.HienThiTatCaNhanVien();
                    LoadDuLieuNhanVien(dtnv);
                }
                else
                {
                    MessageBox.Show("Hãy chọn nhân viên cần xóa.");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Không thể xóa vì đang có ràng buộc.");
                return;
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
                return;
            }
        }




        #endregion

        #region DoanhThu

        private void LoadDuLieuHoaDon(DataTable data)
        {
            List<MHoaDon> lhd = new List<MHoaDon>();
            DataTable dthd = data;
            foreach (DataRow r in dthd.Rows)
            {
                MHoaDon m = new MHoaDon();
                m.Id = int.Parse(r["id"].ToString());
                m.Id_nhanvien = int.Parse(r["id_nhanvien"].ToString());
                m.Tennhanvien = r["tennhanvien"].ToString();
                m.Ngaylap = Convert.ToDateTime(r["ngaylap"].ToString());
                m.Tongtien = int.Parse(r["tongtien"].ToString());
                lhd.Add(m);
            }

            if (lhd.Count != 0)
            {
                DataTable tb = new DataTable();
                tb.Columns.Add("Id", typeof(String));
                tb.Columns.Add("TT", typeof(int));
                tb.Columns.Add("Ngày lập", typeof(String));
                tb.Columns.Add("Nhân viên", typeof(String));
                tb.Columns.Add("Mã nhân viên", typeof(String));
                tb.Columns.Add("Tổng tiền", typeof(String));
                int tt = 0;
                foreach (MHoaDon m in lhd)
                {
                    tt++;
                    DataRow r = tb.NewRow();
                    r["Id"] = m.Id.ToString();
                    r["TT"] = tt.ToString();
                    r["Ngày lập"] = m.Ngaylap.ToString("dd-MM-yyyy");
                    r["Nhân viên"] = m.Tennhanvien;
                    r["Mã nhân viên"] = m.Id_nhanvien;
                    r["Tổng tiền"] = m.Tongtien;

                    tb.Rows.Add(r);
                }
                dtgvDoanhThu.DataSource = tb;
                dtgvDoanhThu.Columns["Id"].Visible = false;
            }

            DataTable da = (DataTable)(dtgvDoanhThu.DataSource);
            int doanhThu = 0;
            if (data.Rows.Count > 1)
            {
                foreach (DataRow r in (da.Rows))
                {
                    doanhThu += int.Parse(r["Tổng tiền"].ToString());
                }
            }
            tbDoanhThu.Text = string.Format(new CultureInfo("vi-VN"), "{0:#,##0}", doanhThu);
        }

        private void btThongKe_Click(object sender, EventArgs e)
        {
            this.dtgvDoanhThu.DataSource = null;
            this.dtgvDoanhThu.Rows.Clear();

            DataTable data =  chd.HienThiHoaDon(dtpTuNgay.Value, dtpDenNgay.Value);
            LoadDuLieuHoaDon(data);
        }

        private void dtgvDoanhThu_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (dtgvDoanhThu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dtgvDoanhThu.CurrentRow.Selected = true;

                    int idHoaDon = int.Parse(dtgvDoanhThu.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString());
                    fCTHD f = new fCTHD(idHoaDon);
                    f.ShowDialog();

                }



            }
            catch (Exception)
            {
                return;
            }
        }
        #endregion

        

        

       
    }
}
