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

        }

        private void button1_Click(object sender, EventArgs e) // Nút "Đăng nhập"
        {
            // Kiểm tra nếu tài khoản hoặc mật khẩu để trống
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (string.IsNullOrEmpty(dk.registeredUsername) || string.IsNullOrEmpty(dk.registeredPassword))
            {
                // Kiểm tra xem có tài khoản đã được đăng ký chưa
                MessageBox.Show("Bạn chưa đăng ký tài khoản, vui lòng đăng ký trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                // Kiểm tra thông tin đăng nhập với tài khoản đã đăng ký
                if (textBox1.Text == dk.registeredUsername && textBox2.Text == dk.registeredPassword)
                {
                    // Nếu đúng thì mở form QLSV
                    QLSV for1 = new QLSV();
                    for1.Show();
                    this.Hide();  // Ẩn form đăng nhập
                }
                else
                {
                    // Nếu sai thì hiển thị thông báo lỗi
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button2_Click(object sender, EventArgs e) // Nút "Đăng ký"
        {
            dk for1 = new dk();
            for1.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

