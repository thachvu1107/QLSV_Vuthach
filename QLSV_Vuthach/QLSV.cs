using System;
using System.Data;
using System.Windows.Forms;

namespace QLSV
{
    public partial class QLSV : Form
    {
        public QLSV()
        {
            InitializeComponent();
        }

        DataTable dataTable = new DataTable();

        private void QLSV_Load(object sender, EventArgs e)
        {
            dataTable.Columns.Add("Tên");
            dataTable.Columns.Add("Lớp");
            dataTable.Columns.Add("Giới tính");
            dataTable.Columns.Add("Mã SV");
            dataTable.Columns.Add("Ngành");

            
            dataGridView1.DataSource = dataTable;

            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];

                
                textBox2.Text = selectedRow.Cells[0].Value.ToString(); 
                textBox3.Text = selectedRow.Cells[1].Value.ToString(); 
                if (selectedRow.Cells[2].Value.ToString() == "Nam")
                {
                    radioButton1.Checked = true; 
                }
                else
                {
                    radioButton2.Checked = true;
                }
                textBox4.Text = selectedRow.Cells[3].Value.ToString(); 
                textBox5.Text = selectedRow.Cells[4].Value.ToString(); 
            }
        }

        private void button1_Click(object sender, EventArgs e) 
        {
            
            if (textBox4.Text.Length > 10)
            {
                MessageBox.Show("Mã sinh viên không được vượt quá 10 số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            string gioiTinh = radioButton1.Checked ? "Nam" : "Nữ";

            dataTable.Rows.Add(textBox2.Text, textBox3.Text, gioiTinh, textBox4.Text, textBox5.Text);

            
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void button2_Click(object sender, EventArgs e) 
        {
            
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.RemoveAt(row.Index);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e) 
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                if (textBox4.Text.Length > 10)
                {
                    MessageBox.Show("Mã sinh viên không được vượt quá 10 số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; 
                }

                selectedRow.Cells[0].Value = textBox2.Text; 
                selectedRow.Cells[1].Value = textBox3.Text; 
                selectedRow.Cells[2].Value = radioButton1.Checked ? "Nam" : "Nữ"; 
                selectedRow.Cells[3].Value = textBox4.Text; 
                selectedRow.Cells[4].Value = textBox5.Text; 
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sinh viên để sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e) 
        {
            
            this.Close();
        }

        private void textBox4_TextChanged(object sender, EventArgs e) { }
        private void textBox5_TextChanged(object sender, EventArgs e) { }
        private void label1_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void textBox3_TextChanged(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void radioButton2_CheckedChanged(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void textBox2_TextChanged(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label5_Click(object sender, EventArgs e) { }
        private void radioButton1_CheckedChanged(object sender, EventArgs e) { }
    }
}
