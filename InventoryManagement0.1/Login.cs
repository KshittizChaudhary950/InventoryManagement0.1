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
            ResetFunction();
        }

        private void Loginbtn_Click(object sender, EventArgs e)
        {
            int i= 0;
            SqlConnection con = new SqlConnection(cs);
            SqlDataAdapter sda = new SqlDataAdapter( "select * from Registration where username='"+Usertxt.Text+"' and password='"+PasswordtextBox.Text+"'",con);
            
            DataTable dt = new DataTable();
          
            sda.Fill(dt);

            i = Convert.ToInt32(dt.Rows.Count.ToString());
            if(i==0)
            {
                MessageBox.Show("Username and password does not match");
                ResetFunction();

            }
            else
            {
                this.Hide();
               MDIParent1 mdi = new MDIParent1();
                mdi.Show();

            }

          
        }
        void ResetFunction()
        {
            Usertxt.Clear();
            PasswordtextBox.Clear();
            Usertxt.Focus();

        }
    }
}
