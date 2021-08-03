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
        DataTable vdt = new DataTable();
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

            vdt.Clear();
            vdt.Columns.Add("product");
           vdt.Columns.Add("price");
           vdt.Columns.Add("qty");
            vdt.Columns.Add("Total");
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
            try
            {
                TotaltextBox.Text = Convert.ToString(Convert.ToInt32(PricetextBox.Text) * Convert.ToInt32(QtytextBox.Text));
            }
            catch(Exception ex)
            {
                
            }
            
            
          
        }

        private void Addbutton_Click(object sender, EventArgs e)
        {
            int stock = 0;
            SqlConnection con = new SqlConnection(cs);
            string query = "select*from Stock where Product_name=@product";
            SqlCommand cmd1 = new SqlCommand(query,con);
            cmd1.Parameters.Add("@product",ProducttextBox.Text);
            //cmd1.ExecuteNonQuery();


            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            da1.Fill(dt1);

            foreach (DataRow dr1 in dt1.Rows)
            {
                stock = Convert.ToInt32(dr1["Product_qty"].ToString());
            }
            if (Convert.ToInt32(QtytextBox.Text) > stock)
            {
                MessageBox.Show("This much value is not aviable");
            }
            else
            {
                DataRow dr = vdt.NewRow();
                dr["product"] = ProducttextBox.Text;
                dr["price"] = PricetextBox.Text;
                dr["qty"] = QtytextBox.Text;
                dr["Total"] = TotaltextBox.Text;
                vdt.Rows.Add(dr);
                dataGridView.DataSource =vdt;
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

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            try
            {
                total = 0;
                vdt.Rows.RemoveAt(Convert.ToInt32(dataGridView.CurrentCell.RowIndex.ToString()));
                foreach (DataRow dr1  in vdt.Rows)
                {
                    total = total +Convert.ToInt32( dr1["Total"].ToString());
                    label10.Text = total.ToString();

                }
            }
            catch (Exception ex)
            {

                
            }
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (FirstnametextBox.Text != "" && ProducttextBox.Text != "" && TotaltextBox.Text != "")
                {
                    string order_id = "";
                    SqlConnection con = new SqlConnection(cs);
                    string query = "insert into Order_user (firstname,lastname,BillType,purchaseDate) values(@firstname,@lastname,@Billtype,@purchaseDate)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@firstname", FirstnametextBox.Text);
                    cmd.Parameters.AddWithValue("@lastname", LastnametextBox.Text);
                    cmd.Parameters.AddWithValue("@Billtype", comboBox1.Text);
                    cmd.Parameters.AddWithValue("@purchaseDate", dateTimePicker1.Value.ToString("dd-MM-yyyy"));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();

                    SqlConnection con2 = new SqlConnection(cs);
                    string query2 = "select top 1* from order_user order by Orderid desc";
                    SqlCommand cmd2 = new SqlCommand(query2, con2);
                    con2.Open();
                    cmd2.ExecuteNonQuery();

                    SqlDataAdapter sda = new SqlDataAdapter(query2, con2);
                    DataTable data = new DataTable();
                    sda.Fill(data);
                    foreach (DataRow dr2 in data.Rows)
                    {
                        order_id = dr2["Orderid"].ToString();
                    }


                    con2.Close();
                    SqlConnection con3 = new SqlConnection(cs);
                    string query3 = "insert into Order_item (Order_id,Product,Price,Qty,Total) values(@order,@Product,@Price,@Qty,@total)";

                    SqlCommand cmd3 = new SqlCommand(query3, con3);
                    cmd3.Parameters.AddWithValue("@order", order_id);
                    cmd3.Parameters.AddWithValue("@Product", ProducttextBox.Text);
                    cmd3.Parameters.AddWithValue("@Price", PricetextBox.Text);
                    cmd3.Parameters.AddWithValue("@Qty", QtytextBox.Text);
                    cmd3.Parameters.AddWithValue("total", TotaltextBox.Text);
                    con3.Open();
                    cmd3.ExecuteNonQuery();
                    con3.Close();

                    MessageBox.Show("Your data is sucessfully save");
                }
                else
                {
                    MessageBox.Show("Please fill all information");
                }

            }
            catch (Exception ex)
            {

            }
            
           


        }
    }
}
