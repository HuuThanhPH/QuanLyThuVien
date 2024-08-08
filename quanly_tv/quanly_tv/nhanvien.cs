using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace quanly_tv
{
    public partial class nhanvien : UserControl
    {

        connect con = new connect();
        string query;
        string oldEmail = "";
        string oldPhone = "";
        public nhanvien()
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

      
        private void nhanvien_VisibleChanged(object sender, EventArgs e)
        {

            txt_1.UseSystemPasswordChar = true;
            txt_2.UseSystemPasswordChar = true;
            txt_3.UseSystemPasswordChar = true;
            txt_pw.UseSystemPasswordChar = true;
            btn_hide.Visible = false;
            btn_fix.Visible = false;
            label_fix.Visible =false;
            label_add.Visible = true;
            btn_add.Visible = true;
            btn_auto.Visible = true;
            ClearText();
            
            query = "select MANV, HOTEN, EMAIL, SDTNV, ISADMIN from NHANVIEN";
            DataSet ds = con.getData(query);
            DataSet ds1 = con.getData(query);
            gunaDataGridView1.DataSource = ds.Tables[0];
            gunaDataGridView3.DataSource = ds1.Tables[0];
            //gunaDataGridView1.AllowUserToAddRows = false;
            //gunaDataGridView3.AllowUserToAddRows = false;

        }
        private string checkId()
        {
            string queryReader = "SELECT TOP 1 * FROM NHANVIEN where MANV LIKE 'NV%' ORDER BY MANV DESC";
            int idAuto = 0;
            string ma;
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
                    ma = "NV010";
                }
                else
                {
                    ma = "NV00" + (idAuto + 1);
                }
            }
            else if (idAuto >= 10 && idAuto < 100)
            {
                if (idAuto == 99)
                {
                    ma = "NV100";
                }
                else
                {
                    ma = "NV0" + (idAuto + 1);
                }
            }
            else
            {
                ma = "NV" + (idAuto + 1);
            }
            return ma;
        }


        private void nhanvien_Load(object sender, EventArgs e)
        {
            checkId();
        }


        private void btn_doimk_Click(object sender, EventArgs e)
        {
            string queryReader = "select * from NHANVIEN";
            SqlDataReader reader = con.loadData(queryReader);
            if (txt_1.Text != "" && txt_2.Text != "" && txt_3.Text != "")
            {
                while(reader.Read())
                { 
                    string nvid = reader["MANV"].ToString();
                    string pw = reader["PASSWORDNV"].ToString();
                    if(IDValue == nvid && pw == txt_1.Text)
                    {
                        query = "UPDATE NHANVIEN SET PASSWORDNV = '"+txt_2.Text+"'  WHERE MANV = '" + nvid + "'";
                        if (MessageBox.Show("Bạn có muốn đổi mật khẩu không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            con.setData(query, "Đổi mật khẩu thành công");

                            nhanvien_VisibleChanged(this, null);
                            return;
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

  

        private void txt_timnv_TextChanged(object sender, EventArgs e)
        {
            txt_timnv.Text.Trim();
            if (txt_timnv.Text == "")
            {
                nhanvien_VisibleChanged(this, null);
            }
            else
            {
                query = "select NHANVIEN.MANV, NHANVIEN.HOTEN, NHANVIEN.EMAIL, NHANVIEN.SDTNV from NHANVIEN WHERE MANV like N'%" + txt_timnv.Text + "%' OR HOTEN like N'%" + txt_timnv.Text + "%' ";
                DataSet ds = con.getData(query);
                gunaDataGridView1.DataSource = ds.Tables[0];
            }
        }

  

        public void ClearText()
        {
            txt_1.Text = "";
            txt_2.Text = "";
            txt_3.Text = "";
            txt_timnv.Text = "";
            txt_idnv.Text = "";
            txt_name.Text = "";
            txt_email.Text = "";
            txt_sdt.Text = "";
            txt_pw.Text = "";
            txt_delete.Text = "";
            label_add.Visible = true;
            btn_add.Visible = true;
        }

        private void btn_hienthi_Click(object sender, EventArgs e)
        {

            txt_1.UseSystemPasswordChar = false;
            txt_2.UseSystemPasswordChar = false;
            txt_3.UseSystemPasswordChar = false;
            btn_hienthi.Visible = false;
            btn_hide.Visible = true;
        }

        private void btn_huy_Click(object sender, EventArgs e)
        {
            txt_1.Text = "";
            txt_2.Text = "";
            txt_3.Text = "";
        }
        private void btn_hide_Click(object sender, EventArgs e)
        {
            txt_1.UseSystemPasswordChar = true;
            txt_2.UseSystemPasswordChar = true;
            txt_3.UseSystemPasswordChar = true;
            btn_hienthi.Visible = true;
            btn_hide.Visible = false;
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

        static bool ValidatePhoneNumber(string phoneNumber)
        {
            string pattern = @"^(0[35789]|0084)[0-9]{8,9}$";
            // Sử dụng biểu thức chính quy để kiểm tra định dạng số điện thoại
          
             // Định dạng số điện thoại ở Việt Nam
            return Regex.IsMatch(phoneNumber, pattern);
        }
        private void btn_add_Click(object sender, EventArgs e)
        {

            string queryReader = "select * from NHANVIEN";
            SqlDataReader reader = con.loadData(queryReader);

            if (txt_idnv.Text != "" && txt_name.Text != "" && txt_email.Text != "" && txt_pw.Text != "" && txt_sdt.Text != "")
            {
                while (reader.Read())
                {
                    string NvId = reader["MANV"].ToString();
                    string emailNV = reader["EMAIL"].ToString();
                    string phoneNV = reader["SDTNV"].ToString();
                    if (NvId == txt_idnv.Text)
                    {
                        MessageBox.Show("Thêm nhân viên bại do trùng ID trong hệ thống", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if(IsEmail(txt_email.Text) == false)
                    {
                        MessageBox.Show("Không đúng dịnh dạng email", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;

                    }
                    if (emailNV == txt_email.Text)
                    {
                        MessageBox.Show("Email đã tồn tại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    bool isValidPhoneNumber = ValidatePhoneNumber(txt_sdt.Text);
                    if(isValidPhoneNumber == false)
                    {
                        MessageBox.Show("Không đúng dịnh dạng số điện thoại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (phoneNV == txt_sdt.Text)
                    {
                        MessageBox.Show("Số điện thoại đã tồn tại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (getIDValue() != "ADMIN" && cb_quyenhan.Text == "Admin")
                {
                    MessageBox.Show("Bạn không có quyền hạn đưa tài khoản này lên Admin!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                query = "insert into NHANVIEN(MANV, HOTEN, EMAIL, PASSWORDNV, SDTNV,ISADMIN) values ('" + txt_idnv.Text + "', N'" + txt_name.Text + "', N'" + txt_email.Text + "', '" + txt_pw.Text + "', '" + txt_sdt.Text + "', N'"+cb_quyenhan.Text+"')";

                con.setData(query, "Thêm nhân viên thành công");

                nhanvien_VisibleChanged(this, null);
                ClearText();
            }
            else
            {
                MessageBox.Show("vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_fix_Click(object sender, EventArgs e)
        {
            string queryReader = "select * from NHANVIEN where MANV = '" + gunaDataGridView3.SelectedRows[0].Cells[0].Value.ToString() + "'";
            SqlDataReader reader = con.loadData(queryReader);
            if (txt_idnv.Text != "" && txt_name.Text != "" && txt_email.Text != "" && txt_pw.Text != "" && txt_sdt.Text != "")
            {
                while (reader.Read())
                {
                    string NvId = reader["MANV"].ToString();
                    string emailNV = reader["EMAIL"].ToString();
                    string phoneNV = reader["SDTNV"].ToString();
                    string roleNV = reader["ISADMIN"].ToString();
                    if(getIDValue() != "ADMIN" && txt_idnv.Text == "ADMIN")
                    {
                        MessageBox.Show("Không thể sửa tài khoản admin cấp cao nhất!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        nhanvien_VisibleChanged(this, null);
                        return;
                    }
                    if (getIDValue() != "ADMIN" && roleNV == "Admin")
                    {
                        MessageBox.Show("Không có quyền sửa tài khoản admin khác!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        nhanvien_VisibleChanged(this, null);
                        return;
                    }
                    
                    if (IsEmail(txt_email.Text) == false)
                    {
                        MessageBox.Show("Không đúng dịnh dạng email", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;

                    }
                    if (oldEmail != txt_email.Text)
                    {
                        if (emailNV == txt_email.Text)
                        {
                            MessageBox.Show("Email đã tồn tại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    bool isValidPhoneNumber = ValidatePhoneNumber(txt_sdt.Text);
                    if (isValidPhoneNumber == false)
                    {
                        MessageBox.Show("Không đúng dịnh dạng số điện thoại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (oldPhone != txt_sdt.Text)
                    {
                        if (phoneNV == txt_sdt.Text)
                        {
                            MessageBox.Show("Số điện thoại đã tồn tại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    if (getIDValue() != "ADMIN" && cb_quyenhan.Text == "Admin")
                    {
                        MessageBox.Show("Không có quyền nâng quyền hạn cho khoản khác!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        nhanvien_VisibleChanged(this, null);
                        return;
                    }
                }

                string choose = gunaDataGridView3.SelectedRows[0].Cells[0].Value.ToString();

                query = "UPDATE NHANVIEN SET HOTEN = N'" + txt_name.Text + "', EMAIL = '" + txt_email.Text + "', PASSWORDNV = '" + txt_pw.Text + "', SDTNV = '" + txt_sdt.Text + "' WHERE MANV = '" + choose + "'";
                if (MessageBox.Show("Bạn có muốn sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.setData(query, "Sửa nhân viên thành công");

                    nhanvien_VisibleChanged(this, null);
                    ClearText();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
               
        }

        private void txt_delete_TextChanged(object sender, EventArgs e)
        {

            txt_delete.Text.Trim();
            if (txt_delete.Text == "")
            {
                nhanvien_VisibleChanged(this, null);
            }
            else
            {
                query = "select NHANVIEN.MANV, NHANVIEN.HOTEN, NHANVIEN.EMAIL, NHANVIEN.SDTNV, NHANVIEN.ISADMIN from NHANVIEN WHERE MANV like '%" + txt_delete.Text + "%'";
                DataSet ds = con.getData(query);
                gunaDataGridView3.DataSource = ds.Tables[0];
            }
        }

        private void gunaDataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            txt_idnv.Text = gunaDataGridView3.SelectedRows[0].Cells[0].Value.ToString();
            txt_name.Text = gunaDataGridView3.SelectedRows[0].Cells[1].Value.ToString();
            oldEmail = gunaDataGridView3.SelectedRows[0].Cells[2].Value.ToString();
            txt_email.Text = gunaDataGridView3.SelectedRows[0].Cells[2].Value.ToString();
            oldPhone = gunaDataGridView3.SelectedRows[0].Cells[3].Value.ToString();
            txt_sdt.Text = gunaDataGridView3.SelectedRows[0].Cells[3].Value.ToString();
            cb_quyenhan.Text = gunaDataGridView3.SelectedRows[0].Cells[4].Value.ToString();
            string queryReader = "select PASSWORDNV, MANV from NHANVIEN";
            SqlDataReader reader = con.loadData(queryReader);
            while (reader.Read())
            {
                string NvId = reader["MANV"].ToString();
                string pw = reader["PASSWORDNV"].ToString();
                if (NvId == txt_idnv.Text)
                {
                    txt_pw.Text = pw;
                }
            }
            
            btn_fix.Visible = true;
            btn_add.Visible = false;
            label_add.Visible = false;
            label_fix.Visible = true;
            txt_idnv.ReadOnly = true;
            btn_auto.Visible = false;
         }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (txt_delete.Text != "")
            {
                string queryReader = "select * from NHANVIEN where MANV = '" + gunaDataGridView3.SelectedRows[0].Cells[0].Value.ToString() + "'";
                SqlDataReader reader = con.loadData(queryReader);
                while (reader.Read())
                {
                    string NvId = reader["MANV"].ToString();
                    string roleNV = reader["ISADMIN"].ToString();
                    if (getIDValue() != "ADMIN" && txt_idnv.Text == "ADMIN")
                    {
                        MessageBox.Show("Không thể xóa tài khoản admin cấp cao nhất!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        nhanvien_VisibleChanged(this, null);
                        return;
                    }
                    if (getIDValue() != "ADMIN" && roleNV == "Admin")
                    {
                        MessageBox.Show("Không có quyền xóa tài khoản admin khác!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        nhanvien_VisibleChanged(this, null);
                        return;
                    }

                }
                string choose = gunaDataGridView3.SelectedRows[0].Cells[0].Value.ToString();
                query = "DELETE NHANVIEN WHERE MANV = '" + choose + "'";
                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.setData(query, "Xóa nhân viên thành công");

                    nhanvien_VisibleChanged(this, null);
                    ClearText();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập ID", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tabPage3_Leave(object sender, EventArgs e)
        {
            ClearText();
        }

        private void tabPage2_Leave(object sender, EventArgs e)
        {
            ClearText();
        }

        private void tabPage1_Leave(object sender, EventArgs e)
        {
            ClearText();
        }


        private void txt_idnv_TextChanged(object sender, EventArgs e)
        {
            if (txt_idnv.Text.Length > 5)
            {
                txt_idnv.Text = txt_idnv.Text.Substring(0, 5);
                txt_idnv.SelectionStart = 5;
            }
        }

        private void txt_sdt_TextChanged(object sender, EventArgs e)
        {
            if (txt_sdt.Text.Length >= 12)
            {
                txt_sdt.Text = txt_idnv.Text.Substring(0, 11);
                txt_sdt.SelectionStart = 11;
            }
        }

        private void txt_sdt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }

        private void btn_updategird_Click(object sender, EventArgs e)
        {
            if (getIDValue() == "ADMIN")
            {
                DataTable dt = (DataTable)gunaDataGridView1.DataSource;
                string query = "select * from NHANVIEN";
                int k = 0;
                if (MessageBox.Show("Bạn có muốn update lại không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    k = con.updateTable(dt, query);
                    nhanvien_VisibleChanged(this, null);
                    ClearText();
                }

                if (k != 0)
                {
                    MessageBox.Show("Update thành công");
                }
            }
            else
            {
                MessageBox.Show("Không có quyền hạn");
                nhanvien_VisibleChanged(this, null);
            }

        }


        private void btn_auto_Click(object sender, EventArgs e)
        {
            txt_idnv.Text = checkId();
        }


    }
}
