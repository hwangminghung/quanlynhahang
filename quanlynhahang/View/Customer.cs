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
    public partial class Customer : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=qlnh;Integrated Security=True");

        public Customer()
        {
            InitializeComponent();
        }

        private void gunaTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_customer.Text = "";
            txt_name.Text = "";
            txt_phone.Text = "";
            
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            HienThiCustomer();
        }
        private void HienThiCustomer()
        {
            SqlCommand cmd = new SqlCommand("select * from customers",con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv_customer.DataSource = dt;
        }

        private void dgv_customer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_customer.Text = dgv_customer.CurrentRow.Cells[0].Value.ToString();
            txt_name.Text = dgv_customer.CurrentRow.Cells[1].Value.ToString();
            txt_phone.Text = dgv_customer.CurrentRow.Cells[2].Value.ToString();     
               
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_customer.Text) || string.IsNullOrEmpty(txt_name.Text) ||
        string.IsNullOrEmpty(txt_phone.Text))
            {
                MessageBox.Show("Xin hãy điền đầy đủ thông tin trước khi thêm");
            }
            else
            {
                SqlDataAdapter da = new SqlDataAdapter("select CustomerID from Customers where CustomerID = '" + txt_customer.Text + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show("Trùng mã khách !");
                }
                else
                {
                    string query = "insert into Customers values('" + txt_customer.Text + "',N'" + txt_name.Text + "','" + txt_phone.Text + "')";
                    SqlCommand cmd = new SqlCommand(query, con);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công");
                    con.Close();

                    HienThiCustomer();
                }
            }
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_customer.Text) || string.IsNullOrEmpty(txt_name.Text) ||
    string.IsNullOrEmpty(txt_phone.Text) )
            {
                MessageBox.Show("Xin hãy chọn đối tượng cần sửa");
            }
            else
            {
                try
                {
                    string query = "update Customers set CustomerID='" + txt_customer.Text + "',NameCustomer=N'" + txt_name.Text + "',Phone='" + txt_phone.Text + "' where CustomerID='" + dgv_customer.CurrentRow.Cells[0].Value + "'";

                    SqlCommand cmd = new SqlCommand(query, con);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Sửa thành công");
                    con.Close();
                    btn_reset.PerformClick();

                    HienThiCustomer();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        private DataTable TimKiem()
        {
            string queryTimKiem = "select * from Customers ";
            queryTimKiem += "where CustomerID Like '%' + @timkiem + '%'";
            queryTimKiem += "or NameCustomer Like '%' + @timkiem + '%'";
            queryTimKiem += "or Phone Like '%' + @timkiem + '%'";
           

            SqlCommand command = new SqlCommand(queryTimKiem, con);
            command.Parameters.AddWithValue("@timkiem", txt_search.Text.Trim());

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgv_customer.DataSource = dt;

            con.Close(); // Lưu ý: Dòng này sẽ không được thực hiện vì đã có return trước đó

            return dt;
        }


        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            dgv_customer.DataSource= TimKiem();
        }


        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txt_customer.Text) || string.IsNullOrEmpty(txt_name.Text) ||
    string.IsNullOrEmpty(txt_phone.Text) )
            {
                MessageBox.Show("Xin hãy chọn đối tượng cần xoá");
            }
            else
            {
                try
                {
                    string query = "delete from Customers where CustomerID=@mcm";
                    SqlCommand command = new SqlCommand(query, con);
                    command.Parameters.AddWithValue("@mcm", txt_customer.Text);

                    con.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Xoá Thành Công");
                    txt_customer.Text = string.Empty;
                    txt_name.Text = string.Empty;
                    txt_phone.Text = string.Empty;
                    con.Close();

                    HienThiCustomer();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
    }
}
