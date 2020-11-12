using QuanLyCHSach.Controller;
using QuanLyCHSach.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCHSach.View
{
    public partial class fChucVu : Form
    {
        public fChucVu()
        {
            InitializeComponent();
        }

        private void XoaDuLieuTextBox()
        {
            tbIdChucVu.Text = "";
            tbTenChucVu.Text = "";
        }

        CChucVu ccv = new CChucVu();

        private void btReloadChucVu_Click(object sender, EventArgs e)
        {
            DataTable dt = ccv.HienThiTatCaChucVu();
            LoadDuLieuChucVu(dt);

        }

        private void btTimChucVu_Click(object sender, EventArgs e)
        {
            DataTable data = ccv.TimKiem(tbTimKiemTenChucVu.Text);
            LoadDuLieuChucVu(data);

        }

        
        private void LoadDuLieuChucVu(DataTable data)
        {
            try
            {
                List<MChucVu> ls = new List<MChucVu>();
                foreach (DataRow r in data.Rows)
                {
                    MChucVu m = new MChucVu();
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
                    foreach (MChucVu m in ls)
                    {
                        tt++;
                        DataRow r = tb.NewRow();
                        r["Id"] = m.Id.ToString();
                        r["TT"] = tt.ToString();
                        r["Tên"] = m.Ten;

                        tb.Rows.Add(r);
                    }
                    dtgvChucVu.DataSource = tb;
                    dtgvChucVu.Columns["Id"].Visible = false;
                }


            }
            catch (Exception)
            {

                return;
            }
        }
        
        private void btThemChucVu_Click(object sender, EventArgs e)
        {
            MChucVu m = new MChucVu();

            if (!String.IsNullOrEmpty(tbTenChucVu.Text))
            {
                try
                {
                    m.Ten = tbTenChucVu.Text.ToString();
                    ccv.ThemChucVu(m.Ten);
                    XoaDuLieuTextBox();
                    DataTable dt = ccv.HienThiTatCaChucVu();
                    LoadDuLieuChucVu(dt);
                }
                catch (Exception)
                {
                    MessageBox.Show("Dữ liệu đã tồn tại hoặc không hợp lệ.");
                    return;
                }
            }

        }

        private void btSuaChucVu_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbIdChucVu.Text))
            {
                MChucVu m = new MChucVu();
                m.Ten = tbTenChucVu.Text;
                ccv.CapNhatChucVu(m.Ten, int.Parse(tbIdChucVu.Text));
                XoaDuLieuTextBox();

                DataTable dt = ccv.HienThiTatCaChucVu();
                LoadDuLieuChucVu(dt);

            }
            else
            {
                MessageBox.Show("Hãy chọn chức vụ cần sửa.");
            }
        }

        private void btXoaChucVu_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrEmpty(tbIdChucVu.Text))
                {
                    ccv.XoaChucVu(int.Parse(tbIdChucVu.Text));
                    XoaDuLieuTextBox();
                    this.dtgvChucVu.DataSource = null;
                    this.dtgvChucVu.Rows.Clear();

                    DataTable dt = ccv.HienThiTatCaChucVu();
                    LoadDuLieuChucVu(dt);


                }
                else
                {
                    MessageBox.Show("Hãy chọn chức vụ cần xóa.");
                }

            }
            catch (Exception)
            {

                MessageBox.Show("Không thể xóa vì đang có ràng buộc.");
                return;
            }
        }


       
        
        private void dtgvChucVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dtgvChucVu.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    dtgvChucVu.CurrentRow.Selected = true;
                    tbIdChucVu.Text = dtgvChucVu.Rows[e.RowIndex].Cells["Id"].FormattedValue.ToString();
                    tbTenChucVu.Text = dtgvChucVu.Rows[e.RowIndex].Cells["Tên"].FormattedValue.ToString();
                }
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
