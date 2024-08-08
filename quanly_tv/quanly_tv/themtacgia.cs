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
    public partial class themtacgia : UserControl
    {

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
        public themtacgia()
        {
            InitializeComponent();
        }


        private void themtacgia_Load(object sender, EventArgs e)
        {
          

        }

        private void checkIdTG()
        {
            string queryReader = "SELECT TOP 1 * FROM TACGIA ORDER BY MATG DESC";
            int count = 0;
            string ma;
            DataSet ds = con.getData(queryReader);

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // Lấy giá trị của cột MA_KIEMTRA từ dòng đầu tiên
                string maKiemTra = ds.Tables[0].Rows[0]["MATG"].ToString();
                string newstring = maKiemTra.Substring(maKiemTra.Length - 3, 3);
                count = int.Parse(newstring);
            }
            if (count < 10)
            {
                if (count == 9)
                {
                    txt_idtg.Text = "TG010"; 
                }
                else
                {
                    txt_idtg.Text = "TG00" + (count + 1);
                }
            }
            else if (count >= 10 && count < 100)
            {
                if (count == 99)
                {
                    txt_idtg.Text = "TG100";
                }
                else
                {
                    txt_idtg.Text = "TG0" + (count + 1);
                }
            }
            else
            {
                txt_idtg.Text = "TG" + (count + 1);
            }
        }

        private void themtacgia_VisibleChanged(object sender, EventArgs e)
        {
            btn_fixtg.Visible = false;
            btn_deletetg.Visible = false;
            lab_fix_dele.Visible = false;
            btn_addtg.Visible = true;
            lab_add.Visible = true;
            query = "select MATG, TENTG, DIACHI from TACGIA";
            DataSet ds = con.getData(query);
            gunaDataGridView2.DataSource = ds.Tables[0];
            checkIdTG();
        }

        private void btn_addtg_Click(object sender, EventArgs e)
        {

            string queryReaderTg = "select * from TACGIA";
            SqlDataReader reader = con.loadData(queryReaderTg);

            if (txt_idtg.Text != "" && txt_nametg.Text != "" && txt_addresstg.Text != "")
            {
                while (reader.Read())
                {
                    string TgId = reader["MATG"].ToString();
                    if (TgId == txt_idtg.Text)
                    {
                        MessageBox.Show("Thêm tác giả thất bại do trùng ID trong hệ thống", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                query = "insert into TACGIA(MATG, TENTG, DIACHI, MANV) values ('" + txt_idtg.Text + "', N'" + txt_nametg.Text + "', N'" + txt_addresstg.Text + "', '"+IDValue+"')";
                con.setData(query, "Thêm tác giả thành công");

                themtacgia_VisibleChanged(this, null);
                ClearText();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ClearText()
        {
            txt_nametg.Text = "";
            txt_addresstg.Text = "";
            btn_addtg.Visible = true;
            lab_add.Visible = true;
        }

        private void btn_fixtg_Click(object sender, EventArgs e)
        {
            if (txt_idtg.Text != "" && txt_nametg.Text != "" && txt_addresstg.Text != "")
            {
                string choose = gunaDataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                query = "UPDATE TACGIA SET TENTG = '" + txt_nametg.Text + "', DIACHI = '" + txt_addresstg.Text + "' WHERE MATG = '" + choose + "'";
                if (MessageBox.Show("Bạn có muốn sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.setData(query, "Sửa tác giả thành công");

                    themtacgia_VisibleChanged(this, null);
                    ClearText();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_deletetg_Click(object sender, EventArgs e)
        {
            if (txt_idtg.Text != "" && txt_nametg.Text != "" && txt_addresstg.Text != "")
            {
                string choose = gunaDataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                string queryReference = "DELETE SACH WHERE MATG = '" + choose + "'";
                query = "DELETE TACGIA WHERE MATG = '" + choose + "'";
                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.setData(queryReference, "");
                    con.setData(query, "Xóa tác giả thành công");

                    //themsach themsach1 = new themsach();
                    //themsach1.ReloadData();
                    themtacgia_VisibleChanged(this, null);
                    ClearText();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void gunaDataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_idtg.Text = gunaDataGridView2.SelectedRows[0].Cells[0].Value.ToString();
            txt_nametg.Text = gunaDataGridView2.SelectedRows[0].Cells[1].Value.ToString();
            txt_addresstg.Text = gunaDataGridView2.SelectedRows[0].Cells[2].Value.ToString();
            btn_fixtg.Visible = true;
            btn_deletetg.Visible = true;
            lab_fix_dele.Visible = true;
            btn_addtg.Visible = false;
            lab_add.Visible = false;
            txt_idtg.ReadOnly = true;
        }

        private void themtacgia_Leave(object sender, EventArgs e)
        {
            ClearText();
        }

        private void txt_timtg_TextChanged(object sender, EventArgs e)
        {
            string name = txt_timtg.Text.Trim();
            if (name == "")
            {
                themtacgia_VisibleChanged(this, null);
            }
            else
            {
                query = "select * from TACGIA WHERE MATG like '%" + name + "%' or TENTG like N'%" + name + "%'";
                DataSet ds = con.getData(query);
                gunaDataGridView2.DataSource = ds.Tables[0];
            }
        }


        private void txt_idtg_TextChanged(object sender, EventArgs e)
        {
            if (txt_idtg.Text.Length > 5)
            {
                txt_idtg.Text = txt_idtg.Text.Substring(0, 5);
                txt_idtg.SelectionStart = 5;
            }
                
        }

        private void btn_updategrid_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)gunaDataGridView2.DataSource;
            string query = "select * from TACGIA";
            int k = 0;
            if (MessageBox.Show("Bạn có muốn update lại không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                k = con.updateTable(dt, query);
                themtacgia_VisibleChanged(this, null);
                ClearText();
            }

            if (k != 0)
            {
                MessageBox.Show("Update thành công");
            }

        }



     
    }
}
