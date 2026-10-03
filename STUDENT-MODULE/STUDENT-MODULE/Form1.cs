namespace STUDENT_MODULE;

public partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent();
    }

    private void btnLogin_Click(object? sender, EventArgs e)
    {
        txtUsername.Text = txtUsername.Text.Trim();
        lblMessage.ForeColor = Color.Firebrick;
        if (string.IsNullOrWhiteSpace(txtUsername.Text))
        {
            lblMessage.Text = "Vui lòng nhập mã sinh viên hoặc email.";
            txtUsername.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            lblMessage.Text = "Vui lòng nhập mật khẩu.";
            txtPassword.Focus();
            return;
        }

        // Bước tiếp theo: gọi dịch vụ xác thực SQL và phân luồng vai trò.
        // Không xem việc nhập đủ dữ liệu là đăng nhập thành công.
        lblMessage.ForeColor = Color.FromArgb(24, 94, 177);
        lblMessage.Text = "Đã nhập đủ thông tin. Chức năng xác thực tài khoản sẽ được nối với SQL Server ở bước tiếp theo.";
    }

    private void chkShowPassword_CheckedChanged(object? sender, EventArgs e)
    {
        txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
    }

    private void lblForgotPassword_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
    {
        MessageBox.Show(this,
            "Vui lòng liên hệ quản trị viên hoặc Phòng Đào tạo để được cấp lại mật khẩu.",
            "Quên mật khẩu", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void LoginInput_TextChanged(object? sender, EventArgs e)
    {
        lblMessage.Text = string.Empty;
    }

    private void lblBrand_Click(object sender, EventArgs e)
    {

    }

    private void lblApplication_Click(object sender, EventArgs e)
    {

    }
}
