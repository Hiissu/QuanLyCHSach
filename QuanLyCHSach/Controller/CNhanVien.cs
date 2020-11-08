using QuanLyCHSach.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCHSach.Controller
{
    class CNhanVien : dbConnection
    {
        public DataTable HienThiTatCaNhanVien()
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = "SELECT * FROM dbo.NhanVien ";

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
        public DataTable TimKiem(string st)
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = $"SELECT * FROM dbo.NhanVien  WHERE ten LIKE '%{st}%'";

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

        public void ThemChucVu(string ten)
        {
            string truyvan = $"INSERT INTO " +
               $"[dbo].[ChucVu]([ten]) " +
               $"VALUES (N'{ten}')";


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

        public void ThemNhanVien(MNhanVien obj)
        {
            string truyvan = $"INSERT INTO " +
               $"[dbo].[NhanVien]([ten], [diachi], [ngaysinh], [sdt], [id_chucvu]) " +
               $"VALUES (N'{obj.Ten}', N'{obj.Diachi}', N'{obj.Ngaysinh}', '{obj.Sdt}', '{obj.Id_chucvu}')";


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


        public void CapNhatNhanVien(MNhanVien obj, object idNhanVien)
        {
            string truyvan = $"UPDATE [dbo].[NhanVien] " +
                $"SET [ten] = N'{obj.Ten}', [diachi] = N'{obj.Diachi}',  [ngaysinh] = '{obj.Ngaysinh}', " +
                    $"[sdt] = '{obj.Sdt}', [id_chucvu] = '{obj.Id_chucvu}' " +
                $"WHERE [id] = '{idNhanVien}'";

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

        public void XoaNhanVien(int id)
        {
            string truyvan = $"DELETE FROM [dbo].[NhanVien] WHERE [id] = '{id}'";

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

        public void XoaTatCaNhanVien()
        {
            string truyvan = "DELETE FROM [dbo].[NhanVien]";

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
