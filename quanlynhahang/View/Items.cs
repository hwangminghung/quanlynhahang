using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace quanlynhahang.View
{
    public partial class Items : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=qlnh;Integrated Security=True");

        public Items()
        {
            InitializeComponent();
        }
        string filepath;
        private void gunaButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.bmp;*.jpg;*.png;*.gif)|*.bmp;*.jpg;*.png;*.gif|All files (*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Lấy đường dẫn tệp đã chọn
                filepath = ofd.FileName;

                // Hiển thị ảnh đã chọn (đây chỉ là một ví dụ, bạn cần thay đổi để phù hợp với ứng dụng của bạn)
                ptb_item.ImageLocation = filepath;
            }
        }
        private void ShowItems()
        {
            using (SqlCommand command = new SqlCommand("SELECT ItemID, ItemName, Price, ProductImage FROM MenuItems", con))
            {
                SqlDataAdapter da = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgv_item.DataSource = dt;
            }
        }
        private void btn_add_Click(object sender, EventArgs e)
        {
            AddItem();
            ShowItems();
        }
        private void AddItem()
        {
            using (SqlCommand command = new SqlCommand("INSERT INTO MenuItems (ItemID,ItemName, Price, ProductImage) VALUES (@ItemID,@ItemName, @Price, @ProductImage)", con))
            {
                command.Parameters.AddWithValue("@ItemID", int.Parse(txt_itemid.Text));

                command.Parameters.AddWithValue("@ItemName", txt_name.Text);
                command.Parameters.AddWithValue("@Price", decimal.Parse(txt_price.Text));
                command.Parameters.AddWithValue("@ProductImage", ConvertImageToBytes(ptb_item.Image));

                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
        }
        private void UpdateItem()
        {
            using (SqlCommand command = new SqlCommand("UPDATE MenuItems SET ItemID= @ItemID, ItemName = @ItemName, Price = @Price, ProductImage = @ProductImage WHERE ItemID = @ItemID", con))
            {
                command.Parameters.AddWithValue("@ItemID", int.Parse(txt_itemid.Text));
                command.Parameters.AddWithValue("@ItemName", txt_name.Text);
                command.Parameters.AddWithValue("@Price", decimal.Parse(txt_price.Text));
                command.Parameters.AddWithValue("@ProductImage", ConvertImageToBytes(ptb_item.Image));

                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
        }
        private void DeleteItem()
        {
            using (SqlCommand command = new SqlCommand("DELETE FROM MenuItems WHERE ItemID = @ItemID", con))
            {
                command.Parameters.AddWithValue("@ItemID", int.Parse(txt_itemid.Text));

                con.Open();
                command.ExecuteNonQuery();
                con.Close();
            }
        }
        private void btn_update_Click(object sender, EventArgs e)
        {
            UpdateItem();
            ShowItems();
        }
        private DataTable TimKiem()
        {
            string queryTimKiem = "select * from MenuItems ";
            queryTimKiem += "where ItemID Like '%' + @timkiem + '%'";
            queryTimKiem += "or ItemName Like '%' + @timkiem + '%'";
            queryTimKiem += "or Price Like '%' + @timkiem + '%'";


            SqlCommand command = new SqlCommand(queryTimKiem, con);
            command.Parameters.AddWithValue("@timkiem", txt_search.Text.Trim());

            SqlDataAdapter da = new SqlDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);

            dgv_item.DataSource = dt;

            con.Close(); // Lưu ý: Dòng này sẽ không được thực hiện vì đã có return trước đó

            return dt;
        }
        private byte[] ConvertImageToBytes(Image image)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                return memoryStream.ToArray();
            }
        }
        private void btn_delete_Click(object sender, EventArgs e)
        {
            DeleteItem();
            ShowItems();
        }

        private void Items_Load(object sender, EventArgs e)
        {
            dgv_item.ColumnHeadersHeight = 50;
            ShowItems();
        }
        private Image ByteArrayToImage(byte[] byteArray)
        {
            using (MemoryStream ms = new MemoryStream(byteArray))
            {
                Image image = Image.FromStream(ms);
                return image;
            }
        }
        private void dgv_item_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txt_itemid.Text = dgv_item.CurrentRow.Cells[0].Value.ToString();
            txt_name.Text = dgv_item.CurrentRow.Cells[1].Value.ToString();
            txt_price.Text = dgv_item.CurrentRow.Cells[2].Value.ToString();
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Kiểm tra xem ô được click có phải là ô trong cột "ItemID" không
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    // Lấy giá trị của cột "ItemID" từ ô hiện tại
                    string itemId = dgv_item.Rows[e.RowIndex].Cells["ItemID"].Value.ToString();

                    // Kiểm tra xem giá trị itemId có tồn tại không
                    if (!string.IsNullOrEmpty(itemId))
                    {
                        // Lấy giá trị của cột "ProductImage" từ dòng được chọn
                        if (dgv_item.Rows[e.RowIndex].Cells["ProductImage"].Value is byte[] imageData)
                        {
                            // Nếu có dữ liệu ảnh
                            if (imageData.Length > 0)
                            {
                                // Tạo một MemoryStream từ dữ liệu ảnh
                                using (MemoryStream ms = new MemoryStream(imageData))
                                {
                                    // Tạo một đối tượng Image từ MemoryStream
                                    Image image = Image.FromStream(ms);

                                    // Hiển thị hình ảnh trong PictureBox
                                    ptb_item.Image = image;
                                }
                            }
                            else
                            {
                                // Nếu không có dữ liệu ảnh, xử lý theo mong muốn của bạn (ví dụ: hiển thị ảnh mặc định)
                                ptb_item.Image = Properties.Resources.add; // DefaultImage là một hình ảnh mặc định bạn có thể cung cấp
                            }
                        }
                    }
                }
            }
        }

        private void txt_search_TextChanged(object sender, EventArgs e)
        {
            dgv_item.DataSource = txt_search.Text;
        }
    }
    }

