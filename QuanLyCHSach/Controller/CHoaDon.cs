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
    class CHoaDon : dbConnection
    {
        public DataTable HienThiTatCaHoaDon()
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = "SELECT * FROM dbo.HoaDon ";

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

        
        public void ThemHoaDon(MHoaDon obj)
        {
            string truyvan = $"INSERT INTO [dbo].[HoaDon]([ngaylap], [id_nhanvien]) " +
                            $"VALUES ('{obj.Ngaylap}', '{obj.Id_nhanvien}')";
                            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            base.GhiDuLieu(cmd);
        }


        //public void CapNhatHoaDon(MHoaDon obj, object idHoaDon)
        //{
        //    string truyvan = $"UPDATE [dbo].[HoaDon] " +
        //        $"SET [ngaylap] = N'{obj.Ngaylap}', [id_nhanvien] = N'{obj.Id_nhanvien}' " +
        //        $"WHERE [id] = '{idHoaDon}'";

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = truyvan;

        //    base.GhiDuLieu(cmd);
        //}

        //public void XoaHoaDon(int id)
        //{
        //    string truyvan = $"DELETE FROM [dbo].[HoaDon] WHERE [id] = '{id}'";

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = truyvan;

        //    base.GhiDuLieu(cmd);
        //}

        //public void XoaTatCaHoaDon()
        //{
        //    string truyvan = "DELETE FROM [dbo].[HoaDon]";

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = truyvan;

        //    base.GhiDuLieu(cmd);
        //}
    }
}
