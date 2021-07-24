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
    public partial class Units : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs1"].ConnectionString;
        public Units()
        {
            InitializeComponent();
        }

        private void Units_Load(object sender, EventArgs e)
        {

        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            if(Unittxt.Text!="")
            {

            SqlConnection con = new SqlConnection(cs);
            string query = "Insert into units (units) values(@units)";
            SqlCommand cmd = new SqlCommand(query,con);
            cmd.Parameters.AddWithValue("@units",Unittxt.Text);

                con.Open();
                    int a = cmd.ExecuteNonQuery();

                if(a>0)
                {
                    MessageBox.Show("Inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Insertion is failed !!", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                }

            }
            else
            {
                MessageBox.Show("You cant save empty units", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "update units set units='"+Unittxt.Text+"' where ID=";
        }
    }
}
