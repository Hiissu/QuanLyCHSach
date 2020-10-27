using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCHSach
{
    class dbConnection
    {
        public static string chuoiketnoi = @"Data Source=DESKTOP-ROOH7EF;Initial Catalog=QLyCHSach;Integrated Security=True";
        public static SqlConnection sqlconn;

        public dbConnection()
        {
            if (sqlconn == null)
            {
                sqlconn = new SqlConnection();
                sqlconn.ConnectionString = chuoiketnoi;
            }
        }

        public void MoKetNoi()
        {
            if (sqlconn.State != ConnectionState.Open || sqlconn.State != ConnectionState.Broken)
            {
                sqlconn.Open();
            }
        }

        public void DongKetNoi()
        {
            if (sqlconn.State == ConnectionState.Open)
            {
                sqlconn.Close();
            }
        }

        public DataSet DocDuLieu(SqlCommand sqlcmd)
        {
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();

            try
            {
                sqlcmd.Connection = sqlconn;
                da.SelectCommand = sqlcmd;
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public int GhiDuLieu(SqlCommand sqlcmd)
        {
            int i = 0;
            try
            {
                MoKetNoi();
                sqlcmd.Connection = sqlconn;
                i = sqlcmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                DongKetNoi();
            }
            return i;
        }
    }
}
