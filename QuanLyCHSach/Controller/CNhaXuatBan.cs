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

            DataSet ds = base.DocDuLieu(cmd);
            if (dtable == null && ds.Tables.Count > 0)
            {
                dtable = ds.Tables[0];
            }

            return dtable;
        }


        public void ThemNhaXuatBan(MNhaXuatBan obj)
        {
            string truyvan = $"INSERT INTO [dbo].[NhaXuatBan] ([ten], [diachi]) "
                           + $"VALUES ('{obj.Ten}', '{obj.Diachi}')";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            base.GhiDuLieu(cmd);
        }


        public void CapNhatNhaXuatBan(MNhaXuatBan obj, object idNhaXuatBan)
        {
            string truyvan = $"UPDATE [dbo].[NhaXuatBan] " +
                $"SET [ten] = '{obj.Ten}', [diachi] = '{obj.Diachi}' " +
                $"WHERE [id] = '{idNhaXuatBan}'";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            base.GhiDuLieu(cmd);
        }

        public void XoaNhaXuatBan(object idNhaXuatBan)
        {
            string truyvan = $"DELETE FROM [dbo].[NhaXuatBan] WHERE [id] = '{idNhaXuatBan}'";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            base.GhiDuLieu(cmd);
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
