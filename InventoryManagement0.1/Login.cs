using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace InventoryManagement0._1
{
  
    public partial class Login : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs1"].ConnectionString;
        

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            if (con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Usertxt.Clear();
            PasswordtextBox.Clear();
            Usertxt.Focus();
        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {
            int i= 0;
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from Registration where username=@username and password=@password";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@username", Usertxt.Text);
            cmd.Parameters.AddWithValue("@password", PasswordtextBox.Text);

            con.Open();
           




            con.Close();
         
        }
    }
}
