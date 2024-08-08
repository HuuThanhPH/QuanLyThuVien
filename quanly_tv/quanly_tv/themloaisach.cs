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
    public partial class themloaisach : UserControl
    {
        connect con = new connect();
        string query;
        public themloaisach()
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

        private void themloaisach_Load(object sender, EventArgs e)
        {
         
        }

        private void themloaisach_VisibleChanged(object sender, EventArgs e)
        {
            query = "select MALOAI, TENLOAI from LOAISACH";
            DataSet ds = con.getData(query);
            gunaDataGridView1.DataSource = ds.Tables[0];
            btn_fixbook.Visible = false;
            btn_deletetypebook.Visible = false;
            lab_fix.Visible = false;
            lab_add.Visible = true;
            btn_addtypebook.Visible = true;
        }

        private void btn_addtypebook_Click(object sender, EventArgs e)
        {
            string queryReader = "select * from LOAISACH";
            SqlDataReader reader = con.loadData(queryReader);
            if (txt_typebook.Text != "" && txt_nametypebook.Text != "" )
            {
                while (reader.Read())
                {
                    string typebookId = reader["MALOAI"].ToString();
                    if (typebookId == txt_typebook.Text)
                    {
                        MessageBox.Show("Thêm loại sách thất bại do trùng ID trong hệ thống", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                query = "insert into LOAISACH(MALOAI, TENLOAI,MANV) values ('" + txt_typebook.Text + "', N'" + txt_nametypebook.Text + "','" + IDValue + "')";
                con.setData(query, "Thêm loại sách thành công");
                themsach themsach1 = new themsach();
                //themsach1.ReloadData();
                themloaisach_VisibleChanged(this, null);
                ClearText();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ClearText()
        {
            txt_typebook.Text = "";
            txt_nametypebook.Text = "";
            lab_add.Visible = true;
            btn_addtypebook.Visible = true;
        }

        string txt_typebookupdate = "";
        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_typebookupdate = txt_typebook.Text;
            txt_typebook.Text = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            txt_nametypebook.Text = gunaDataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            btn_fixbook.Visible = true;
            btn_deletetypebook.Visible = true;
            lab_fix.Visible = true;
            btn_addtypebook.Visible = false;
            lab_add.Visible = false;
            txt_typebook.ReadOnly = true;
        }

        private void btn_fixbook_Click(object sender, EventArgs e)
        {
            if (txt_typebook.Text != "" && txt_nametypebook.Text != "")
            {
                string choose = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                query = "UPDATE LOAISACH SET TENLOAI = N'" + txt_nametypebook.Text + "' WHERE MALOAI = '" + choose + "'";
                if(MessageBox.Show("Bạn có muốn sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.setData(query, "Sửa loại sách thành công");

                    themloaisach_VisibleChanged(this, null);
                    ClearText();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }



        delegate void ResetForm();
        private void btn_deletetypebook_Click(object sender, EventArgs e)
        {
            if (txt_typebook.Text != "" && txt_nametypebook.Text != "")
            {
                string choose = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string queryReference = "DELETE SACH WHERE MALOAI = '" + choose + "'";
                query = "DELETE LOAISACH WHERE MALOAI = '" + choose + "'";
                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.setData(queryReference, "");
                    con.setData(query, "Xóa loại sách thành công");
                    //themsach themsach1 = new themsach();
                    //ResetForm refreshFrom = new ResetForm(themsach1.ReloadData);
                    //refreshFrom();
                    themloaisach_VisibleChanged(this, null);
                    ClearText();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void themloaisach_Leave(object sender, EventArgs e)
        {
            ClearText();
        }

        private void txt_timls_TextChanged(object sender, EventArgs e)
        {
            string name = txt_timls.Text.Trim();
            if (name == "")
            {
                themloaisach_VisibleChanged(this, null);
            }
            else
            {
                query = "select * from LOAISACH WHERE MALOAI like '%" + name + "%' or TENLOAI like N'%" + name + "%'";
                DataSet ds = con.getData(query);
                gunaDataGridView1.DataSource = ds.Tables[0];
            }
        }


        private void txt_typebook_TextChanged(object sender, EventArgs e)
        {
            if (txt_typebook.Text.Length > 5)
            {
                txt_typebook.Text = txt_typebook.Text.Substring(0, 5);
                txt_typebook.SelectionStart = 5;
            }
        }

        private void btn_updategird_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)gunaDataGridView1.DataSource;
            string query = "select * from LOAISACH";
            int k = 0;
            if (MessageBox.Show("Bạn có muốn update lại không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                k = con.updateTable(dt, query);
                themloaisach_VisibleChanged(this, null);
                ClearText();
            }

            if (k != 0)
            {
                MessageBox.Show("Update thành công");
            }

        }




    }
}
