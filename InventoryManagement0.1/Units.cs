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

        void DataGridviewFunction()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select*from units";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

        }

        void ClearFunction()
        {
            Idincrese.Value = 0;
            Unittxt.Clear();
            SearchTxt.Clear();
            Unittxt.Focus();
        }
        public Units()
        {
            InitializeComponent();
        }

        private void Units_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open(); 


            // working on search
           if(SearchTxt.Text!="")
            {

            
            string query = "select*from units where units like @units + '%'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            sda.SelectCommand.Parameters.AddWithValue("@units", SearchTxt.Text.Trim());

            DataTable data = new DataTable();
            sda.Fill(data);

            if (data.Rows.Count > 0)
            {
                dataGridView1.DataSource = data;
                

            }
            else
            {
                MessageBox.Show("No data is found");
                dataGridView1.DataSource = null;
            }
                ClearFunction()
            }

            DataGridviewFunction();
   




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
            ClearFunction();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {
            if(Idincrese.Value!=0 && Unittxt.Text!="")
            {

            SqlConnection con = new SqlConnection(cs);
            string query = "update units set units='"+Unittxt.Text+"' where ID='"+Idincrese.Value+"'";

            SqlCommand cmd = new SqlCommand(query,con);
            con.Open();
            int a=cmd.ExecuteNonQuery();
                if(a>0)
                {
                    MessageBox.Show("Update successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Updation is failed !!", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Please provide id to update !!", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void Viewbtn_Click(object sender, EventArgs e)
        {
            DataGridviewFunction();
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (Idincrese.Value != 0)
            {
                SqlConnection con = new SqlConnection(cs);
                string query = "Delete from units where Id='" + Idincrese.Value + "'";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();
                int a = cmd.ExecuteNonQuery();

                if (a > 0)
                {
                    MessageBox.Show("Delete successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Delection is failed !!", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("No units hasve zero Id in data !!", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

            }
            ClearFunction();
        }

        private void homelbl_Click(object sender, EventArgs e)
        {
            this.Hide();
            MDIParent1 mdi = new MDIParent1();
            mdi.Show();
        }

        private void Searchbtn_Click(object sender, EventArgs e)
        {
            if (SearchTxt.Text!="")
            {


                // working on search
                SqlConnection con = new SqlConnection(cs);
                string query = "select*from units where units like @units + '%'";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.Parameters.AddWithValue("@units", SearchTxt.Text.Trim());

                DataTable data = new DataTable();
                sda.Fill(data);

                if (data.Rows.Count > 0)
                {
                    dataGridView1.DataSource = data;

                }
                else
                {
                    MessageBox.Show("No data is found");
                    dataGridView1.DataSource = null;
                }
            }
            else
            {
                MessageBox.Show("Please provide unit to search operation","Failed",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            ClearFunction();

        }


        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                Idincrese.Value = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                Unittxt.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

            }
            catch (Exception)
            {

                MessageBox.Show("Please select in id row");
            }
        }
    }
}
