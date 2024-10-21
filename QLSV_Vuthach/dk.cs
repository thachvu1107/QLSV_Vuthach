using System;
using System.Windows.Forms;

namespace QLSV
{
    public partial class dk : Form
    {
        // Biến tạm để lưu tài khoản và mật khẩu
        public static string registeredUsername = "";
        public static string registeredPassword = "";

        public dk()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) // Nút "Đăng ký"
        {
            // Kiểm tra xem các trường nhập liệu có trống hay không
            if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tài khoản và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Lưu tài khoản và mật khẩu đã đăng ký
                registeredUsername = textBox2.Text;
                registeredPassword = textBox3.Text;

                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Sau khi đăng ký xong, đóng form đăng ký và trở về form đăng nhập
                this.Close();
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e) { }

        private void textBox3_TextChanged(object sender, EventArgs e) { }
    }
}
