namespace HeThongDiemDanh
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TaiKhoan_tb = new TextBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            MatKhau_tb = new TextBox();
            QuenMatKhau_llb = new LinkLabel();
            DangNhap_bt = new Button();
            DangKi_bt = new Button();
            label5 = new Label();
            Exit_bt = new Button();
            panelLoginCenter = new Panel();
            SuspendLayout();
            panelLoginCenter.SuspendLayout();
            // 
            // panelLoginCenter
            // 
            panelLoginCenter.BackColor = Color.Transparent;
            panelLoginCenter.Controls.Add(label1);
            panelLoginCenter.Controls.Add(label5);
            panelLoginCenter.Controls.Add(label2);
            panelLoginCenter.Controls.Add(label3);
            panelLoginCenter.Controls.Add(TaiKhoan_tb);
            panelLoginCenter.Controls.Add(label4);
            panelLoginCenter.Controls.Add(MatKhau_tb);
            panelLoginCenter.Controls.Add(QuenMatKhau_llb);
            panelLoginCenter.Controls.Add(DangNhap_bt);
            panelLoginCenter.Controls.Add(DangKi_bt);
            panelLoginCenter.Controls.Add(Exit_bt);
            panelLoginCenter.Location = new Point(19, 0);
            panelLoginCenter.Name = "panelLoginCenter";
            panelLoginCenter.Size = new Size(800, 520);
            panelLoginCenter.TabIndex = 0;
            // 
            // TaiKhoan_tb
            // 
            TaiKhoan_tb.Location = new Point(313, 273);
            TaiKhoan_tb.Name = "TaiKhoan_tb";
            TaiKhoan_tb.Size = new Size(347, 39);
            TaiKhoan_tb.TabIndex = 0;
            TaiKhoan_tb.TextChanged += textBox1_TextChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Times New Roman", 16.125F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(179, 31);
            label1.Name = "label1";
            label1.Size = new Size(493, 49);
            label1.TabIndex = 1;
            label1.Text = "CHÀO MỪNG ĐẾN VỚI";
            label1.Click += label1_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Times New Roman", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(271, 216);
            label2.Name = "label2";
            label2.Size = new Size(330, 42);
            label2.TabIndex = 2;
            label2.Text = "Vui lòng đăng nhập";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Times New Roman", 10.125F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label3.ForeColor = SystemColors.Info;
            label3.Location = new Point(159, 277);
            label3.Name = "label3";
            label3.Size = new Size(148, 31);
            label3.TabIndex = 3;
            label3.Text = "Tài khoản: ";
            label3.Click += label3_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Times New Roman", 10.125F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label4.ForeColor = SystemColors.Info;
            label4.Location = new Point(159, 325);
            label4.Name = "label4";
            label4.Size = new Size(134, 31);
            label4.TabIndex = 4;
            label4.Text = "Mật khẩu:";
            label4.Click += label4_Click;
            // 
            // MatKhau_tb
            // 
            MatKhau_tb.Location = new Point(313, 318);
            MatKhau_tb.Name = "MatKhau_tb";
            MatKhau_tb.PasswordChar = '*';
            MatKhau_tb.Size = new Size(347, 39);
            MatKhau_tb.TabIndex = 5;
            MatKhau_tb.TextChanged += MatKhau_tb_TextChanged;
            // 
            // QuenMatKhau_llb
            // 
            QuenMatKhau_llb.AutoSize = true;
            QuenMatKhau_llb.BackColor = Color.Transparent;
            QuenMatKhau_llb.Location = new Point(469, 360);
            QuenMatKhau_llb.Name = "QuenMatKhau_llb";
            QuenMatKhau_llb.Size = new Size(191, 32);
            QuenMatKhau_llb.TabIndex = 6;
            QuenMatKhau_llb.TabStop = true;
            QuenMatKhau_llb.Text = "Quên mật khẩu?";
            QuenMatKhau_llb.LinkClicked += QuenMatKhau_llb_LinkClicked;
            // 
            // DangNhap_bt
            // 
            DangNhap_bt.Location = new Point(91, 440);
            DangNhap_bt.Name = "DangNhap_bt";
            DangNhap_bt.Size = new Size(150, 46);
            DangNhap_bt.TabIndex = 7;
            DangNhap_bt.Text = "Đăng nhập";
            DangNhap_bt.UseVisualStyleBackColor = true;
            DangNhap_bt.Click += button1_Click;
            // 
            // DangKi_bt
            // 
            DangKi_bt.Location = new Point(330, 440);
            DangKi_bt.Name = "DangKi_bt";
            DangKi_bt.Size = new Size(150, 46);
            DangKi_bt.TabIndex = 8;
            DangKi_bt.Text = "Đăng kí";
            DangKi_bt.UseVisualStyleBackColor = true;
            DangKi_bt.Click += DangKi_bt_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Times New Roman", 19.875F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.MintCream;
            label5.Location = new Point(104, 80);
            label5.Name = "label5";
            label5.Size = new Size(649, 61);
            label5.TabIndex = 9;
            label5.Text = "HỆ THỐNG ĐIỂM DANH ";
            label5.Click += label5_Click;
            // 
            // Exit_bt
            // 
            Exit_bt.Location = new Point(570, 440);
            Exit_bt.Name = "Exit_bt";
            Exit_bt.Size = new Size(150, 46);
            Exit_bt.TabIndex = 10;
            Exit_bt.Text = "Thoát";
            Exit_bt.UseVisualStyleBackColor = true;
            Exit_bt.Click += button1_Click_1;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.AnhNenUtt1;
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(839, 525);
            Controls.Add(panelLoginCenter);
            DoubleBuffered = true;
            Name = "Form1";
            Text = "Form1";
            panelLoginCenter.ResumeLayout(false);
            panelLoginCenter.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelLoginCenter;
        private TextBox TaiKhoan_tb;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox MatKhau_tb;
        private LinkLabel QuenMatKhau_llb;
        private Button DangNhap_bt;
        private Button DangKi_bt;
        private Label label5;
        private Button Exit_bt;
    }
}
