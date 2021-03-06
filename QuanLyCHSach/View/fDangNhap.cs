﻿using System;
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
    public partial class fDangNhap : Form
    {
        public fDangNhap()
        {
            InitializeComponent();
        }

        private void btDangNhap_Click(object sender, EventArgs e)
        {
            string tenDangNhap = tbTenDangNhap.Text;
            string matKhau = tbMatKhau.Text;
            DataTable dt = Login(tenDangNhap, matKhau);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow r in dt.Rows)
                {
                    fQuanLy f = new fQuanLy();
                    f.tenDangNhap = r["tendangnhap"].ToString();
                    f.tenNhanVien = r["tennhanvien"].ToString();
                    f.idNhanVien = int.Parse(r["id_nhanvien"].ToString());
                    f.loaiTaiKhoan = Convert.ToBoolean(r["loaitaikhoan"].ToString());
                    this.Hide();
                    f.ShowDialog();
                    this.Show();
                }
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác.");
            }
        }
        CTaiKhoan ctk = new CTaiKhoan();
        DataTable Login(string tenDangNhap, string matKhau)
        {
            return ctk.Login(tenDangNhap, matKhau);
        }

        private void tbMatKhau_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btDangNhap.PerformClick();
            }
        }
    }
}
