using quanlynhahang.View;
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

namespace quanlynhahang
{
    public partial class Menu : Form
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=qlnh;Integrated Security=True");

        public Menu()
        {
            InitializeComponent();
        }

     
        private void AddControls(Form f)
        {
            centerpanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            centerpanel.Controls.Add(f);
            f.Show();
        }

        private void gunaPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btn_item_Click(object sender, EventArgs e)
        {
            AddControls(new Items());
        }

        private void btn_customer_Click(object sender, EventArgs e)
        {
            AddControls(new Customer());
        }

        private void btn_table_Click(object sender, EventArgs e)
        {
            AddControls(new Table());
        }
    }
}
