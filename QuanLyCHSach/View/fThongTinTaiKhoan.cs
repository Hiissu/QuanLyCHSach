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

    public partial class fThongTinTaiKhoan : Form
    {
        public fThongTinTaiKhoan()
        {
            InitializeComponent();
        }

        public string tenDangNhap { get; set; }

        CTaiKhoan ctk = new CTaiKhoan();
        private void lbDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbMatKhau.Text))
            {
                DataTable dt = ctk.Login(tbTenDangNhap.Text, tbMatKhau.Text);
                if (dt.Rows.Count > 0)
                {
                    lbMatKhauMoi.Visible = true;
                    tbMatKhauMoi.Visible = true;
                    lbNhapLaiMatKhauMoi.Visible = true;
                    tbNhapLaiMatKhauMoi.Visible = true;
                    btXacNhan.Visible = true;
                }
                else
                {
                    MessageBox.Show("Mật khẩu không chính xác.");
                }
            }
            else
            {
                MessageBox.Show("Bạn cần nhập mật khẩu chính xác để xác thực.");
            }
        }

        private void btXacNhan_Click(object sender, EventArgs e)
        {
            if (tbMatKhau.Text == tbMatKhauMoi.Text)
            {
                MessageBox.Show("Bạn không thể đặt mật khẩu mới giống như mật khẩu cũ.");
                return;
            }

            if (tbNhapLaiMatKhauMoi.Text != tbMatKhauMoi.Text)
            {
                MessageBox.Show("Mật khẩu mới nhập lại không chính xác.");
                return;

            }

            if (ctk.CapNhatMatKhau(tbTenDangNhap.Text, tbMatKhauMoi.Text))
            {
                lbMatKhauMoi.Visible = false;
                tbMatKhauMoi.Visible = false;
                tbMatKhauMoi.Text = "";
                lbNhapLaiMatKhauMoi.Visible = false;
                tbNhapLaiMatKhauMoi.Visible = false;
                tbNhapLaiMatKhauMoi.Text = "";
                btXacNhan.Visible = false;
                MessageBox.Show("Cập nhật mật khẩu thành công.");
                return;

            }
            
            
        }

        private void fThongTinTaiKhoan_Load(object sender, EventArgs e)
        {
            tbTenDangNhap.Text = tenDangNhap;

        }
    }
}
