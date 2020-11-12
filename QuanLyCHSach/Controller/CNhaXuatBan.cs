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
    class CNhaXuatBan :dbConnection
    {
        public DataTable HienThiTatCaNhaXuatBan()
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = "SELECT * FROM dbo.NhaXuatBan ";

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
        public bool KiemTraNXB(string tenNXB)
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = $"SELECT * FROM dbo.NhaXuatBan WHERE ten = N'{tenNXB}' ";

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

            string truyvan = $"SELECT * FROM dbo.NhaXuatBan  WHERE ten LIKE N'%{st}%'";

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

        public void ThemNhaXuatBan(MNhaXuatBan obj)
        {
            string truyvan = $"INSERT INTO [dbo].[NhaXuatBan] ([ten], [diachi]) "
                           + $"VALUES (N'{obj.Ten}', N'{obj.Diachi}')";

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


        public void CapNhatNhaXuatBan(MNhaXuatBan obj, object idNhaXuatBan)
        {
            string truyvan = $"UPDATE [dbo].[NhaXuatBan] " +
                $"SET [ten] = N'{obj.Ten}', [diachi] = N'{obj.Diachi}' " +
                $"WHERE [id] = '{idNhaXuatBan}'";

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

        public void XoaNhaXuatBan(object idNhaXuatBan)
        {
            string truyvan = $"DELETE FROM [dbo].[NhaXuatBan] WHERE [id] = '{idNhaXuatBan}'";

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

        public void XoaTatCaNhaXuatBan()
        {
            string truyvan = "DELETE FROM [dbo].[NhaXuatBan]";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            base.GhiDuLieu(cmd);
        }
    }
}
