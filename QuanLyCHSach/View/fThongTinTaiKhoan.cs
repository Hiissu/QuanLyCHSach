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

        private void fThongTinTaiKhoan_Load(object sender, EventArgs e)
        {

        }

        private void lbDoiMatKhau_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbMatKhau.Text))
            {
                if (tbMatKhau.Text is correct)
                {
                    lbMatKhauMoi.Visible = true;
                    tbMatKhauMoi.Visible = true;
                    lbNhapLaiMatKhauMoi.Visible = true;
                    tbNhapLaiMatKhauMoi.Visible = true;
                    btXacNhan.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("Bạn cần nhập mật khẩu chính xác để xác thực.");
            }
        }

        private void btXacNhan_Click(object sender, EventArgs e)
        {

        }
    }
}
