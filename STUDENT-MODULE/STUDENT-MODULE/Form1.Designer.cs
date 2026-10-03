namespace STUDENT_MODULE
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            pnlHeader = new Panel();
            pictureBox1 = new PictureBox();
            lblApplication = new Label();
            lblTitle = new Label();
            lblSubtitle = new Label();
            lblUsername = new Label();
            txtUsername = new TextBox();
            lblPassword = new Label();
            txtPassword = new TextBox();
            chkShowPassword = new CheckBox();
            lblForgotPassword = new LinkLabel();
            btnLogin = new Button();
            lblMessage = new Label();
            lblStage = new Label();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.FromArgb(20, 49, 83);
            pnlHeader.Controls.Add(pictureBox1);
            pnlHeader.Controls.Add(lblApplication);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(540, 107);
            pnlHeader.TabIndex = 0;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(159, 85);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // lblApplication
            // 
            lblApplication.AutoSize = true;
            lblApplication.Font = new Font("Times New Roman", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblApplication.ForeColor = Color.FromArgb(255, 128, 0);
            lblApplication.Location = new Point(187, 41);
            lblApplication.Name = "lblApplication";
            lblApplication.Size = new Size(341, 23);
            lblApplication.TabIndex = 1;
            lblApplication.Text = "HỆ THỐNG ĐIỂM DANH SINH VIÊN";
            lblApplication.Click += lblApplication_Click;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(20, 49, 83);
            lblTitle.Location = new Point(40, 123);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(211, 50);
            lblTitle.TabIndex = 1;
            lblTitle.Text = "Đăng nhập";
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = true;
            lblSubtitle.ForeColor = Color.FromArgb(91, 108, 128);
            lblSubtitle.Location = new Point(40, 173);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(363, 23);
            lblSubtitle.TabIndex = 2;
            lblSubtitle.Text = "Sử dụng tài khoản được nhà trường cung cấp.";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(40, 219);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(194, 23);
            lblUsername.TabIndex = 3;
            lblUsername.Text = "Mã sinh viên hoặc email";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 12F);
            txtUsername.Location = new Point(40, 246);
            txtUsername.MaxLength = 100;
            txtUsername.Name = "txtUsername";
            txtUsername.PlaceholderText = "Nhập mã sinh viên hoặc email UTT";
            txtUsername.Size = new Size(460, 34);
            txtUsername.TabIndex = 0;
            txtUsername.TextChanged += LoginInput_TextChanged;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(40, 299);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(82, 23);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Mật khẩu";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(40, 326);
            txtPassword.MaxLength = 128;
            txtPassword.Name = "txtPassword";
            txtPassword.PlaceholderText = "Nhập mật khẩu";
            txtPassword.Size = new Size(460, 34);
            txtPassword.TabIndex = 1;
            txtPassword.UseSystemPasswordChar = true;
            txtPassword.TextChanged += LoginInput_TextChanged;
            // 
            // chkShowPassword
            // 
            chkShowPassword.AutoSize = true;
            chkShowPassword.Location = new Point(40, 378);
            chkShowPassword.Name = "chkShowPassword";
            chkShowPassword.Size = new Size(144, 27);
            chkShowPassword.TabIndex = 2;
            chkShowPassword.Text = "Hiện mật khẩu";
            chkShowPassword.UseVisualStyleBackColor = true;
            chkShowPassword.CheckedChanged += chkShowPassword_CheckedChanged;
            // 
            // lblForgotPassword
            // 
            lblForgotPassword.AutoSize = true;
            lblForgotPassword.LinkColor = Color.FromArgb(24, 94, 177);
            lblForgotPassword.Location = new Point(353, 379);
            lblForgotPassword.Name = "lblForgotPassword";
            lblForgotPassword.Size = new Size(137, 23);
            lblForgotPassword.TabIndex = 3;
            lblForgotPassword.TabStop = true;
            lblForgotPassword.Text = "Quên mật khẩu?";
            lblForgotPassword.LinkClicked += lblForgotPassword_LinkClicked;
            // 
            // btnLogin
            // 
            btnLogin.BackColor = Color.FromArgb(24, 94, 177);
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnLogin.ForeColor = Color.White;
            btnLogin.Location = new Point(40, 428);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(460, 48);
            btnLogin.TabIndex = 4;
            btnLogin.Text = "Đăng nhập";
            btnLogin.UseVisualStyleBackColor = false;
            btnLogin.Click += btnLogin_Click;
            // 
            // lblMessage
            // 
            lblMessage.ForeColor = Color.Firebrick;
            lblMessage.Location = new Point(40, 491);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(460, 66);
            lblMessage.TabIndex = 5;
            // 
            // lblStage
            // 
            lblStage.ForeColor = Color.FromArgb(91, 108, 128);
            lblStage.Location = new Point(40, 575);
            lblStage.Name = "lblStage";
            lblStage.Size = new Size(460, 28);
            lblStage.TabIndex = 6;
            lblStage.Text = "Demo thoi";
            lblStage.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            AcceptButton = btnLogin;
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(540, 625);
            Controls.Add(pnlHeader);
            Controls.Add(lblTitle);
            Controls.Add(lblSubtitle);
            Controls.Add(lblUsername);
            Controls.Add(txtUsername);
            Controls.Add(lblPassword);
            Controls.Add(txtPassword);
            Controls.Add(chkShowPassword);
            Controls.Add(lblForgotPassword);
            Controls.Add(btnLogin);
            Controls.Add(lblMessage);
            Controls.Add(lblStage);
            Font = new Font("Segoe UI", 10F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Đăng nhập | Hệ thống điểm danh sinh viên";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion

        private Panel pnlHeader;
        private Label lblApplication;
        private Label lblTitle;
        private Label lblSubtitle;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private CheckBox chkShowPassword;
        private LinkLabel lblForgotPassword;
        private Button btnLogin;
        private Label lblMessage;
        private Label lblStage;
        private PictureBox pictureBox1;
    }
}
