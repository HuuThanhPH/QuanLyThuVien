using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace quanly_tv
{
    class connect
    {
        protected SqlConnection getConnection()
        {
            SqlConnection con = new SqlConnection();
            con.ConnectionString = "Data Source=MSI-GF63-THIN11\\SQLEXPRESS;Initial Catalog=QLTV;Integrated Security=True";
            return con;
        }

        public DataSet getData(string query)
        {
            SqlConnection con = getConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            da.SelectCommand = cmd;
            da.Fill(ds);
            return ds;
        }

        public void setData(string query, string message)
        {
            SqlConnection con = getConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = query; // câu lệnh query
            cmd.ExecuteNonQuery(); // thực hiện query (no return data)
            con.Close();

            if(message != "")
            {
                MessageBox.Show(message, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public SqlDataReader loadData(string query)
        {
            SqlConnection con = getConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd = new SqlCommand(query, con);
            SqlDataReader sdr = cmd.ExecuteReader(); 
            return sdr;
        }

        public int updateTable(DataTable newdt, string query)
        {
            SqlConnection con = getConnection();
            SqlDataAdapter da = new SqlDataAdapter(query,con);
            SqlCommandBuilder cb = new SqlCommandBuilder(da);
            int kq = da.Update(newdt);
            return kq;
        }

        public int getCount(string query)
        {
            SqlConnection con = getConnection();
            SqlCommand cmd = new SqlCommand(query, con);

            con.Open();
            int count = (int)cmd.ExecuteScalar();
            con.Close();

            return count;
        }
    }
}
