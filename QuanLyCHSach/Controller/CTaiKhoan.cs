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
        public DataTable HienThiTatCaTaiKhoan()
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = "SELECT tk.id, tk.tendangnhap, tk.tenhienthi, tk.matkhau, tk.loaitaikhoan, nv.ten as tennhanvien FROM dbo.TaiKhoan as tk " +
                "INNER JOIN dbo.NhanVien as nv ON tk.id_nhanvien = nv.id ";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            DataSet ds = base.DocDuLieu(cmd);
            if (dtable == null && ds.Tables.Count > 0)
            {
                dtable = ds.Tables[0];
            }

            return dtable;
        }

        public DataTable HienThiTheoIdTaiKhoan(String id)
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = $"SELECT id_nhanvien FROM dbo.TaiKhoan WHERE id = {id}";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            DataSet ds = base.DocDuLieu(cmd);
            if (dtable == null && ds.Tables.Count > 0)
            {
                dtable = ds.Tables[0];
            }

            return dtable;
        }

        public void ThemTaiKhoan(MTaiKhoan obj)
        {
            string truyvan = $"INSERT INTO " +
               $"[dbo].[TaiKhoan]([tendangnhap], [tenhienthi], [matkhau], [loaitaikhoan], [id_nhanvien]) " +
               $"VALUES (N'{obj.Tendangnhap}', N'{obj.Tenhienthi}', '{obj.Matkhau}', '{obj.Loaitaikhoan}', '{obj.Id_nhanvien}')";


            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            base.GhiDuLieu(cmd);
        }


        public void CapNhatTaiKhoan(MTaiKhoan obj, object idTaiKhoan)
        {
            string truyvan = $"UPDATE [dbo].[TaiKhoan] " +
                $"SET [tendangnhap] = N'{obj.Tendangnhap}', [tenhienthi] = N'{obj.Tenhienthi}',  [matkhau] = '{obj.Matkhau}', " +
                    $"[loaitaikhoan] = '{obj.Loaitaikhoan}', [id_nhanvien] = '{obj.Id_nhanvien}' " +
                $"WHERE [id] = '{idTaiKhoan}'";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            base.GhiDuLieu(cmd);
        }

        public void XoaTaiKhoan(int id)
        {
            string truyvan = $"DELETE FROM [dbo].[TaiKhoan] WHERE [id] = '{id}'";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            base.GhiDuLieu(cmd);
        }

        public void XoaTatCaTaiKhoan()
        {
            string truyvan = "DELETE FROM [dbo].[TaiKhoan]";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            base.GhiDuLieu(cmd);
        }
    }
}
