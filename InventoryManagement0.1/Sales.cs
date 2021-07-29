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
        DataTable dt = new DataTable();
        int total = 0;
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

            dt.Clear();
            dt.Columns.Add("product");
            dt.Columns.Add("price");
            dt.Columns.Add("qty");
            dt.Columns.Add("Total");
        }

        private void ProducttextBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                SqlConnection con = new SqlConnection(cs);
                listBox1.Visible = true;
                listBox1.Items.Clear();


                string query = "select * from Stock where Product_name like @Productname +'%'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Productname", ProducttextBox.Text);

                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow dr in dt.Rows)
                {
                    listBox1.Items.Add(dr["Product_name"].ToString());
                }
            }
            catch (Exception ex)
            {
                
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
                   PricetextBox.Focus();
                }

            }
            catch (Exception)
            {

            }
        }

        private void PricetextBox_Enter(object sender, EventArgs e)
        {

            try
            {

                SqlConnection con = new SqlConnection(cs);
                string query = "select * from Purchase where ProductName=@productname ";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@productname", ProducttextBox.Text);

                // cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    PricetextBox.Text = dr["ProductPrice"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
         

        }

        private void QtytextBox_Leave(object sender, EventArgs e)
        {

            TotaltextBox.Text= Convert.ToString(Convert.ToInt32(PricetextBox.Text) *Convert.ToInt32(QtytextBox.Text));
            
          
        }

        private void Addbutton_Click(object sender, EventArgs e)
        {
            int stock = 0;
            SqlCommand cmd1 = new SqlCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select*from Stock where Product_name='" + ProducttextBox.Text + "'";
            cmd1.ExecuteNonQuery();

            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);

            foreach (DataRow dr in dt1.Rows)
            {
                stock = Convert.ToInt32(dr["Product_qty"].ToString());
            }
            if (Convert.ToInt32(QtytextBox.Text) > stock)
            {
                MessageBox.Show("This much value is not aviable");
            }
            else
            {
                DataRow dr = dt.NewRow();
                dr["product"] = ProducttextBox.Text;
                dr["price"] = PricetextBox.Text;
                dr["qty"] = QtytextBox.Text;
                dr["Total"] = TotaltextBox.Text;
                dt.Rows.Add(dr);
                dataGridView.DataSource = dt;
                total = total +Convert.ToInt32( dr["Total"].ToString());
                label10.Text = total.ToString();
            }
        }

        private void TotaltextBox_TextChanged(object sender, EventArgs e)
        {//this is not working 
          //  TotaltextBox.Text = Convert.ToString(Convert.ToInt32(QtytextBox.Text) * Convert.ToInt32(PricetextBox.Text));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            TotaltextBox.Text = Convert.ToString(Convert.ToInt32(QtytextBox.Text) * Convert.ToInt32(PricetextBox.Text));
        }
    }
}
