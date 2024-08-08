using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanly_tv
{
    public partial class Home : Form
    {


        public Home()
        {
            InitializeComponent();
        }

         private void Home_VisibleChanged(object sender, EventArgs e)
        {
            themsach1.Visible = false;
            themtacgia1.Visible = false;
            themloaisach1.Visible = false;
            themdocgia1.Visible = false;
            nhanvien1.Visible = false;
            themnxb1.Visible = false;
            muonsach1.Visible = false;
            btn_themsach.PerformClick();
            nhanvien1.setIDValue(IDValue);
            themsach1.setIDValue(IDValue);
            themtacgia1.setIDValue(IDValue);
            themdocgia1.setIDValue(IDValue);
            themnxb1.setIDValue(IDValue);
            themloaisach1.setIDValue(IDValue);
            muonsach1.setIDValue(IDValue);
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

        

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
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

        private void btn_themsach_Click(object sender, EventArgs e)
        {
            themtacgia1.Visible = false;
            themloaisach1.Visible = false;
            themdocgia1.Visible = false;
            themnxb1.Visible = false;
            muonsach1.Visible = false;
            nhanvien1.Visible = false;
            themsach1.Visible = true;
            btn_themsach.BaseColor = Color.Blue;
            btn_themtg.BaseColor = Color.DarkOrchid;
            btn_them_dg.BaseColor = Color.DarkOrchid;
            btn_them_nxb.BaseColor = Color.DarkOrchid;
            btn_them_ls.BaseColor = Color.DarkOrchid;
            btn_nv.BaseColor = Color.DarkOrchid;
            btn_lich_muon_tra_sach.BaseColor = Color.DarkOrchid;
        }

        private void btn_themtg_Click(object sender, EventArgs e)
        {
            themloaisach1.Visible = false;
            themdocgia1.Visible = false;
            themnxb1.Visible = false;
            themsach1.Visible = false;
            muonsach1.Visible = false;
            nhanvien1.Visible = false;
            themtacgia1.Visible = true;
            btn_themtg.BaseColor = Color.Blue;
            btn_themsach.BaseColor = Color.DarkOrchid;
            btn_them_dg.BaseColor = Color.DarkOrchid;
            btn_them_nxb.BaseColor = Color.DarkOrchid;
            btn_them_ls.BaseColor = Color.DarkOrchid;
            btn_nv.BaseColor = Color.DarkOrchid;
            btn_lich_muon_tra_sach.BaseColor = Color.DarkOrchid;
        }

        private void btn_them_dg_Click(object sender, EventArgs e)
        {
            themtacgia1.Visible = false;
            themloaisach1.Visible = false;
            themsach1.Visible = false;
            nhanvien1.Visible = false;
            muonsach1.Visible = false;
            themnxb1.Visible = false;
            themdocgia1.Visible = true;
            btn_them_dg.BaseColor = Color.Blue;
            btn_themtg.BaseColor = Color.DarkOrchid;
            btn_themsach.BaseColor = Color.DarkOrchid;
            btn_them_nxb.BaseColor = Color.DarkOrchid;
            btn_them_ls.BaseColor = Color.DarkOrchid;
            btn_nv.BaseColor = Color.DarkOrchid;
            btn_lich_muon_tra_sach.BaseColor = Color.DarkOrchid;
        }

        private void btn_them_nxb_Click(object sender, EventArgs e)
        {
            themtacgia1.Visible = false;
            themloaisach1.Visible = false;
            themdocgia1.Visible = false;
            themsach1.Visible = false;
            nhanvien1.Visible = false;
            muonsach1.Visible = false;
            themnxb1.Visible = true;
            btn_them_nxb.BaseColor = Color.Blue;
            btn_themtg.BaseColor = Color.DarkOrchid;
            btn_them_dg.BaseColor = Color.DarkOrchid;
            btn_themsach.BaseColor = Color.DarkOrchid;
            btn_them_ls.BaseColor = Color.DarkOrchid;
            btn_nv.BaseColor = Color.DarkOrchid;
            btn_lich_muon_tra_sach.BaseColor = Color.DarkOrchid;
        }

        private void btn_them_ls_Click(object sender, EventArgs e)
        {
            themtacgia1.Visible = false;
            themdocgia1.Visible = false;
            themnxb1.Visible = false;
            themsach1.Visible = false;
            nhanvien1.Visible = false;
            muonsach1.Visible = false;
            themloaisach1.Visible = true;
            btn_them_ls.BaseColor = Color.Blue;
            btn_themtg.BaseColor = Color.DarkOrchid;
            btn_them_dg.BaseColor = Color.DarkOrchid;
            btn_them_nxb.BaseColor = Color.DarkOrchid;
            btn_themsach.BaseColor = Color.DarkOrchid;
            btn_nv.BaseColor = Color.DarkOrchid;
            btn_lich_muon_tra_sach.BaseColor = Color.DarkOrchid;
        }

        private void btn_nv_Click(object sender, EventArgs e)
        {
            themtacgia1.Visible = false;
            themdocgia1.Visible = false;
            themnxb1.Visible = false;
            themsach1.Visible = false;
            themloaisach1.Visible = false;
            muonsach1.Visible = false;
            nhanvien1.Visible = true;
            btn_nv.BaseColor = Color.Blue;
            btn_them_ls.BaseColor = Color.DarkOrchid;
            btn_themtg.BaseColor = Color.DarkOrchid;
            btn_them_dg.BaseColor = Color.DarkOrchid;
            btn_them_nxb.BaseColor = Color.DarkOrchid;
            btn_themsach.BaseColor = Color.DarkOrchid;
            btn_lich_muon_tra_sach.BaseColor = Color.DarkOrchid;
        }

        private void btn_lich_muon_tra_sach_Click(object sender, EventArgs e)
        {
            themtacgia1.Visible = false;
            themdocgia1.Visible = false;
            themnxb1.Visible = false;
            themsach1.Visible = false;
            themloaisach1.Visible = false;
            nhanvien1.Visible = false;
            muonsach1.Visible = true;
            btn_lich_muon_tra_sach.BaseColor = Color.Blue;
            btn_nv.BaseColor = Color.DarkOrchid;
            btn_them_ls.BaseColor = Color.DarkOrchid;
            btn_themtg.BaseColor = Color.DarkOrchid;
            btn_them_dg.BaseColor = Color.DarkOrchid;
            btn_them_nxb.BaseColor = Color.DarkOrchid;
            btn_themsach.BaseColor = Color.DarkOrchid;
        }




        
    }
}
