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
    public partial class Productname : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs1"].ConnectionString;

        void DataGridviewFunction()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select*from Productname";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

        }

        void ClearFunction()
        {
            comboBox1.Items.Clear();
            ProducttextBox.Clear();

            SearchtextBox.Clear();
            ProducttextBox.Focus();
        }
        void CombomboxFill()
        {
            SqlConnection con = new SqlConnection(cs);

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select*from units";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["units"].ToString());
            }
            con.Close();

        }
        public Productname()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            this.Hide();
            MDIParent1 mdi = new MDIParent1();
            mdi.Show();
        }

        private void Addbtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "insert into Productname(Productname,units) values(@productname,@units)";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@productname", ProducttextBox.Text);
            cmd.Parameters.AddWithValue("@units", comboBox1.SelectedItem);

            con.Open();


            if (comboBox1.Text != "" && ProducttextBox.Text != "")
            {
                cmd.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Please provide product", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            DataGridviewFunction();
            ClearFunction();

        }


        private void Productname_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

           
            DataGridviewFunction();
          
            CombomboxFill();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //CombomboxFill();
        }

        private void Viewbutton_Click(object sender, EventArgs e)
        {
            DataGridviewFunction();
        }

        private void Updatebtn_Click(object sender, EventArgs e)
        {

            //working on update 
            SqlConnection con = new SqlConnection(cs);
            string query = "Update Productname set Productname=@productname , units=@units where Id=@id";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", idnumericUpDown.Value);
            cmd.Parameters.AddWithValue("@productname", ProducttextBox.Text);
            cmd.Parameters.AddWithValue("@units", comboBox1.SelectedItem);


            con.Open();
            if (idnumericUpDown.Value != 0)
            {
                cmd.ExecuteNonQuery();
            }
            else
            {
                MessageBox.Show("Please provide Id and productname !!!", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
            DataGridviewFunction();

            con.Close();

        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "delete from Productname where Id=@id";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", idnumericUpDown.Value);
            con.Open();
            try
            {
                if (idnumericUpDown.Value != 0)
                {
                    cmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Please select id number to delete respective data");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Delection is failed");
            }
            con.Close();

            DataGridviewFunction();
            ClearFunction();





        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            try
            {
                idnumericUpDown.Value = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ProducttextBox.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

                comboBox1.SelectedItem = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

            }
            catch (Exception)
            {

                MessageBox.Show("Please select in id row");
            }


        }

        private void Searchbutton_Click(object sender, EventArgs e)
        {
            // working on search
            SqlConnection con = new SqlConnection(cs);
            string query = "select*from Productname where Productname like @Productname + '%'";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            sda.SelectCommand.Parameters.AddWithValue("@Productname", SearchtextBox.Text.Trim());

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

            SearchtextBox.Clear();
            SearchtextBox.Focus();
     

        }

    }

}
    
