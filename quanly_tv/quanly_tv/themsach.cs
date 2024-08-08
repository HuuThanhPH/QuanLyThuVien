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
    public partial class themsach : UserControl
    {
        connect con = new connect();
        string query;
        bool check_cb = false;
        themloaisach type = new themloaisach();
        public themsach()
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

        //public void ReloadData()
        //{
        //    query = "select * from SACH";
        //    DataSet ds = con.getData(query);
            
        //    // Cập nhật dữ liệu trong GridView
        //    gunaDataGridView1.DataSource = ds.Tables[0];
        //    gunaDataGridView1.Refresh();
        //    dataComBoBox();
        //}

        private void checkIdSh(string key)
        {

            string queryReader = "SELECT TOP 1 * FROM SACH where MASH LIKE '"+key+"%' ORDER BY MASH DESC";
            int count = 0;
            DataSet ds = con.getData(queryReader);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Lấy giá trị của cột MA_KIEMTRA từ dòng đầu tiên
                string maKiemTra = ds.Tables[0].Rows[0]["MASH"].ToString();
                string newstring = maKiemTra.Substring(maKiemTra.Length - 3, 3);
                count = int.Parse(newstring);
            }

            if (count < 10)
            {
                if (count == 9)
                {
                    txt_idbook.Text = key + "010";
                }
                else
                {
                    count = count + 1;
                    txt_idbook.Text = key + "00" + count;
                }
            }
            else if (count >= 10 && count < 100)
            {
                if (count == 99)
                {
                    txt_idbook.Text = key + "100";
                }
                else
                {
                    count = count + 1;
                    txt_idbook.Text = key + "0" + count;
                }
            }
            else
            {
                count = count + 1;
                txt_idbook.Text = "" + key + count;
            }
        }

        private void themsach_VisibleChanged(object sender, EventArgs e)
        {

            btn_fixbook.Visible = false;
            btn_deletebook.Visible = false;
            label_fix_dele.Visible = false;
            txt_typebook.SelectedIndex = -1;
            txt_idtg.SelectedIndex = -1;
            txt_idnxb1.SelectedIndex = -1;
            query = "select a.MASH, a.TENSH, a.SL ,a.MATG, b.TENTG, a.NAMXB, a.MANXB, c.TENNXB, a.MALOAI, d.TENLOAI from SACH a join TACGIA b on a.MATG = b.MATG join NHAXUATBAN c on a.MANXB = c.MANXB join LOAISACH d on a.MALOAI = d.MALOAI";
            DataSet ds = con.getData(query);
            gunaDataGridView1.DataSource = ds.Tables[0];
            dataComBoBox();
            if (check_cb == true)
            {
                if (txt_typebook.SelectedValue != null)
                {
                    checkIdSh(txt_typebook.SelectedValue.ToString());
                }
            }
        }

        

        private void dataComBoBox()
        {
            check_cb = false;
            DataSet ds = con.getData("select MALOAI,TENLOAI from LOAISACH");
            DataSet ds1 = con.getData("select MATG,TENTG from TACGIA");
            DataSet ds3 = con.getData("select MANXB,TENNXB from NHAXUATBAN");
            txt_typebook.DataSource = ds.Tables[0];
            txt_idtg.DataSource = ds1.Tables[0];
            txt_idnxb1.DataSource = ds3.Tables[0];
            txt_typebook.DisplayMember = "TENLOAI";
            txt_typebook.ValueMember = "MALOAI";
            txt_idtg.DisplayMember = "TENTG";
            txt_idtg.ValueMember = "MATG";
            txt_idnxb1.DisplayMember = "TENNXB";
            txt_idnxb1.ValueMember = "MANXB";
            check_cb = true;            
        }

        private void btn_addbook_Click(object sender, EventArgs e)
        {
            string queryReader = "select * from SACH";
            SqlDataReader reader = con.loadData(queryReader);


            if(txt_idbook.Text != "" && txt_namebook.Text != ""  
            && txt_idtg.Text != "" && txt_nxb.Text != "" 
            && txt_idnxb1.Text != "" && txt_typebook.Text != "" && txt_sl_book.Text != "")
            {

                if (int.Parse(txt_nxb.Text) < 1950 || int.Parse(txt_nxb.Text) > DateTime.Now.Year)
                {
                    MessageBox.Show("Thêm sách thất bại do năm xuất bản không phù hợp", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if(int.Parse(txt_sl_book.Text) < 0)
                {
                    MessageBox.Show("Số lượng phải lớn hơn hoặc = 0", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                while(reader.Read())
                { string bookid = reader["MASH"].ToString();
                    if(bookid == txt_idbook.Text)
                    {
                        MessageBox.Show("Thêm sách thất bại do trùng ID sách trong hệ thống", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                query = "insert into SACH(MASH, TENSH,SL, MATG, NAMXB, MANXB, MALOAI,MANV) values ('" + txt_idbook.Text + "', N'" + txt_namebook.Text + "','" + txt_sl_book.Text + "' ,'" + txt_idtg.SelectedValue.ToString() + "', '" + txt_nxb.Text + "', '" + txt_idnxb1.SelectedValue.ToString() + "', '" + txt_typebook.SelectedValue.ToString() + "','"+IDValue+"')";
                con.setData(query, "Thêm sách thành công");

                themsach_VisibleChanged(this, null);
                ClearText();
                return;
                                 
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ClearText()
        {
            txt_namebook.Text = "";
            txt_idtg.Text = "";
            txt_idnxb1.Text = "";
            txt_nxb.Text = "";
            txt_sl_book.Text = "";
            txt_typebook.SelectedIndex = 0;
        }

        private void btn_fixbook_Click(object sender, EventArgs e)
        {
            if (txt_idbook.Text != "" && txt_namebook.Text != "" && txt_idtg.Text != "" && txt_idnxb1.Text != "" && txt_nxb.Text != "" && txt_typebook.Text != "" && txt_sl_book.Text != "")
            {
                string choose = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                query = "UPDATE SACH SET TENSH = N'" + txt_namebook.Text + "', SL = '"+ txt_sl_book.Text +"' ,MATG = '" + txt_idtg.SelectedValue.ToString() + "', NAMXB = '" + txt_nxb.Text + "', MANXB = '" + txt_idnxb1.SelectedValue.ToString() + "', MALOAI = '" + txt_typebook.SelectedValue.ToString() + "'  WHERE MASH = '" + choose + "'";
                if (MessageBox.Show("Bạn có muốn sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.setData(query, "Sửa sách thành công");

                    themsach_VisibleChanged(this, null);
                    ClearText();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_deletebook_Click(object sender, EventArgs e)
        {
            if (txt_idbook.Text != "" && txt_namebook.Text != "" && txt_idtg.Text != "" && txt_idnxb1.Text != "" && txt_nxb.Text != "" && txt_typebook.Text != "" && txt_sl_book.Text != "")
            {
                string choose = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                query = "DELETE SACH WHERE MASH = '" + choose + "'";
                string queryReference = "DELETE MUONTRASACH WHERE MASH= '" + choose + "'";
                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    con.setData(queryReference, "");
                    con.setData(query, "Xóa sách thành công");

                    themsach_VisibleChanged(this, null);
                    ClearText();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_idbook.Text = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txt_namebook.Text = gunaDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txt_sl_book.Text = gunaDataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txt_idtg.Text = gunaDataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            txt_nxb.Text = gunaDataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            txt_idnxb1.Text = gunaDataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            txt_typebook.Text = gunaDataGridView1.SelectedRows[0].Cells[9].Value.ToString();
            btn_fixbook.Visible = true;
            btn_deletebook.Visible = true;
            label_fix_dele.Visible = true;
            label_add.Visible = false;
            btn_addbook.Visible = true;
            txt_idbook.ReadOnly = true;

        }

        private void themsach_Leave(object sender, EventArgs e)
        {
            ClearText();
        }

        private void txt_timkemnv_TextChanged(object sender, EventArgs e)
        {
            txt_timkemsach.Text.Trim();
            if (txt_timkemsach.Text == "")
            {
                themsach_VisibleChanged(this, null);
            }
            else
            {
                query = "select * from SACH WHERE MASH like '%" + txt_timkemsach.Text + "%' OR TENSH like N'%" + txt_timkemsach.Text + "%' ";
                DataSet ds = con.getData(query);
                gunaDataGridView1.DataSource = ds.Tables[0];
            }
        }

        private void txt_nxb_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txt_idbook_TextChanged(object sender, EventArgs e)
        {

            if (txt_idbook.Text.Length >= 6)
            {
                txt_idbook.Text =txt_idbook.Text.Substring(0, 5);
                txt_idbook.SelectionStart = 5;
            }
        }

        private void txt_nxb_TextChanged(object sender, EventArgs e)
        {
            if (txt_nxb.Text.Length >= 5)
            {
                txt_nxb.Text = txt_nxb.Text.Substring(0, 4);
                txt_nxb.SelectionStart = 4;
            }
        }


        private void txt_sl_book_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        private void btn_updategrid_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)gunaDataGridView1.DataSource;
            string query = "select * from SACH";
            int k =0;
            if (MessageBox.Show("Bạn có muốn update lại không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                k = con.updateTable(dt, query);
                themsach_VisibleChanged(this, null);
                ClearText();
            }

            if (k != 0)
            {
                MessageBox.Show("Update thành công");
            }
           
        }

        private void txt_typebook_SelectedValueChanged(object sender, EventArgs e)
        {
            if (check_cb == true)
            {
                if (txt_typebook.SelectedValue != null)
                {
                    checkIdSh(txt_typebook.SelectedValue.ToString());
                }
            }
        }


    }
}
