using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QLSV
{
    public partial class QLSV : Form
    {
        private string connectionString = "Data Source=MSI\\SQLEXPRESS;Initial Catalog=QLSVDB;Integrated Security=True";

        public QLSV()
        {
            InitializeComponent();
            this.dataGridView1.CellClick += new DataGridViewCellEventHandler(this.dataGridView1_CellClick);
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM SinhVien", conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;

                    // Không thêm dòng mới vào cuối DataGridView
                    // Chỉ hiển thị các dòng có dữ liệu
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Lỗi SQL khi kết nối: " + sqlEx.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void QLSV_Load(object sender, EventArgs e)
        {
            LoadData();
            dataGridView1.ReadOnly = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra chỉ số hàng hợp lệ
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                // Nếu nhấp vào dòng không có dữ liệu, xóa các ô nhập liệu
                if (selectedRow.IsNewRow) // Kiểm tra xem đây có phải là dòng mới hay không
                {
                    ClearTextBoxes(); // Xóa các ô nhập liệu để chuẩn bị cho việc thêm mới
                    textBox4.Enabled = true; // Kích hoạt lại ô nhập mã sinh viên
                    return;
                }

                // Tải dữ liệu từ dòng đã chọn vào các ô nhập
                textBox2.Text = selectedRow.Cells["Ten"].Value?.ToString() ?? string.Empty;
                textBox3.Text = selectedRow.Cells["Lop"].Value?.ToString() ?? string.Empty;
                radioButton1.Checked = selectedRow.Cells["GioiTinh"].Value?.ToString() == "Nam";
                radioButton2.Checked = selectedRow.Cells["GioiTinh"].Value?.ToString() == "Nữ";
                textBox4.Text = selectedRow.Cells["MaSV"].Value?.ToString() ?? string.Empty;
                textBox5.Text = selectedRow.Cells["Nganh"].Value?.ToString() ?? string.Empty;

                textBox4.Enabled = false; // Vô hiệu hóa ô nhập mã sinh viên khi đang sửa thông tin
            }
        }

        private void button1_Click(object sender, EventArgs e) // Thêm sinh viên
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) || // Tên
                string.IsNullOrWhiteSpace(textBox3.Text) || // Lớp
                string.IsNullOrWhiteSpace(textBox4.Text) || // Mã SV
                string.IsNullOrWhiteSpace(textBox5.Text))   // Ngành
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (textBox4.Text.Length > 10)
            {
                MessageBox.Show("Mã sinh viên không được vượt quá 10 ký tự!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Kiểm tra xem mã sinh viên đã tồn tại chưa
                    SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM SinhVien WHERE MaSV = @MaSV", conn);
                    checkCmd.Parameters.AddWithValue("@MaSV", textBox4.Text);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Mã sinh viên đã tồn tại. Vui lòng chọn mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string gioiTinh = radioButton1.Checked ? "Nam" : "Nữ";
                    string query = "INSERT INTO SinhVien (Ten, Lop, GioiTinh, MaSV, Nganh) VALUES (@Ten, @Lop, @GioiTinh, @MaSV, @Nganh)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Ten", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Lop", textBox3.Text);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@MaSV", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Nganh", textBox5.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearTextBoxes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm sinh viên: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) // Xóa sinh viên
        {
            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
            if (selectedRow.Cells["MaSV"].Value != null)
            {
                string maSV = selectedRow.Cells["MaSV"].Value.ToString();

                if (string.IsNullOrWhiteSpace(maSV))
                {
                    MessageBox.Show("Không có dữ liệu để xóa! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM SinhVien WHERE MaSV = @MaSV";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@MaSV", maSV);
                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            dataGridView1.Rows.Remove(selectedRow);
                            MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy sinh viên với mã đã cho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa sinh viên: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e) // Sửa thông tin sinh viên
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để sửa thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                    if (selectedRow.Cells["MaSV"].Value == null)
                    {
                        MessageBox.Show("Không tìm thấy mã sinh viên trong dòng đã chọn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string originalMaSV = selectedRow.Cells["MaSV"].Value.ToString();

                    if (textBox4.Text.Length > 10)
                    {
                        MessageBox.Show("Mã sinh viên không được vượt quá 10 ký tự!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Kiểm tra xem mã sinh viên mới có tồn tại hay không
                    if (textBox4.Text != originalMaSV)
                    {
                        SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM SinhVien WHERE MaSV = @MaSV", conn);
                        checkCmd.Parameters.AddWithValue("@MaSV", textBox4.Text);
                        int count = (int)checkCmd.ExecuteScalar();

                        if (count > 0)
                        {
                            MessageBox.Show("Mã sinh viên đã tồn tại. Vui lòng chọn mã khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string gioiTinh = radioButton1.Checked ? "Nam" : "Nữ";
                    string query = "UPDATE SinhVien SET Ten = @Ten, Lop = @Lop, GioiTinh = @GioiTinh, MaSV = @MaSV, Nganh = @Nganh WHERE MaSV = @OriginalMaSV";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Ten", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Lop", textBox3.Text);
                    cmd.Parameters.AddWithValue("@GioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@MaSV", textBox4.Text);
                    cmd.Parameters.AddWithValue("@Nganh", textBox5.Text);
                    cmd.Parameters.AddWithValue("@OriginalMaSV", originalMaSV);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ClearTextBoxes();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật thông tin sinh viên: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ClearTextBoxes()
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            textBox4.Enabled = true; // Kích hoạt lại ô nhập mã sinh viên
        }
    }
}
