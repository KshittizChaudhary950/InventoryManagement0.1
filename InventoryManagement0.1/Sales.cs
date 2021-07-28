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
            listBox1.Visible = true;
        }

        private void ProducttextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
