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
using System.Text.RegularExpressions;

namespace quanly_tv
{
    public partial class themnxb : UserControl
    {
        connect con = new connect();
        string query;
        string oldPhone = "";
        public themnxb()
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


        private void themnxb_Load(object sender, EventArgs e)
        {
           
        }

        private void checkIdTG()
        {
            string queryReader = "SELECT TOP 1 * FROM NHAXUATBAN ORDER BY MANXB DESC";
            int count = 0;
            DataSet ds = con.getData(queryReader);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Lấy giá trị của cột MA_KIEMTRA từ dòng đầu tiên
                string maKiemTra = ds.Tables[0].Rows[0]["MANXB"].ToString();
                string newstring = maKiemTra.Substring(maKiemTra.Length - 2, 2);
                count = int.Parse(newstring);
            }
           
            if (count < 10)
            {
                if (count == 9)
                {
                    txt_idnxb.Text = "NXB10";  
                }
                else
                {
                    txt_idnxb.Text = "NXB0" + (count + 1);
                }
            }
            else
            {
                txt_idnxb.Text = "NXB" + (count + 1);
            }
        }

        private void themnxb_VisibleChanged(object sender, EventArgs e)
        {
            btn_fix_nxb.Visible = false;
            btn_delete_nxb.Visible = false;
            lab_fix.Visible = false;
            lab_add.Visible = true;
            btn_add_nxb.Visible = true;
            query = "select MANXB, TENNXB, DCNXB, DTNXB from NHAXUATBAN";
            DataSet ds = con.getData(query);
            gunaDataGridView1.DataSource = ds.Tables[0];
            checkIdTG();
        }

        static bool ValidatePhoneNumber(string phoneNumber)
        {
            string pattern = @"^(0[35789]|0084)[0-9]{8,9}$";
            
            // Định dạng số điện thoại ở Việt Nam
            return Regex.IsMatch(phoneNumber, pattern);
        }

