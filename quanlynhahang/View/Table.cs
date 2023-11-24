using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlynhahang.View
{
    public partial class Table : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=qlnh;Integrated Security=True");

        public Table()
        {
            InitializeComponent();
        }

        private void Table_Load(object sender, EventArgs e)
        {
            HienThiTable();
        }
        private void HienThiTable()
        {
            SqlCommand cmd = new SqlCommand("select * from Tables", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv_table.DataSource = dt;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_capacity.Text) || string.IsNullOrEmpty(txt_name.Text) ||
        string.IsNullOrEmpty(txt_tableid.Text))
            {
                MessageBox.Show("Xin hãy điền đầy đủ thông tin trước khi thêm");
            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter("select TableID from Tables where TableID = '" + txt_tableid.Text + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show("Trùng mã bàn !");
                }
                else
                {
                    string query = "insert into Tables values('" + txt_tableid.Text + "',N'" + txt_name.Text + "','" + txt_capacity.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công");
                    con.Close();

                    HienThiTable();
                }
            }
        }

        private void dgv_table_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_tableid.Text = dgv_table.CurrentRow.Cells[0].Value.ToString();
            txt_name.Text = dgv_table.CurrentRow.Cells[1].Value.ToString();
            txt_capacity.Text = dgv_table.CurrentRow.Cells[2].Value.ToString();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_tableid.Text) || string.IsNullOrEmpty(txt_name.Text) ||
   string.IsNullOrEmpty(txt_capacity.Text))
            {
                MessageBox.Show("Xin hãy chọn đối tượng cần sửa");
            }
            else
            {
                try
                {
                    string query = "update Tables set TableID='" + txt_tableid.Text + "',TableName=N'" + txt_name.Text + "',Capacity='" + txt_capacity.Text + "' where TableID='" + dgv_table.CurrentRow.Cells[0].Value + "'";

                    SqlCommand cmd = new SqlCommand(query, con);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sửa thành công");
                    con.Close();
                    btn_reset.PerformClick();

                    HienThiTable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_tableid.Text) || string.IsNullOrEmpty(txt_name.Text) ||
   string.IsNullOrEmpty(txt_capacity.Text))
            {
                MessageBox.Show("Xin hãy chọn đối tượng cần xoá");
            }
            else
            {
                try
                {
                    string query = "delete from Tables where TableID=@mtb";
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@mtb", txt_tableid.Text);

                    con.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Xoá Thành Công");
                    txt_tableid.Text = string.Empty;
                    txt_name.Text = string.Empty;
                    txt_capacity.Text = string.Empty;
                    con.Close();

                    HienThiTable();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private DataTable TimKiem()
        {
            string queryTimKiem = "select * from Tables ";
            queryTimKiem += "where TableID Like '%' + @timkiem + '%'";
            queryTimKiem += "or TableName Like '%' + @timkiem + '%'";
            queryTimKiem += "or Capacity Like '%' + @timkiem + '%'";


            SqlCommand command = new SqlCommand(queryTimKiem, con);
            command.Parameters.AddWithValue("@timkiem", txt_search.Text.Trim());

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgv_table.DataSource = dt;

            con.Close(); // Lưu ý: Dòng này sẽ không được thực hiện vì đã có return trước đó

            return dt;
        }
        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            dgv_table.DataSource = TimKiem();
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_capacity.Text = string.Empty;
            txt_name.Text = string.Empty;
            txt_tableid.Text = string.Empty;
        }

        private void gunaSeparator1_Click(object sender, EventArgs e)
        {

        }

        private void gunaLabel3_Click(object sender, EventArgs e)
        {

        }

        private void txt_capacity_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaLabel2_Click(object sender, EventArgs e)
        {

        }

        private void txt_name_TextChanged(object sender, EventArgs e)
        {

        }

        private void gunaLabel1_Click(object sender, EventArgs e)
        {

        }

        private void txt_tableid_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgv_table_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void gunaPictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
