using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace InventoryManagement0._1
{
   
    public partial class Sales : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs1"].ConnectionString;
        public Sales()
        {
            InitializeComponent();
        }

        private void Sales_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);

            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        private void ProducttextBox_KeyUp(object sender, KeyEventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            listBox1.Visible = true;
            listBox1.Items.Clear();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from Stock where Product_name like('"+ProducttextBox.Text+"%')";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            foreach(DataRow dr in dt.Rows)
            {
                listBox1.Items.Add(dr["Product_name"].ToString());
            }
        }

        private void ProducttextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void ProducttextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Down)
            {
                listBox1.Focus();
                listBox1.SelectedIndex = 0;
            }
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if(e.KeyCode==Keys.Down)
                {
                    this.listBox1.SelectedIndex = this.listBox1.SelectedIndex + 1;
                }  
                
                if(e.KeyCode==Keys.Up)
                {
                    this.listBox1.SelectedIndex = this.listBox1.SelectedIndex - 1;
                }  
                if(e.KeyCode==Keys.Enter)
                {
                    ProducttextBox.Text = listBox1.SelectedItem.ToString();
                    listBox1.Visible = false;
                    ProducttextBox.Focus();
                }

            }
            catch (Exception)
            {

            }
        }

        private void PricetextBox_Enter(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select top1 * from Purchase where ProductName ('" + ProducttextBox.Text + "' order by id desc)";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            foreach(DataRow dr in dt.Rows)
            {
                PricetextBox.Text = dr["ProductPrice"].ToString();
            }
        }
    }
}
