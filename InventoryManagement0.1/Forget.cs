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
using System.Security.Cryptography;

namespace InventoryManagement0._1
{
    public partial class Forget : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs1"].ConnectionString;
        public Forget()
        {
            InitializeComponent();
        }


        private void Forget_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        private void Savebutton_Click(object sender, EventArgs e)
        {
            if(Username2textBox.Text!="" && NewpasswordtextBox.Text!="" && confirmtextBox.Text!="")
            {
                if(NewpasswordtextBox.Text==confirmtextBox.Text)
                {

                
                // working on update 
               SqlConnection con = new SqlConnection(cs);
                string query = "Update Registration set password=@password where Id=@id and username=@username";
                SqlCommand cmd = new SqlCommand(query, con);
                  
                    cmd.Parameters.AddWithValue("@password", confirmtextBox.Text);
                    cmd.Parameters.AddWithValue("@id", FidnumericUpDown.Value);
                cmd.Parameters.AddWithValue("@username", Username2textBox.Text);
                
             
                
         

                con.Open();


                    if (FidnumericUpDown.Value != 0)
                    {

                        try
                        {
                            int a = cmd.ExecuteNonQuery();
                            if (a > 0)
                            {

                                MessageBox.Show("Password has successfully change", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.Hide();
                                Login f1 = new Login();
                                f1.Show();

                            }

                            else
                            {
                                MessageBox.Show("Password updation is fail", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                            }
                        }
                        catch (Exception)
                        {

                            MessageBox.Show("Please fill up all field");
                        }
                        


                    }
                    else
                    {
                        MessageBox.Show("Password does not match !!", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    }
                    con.Close();
                }
           



            }
            else
            {
                MessageBox.Show("You cant save without filling above form!!", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            ClearFuntion();
        }

        private void Clearbutton_Click(object sender, EventArgs e)
        {
            ClearFuntion();
        }
        void ClearFuntion()
        {
            FidnumericUpDown.Value = 0;
            Username2textBox.Clear();
            confirmtextBox.Clear();
            NewpasswordtextBox.Clear();

            Username2textBox.Focus();

        }

        private void label6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login f1 = new Login();
            f1.Show();
        }
    }
}