        private void btn_add_nxb_Click(object sender, EventArgs e)
        {

            string queryReader = "select * from NHAXUATBAN";
            SqlDataReader reader = con.loadData(queryReader);

            if (txt_idnxb.Text != "" && txt_namenxb.Text != "" && txt_address_nxb.Text != "" && txt_phone_nxb.Text != "")
            {
                while (reader.Read())
                {
                    string NxbId = reader["MANXB"].ToString();
                    string phoneNXB = reader["DTNXB"].ToString();
                    if (NxbId == txt_idnxb.Text)
                    {
                        MessageBox.Show("Thêm nhà xuất bản thất bại do trùng ID trong hệ thống", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    bool isValidPhoneNumber = ValidatePhoneNumber(txt_phone_nxb.Text);
                    if (isValidPhoneNumber == false)
                    {
                        MessageBox.Show("Không đúng dịnh dạng số điện thoại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    
                    if (phoneNXB == txt_phone_nxb.Text)
                    {
                        MessageBox.Show("Thêm nhà xuất bản thất bại do trùng số điện thoại trong hệ thống", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                query = "insert into NHAXUATBAN(MANXB, TENNXB, DCNXB, DTNXB, MANV) values ('" + txt_idnxb.Text + "', N'" + txt_namenxb.Text + "', N'" + txt_address_nxb.Text + "', '" + txt_phone_nxb.Text + "','" + IDValue + "')";
                con.setData(query, "Thêm nhà xuất bản thành công");

                themnxb_VisibleChanged(this, null);
                ClearText();
            }
            else
            {
                MessageBox.Show("Thêm nhà xuất bản thất bại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ClearText()
        {
            txt_namenxb.Text = "";
            txt_address_nxb.Text = "";
            txt_phone_nxb.Text = "";
            lab_add.Visible = true;
            btn_add_nxb.Visible = true;
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_idnxb.Text = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txt_namenxb.Text = gunaDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txt_address_nxb.Text = gunaDataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txt_phone_nxb.Text = gunaDataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            oldPhone = gunaDataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            btn_fix_nxb.Visible = true;
            btn_delete_nxb.Visible = true;
            lab_fix.Visible = true;
            lab_add.Visible = false;
            btn_add_nxb.Visible = false;
            txt_idnxb.ReadOnly = true;
        }

        private void btn_fix_nxb_Click(object sender, EventArgs e)
        {
            if (txt_idnxb.Text != "" && txt_namenxb.Text != "" && txt_address_nxb.Text != "" && txt_phone_nxb.Text != "")
            {
                string queryReader = "select * from NHAXUATBAN where MANXB = '" + gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'";
                SqlDataReader reader = con.loadData(queryReader);
                while (reader.Read())
                {
                    string phoneNXB = reader["DTNXB"].ToString();
                    bool isValidPhoneNumber = ValidatePhoneNumber(txt_phone_nxb.Text);
                    if (isValidPhoneNumber == false)
                    {
                        MessageBox.Show("Không đúng dịnh dạng số điện thoại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                   if(oldPhone != txt_phone_nxb.Text) {
                        if (phoneNXB == txt_phone_nxb.Text)
                        {
                            MessageBox.Show("Thêm nhà xuất bản thất bại do trùng số điện thoại trong hệ thống", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                   }
                }
               
                string choose = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                query = "UPDATE NHAXUATBAN SET TENNXB = N'" + txt_namenxb.Text + "', DCNXB = '" + txt_address_nxb.Text + "', DTNXB = '" + txt_phone_nxb.Text + "' WHERE MANXB = '" + choose + "'";
                if (MessageBox.Show("Bạn có muốn sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.setData(query, "Sửa nhà xuất bản thành công");

                    themnxb_VisibleChanged(this, null);
                    ClearText();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_delete_nxb_Click(object sender, EventArgs e)
        {
            if (txt_idnxb.Text != "" && txt_namenxb.Text != "" && txt_address_nxb.Text != "" && txt_phone_nxb.Text != "")
            {
                string choose = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string queryReference = "DELETE SACH WHERE MANXB = '" + choose + "'";
                query = "DELETE NHAXUATBAN WHERE MANXB = '" + choose + "'";
                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.setData(queryReference, "");
                    con.setData(query, "Xóa nhà xuất bản thành công");

                    //themsach themsach1 = new themsach();
                    //themsach1.ReloadData();
                    themnxb_VisibleChanged(this, null);
                    ClearText();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void themnxb_Leave(object sender, EventArgs e)
        {
            ClearText();
        }

        private void txt_timnxb_TextChanged(object sender, EventArgs e)
        {
            string name = txt_timnxb.Text.Trim();
            if (name == "")
            {
                themnxb_VisibleChanged(this, null);
            }
            else
            {
                query = "select * from NHAXUATBAN WHERE MANXB like '%" + name + "%' or TENNXB like N'%" + name + "%'";
                DataSet ds = con.getData(query);
                gunaDataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void txt_phone_nxb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }


        private void txt_idnxb_TextChanged(object sender, EventArgs e)
        {
            if (txt_idnxb.Text.Length > 5)
            {
                txt_idnxb.Text = txt_idnxb.Text.Substring(0, 5);
                txt_idnxb.SelectionStart = 5;
            }
        }

        private void txt_phone_nxb_TextChanged(object sender, EventArgs e)
        {
            if (txt_phone_nxb.Text.Length > 11)
            {
                txt_phone_nxb.Text = txt_phone_nxb.Text.Substring(0, 11);
                txt_phone_nxb.SelectionStart =11;
            }
        }

        private void btn_updategrid_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)gunaDataGridView1.DataSource;
            string query = "select * from NHAXUATBAN";
            int k = 0;
            if (MessageBox.Show("Bạn có muốn update lại không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                k = con.updateTable(dt, query);
                themnxb_VisibleChanged(this, null);
                ClearText();
            }

            if (k != 0)
            {
                MessageBox.Show("Update thành công");
            }

        }


    }
}
