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
    public partial class Purchase : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs1"].ConnectionString;
        public Purchase()
        {
            InitializeComponent();
        }
        public void ClearFunction()
        {
            ProductcomboBox.SelectedItem = null;
            ProductqtytextBox.Clear();
            Unit.Text= null;
            ProductPricetextBox.Clear();
            ProductTotaltextBox.Clear();

            PurchasedateTimePicker.Value.ToString("");
            CustomernamecomboBox.SelectedItem = null;

            PurchasetypecomboBox.SelectedItem = null;
            ExpirydateTimePicker.Value.ToString("");
            ProfittextBox.Clear();
            ProductcomboBox.Focus();


        }
        public void InsertPurchase()
        {
            // Insert part
            SqlConnection con = new SqlConnection(cs);

            string query = "insert into Purchase (ProductName,ProductQty, ProductUnit,ProductPrice,ProductTotal,PurchaseDate , PurchasePartyName,PurchaseType,ExpiryDate,Profit) values (@Productname,@Qty,@unit,@price,@total,@pdate,@Prname,@ptype,@expriy,@profit)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@Productname", ProductcomboBox.Text);

            cmd.Parameters.AddWithValue("@Qty", ProductqtytextBox.Text);
            cmd.Parameters.AddWithValue("@unit", Unit.Text);

            cmd.Parameters.AddWithValue("@price", ProductPricetextBox.Text);
            cmd.Parameters.AddWithValue("@total", ProductTotaltextBox.Text);
            cmd.Parameters.AddWithValue("@pdate", PurchasedateTimePicker.Value.ToString("dd-MM-yyyy"));
            cmd.Parameters.AddWithValue("@Prname", CustomernamecomboBox.Text);

            cmd.Parameters.AddWithValue("@ptype", PurchasetypecomboBox.Text);
            cmd.Parameters.AddWithValue("@expriy", ExpirydateTimePicker.Value.ToString("dd-MM-yyyy"));
            cmd.Parameters.AddWithValue("@profit", ProfittextBox.Text);


            con.Open();



            if (ProductcomboBox.Text != "" && ProductqtytextBox.Text != "" && ProductPricetextBox.Text != "" && PurchasetypecomboBox.Text != "")
            {

                try
                {
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {

                        MessageBox.Show("Inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                    else
                    {
                        MessageBox.Show("Please fill all fields in correct manner", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Please fill up all field");
                }
            }
            else
            {
                MessageBox.Show("Please insert all information");
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {
            this.Hide();
            MDIParent1 mdi1 = new MDIParent1();
            mdi1.Show();
        }

        private void Purchase_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
            FilProductName(); // funtion is called

            FillCustomerName();// Customer function is call
        }
        public void FilProductName()
        { // working on comboBOx
            SqlConnection con = new SqlConnection(cs);

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select*from ProductName";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            foreach(DataRow dr in dt.Rows)
            {
                ProductcomboBox.Items.Add(dr["ProductName"].ToString());
            }
            con.Close();

        }

        private void ProductcomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select*from ProductName where ProductName='"+ProductcomboBox.Text+"'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                Unit.Text = dr["units"].ToString();
            }
            con.Close();
        }
        public void FillCustomerName()
        {

            SqlConnection con = new SqlConnection(cs);

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select*from CostumerInfomation";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                CustomernamecomboBox.Items.Add(dr["CostumerName"].ToString());
            }
            con.Close();
        }

        private void ProductPricetextBox_Leave(object sender, EventArgs e)
        {
            ProductTotaltextBox.Text =Convert.ToString( Convert.ToInt32( ProductqtytextBox.Text) *Convert.ToInt32 (ProductPricetextBox.Text));
        }

        private void Purchasebutton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);

            int a;
            con.Open();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select*from Stock where Product_name ='" + ProductcomboBox.Text+"'";
            cmd1.ExecuteNonQuery();
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

            da1.Fill(dt1);
            a = Convert.ToInt32(dt1.Rows.Count.ToString());


            if (a == 0)
            {
                InsertPurchase();
                // Insert part

                SqlConnection con2 = new SqlConnection(cs);

                string query = "insert into Stock (Product_name,Product_qty, Product_unit) values (@Productname,@Qty,@unit)";
                SqlCommand cmd2 = new SqlCommand(query, con2);

                cmd2.Parameters.AddWithValue("@Productname", ProductcomboBox.Text);

                cmd2.Parameters.AddWithValue("@Qty", ProductqtytextBox.Text);
                cmd2.Parameters.AddWithValue("@unit", Unit.Text);

         

                con2.Open();



                if (ProductcomboBox.Text != "" && ProductqtytextBox.Text != "" && Unit.Text!="")
                {

                    try
                    {
                        int i = cmd2.ExecuteNonQuery();
                        if (i > 0)
                        {

                            MessageBox.Show("Inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }

                        else
                        {
                            MessageBox.Show("Please fill all fields in correct manner", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                        }
                    }
                    catch (Exception)
                    {

                        MessageBox.Show("Please fill up all field");
                    }
                }
                else
                {
                    MessageBox.Show("Please insert all information");
                }

            }
            else
            {
                InsertPurchase();

             
                SqlConnection con4 = new SqlConnection(cs);

                string query2 = "Update Stock set Product_qty=@Product_qty where Product_name=@productname";
                SqlCommand cmd4 = new SqlCommand(query2, con4);
                cmd4.Parameters.AddWithValue("@productname", ProductcomboBox.Text); 
                cmd4.Parameters.AddWithValue("@Product_qty", ProductqtytextBox.Text);
              
               
                con4.Open();

                if (ProductqtytextBox.Text!="" && ProductcomboBox.Text!="")
                {
                    int b = cmd4.ExecuteNonQuery();

                    if (b > 0)
                    {

                        MessageBox.Show("Stock is updated");
                    }
                    else
                    {
                       
                    }
                }
                else
                {
                    MessageBox.Show("Unable to update stock");
                }
                con4.Close();



            }


            con.Close();

            

            ClearFunction();

            

           
           
        }
    }
}
