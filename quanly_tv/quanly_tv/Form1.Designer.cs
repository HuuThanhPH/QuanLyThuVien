namespace quanly_tv
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.gunaPanel1 = new Guna.UI.WinForms.GunaPanel();
            this.gunaLabel3 = new Guna.UI.WinForms.GunaLabel();
            this.gunaLabel2 = new Guna.UI.WinForms.GunaLabel();
            this.error_login = new Guna.UI.WinForms.GunaLabel();
            this.btn_dang_nhap = new Guna.UI.WinForms.GunaButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gunaLabel1 = new Guna.UI.WinForms.GunaLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_password = new Guna.UI.WinForms.GunaTextBox();
            this.txt_email = new Guna.UI.WinForms.GunaTextBox();
            this.dang_nhap_label = new System.Windows.Forms.Label();
            this.picture_login = new System.Windows.Forms.PictureBox();
            this.gunaAdvenceButton1 = new Guna.UI.WinForms.GunaAdvenceButton();
            this.btn_exit = new Guna.UI.WinForms.GunaCircleButton();
            this.gunaPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_login)).BeginInit();
            this.SuspendLayout();
            // 
            // gunaPanel1
            // 
            this.gunaPanel1.BackColor = System.Drawing.Color.White;
            this.gunaPanel1.Controls.Add(this.gunaLabel3);
            this.gunaPanel1.Controls.Add(this.gunaLabel2);
            this.gunaPanel1.Controls.Add(this.error_login);
            this.gunaPanel1.Controls.Add(this.btn_dang_nhap);
            this.gunaPanel1.Controls.Add(this.label3);
            this.gunaPanel1.Controls.Add(this.label2);
            this.gunaPanel1.Controls.Add(this.gunaLabel1);
            this.gunaPanel1.Controls.Add(this.label1);
            this.gunaPanel1.Controls.Add(this.txt_password);
            this.gunaPanel1.Controls.Add(this.txt_email);
            this.gunaPanel1.Controls.Add(this.dang_nhap_label);
            this.gunaPanel1.Controls.Add(this.picture_login);
            this.gunaPanel1.Controls.Add(this.gunaAdvenceButton1);
            this.gunaPanel1.Controls.Add(this.btn_exit);
            this.gunaPanel1.Location = new System.Drawing.Point(472, 254);
            this.gunaPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gunaPanel1.Name = "gunaPanel1";
            this.gunaPanel1.Size = new System.Drawing.Size(1800, 938);
            this.gunaPanel1.TabIndex = 0;
            // 
            // gunaLabel3
            // 
            this.gunaLabel3.AutoSize = true;
            this.gunaLabel3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.gunaLabel3.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel3.ForeColor = System.Drawing.Color.RoyalBlue;
            this.gunaLabel3.Location = new System.Drawing.Point(1406, 702);
            this.gunaLabel3.Name = "gunaLabel3";
            this.gunaLabel3.Size = new System.Drawing.Size(122, 37);
            this.gunaLabel3.TabIndex = 12;
            this.gunaLabel3.Text = "Đăng ký";
            this.gunaLabel3.Click += new System.EventHandler(this.gunaLabel3_Click);
            // 
            // gunaLabel2
            // 
            this.gunaLabel2.AutoSize = true;
            this.gunaLabel2.Font = new System.Drawing.Font("Segoe UI", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gunaLabel2.Location = new System.Drawing.Point(1141, 702);
            this.gunaLabel2.Name = "gunaLabel2";
            this.gunaLabel2.Size = new System.Drawing.Size(259, 37);
            this.gunaLabel2.TabIndex = 11;
            this.gunaLabel2.Text = "Chưa có tài khoản?";
            // 
            // error_login
            // 
            this.error_login.AutoSize = true;
            this.error_login.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.error_login.ForeColor = System.Drawing.Color.Red;
            this.error_login.Location = new System.Drawing.Point(982, 838);
            this.error_login.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.error_login.Name = "error_login";
            this.error_login.Size = new System.Drawing.Size(606, 32);
            this.error_login.TabIndex = 10;
            this.error_login.Text = "Thông tin tài khoản hoặc mật khẩu không chính xác";
            this.error_login.Visible = false;
            // 
            // btn_dang_nhap
            // 
            this.btn_dang_nhap.AnimationHoverSpeed = 0.07F;
            this.btn_dang_nhap.AnimationSpeed = 0.03F;
            this.btn_dang_nhap.BackColor = System.Drawing.Color.Transparent;
            this.btn_dang_nhap.BaseColor = System.Drawing.Color.LimeGreen;
            this.btn_dang_nhap.BorderColor = System.Drawing.Color.Black;
            this.btn_dang_nhap.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_dang_nhap.FocusedColor = System.Drawing.Color.Empty;
            this.btn_dang_nhap.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dang_nhap.ForeColor = System.Drawing.Color.White;
            this.btn_dang_nhap.Image = null;
            this.btn_dang_nhap.ImageSize = new System.Drawing.Size(20, 20);
            this.btn_dang_nhap.Location = new System.Drawing.Point(988, 600);
            this.btn_dang_nhap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_dang_nhap.Name = "btn_dang_nhap";
            this.btn_dang_nhap.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.btn_dang_nhap.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btn_dang_nhap.OnHoverForeColor = System.Drawing.Color.White;
            this.btn_dang_nhap.OnHoverImage = null;
            this.btn_dang_nhap.OnPressedColor = System.Drawing.Color.Black;
            this.btn_dang_nhap.Radius = 20;
            this.btn_dang_nhap.Size = new System.Drawing.Size(540, 78);
            this.btn_dang_nhap.TabIndex = 8;
            this.btn_dang_nhap.Text = "Đăng nhập";
            this.btn_dang_nhap.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btn_dang_nhap.Click += new System.EventHandler(this.btn_dang_nhap_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(981, 383);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(167, 37);
            this.label3.TabIndex = 7;
            this.label3.Text = "Mật khẩu :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(981, 200);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(253, 37);
            this.label2.TabIndex = 7;
            this.label2.Text = "Tên đăng nhập :";
            // 
            // gunaLabel1
            // 
            this.gunaLabel1.AutoSize = true;
            this.gunaLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaLabel1.Image = ((System.Drawing.Image)(resources.GetObject("gunaLabel1.Image")));
            this.gunaLabel1.Location = new System.Drawing.Point(918, 280);
            this.gunaLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.gunaLabel1.Name = "gunaLabel1";
            this.gunaLabel1.Size = new System.Drawing.Size(0, 32);
            this.gunaLabel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Image = ((System.Drawing.Image)(resources.GetObject("label1.Image")));
            this.label1.Location = new System.Drawing.Point(972, 284);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 25);
            this.label1.TabIndex = 5;
            // 
            // txt_password
            // 
            this.txt_password.BackColor = System.Drawing.Color.Transparent;
            this.txt_password.BaseColor = System.Drawing.Color.White;
            this.txt_password.BorderColor = System.Drawing.Color.Silver;
            this.txt_password.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_password.FocusedBaseColor = System.Drawing.Color.White;
            this.txt_password.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txt_password.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txt_password.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_password.Location = new System.Drawing.Point(988, 450);
            this.txt_password.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '\0';
            this.txt_password.Radius = 20;
            this.txt_password.SelectedText = "";
            this.txt_password.Size = new System.Drawing.Size(540, 78);
            this.txt_password.TabIndex = 4;
            // 
            // txt_email
            // 
            this.txt_email.BackColor = System.Drawing.Color.Transparent;
            this.txt_email.BaseColor = System.Drawing.Color.White;
            this.txt_email.BorderColor = System.Drawing.Color.Silver;
            this.txt_email.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_email.FocusedBaseColor = System.Drawing.Color.White;
            this.txt_email.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.txt_email.FocusedForeColor = System.Drawing.SystemColors.ControlText;
            this.txt_email.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txt_email.Location = new System.Drawing.Point(988, 262);
            this.txt_email.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_email.Name = "txt_email";
            this.txt_email.PasswordChar = '\0';
            this.txt_email.Radius = 20;
            this.txt_email.SelectedText = "";
            this.txt_email.Size = new System.Drawing.Size(540, 78);
            this.txt_email.TabIndex = 4;
            // 
            // dang_nhap_label
            // 
            this.dang_nhap_label.AutoSize = true;
            this.dang_nhap_label.Font = new System.Drawing.Font("MS Reference Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dang_nhap_label.ForeColor = System.Drawing.Color.DodgerBlue;
            this.dang_nhap_label.Location = new System.Drawing.Point(1096, 88);
            this.dang_nhap_label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.dang_nhap_label.Name = "dang_nhap_label";
            this.dang_nhap_label.Size = new System.Drawing.Size(362, 66);
            this.dang_nhap_label.TabIndex = 3;
            this.dang_nhap_label.Text = "ĐĂNG NHẬP";
            // 
            // picture_login
            // 
            this.picture_login.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("picture_login.BackgroundImage")));
            this.picture_login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picture_login.Location = new System.Drawing.Point(94, 167);
            this.picture_login.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.picture_login.Name = "picture_login";
            this.picture_login.Size = new System.Drawing.Size(738, 531);
            this.picture_login.TabIndex = 2;
            this.picture_login.TabStop = false;
            // 
            // gunaAdvenceButton1
            // 
            this.gunaAdvenceButton1.AnimationHoverSpeed = 0.07F;
            this.gunaAdvenceButton1.AnimationSpeed = 0.03F;
            this.gunaAdvenceButton1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.gunaAdvenceButton1.BorderColor = System.Drawing.Color.Black;
            this.gunaAdvenceButton1.CheckedBaseColor = System.Drawing.Color.Gray;
            this.gunaAdvenceButton1.CheckedBorderColor = System.Drawing.Color.Black;
            this.gunaAdvenceButton1.CheckedForeColor = System.Drawing.Color.White;
            this.gunaAdvenceButton1.CheckedImage = ((System.Drawing.Image)(resources.GetObject("gunaAdvenceButton1.CheckedImage")));
            this.gunaAdvenceButton1.CheckedLineColor = System.Drawing.Color.DimGray;
            this.gunaAdvenceButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.gunaAdvenceButton1.FocusedColor = System.Drawing.Color.Empty;
            this.gunaAdvenceButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gunaAdvenceButton1.ForeColor = System.Drawing.Color.White;
            this.gunaAdvenceButton1.Image = ((System.Drawing.Image)(resources.GetObject("gunaAdvenceButton1.Image")));
            this.gunaAdvenceButton1.ImageSize = new System.Drawing.Size(20, 20);
            this.gunaAdvenceButton1.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.gunaAdvenceButton1.Location = new System.Drawing.Point(232, 344);
            this.gunaAdvenceButton1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.gunaAdvenceButton1.Name = "gunaAdvenceButton1";
            this.gunaAdvenceButton1.OnHoverBaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(151)))), ((int)(((byte)(143)))), ((int)(((byte)(255)))));
            this.gunaAdvenceButton1.OnHoverBorderColor = System.Drawing.Color.Black;
            this.gunaAdvenceButton1.OnHoverForeColor = System.Drawing.Color.White;
            this.gunaAdvenceButton1.OnHoverImage = null;
            this.gunaAdvenceButton1.OnHoverLineColor = System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(58)))), ((int)(((byte)(170)))));
            this.gunaAdvenceButton1.OnPressedColor = System.Drawing.Color.Black;
            this.gunaAdvenceButton1.Size = new System.Drawing.Size(12, 12);
            this.gunaAdvenceButton1.TabIndex = 1;
            this.gunaAdvenceButton1.Text = "gunaAdvenceButton1";
            // 
            // btn_exit
            // 
            this.btn_exit.AnimationHoverSpeed = 0.07F;
            this.btn_exit.AnimationSpeed = 0.03F;
            this.btn_exit.BackColor = System.Drawing.Color.White;
            this.btn_exit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btn_exit.BackgroundImage")));
            this.btn_exit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btn_exit.BaseColor = System.Drawing.Color.Transparent;
            this.btn_exit.BorderColor = System.Drawing.Color.Black;
            this.btn_exit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_exit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btn_exit.FocusedColor = System.Drawing.Color.Empty;
            this.btn_exit.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btn_exit.ForeColor = System.Drawing.Color.White;
            this.btn_exit.Image = null;
            this.btn_exit.ImageSize = new System.Drawing.Size(25, 25);
            this.btn_exit.Location = new System.Drawing.Point(24, 22);
            this.btn_exit.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.OnHoverBaseColor = System.Drawing.Color.Transparent;
            this.btn_exit.OnHoverBorderColor = System.Drawing.Color.Black;
            this.btn_exit.OnHoverForeColor = System.Drawing.Color.White;
            this.btn_exit.OnHoverImage = null;
            this.btn_exit.OnPressedColor = System.Drawing.Color.Black;
            this.btn_exit.Size = new System.Drawing.Size(45, 47);
            this.btn_exit.TabIndex = 0;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(2700, 1250);
            this.Controls.Add(this.gunaPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gunaPanel1.ResumeLayout(false);
            this.gunaPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture_login)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI.WinForms.GunaPanel gunaPanel1;
        private Guna.UI.WinForms.GunaCircleButton btn_exit;
        private Guna.UI.WinForms.GunaAdvenceButton gunaAdvenceButton1;
        private System.Windows.Forms.Label dang_nhap_label;
        private System.Windows.Forms.PictureBox picture_login;
        private Guna.UI.WinForms.GunaTextBox txt_email;
        private Guna.UI.WinForms.GunaLabel gunaLabel1;
        private System.Windows.Forms.Label label1;
        private Guna.UI.WinForms.GunaTextBox txt_password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Guna.UI.WinForms.GunaButton btn_dang_nhap;
        private Guna.UI.WinForms.GunaLabel error_login;
        private Guna.UI.WinForms.GunaLabel gunaLabel3;
        private Guna.UI.WinForms.GunaLabel gunaLabel2;



    }
}

