using System;
using System.Data.SqlClient;
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
                // Chuỗi kết nối (thay đổi theo server của bạn)
                string connectionString = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=QLSVDB;Integrated Security=True";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "INSERT INTO Users (Username, Password) VALUES (@Username, @Password)";
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@Username", textBox2.Text);
                            cmd.Parameters.AddWithValue("@Password", textBox3.Text);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                // Sau khi đăng ký xong, đóng form đăng ký
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Đăng ký thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void textBox2_TextChanged(object sender, EventArgs e) { }

        private void textBox3_TextChanged(object sender, EventArgs e) { }
    }
}
