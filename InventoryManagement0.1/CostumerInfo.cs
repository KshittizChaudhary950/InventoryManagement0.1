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
    public partial class CostumerInfo : Form
    {
        string cs = ConfigurationManager.ConnectionStrings["dbcs1"].ConnectionString;

        void DataGridviewFunction()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select*from CostumerInfomation";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

        }
        void ClearFunction()
        {
            iDnumericUpDown.Value = 0;
            Nametxt.Clear();
            CompanytextBox.Clear();
            ContacttextBox.Clear();
            addresstextBox.Clear();
            Citytxt.Clear();

            Nametxt.Focus();

        }
        public CostumerInfo()
        {
            InitializeComponent();
        }

        private void CostumerInfo_Load(object sender, EventArgs e)
        {
            DataGridviewFunction();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Addbutton_Click(object sender, EventArgs e)
        {
            try
            {

                if (NametextBox.Text != "" && CompanytextBox.Text != "" && ContacttextBox.Text != "" && addresstextBox.Text != "" && CitytextBox.Text != "")
                {
                    SqlConnection con = new SqlConnection(cs);
                    string query = "insert into CostumerInformation(CostumerName,CostumerCompanyName, Contact,Address,city) values(@Name,@company,@contact,@address,@city)";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Name", Nametxt.Text);
                    cmd.Parameters.AddWithValue("@company", CompanytextBox.Text);
                    cmd.Parameters.AddWithValue("@contact", ContacttextBox.Text);
                    cmd.Parameters.AddWithValue("@address", addresstextBox.Text);
                    cmd.Parameters.AddWithValue("@city", Citytxt.Text);
                    con.Open();

                    cmd.ExecuteNonQuery();

                }
                else
                {
                    MessageBox.Show("Please fill all information");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to add data ");

            }
            DataGridviewFunction();
            ClearFunction();

        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Hide();
            MDIParent1 mdi = new MDIParent1();
            mdi.Show();
        }

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            try
            {

                if (iDnumericUpDown.Value != 0)
                {

                    SqlConnection con = new SqlConnection(cs);
                    string query = "Update CostumerInfomation set CostumerName=@name , CostumerCompanyName=@company,Contact=@contact,Address=@address, city=@city where Id=@id";
                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@id", iDnumericUpDown.Value);
                    cmd.Parameters.AddWithValue("@name", Nametxt.Text);
                    cmd.Parameters.AddWithValue("@company", CompanytextBox.Text);
                    cmd.Parameters.AddWithValue("@contact", ContacttextBox.Text);
                    cmd.Parameters.AddWithValue("@address", addresstextBox.Text);
                    cmd.Parameters.AddWithValue("@city", Citytxt.Text);
                    con.Open();

                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Please select id to update costumer");
                }


            }
            catch (Exception)
            {
                MessageBox.Show("Updation is failed");
            }
            DataGridviewFunction();
            ClearFunction();
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            try
            {
                iDnumericUpDown.Value = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                Nametxt.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

                CompanytextBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                ContacttextBox.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                addresstextBox.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                Citytxt.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();

            }
            catch (Exception)
            {

                MessageBox.Show("Please select in id row");
            }

        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            try
            {
                if (iDnumericUpDown.Value != 0)
                {

                    SqlConnection con = new SqlConnection(cs);
                    string query = "Delete from CostumerInfomation where Id=@id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@id", iDnumericUpDown.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Please select id to delete costumer infromation");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("Delection is failed !!");
            }
            DataGridviewFunction();
            ClearFunction();


        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
            if (SearchtextBox.Text!="")
            {

                // working on search
                SqlConnection con = new SqlConnection(cs);
                string query = "select*from CostumerInfomation where CostumerName like @name + '%'";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.Parameters.AddWithValue("@name", SearchtextBox.Text.Trim());

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
                MessageBox.Show("Please type costumer name in search box");
            }

            SearchtextBox.Clear();
            ClearFunction();



        }

    }
}

