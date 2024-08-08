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
    public partial class themdocgia : UserControl
    {
        connect con = new connect();
        string query;
        string oldPhone = "";
        public themdocgia()
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

        private void themdocgia_Load(object sender, EventArgs e)
        {
           
        }

        private void checkIdDG()
        {
            string queryReader = "SELECT TOP 1 * FROM DOCGIA ORDER BY MADG DESC";
            int count = 0;
            string ma;
            DataSet ds = con.getData(queryReader);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Lấy giá trị của cột MA_KIEMTRA từ dòng đầu tiên
                string maKiemTra = ds.Tables[0].Rows[0]["MADG"].ToString();
                string newstring = maKiemTra.Substring(maKiemTra.Length - 3, 3);
                count = int.Parse(newstring);
            }

            if (count < 10)
            {
                if (count == 9)
                {
                    txt_madg.Text = "DG010";
                }
                else
                {
                    txt_madg.Text = "DG00" + (count + 1);
                }
            }
            else if (count >= 10 && count < 100)
            {
                if (count == 99)
                {
                    txt_madg.Text = "DG100";
                }
                else
                {
                    txt_madg.Text = "DG0" + (count + 1);
                }
            }
            else
            {
                txt_madg.Text = "DG" + (count + 1);
            }
        }

        private void themdocgia_VisibleChanged(object sender, EventArgs e)
        {
            btn_fixdg.Visible = false;
            btn_deletedg.Visible = false;
            lab_fix.Visible = false;
            lab_add.Visible = true;
            btn_adddg.Visible = true;
            query = "select MADG, TENDG, NGAYSINH, GIOITINH, LIENHE from DOCGIA";
            DataSet ds = con.getData(query);
            gunaDataGridView1.DataSource = ds.Tables[0];
            checkIdDG();
        }

        static bool ValidatePhoneNumber(string phoneNumber)
        {
            string pattern = @"^(0[35789]|0084)[0-9]{8,9}$";
            
            return Regex.IsMatch(phoneNumber, pattern);
        }

        private void btn_adddg_Click(object sender, EventArgs e)
        {
            string formattedDate = txt_birthday_dg.Value.ToString("yyyy-MM-dd");
            string queryReaderDg = "select * from DOCGIA";
            SqlDataReader reader = con.loadData(queryReaderDg);

            if (txt_madg.Text != "" && txt_namedg.Text != "" && txt_birthday_dg.Value != null && txt_sex.Text != "" && txt_phone.Text != "")
            {

                while (reader.Read())
                {
                    string DgId = reader["MADG"].ToString();
                    DateTime txt_date = Convert.ToDateTime(formattedDate);
                    DateTime now = DateTime.Now;
                    string phoneDG = reader["LIENHE"].ToString();
                    if (DgId == txt_madg.Text)
                    {
                        MessageBox.Show("Thêm độc giả thất bại do trùng ID trong hệ thống", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (txt_date > now)
                    {
                        MessageBox.Show("Thêm độc giả thất bại do ngày muốn thêm lớn hơn hiện tại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    bool isValidPhoneNumber = ValidatePhoneNumber(txt_phone.Text);
                    if (isValidPhoneNumber == false)
                    {
                        MessageBox.Show("Không đúng dịnh dạng số điện thoại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (phoneDG == txt_phone.Text)
                    {
                        MessageBox.Show("Thêm độc giả thất bại do trùng số điện thoại trong hệ thống", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                query = "insert into DOCGIA(MADG, TENDG, NGAYSINH, GIOITINH, LIENHE,MANV) values ('" + txt_madg.Text + "', N'" + txt_namedg.Text + "', '" + formattedDate + "', N'" + txt_sex.Text + "', '" + txt_phone.Text + "','" + IDValue + "')";
                con.setData(query, "Thêm độc giả thành công");

                themdocgia_VisibleChanged(this, null);
                ClearText();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ClearText()
        {
            txt_namedg.Text = "";
            txt_sex.Text = "";
            txt_phone.Text = "";
            lab_add.Visible = true;
            btn_adddg.Visible = true;
        }

        private void txt_birthday_dg_ValueChanged(object sender, EventArgs e)
        {

        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DateTime formattedDate = Convert.ToDateTime(gunaDataGridView1.SelectedRows[0].Cells[2].Value.ToString());
            txt_madg.Text = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txt_namedg.Text = gunaDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txt_birthday_dg.Value = formattedDate;
            txt_sex.Text = gunaDataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            oldPhone = gunaDataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            txt_phone.Text = gunaDataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            btn_fixdg.Visible = true;
            btn_deletedg.Visible = true;
            lab_fix.Visible = true;
            lab_add.Visible = false;
            btn_adddg.Visible = false;
            txt_madg.ReadOnly = true;
        }

        private void btn_fixdg_Click(object sender, EventArgs e)
        {
            if (txt_madg.Text != "" && txt_namedg.Text != "" && txt_sex.Text != "" && txt_phone.Text != "")
            {
                string queryReader = "select * from DOCGIA where MADG = '" + gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString() + "'";
                SqlDataReader reader = con.loadData(queryReader);
                while (reader.Read())
                {
                    string phoneDG = reader["LIENHE"].ToString();
                    bool isValidPhoneNumber = ValidatePhoneNumber(txt_phone.Text);
                    if (isValidPhoneNumber == false)
                    {
                        MessageBox.Show("Không đúng dịnh dạng số điện thoại", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (oldPhone != txt_phone.Text)
                    {
                        if (phoneDG == txt_phone.Text)
                        {
                            MessageBox.Show("Thêm độc giả thất bại do trùng số điện thoại trong hệ thống", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                
                string formattedDate = txt_birthday_dg.Value.ToString("yyyy-MM-dd");
                string choose = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                query = "UPDATE DOCGIA SET TENDG = '" + txt_namedg.Text + "', NGAYSINH = '" + formattedDate + "',GIOITINH = '" + txt_sex.Text + "', LIENHE = '" + txt_phone.Text + "'  WHERE MADG = '" + choose + "'";
                if (MessageBox.Show("Bạn có muốn sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.setData(query, "Sửa độc giả thành công");

                    themdocgia_VisibleChanged(this, null);
                    ClearText();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_deletedg_Click(object sender, EventArgs e)
        {
            if (txt_madg.Text != "" && txt_namedg.Text != "" && txt_sex.Text != "" && txt_phone.Text != "")
            {
                string choose = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string queryReference = "DELETE MUONTRASACH WHERE MADG= '" + choose + "'";
                query = "DELETE DOCGIA WHERE MADG = '" + choose + "'";
                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.setData(queryReference, "");
                    con.setData(query, "Xóa độc giả thành công");

                    themsach themsach1 = new themsach();
                    //themsach1.ReloadData();
                    themdocgia_VisibleChanged(this, null);
                    ClearText();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void themdocgia_Leave(object sender, EventArgs e)
        {
            ClearText();
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            string name = txt_timdg.Text.Trim();
            if (name == "")
            {
                themdocgia_VisibleChanged(this, null);
            }
            else
            {
                query = "select * from DOCGIA WHERE MADG like '%" + name + "%' or TENDG like N'%" + name + "%'";
                DataSet ds = con.getData(query);
                gunaDataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void txt_phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                e.Handled = true;
        }


        private void txt_madg_TextChanged(object sender, EventArgs e)
        {
            if (txt_madg.Text.Length > 5)
            {
                txt_madg.Text = txt_madg.Text.Substring(0,5);
                txt_madg.SelectionStart = 5;
            }
        }

        private void txt_phone_TextChanged(object sender, EventArgs e)
        {
            if (txt_phone.Text.Length > 11)
            {
                txt_phone.Text = txt_phone.Text.Substring(0, 11);
                txt_phone.SelectionStart = 11;
            }
        }

        private void btn_updategird_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)gunaDataGridView1.DataSource;
            string query = "select * from DOCGIA";
            int k = 0;
            if (MessageBox.Show("Bạn có muốn update lại không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                k = con.updateTable(dt, query);
                themdocgia_VisibleChanged(this, null);
                ClearText();
            }

            if (k != 0)
            {
                MessageBox.Show("Update thành công");
            }

        }


    }
}
