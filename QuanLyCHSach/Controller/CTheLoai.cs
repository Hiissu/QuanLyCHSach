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
    class CTheLoai : dbConnection
    {
        public DataTable HienThiTatCaTheLoai()
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = "SELECT * FROM dbo.TheLoai ";

            //"SELECT id as IdTheLoai, ten as TenTheLoai, " +
            //"theloai as TheLoai, quocgia as QuocGia, diemdanhgia as DiemDanhGia FROM dbo.TheLoai ";

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

            string truyvan = $"SELECT * FROM dbo.TheLoai  WHERE ten LIKE '%{st}%'";

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

        public void ThemTheLoai(string ten)
        {
            string truyvan = $"INSERT INTO [dbo].[TheLoai] ([ten]) " 
                           + $"VALUES ('{ten}')";

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


        public void CapNhatTheLoai(string ten, int id)
        {
            string truyvan = $"UPDATE [dbo].[TheLoai] " +
                $"SET [ten] = '{ten}' " +
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

                return;            }
        }

        public void XoaTheLoai(int id)
        {
            string truyvan = $"DELETE FROM [dbo].[TheLoai] WHERE [id] = '{id}'";

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

        public void XoaTatCaTheLoai()
        {
            string truyvan = "DELETE FROM [dbo].[TheLoai]";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            base.GhiDuLieu(cmd);
        }
    }
}
