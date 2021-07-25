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
    }
}
