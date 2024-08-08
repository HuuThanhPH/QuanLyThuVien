using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace quanly_tv
{
    public partial class passwordnv : UserControl
    {
        public passwordnv()
        {
            InitializeComponent();
        }
        
        private string IDValue;
        public void setIDValue(string value)
        {
            IDValue = value;
        }

        public string getIDValue()
        {
            return IDValue;
        }

        connect con = new connect();
        string query;

        private void passwordnv_VisibleChanged(object sender, EventArgs e)
        {
            string queryReader = "select * from NHANVIEN";
            SqlDataReader reader = con.loadData(queryReader);

            while (reader.Read())
            {
                string nvid = reader["MANV"].ToString();
                string ten = reader["HOTEN"].ToString();
                string email = reader["EMAIL"].ToString();
                string sdt = reader["SDTNV"].ToString();
                if (nvid == this.IDValue)
                {
                    lab_idnv.Text = "ID nhân viên : " + nvid + "";
                    lab_tennv.Text = "Tên nhân viên : " + ten + "";
                    lab_emailnv.Text = "Email nhân viên : " + email + "";
                    lab_sdtnv.Text = "Số điện thoại nhân viên : " + sdt + "";
                }
            }

            txt_oldmk.UseSystemPasswordChar = true;
            txt_newpw.UseSystemPasswordChar = true;
            txt_newpw1.UseSystemPasswordChar = true;
            btn_hide.Visible = false;
            clearText();
        }

        private void btn_doimk_Click(object sender, EventArgs e)
        {
            string queryReader = "select * from NHANVIEN";
            SqlDataReader reader = con.loadData(queryReader);
            if (txt_oldmk.Text != "" && txt_newpw.Text != "" && txt_newpw1.Text != "")
            {
                while (reader.Read())
                {
                    string nvid = reader["MANV"].ToString();
                    string pw = reader["PASSWORDNV"].ToString();
                    if (IDValue == nvid && pw == txt_oldmk.Text)
                    {
                        query = "UPDATE NHANVIEN SET PASSWORDNV = '" + txt_newpw.Text + "'  WHERE MANV = '" + nvid + "'";
                        if (MessageBox.Show("Bạn có muốn đổi mật khẩu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            con.setData(query, "Đổi mật khẩu thành công");

                            passwordnv_VisibleChanged(this, null);
                            return;
                        }
                    }
                }
            }
        }

        private void btn_show_Click(object sender, EventArgs e)
        {
            txt_oldmk.UseSystemPasswordChar = false;
            txt_newpw.UseSystemPasswordChar = false;
            txt_newpw1.UseSystemPasswordChar = false;
            btn_hide.Visible = true;
            btn_show.Visible = false;
        }

        private void btn_hide_Click(object sender, EventArgs e)
        {
            txt_oldmk.UseSystemPasswordChar = true;
            txt_newpw.UseSystemPasswordChar = true;
            txt_newpw1.UseSystemPasswordChar = true;
            btn_hide.Visible = false;
            btn_show.Visible = true;
        }

        private void btn_huybo_Click(object sender, EventArgs e)
        {
            clearText();
        }


        public void clearText()
        {
            txt_oldmk.Text = "";
            txt_newpw.Text = "";
            txt_newpw1.Text = "";
        }

    }
}
