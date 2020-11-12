using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCHSach.Controller
{
    class CChucVu :dbConnection
    {
        public DataTable HienThiTatCaChucVu()
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = "SELECT * FROM dbo.ChucVu ";

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

        public bool KiemTraChucVu(string tenChucVu)
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = $"SELECT * FROM dbo.ChucVu  WHERE ten = N'{tenChucVu}' ";

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

        public DataTable TimKiem(string st)
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = $"SELECT * FROM dbo.ChucVu  WHERE ten LIKE N'%{st}%'";

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
            string truyvan = $"INSERT INTO [dbo].[ChucVu] ([ten]) "
                           + $"VALUES (N'{ten}')";

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


        public void CapNhatChucVu(string ten, int id)
        {
            string truyvan = $"UPDATE [dbo].[ChucVu] " +
                $"SET [ten] = N'{ten}' " +
                $"WHERE [id] = '{id}'";

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

        public void XoaChucVu(int id)
        {
            string truyvan = $"DELETE FROM [dbo].[ChucVu] WHERE [id] = '{id}'";

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

        public void XoaTatCaChucVu()
        {
            string truyvan = "DELETE FROM [dbo].[ChucVu]";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            base.GhiDuLieu(cmd);
        }
    }
}
