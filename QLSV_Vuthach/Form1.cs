using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLSV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Code xử lý khi form load (nếu cần)
        }

        private void button1_Click(object sender, EventArgs e) // Nút "Đăng nhập"
        {
            // Kiểm tra nếu tài khoản hoặc mật khẩu để trống
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Chuỗi kết nối (thay đổi theo server của bạn)
                string connectionString = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=QLSVDB;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "SELECT COUNT(*) FROM Users WHERE Username = @Username AND Password = @Password";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Username", textBox1.Text);
                            cmd.Parameters.AddWithValue("@Password", textBox2.Text);

                            int userCount = (int)cmd.ExecuteScalar();
                            if (userCount > 0)
                            {
                                // Nếu thông tin đăng nhập đúng
                                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                QLSV for1 = new QLSV();
                                for1.Show();
                                this.Hide();
                            }
                            else
                            {
                                // Nếu sai thì hiển thị thông báo lỗi
                                MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("Có lỗi xảy ra trong quá trình kết nối với cơ sở dữ liệu: " + sqlEx.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // Nút "Đăng ký"
        {
            dk for1 = new dk();
            for1.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Xử lý khi nội dung textBox1 thay đổi (nếu cần)
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Xử lý khi nội dung textBox2 thay đổi (nếu cần)
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Xử lý khi click vào label3 (nếu cần)
        }
    }
}
