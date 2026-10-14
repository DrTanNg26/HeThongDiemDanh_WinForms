namespace HeThongDiemDanh
{
    partial class FrmQuenMK
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
            label1 = new Label();
            lb_NhapTK = new Label();
            tb_NhapTK = new TextBox();
            infoTK_lb = new ListBox();
            Ngaysinh_lb = new Label();
            cccd_tb = new TextBox();
            email_tb = new TextBox();
            CCCD_lb = new Label();
            email_lb = new Label();
            ngaysinh_dtp = new DateTimePicker();
            button1 = new Button();
            button2 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Times New Roman", 10.875F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(164, 23);
            label1.Name = "label1";
            label1.Size = new Size(438, 33);
            label1.TabIndex = 0;
            label1.Text = "Quên mật khẩu dành cho sinh viên";
            label1.Click += label1_Click;
            // 
            // lb_NhapTK
            // 
            lb_NhapTK.AutoSize = true;
            lb_NhapTK.Location = new Point(31, 82);
            lb_NhapTK.Name = "lb_NhapTK";
            lb_NhapTK.Size = new Size(190, 32);
            lb_NhapTK.TabIndex = 1;
            lb_NhapTK.Text = "Nhập tài khoản: ";
            lb_NhapTK.Click += label2_Click;
            // 
            // tb_NhapTK
            // 
            tb_NhapTK.Location = new Point(234, 75);
            tb_NhapTK.Name = "tb_NhapTK";
            tb_NhapTK.Size = new Size(400, 39);
            tb_NhapTK.TabIndex = 2;
            // 
            // infoTK_lb
            // 
            infoTK_lb.FormattingEnabled = true;
            infoTK_lb.Location = new Point(42, 304);
            infoTK_lb.Name = "infoTK_lb";
            infoTK_lb.Size = new Size(592, 132);
            infoTK_lb.TabIndex = 3;
            // 
            // Ngaysinh_lb
            // 
            Ngaysinh_lb.AutoSize = true;
            Ngaysinh_lb.Location = new Point(86, 125);
            Ngaysinh_lb.Name = "Ngaysinh_lb";
            Ngaysinh_lb.Size = new Size(126, 32);
            Ngaysinh_lb.TabIndex = 4;
            Ngaysinh_lb.Text = "Ngày sinh:";
            Ngaysinh_lb.Click += label2_Click_1;
            // 
            // cccd_tb
            // 
            cccd_tb.Location = new Point(234, 190);
            cccd_tb.Name = "cccd_tb";
            cccd_tb.Size = new Size(400, 39);
            cccd_tb.TabIndex = 6;
            // 
            // email_tb
            // 
            email_tb.Location = new Point(234, 248);
            email_tb.Name = "email_tb";
            email_tb.Size = new Size(400, 39);
            email_tb.TabIndex = 7;
            // 
            // CCCD_lb
            // 
            CCCD_lb.AutoSize = true;
            CCCD_lb.Location = new Point(133, 193);
            CCCD_lb.Name = "CCCD_lb";
            CCCD_lb.Size = new Size(79, 32);
            CCCD_lb.TabIndex = 8;
            CCCD_lb.Text = "CCCD:";
            CCCD_lb.Click += label3_Click;
            // 
            // email_lb
            // 
            email_lb.AutoSize = true;
            email_lb.Location = new Point(133, 248);
            email_lb.Name = "email_lb";
            email_lb.Size = new Size(71, 32);
            email_lb.TabIndex = 9;
            email_lb.Text = "Email";
            // 
            // ngaysinh_dtp
            // 
            ngaysinh_dtp.Location = new Point(234, 130);
            ngaysinh_dtp.Name = "ngaysinh_dtp";
            ngaysinh_dtp.Size = new Size(400, 39);
            ngaysinh_dtp.TabIndex = 10;
            // 
            // button1
            // 
            button1.Location = new Point(86, 451);
            button1.Name = "button1";
            button1.Size = new Size(150, 46);
            button1.TabIndex = 11;
            button1.Text = "Xác nhận";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(484, 451);
            button2.Name = "button2";
            button2.Size = new Size(150, 46);
            button2.TabIndex = 12;
            button2.Text = "Thoát";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // FrmQuenMK
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(808, 509);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(ngaysinh_dtp);
            Controls.Add(email_lb);
            Controls.Add(CCCD_lb);
            Controls.Add(email_tb);
            Controls.Add(cccd_tb);
            Controls.Add(Ngaysinh_lb);
            Controls.Add(infoTK_lb);
            Controls.Add(tb_NhapTK);
            Controls.Add(lb_NhapTK);
            Controls.Add(label1);
            Name = "FrmQuenMK";
            Text = "FrmQuenMK";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lb_NhapTK;
        private TextBox tb_NhapTK;
        private ListBox infoTK_lb;
        private Label Ngaysinh_lb;
        private TextBox cccd_tb;
        private TextBox email_tb;
        private Label CCCD_lb;
        private Label email_lb;
        private DateTimePicker ngaysinh_dtp;
        private Button button1;
        private Button button2;
    }
}