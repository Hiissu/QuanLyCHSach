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
    class CCTHD : dbConnection
    {
        public DataTable HienThiCTHD(int idHoaDon)
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = $"select ct.id, ct.id_sach, s.ten as tensach, ct.soluong, s.dongia, s.dongia * ct.soluong as thanhtien from CTHD as ct, Sach as s " +
                $"where ct.id_sach = s.id and ct.id_hoadon = {idHoaDon}";

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

       
        public void ThemCTHD(MCTHD obj)
        {
            string truyvan = $"INSERT INTO [dbo].[CTHD]([id_hoadon], [id_sach], [soluong]) " +
                            $"VALUES (IDENT_CURRENT('HoaDon'), '{obj.Id_sach}', '{obj.Soluong}') ";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            base.GhiDuLieu(cmd);
        }


        //public void CapNhatCTHD(MCTHD obj, object idCTHD)
        //{
        //    string truyvan = $"UPDATE [dbo].[CTHD] " +
        //        $"SET [id_hoadon] = '{obj.Id_hoadon}', [id_sach] = '{obj.Id_sach}',  [soluong] = '{obj.Soluong}', " +
        //        $"WHERE [id] = '{idCTHD}'";

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = truyvan;

        //    base.GhiDuLieu(cmd);
        //}

        //public void XoaCTHD(int id)
        //{
        //    string truyvan = $"DELETE FROM [dbo].[CTHD] WHERE [id] = '{id}'";

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = truyvan;

        //    base.GhiDuLieu(cmd);
        //}

        //public void XoaTatCaCTHD()
        //{
        //    string truyvan = "DELETE FROM [dbo].[CTHD]";

        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandType = CommandType.Text;
        //    cmd.CommandText = truyvan;

        //    base.GhiDuLieu(cmd);
        //}
    }
}
