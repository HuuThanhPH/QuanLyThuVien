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
    public partial class muonsach : UserControl
    {
        connect con = new connect();
        string query, checkTraSach;
        public muonsach()
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

        private void muonsach_Load(object sender, EventArgs e)
        {
            
        }

        private void dataComBoBox()
        {
            DataSet ds1 = con.getData("select MADG,TENDG from DOCGIA");
            DataSet ds2 = con.getData("select MASH,TENSH from SACH");
            txt_id_dg_muon.DataSource = ds1.Tables[0];
            txt_mash_muon.DataSource = ds2.Tables[0];
            txt_id_dg_muon.DisplayMember = "TENDG";
            txt_id_dg_muon.ValueMember = "MADG";
            txt_mash_muon.DisplayMember = "TENSH";
            txt_mash_muon.ValueMember = "MASH";
            
        }

        private void muonsach_VisibleChanged(object sender, EventArgs e)
        {
            label_ngaytra.Visible = false;
            txt_ngaytra.Visible = false;
            btn_fix_muonsach.Visible = false;
            btn_delete_muonsach.Visible = false;
            lab_fix.Visible = false;
            lab_add.Visible = true;
            btn_add_muonsach.Visible = true;
            lb_status.Visible = false;
            cb_status.Visible = false;
            cb_status.SelectedIndex = 0;
            cb_status.Enabled = false;
            txt_ngaymuon.Value = DateTime.Now;
            txt_ngaytra.Value = DateTime.Now;
            query = "select a.MAMTS, a.MADG, c.TENDG, a.MASH, b.TENSH, a.NGAYMUON, a.NGAYTRA, a.TRANGTHAI from MUONTRASACH a join SACH b on a.MASH = b.MASH join DOCGIA c on a.MADG = c.MADG";
            DataSet ds = con.getData(query);
            gunaDataGridView1.DataSource = ds.Tables[0];
            gunaDataGridView1.Columns["NGAYMUON"].DefaultCellStyle.Format = "dd/MM/yyyy";
            gunaDataGridView1.Columns["NGAYTRA"].DefaultCellStyle.Format = "dd/MM/yyyy";
            gunaDataGridView1.AllowUserToAddRows = false;
            combobox();
            dataComBoBox();
        }
        private void combobox()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Name", typeof(string));

            // Thêm dữ liệu vào DataTable
            dataTable.Rows.Add("Chưa xác định");
            dataTable.Rows.Add("Đã xác định");

            // Gán DataTable vào ComboBox
            txt_xacdinhngay.DataSource = dataTable;
            txt_xacdinhngay.DisplayMember = "Name"; // Hiển thị cột "Name"


            // Chọn một mục theo mặc định
            txt_xacdinhngay.SelectedIndex = 0;
        }

        private void txt_xacdinhngay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (txt_xacdinhngay.Text == "Đã xác định")
            {
                label_ngaytra.Visible = true;
                txt_ngaytra.Visible = true;
                lb_status.Visible = true;
                cb_status.Visible = true;       
            }
            else
            {
                 label_ngaytra.Visible = false;
                 cb_status.Visible = false;
                 lb_status.Visible = false;
                 txt_ngaytra.Visible = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void btn_add_muonsach_Click(object sender, EventArgs e)
        {

            string formattedDateMuon = txt_ngaymuon.Value.ToString("yyyy-MM-dd");
            string formattedDateTra = txt_ngaytra.Value.ToString("yyyy-MM-dd");

            string queryReaderDG = "select * from DOCGIA";
            SqlDataReader readerDG = con.loadData(queryReaderDG);
            string queryReaderSH = "select * from SACH";
            SqlDataReader readerSH = con.loadData(queryReaderSH);
            string queryReaderMuonSH = "select * from MUONTRASACH";
            SqlDataReader readerMuonSH = con.loadData(queryReaderMuonSH);
            int count = 0;
            if (txt_id_dg_muon.Text != "" && txt_mash_muon.Text != "")
            {
                while (readerDG.Read())
                {
                    string DgId = readerDG["MADG"].ToString();
                    if (DgId == txt_id_dg_muon.SelectedValue.ToString())
                    {
                        count = 1;
                        while (readerSH.Read())
                        {
                            string ShId = readerSH["MASH"].ToString();
                            if(ShId ==txt_mash_muon.SelectedValue.ToString())
                            {
                                DateTime currentDate = DateTime.Now;
                                while(readerMuonSH.Read())
                                {
                                     string DgIdMuon = readerMuonSH["MADG"].ToString();
                                     string ShIdMuon = readerMuonSH["MASH"].ToString();
                                    //chuyển đổi thành date để so sánh
                                     DateTime ngaytra;
                                    if(DateTime.TryParse(readerMuonSH["NGAYTRA"].ToString(), out ngaytra))
                                    {
                                        if(DgIdMuon == txt_id_dg_muon.SelectedValue.ToString() && ShIdMuon == txt_mash_muon.SelectedValue.ToString() && currentDate < ngaytra)
                                        {
                                            MessageBox.Show("Độc giả chưa trả cuốn sách này chưa thể mượn lại!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                            return;
                                        }
                                    }
                                    
                                }
                               

                                string SlBook = readerSH["SL"].ToString();
                                if (int.Parse(SlBook) < 1)
                                {
                                    MessageBox.Show("số lượng sách này trong kho đã hết!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (ShId == txt_mash_muon.SelectedValue.ToString())
                                {
                                    string queryRef = "UPDATE SACH SET SL = SL - 1 WHERE MASH = '" + txt_mash_muon.SelectedValue.ToString() + "'";
                                    if (txt_xacdinhngay.Text == "Đã xác định")
                                    {
                                        query = "insert into MUONTRASACH(MADG, MASH, NGAYMUON, NGAYTRA, TRANGTHAI, MANV) values ('" + txt_id_dg_muon.SelectedValue.ToString() + "', N'" + txt_mash_muon.SelectedValue.ToString() + "', N'" + formattedDateMuon + "', '" + formattedDateTra + "', N'Chưa trả','" + IDValue + "')";
                                        
                                        con.setData(queryRef, "");
                                        con.setData(query, "Thêm thành công");

                                        muonsach_Load(this, null);
                                        ClearText();
                                    }
                                    else
                                    {
                                        query = "insert into MUONTRASACH(MADG, MASH, NGAYMUON, TRANGTHAI) values ('" + txt_id_dg_muon.SelectedValue.ToString() + "', N'" + txt_mash_muon.SelectedValue.ToString() + "', N'" + formattedDateMuon + "', N'Chưa trả')";
                                        con.setData(queryRef, "");
                                        con.setData(query, "Thêm thành công");

                                        ClearText();
                                        muonsach_VisibleChanged(this, null);
                                    }
                                    return;
                                }
                            }
                        }
                    }
                }


                if(count == 1)
                {
                    MessageBox.Show("không tìm thấy ID sách này", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    MessageBox.Show("không tìm thấy ID độc giả này", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
              
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void ClearText()
        {
            txt_id_dg_muon.Text = "";
            txt_mash_muon.Text = "";
            lab_add.Visible = true;
            btn_add_muonsach.Visible = true;
            txt_ngaymuon.Value = DateTime.Now;
            txt_ngaytra.Value = DateTime.Now;
            cb_status.SelectedIndex = 0;
            txt_xacdinhngay.SelectedIndex = 0;
        }

        private void gunaDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DateTime formattedDate1 = Convert.ToDateTime(gunaDataGridView1.SelectedRows[0].Cells[5].Value.ToString());
            DateTime formattedDate2;
            txt_xacdinhngay.SelectedIndex = 0;
            if (gunaDataGridView1.SelectedRows[0].Cells[7].Value.ToString() == "Đã trả")
            {
                cb_status.Enabled = false;
                checkTraSach = "Đã trả";
                cb_status.Text = gunaDataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            }
            else
            {
                cb_status.Enabled = true;
                cb_status.SelectedIndex = 0;
            }
            if (!string.IsNullOrEmpty(gunaDataGridView1.SelectedRows[0].Cells[6].Value.ToString()))
            {
                txt_xacdinhngay.SelectedIndex = 1;
               formattedDate2 = Convert.ToDateTime(gunaDataGridView1.SelectedRows[0].Cells[6].Value.ToString());
               txt_ngaytra.Value = formattedDate2;
               cb_status.Visible = true;
               lb_status.Visible = true;
            }
            else
            {
                txt_ngaytra.Value = DateTime.Now;
                cb_status.Visible = false;
                lb_status.Visible = false;
            }
            txt_id_dg_muon.Text = gunaDataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            txt_mash_muon.Text = gunaDataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            txt_ngaymuon.Value = formattedDate1;
            btn_fix_muonsach.Visible = true;
            btn_delete_muonsach.Visible = true;
            btn_add_muonsach.Visible = false;
            lab_fix.Visible = true;
            lab_add.Visible = false;
            //label_add.Visible = false;
            //label_fix_delete.Visible = true;
        }

        private void btn_fix_muonsach_Click(object sender, EventArgs e)
        {
            if (txt_id_dg_muon.Text != "" && txt_mash_muon.Text != "")
            {

                string choose2 = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string queryRef = "";
                string queryReaderSH = "select * from SACH";
                SqlDataReader readerSH = con.loadData(queryReaderSH);
                string formattedDateMuon = txt_ngaymuon.Value.ToString("yyyy-MM-dd");

                while (readerSH.Read())
                {
                    string IdSach = readerSH["MASH"].ToString();
                    string SlSach = readerSH["SL"].ToString();
                    //chuyển đổi thành date để so sánh
                    if(IdSach == txt_mash_muon.SelectedValue.ToString())
                    {
                        if (cb_status.Text == "Chưa trả")
                        {
                            if (txt_xacdinhngay.Text == "" || txt_xacdinhngay.Text == "Chưa xác định")
                            {
                                query = "UPDATE MUONTRASACH SET NGAYMUON = '" + formattedDateMuon + ", MASH = '" + txt_mash_muon.SelectedValue.ToString() + "' , MADG = '" + txt_id_dg_muon.SelectedValue.ToString() + "' WHERE MAMTS = '" + choose2 + "'";
                            }
                            else
                            {
                                string formattedDateTra = txt_ngaytra.Value.ToString("yyyy-MM-dd");
                                query = "UPDATE MUONTRASACH SET NGAYMUON = '" + formattedDateMuon + "', NGAYTRA = '" + formattedDateTra + "', MASH = '" + txt_mash_muon.SelectedValue.ToString() + "', MADG = '" + txt_id_dg_muon.SelectedValue.ToString() + "' WHERE MAMTS = '" + choose2 + "'";
                            }
                        }
                        else
                        {

                            if(cb_status.Enabled == false)
                            {
                                if (txt_xacdinhngay.Text == "" || txt_xacdinhngay.Text == "Chưa xác định")
                                {

                                    query = "UPDATE MUONTRASACH SET NGAYMUON = '" + formattedDateMuon + ", MASH = '" + txt_mash_muon.SelectedValue.ToString() + "' , MADG = '" + txt_id_dg_muon.SelectedValue.ToString() + "' WHERE MAMTS = '" + choose2 + "'";
                                }
                                else
                                {
                                    string formattedDateTra = txt_ngaytra.Value.ToString("yyyy-MM-dd");
                                    query = "UPDATE MUONTRASACH SET NGAYMUON = '" + formattedDateMuon + "', NGAYTRA = '" + formattedDateTra + "', MASH = '" + txt_mash_muon.SelectedValue.ToString() + "', MADG = '" + txt_id_dg_muon.SelectedValue.ToString() + "' WHERE MAMTS = '" + choose2 + "'";
                                }
                            }
                            else
                            {
                                queryRef = "UPDATE SACH SET SL = SL + 1 WHERE MASH = '" + IdSach + "'";
                                if (txt_xacdinhngay.Text == "" || txt_xacdinhngay.Text == "Chưa xác định")
                                {

                                    query = "UPDATE MUONTRASACH SET NGAYMUON = '" + formattedDateMuon + ", MASH = '" + txt_mash_muon.SelectedValue.ToString() + "' , MADG = '" + txt_id_dg_muon.SelectedValue.ToString() + "', TRANGTHAI = N'Đã trả' WHERE MAMTS = '" + choose2 + "'";
                                }
                                else
                                {
                                    string formattedDateTra = txt_ngaytra.Value.ToString("yyyy-MM-dd");
                                    query = "UPDATE MUONTRASACH SET NGAYMUON = '" + formattedDateMuon + "', NGAYTRA = '" + formattedDateTra + "', MASH = '" + txt_mash_muon.SelectedValue.ToString() + "', MADG = '" + txt_id_dg_muon.SelectedValue.ToString() + "', TRANGTHAI = N'Đã trả' WHERE MAMTS = '" + choose2 + "'";
                                }
                            }
                        }
                        if (MessageBox.Show("Bạn có muốn sửa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            if (queryRef != "")
                            {
                                con.setData(queryRef,"");
                            }
                            con.setData(query, "Sửa thành công");
                            
                            muonsach_VisibleChanged(this, null);
                            ClearText();
                        }
                    }

                }
               
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btn_delete_muonsach_Click(object sender, EventArgs e)
        {
            if (txt_id_dg_muon.Text != "" && txt_mash_muon.Text != "")
            {
                string choose = gunaDataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                string queryReference = "DELETE MUONTRASACH WHERE MAMTS= '" + choose + "'";
                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.setData(queryReference, "");
                    con.setData(query, "Xóa thành công");

                    //themsach themsach1 = new themsach();
                    //themsach1.ReloadData();
                    muonsach_VisibleChanged(this, null);
                    ClearText();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void muonsach_Leave(object sender, EventArgs e)
        {
            ClearText();
        }

        private void txt_muonsach_TextChanged(object sender, EventArgs e)
        {
            string name = txt_muonsach.Text.Trim();
            if (name == "")
            {
                muonsach_VisibleChanged(this, null);
            }
            else
            {
                query = "select * from MUONTRASACH WHERE MADG like '%" + name + "%' or MASH like '%" + name + "%'";
                DataSet ds = con.getData(query);
                gunaDataGridView1.DataSource = ds.Tables[0];
            }
        }


        private void txt_id_dg_muon_TextChanged(object sender, EventArgs e)
        {
            if (txt_id_dg_muon.Text.Length >= 5)
            {
                txt_id_dg_muon.Text = txt_id_dg_muon.Text.Substring(0, 4);
                txt_id_dg_muon.SelectionStart = 4;
            }
        }

        private void txt_mash_muon_TextChanged(object sender, EventArgs e)
        {
            if (txt_mash_muon.Text.Length >= 5)
            {
                txt_mash_muon.Text = txt_mash_muon.Text.Substring(0, 4);
                txt_mash_muon.SelectionStart = 4;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btn_updategird_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)gunaDataGridView1.DataSource;
            string query = "select * from MUONTRASACH";
            int k = 0;
            if (MessageBox.Show("Bạn có muốn update lại không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                k = con.updateTable(dt, query);
                muonsach_VisibleChanged(this, null);
                ClearText();
            }

            if (k != 0)
            {
                MessageBox.Show("Update thành công");
            }

        }



        
    }
}
