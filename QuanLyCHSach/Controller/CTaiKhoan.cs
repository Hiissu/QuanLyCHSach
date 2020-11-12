using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCHSach.Model;
namespace QuanLyCHSach
{
    class CTaiKhoan : dbConnection
    {
        public DataTable Login(string tenDangNhap, string matKhau)
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = $"SELECT tendangnhap, loaitaikhoan, nv.ten as tennhanvien FROM dbo.TaiKhoan as tk " +
                $"INNER JOIN NhanVien as nv ON tk.id_nhanvien = nv.id " +
                $"WHERE tendangnhap = '{tenDangNhap}' AND matkhau = '{matKhau}'";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            try
            {
                DataSet ds = base.DocDuLieu(cmd);
                if (dtable == null && ds.Tables.Count > 0)
                {
                    dtable = ds.Tables[0];
                }

                if (dtable.Rows.Count > 0)
                {
                    return dtable;
                }

                return dtable;

            }
            catch (Exception)
            {

                return dtable;
            }
        }

        public DataTable TimKiem(string st)
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = "SELECT tk.id, tk.tendangnhap, tk.tenhienthi, tk.matkhau, tk.loaitaikhoan, nv.ten as tennhanvien FROM dbo.TaiKhoan as tk " +
                "INNER JOIN dbo.NhanVien as nv ON tk.id_nhanvien = nv.id " +
                $" WHERE tendangnhap LIKE '%{st}%'";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;
            try
            {
                DataSet ds = base.DocDuLieu(cmd);
                if (dtable == null && ds.Tables.Count > 0)
                {
                    dtable = ds.Tables[0];
                }

                return dtable;

            }
            catch (Exception)
            {

                return dtable;
            }
        }

        public bool CapNhatMatKhau(string tenDangNhap, string matKhau)
        {
            string truyvan = $"UPDATE [dbo].[TaiKhoan] " +
                $"SET [matkhau] = '{matKhau}' " +
                $"WHERE tendangnhap = '{tenDangNhap}'";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;
            try
            {
                base.GhiDuLieu(cmd);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public DataTable HienThiTatCaTaiKhoan()
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = "SELECT tk.id, tk.tendangnhap, tk.tenhienthi, tk.matkhau, tk.loaitaikhoan, nv.ten as tennhanvien FROM dbo.TaiKhoan as tk " +
                "INNER JOIN dbo.NhanVien as nv ON tk.id_nhanvien = nv.id ";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;
            try
            {
                DataSet ds = base.DocDuLieu(cmd);
                if (dtable == null && ds.Tables.Count > 0)
                {
                    dtable = ds.Tables[0];
                }

                return dtable;

            }
            catch (Exception)
            {

                return dtable;
            }
        }

        public DataTable HienThiTheoIdTaiKhoan(String id)
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = $"SELECT id_nhanvien FROM dbo.TaiKhoan WHERE id = {id}";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            try
            {
                DataSet ds = base.DocDuLieu(cmd);
                if (dtable == null && ds.Tables.Count > 0)
                {
                    dtable = ds.Tables[0];
                }

                return dtable;

            }
            catch (Exception)
            {

                return dtable;
            }
        }

        public bool KiemTraTaiKhoan(string tenDangNhap)
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = $"SELECT * FROM dbo.TaiKhoan as tk WHERE tendangnhap = '{tenDangNhap}' ";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            try
            {
                DataSet ds = base.DocDuLieu(cmd);
                if (dtable == null && ds.Tables.Count > 0)
                {
                    dtable = ds.Tables[0];
                }

                return dtable.Rows.Count > 0;

            }
            catch (Exception)
            {

                return false;
            }


        }

        public bool ThemTaiKhoan(MTaiKhoan obj)
        {
            if (!KiemTraTaiKhoan(obj.Tendangnhap))
            {
                string truyvan = $"INSERT INTO " +
                   $"[dbo].[TaiKhoan]([tendangnhap], [tenhienthi], [matkhau], [loaitaikhoan], [id_nhanvien]) " +
                   $"VALUES ('{obj.Tendangnhap}', N'{obj.Tenhienthi}', '{obj.Matkhau}', '{obj.Loaitaikhoan}', '{obj.Id_nhanvien}')";
                
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = truyvan;
                try
                {
                    base.GhiDuLieu(cmd);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool CapNhatTaiKhoan(MTaiKhoan obj, object idTaiKhoan)
        {
            if (!KiemTraTaiKhoan(obj.Tendangnhap))
            {
                string truyvan = $"UPDATE [dbo].[TaiKhoan] " +
                $"SET [tendangnhap] = '{obj.Tendangnhap}', [tenhienthi] = N'{obj.Tenhienthi}',  [matkhau] = '{obj.Matkhau}', " +
                    $"[loaitaikhoan] = '{obj.Loaitaikhoan}', [id_nhanvien] = '{obj.Id_nhanvien}' " +
                $"WHERE [id] = '{idTaiKhoan}'";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = truyvan;

                try
                {
                    base.GhiDuLieu(cmd);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;

        }

        public void XoaTaiKhoan(int id)
        {
            string truyvan = $"DELETE FROM [dbo].[TaiKhoan] WHERE [id] = '{id}'";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;
            try
            {
                base.GhiDuLieu(cmd);

            }
            catch (Exception)
            {

                return;
            }
        }
    }
}
