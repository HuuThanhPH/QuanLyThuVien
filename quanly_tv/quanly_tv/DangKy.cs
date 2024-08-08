using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanly_tv
{
    public partial class DangKy : Form
    {
        public DangKy()
        {
            InitializeComponent();
        }
        connect con = new connect();
        string query;
        string txt_ma;
        private void DangKy_Load(object sender, EventArgs e)
        {
            txt_mk.UseSystemPasswordChar = true;
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        static bool ValidatePhoneNumber(string phoneNumber)
        {
            string pattern = @"^(0[35789]|0084)[0-9]{8,9}$";

            return Regex.IsMatch(phoneNumber, pattern);
        }
        public bool IsEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        private void checkId()
        {
            string queryReader = "SELECT TOP 1 * FROM NHANVIEN where MANV LIKE 'NV%' ORDER BY MANV DESC";
            int idAuto = 0;
            DataSet ds = con.getData(queryReader);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Lấy giá trị của cột MA_KIEMTRA từ dòng đầu tiên
                string maKiemTra = ds.Tables[0].Rows[0]["MANV"].ToString();
                string newstring = maKiemTra.Substring(maKiemTra.Length - 3, 3);
                idAuto = int.Parse(newstring);
            }

            if (idAuto < 10)
            {
                if (idAuto == 9)
                {
                    txt_ma = "NV010";
                }
                else
                {
                    txt_ma = "NV00" + (idAuto + 1);
                }
            }
            else if (idAuto >= 10 && idAuto < 100)
            {
                if (idAuto == 99)
                {
                    txt_ma = "NV100";
                }
                else
                {
                    txt_ma = "NV0" + (idAuto + 1);
                }
            }
            else
            {
                txt_ma = "NV" + (idAuto + 1);
            }
        }
        private void button_dki_Click(object sender, EventArgs e)
        {
            checkId();
            if (txt_ten.Text != "" && txt_mail.Text != "" && txt_mk.Text != "" && txt_sdt.Text != "")
            {
                string queryReader = "select * from NHANVIEN";
                SqlDataReader reader = con.loadData(queryReader);
                while (reader.Read())
                {
                    string phone = reader["SDTNV"].ToString();
                    string email = reader["EMAIL"].ToString();
                    if (email == txt_mail.Text)
                    {
                        MessageBox.Show("Email đã tồn tại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    bool isValidPhoneNumber = ValidatePhoneNumber(txt_sdt.Text);
                    if (isValidPhoneNumber == false)
                    {
                        MessageBox.Show("Không đúng dịnh dạng số điện thoại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (phone == txt_sdt.Text)
                    {
                        MessageBox.Show("Số điện thoại trong hệ thống", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                query = "insert into NHANVIEN(MANV, HOTEN, EMAIL, PASSWORDNV, SDTNV,ISADMIN) values ('" + txt_ma + "', N'" + txt_ten.Text + "', '" + txt_mail.Text + "', '" + txt_mk.Text + "', '" + txt_sdt.Text + "', N'Nhân viên')";

                con.setData(query, "Đăng ký thành công");
                Form1 dangnhap = new Form1();
                dangnhap.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DangKy_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result;
            result = MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

    }
}
