using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace quanly_tv
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_dang_nhap_Click(object sender, EventArgs e)
        {
            connect con = new connect();
            string query = "select EMAIL, MANV, ISADMIN, PASSWORDNV from NHANVIEN";
            SqlDataReader reader = con.loadData(query);

            while (reader.Read())
            {
                string emailnv = reader["EMAIL"].ToString();
                string passwordnv = reader["PASSWORDNV"].ToString();
                string idnv = reader["MANV"].ToString();
                string isAdmin = reader["ISADMIN"].ToString();

                if (txt_email.Text == emailnv && txt_password.Text == passwordnv)
                {
                    if (isAdmin == "Admin")
                    {
                        Home home = new Home();
                        home.setIDValue(idnv);
                        error_login.Visible = false;
                        home.Show();
                        this.Hide();
                        return;
                    }
                    else
                    {
                        NhanVienDashBoard nv = new NhanVienDashBoard();
                        error_login.Visible = false;
                        nv.setIDValue(idnv);
                        nv.Show();
                        this.Hide();
                        return;
                    }
                 
                }
            }
            error_login.Visible = true;
        }

        private void gunaLabel3_Click(object sender, EventArgs e)
        {
            DangKy dangky = new DangKy();
            this.Hide();
            dangky.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txt_password.UseSystemPasswordChar = true;
        }

        


    }
}
