﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyCHSach.Model;

namespace QuanLyCHSach
{
    class CSach : dbConnection
    {
        public DataTable HienThiTatCaSach()
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = "SELECT s.id, s.ten, s.tacgia, s.ngayxuatban, s.soluong, s.dongia, tl.ten as tentheloai, nxb.ten as tennhaxuatban FROM dbo.Sach as s " +
                "INNER JOIN dbo.TheLoai as tl ON s.id_theloai = tl.id " +
                "INNER JOIN dbo.NhaXuatBan as nxb ON s.id_nhaxuatban = nxb.id";

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

        public bool KiemTraSach(string tenSach)
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = $"SELECT * FROM dbo.Sach WHERE ten = N'{tenSach}' ";

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
        public DataTable TimKiem(object column, string st)
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = "SELECT s.id, s.ten, s.tacgia, s.ngayxuatban, s.soluong, s.dongia, tl.ten as tentheloai, nxb.ten as tennhaxuatban FROM dbo.Sach as s " +
                "INNER JOIN dbo.TheLoai as tl ON s.id_theloai = tl.id " +
                $"INNER JOIN dbo.NhaXuatBan as nxb ON s.id_nhaxuatban = nxb.id WHERE {column} LIKE N'%{st}%'";

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

        public DataTable HienThiTheoIdSach(String id)
        {
            DataTable dtable = new DataTable();
            dtable = null;

            string truyvan = $"SELECT id_theloai, id_nhaxuatban FROM dbo.Sach WHERE id = {id}";
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
        
        public void ThemSach(MSach obj)
        {
            string truyvan = $"INSERT INTO " +
               $"[dbo].[Sach]([ten], [tacgia], [id_theloai], [ngayxuatban], [id_nhaxuatban], [soluong], [dongia]) " +
               $"VALUES (N'{obj.Ten}', N'{obj.Tacgia}', '{obj.Id_theloai}', '{obj.Ngayxuatban}', N'{obj.Id_nhaxuatban}', '{obj.Soluong}', '{obj.Dongia}')";

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

        public void CapNhatSach(MSach obj, object idSach)
        {
            string truyvan = $"UPDATE [dbo].[Sach] " +
                $"SET [ten] = N'{obj.Ten}', [id_theloai] = '{obj.Id_theloai}',  [tacgia] = N'{obj.Tacgia}', [ngayxuatban] = '{obj.Ngayxuatban}', " + 
                    $"[id_nhaxuatban] = '{obj.Id_nhaxuatban}' , [soluong] = '{obj.Soluong}' , [dongia] = '{obj.Dongia}' " +
                $"WHERE [id] = '{idSach}'";

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

        public void CapNhatSoLuongSach(MCTHD m)
        {
            string truyvan = $"UPDATE [dbo].[Sach] " +
                    $"SET [soluong] = [soluong] - {m.Soluong} WHERE [id] = '{m.Id_sach}'";

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

        public void XoaSach(object idSach)
        {
            string truyvan = $"DELETE FROM [dbo].[Sach] WHERE [id] = '{idSach}'";

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

        public void XoaTatCaSach()
        {
            string truyvan = "DELETE FROM [dbo].[Sach]";

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = truyvan;

            base.GhiDuLieu(cmd);
        }

       

    }
}
