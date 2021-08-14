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
    public partial class PucharseReport : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs1"].ConnectionString;

        public PucharseReport()
        {
            InitializeComponent();
        }

        private void PucharseReport_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            if(con.State==ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i = 0;
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from Purchase";
            SqlCommand cmd = new SqlCommand(query, con);
           // cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            foreach(DataRow dr in dt.Rows)
            {
                i = i +Convert.ToInt32( dr["ProductTotal"].ToString());
            }
            total.Text = i.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string startdate = "";
            string enddate = "";
            startdate = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            enddate = dateTimePicker2.Value.ToString("dd/MM/yyyy");
            int i = 0;
            SqlConnection con = new SqlConnection(cs);
            string query = "select * from Purchase where PurchaseDate >='"+startdate.ToString()+"'AND PurchaseDate<='"+enddate.ToString()+"'";
            SqlCommand cmd = new SqlCommand(query, con);
            // cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            foreach (DataRow dr in dt.Rows)
            {
               // i = i + Convert.ToInt32(dr["ProductTotal"].ToString());
            }
            total.Text = i.ToString();
        }
    }
}
