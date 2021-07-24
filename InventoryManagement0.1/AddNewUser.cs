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
    public partial class AddNewUser : Form
    {

        string cs = ConfigurationManager.ConnectionStrings["dbcs1"].ConnectionString;

        void DataGridviewFunction()
        {
            SqlConnection con = new SqlConnection(cs);
            string query = "select*from Registration";
            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable data = new DataTable();
            sda.Fill(data);
            dataGridView1.DataSource = data;

        }
        void ClearFunction()
        {
            IDnumericUpDown.Value = 0;
            FirstNametxt.Clear();
            lastnametextBox.Clear();
            Username1textBox.Clear();
            Password1textBox.Clear();
            emailtextBox.Clear();
            ContacttextBox.Clear();

            FirstNametxt.Focus();

        }


        public AddNewUser()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Hide();
            MDIParent1 mdi = new MDIParent1();
            mdi.Show();
        }

        private void ADDbtn_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);

            // Insert part

            string query = "insert into Registration (Firstname,Lastname,username,[password],email,contact) values (@firstname,@lastname,@username,@password,@email,@contact)";
            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue("@firstname", FirstNametxt.Text);

            cmd.Parameters.AddWithValue("@lastname", lastnametextBox.Text);
            cmd.Parameters.AddWithValue("@username", Username1textBox.Text);
            cmd.Parameters.AddWithValue("@password", Password1textBox.Text);
            cmd.Parameters.AddWithValue("@email", emailtextBox.Text);
            cmd.Parameters.AddWithValue("@contact", ContacttextBox.Text);

            con.Open();

            if (FirstNametxt.Text != "" && lastnametextBox.Text != "" && Username1textBox.Text != "" && Password1textBox.Text != "" && emailtextBox.Text != "" && ContacttextBox.Text != "")
            {



                try
                {
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {

                        MessageBox.Show("Inserted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                    else
                    {
                        MessageBox.Show("Please fill all fields in correct manner", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Please fill up all field");
                }




                con.Close();
                
            }
            else
            {
                MessageBox.Show("Every information shoulld be fill !!", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
            ClearFunction();

        }

        private void AddNewUser_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();

            DataGridviewFunction();


        }

        private void REset1btn_Click(object sender, EventArgs e)
        {
            if (FirstNametxt.Text == "" && lastnametextBox.Text == "" && Username1textBox.Text == "" && Password1textBox.Text == "" && emailtextBox.Text == "" && ContacttextBox.Text == "")
            {
                MessageBox.Show("There is nothing to reset", "Failed");
            }
            else
            {
                ClearFunction();


            }

        }
   

        private void Viewbutton_Click(object sender, EventArgs e)
        {
            DataGridviewFunction();
        }

        private void Updatebutton_Click(object sender, EventArgs e)
        {
            // working on update 
            SqlConnection con = new SqlConnection(cs);
            string query = "Update Registration set Firstname=@firstname, Lastname=@lastname,username=@username, password=@password, email=@email, contact=@contact where Id=@id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", IDnumericUpDown.Value);
            cmd.Parameters.AddWithValue("@firstname", FirstNametxt.Text);

            cmd.Parameters.AddWithValue("@lastname", lastnametextBox.Text);
            cmd.Parameters.AddWithValue("@username", Username1textBox.Text);
            cmd.Parameters.AddWithValue("@password", Password1textBox.Text);
            cmd.Parameters.AddWithValue("@email", emailtextBox.Text);
            cmd.Parameters.AddWithValue("contact", ContacttextBox.Text);

            con.Open();


            if (IDnumericUpDown.Value != 0)
            {

                try
                {
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {

                        MessageBox.Show("Update successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                    else
                    {
                        MessageBox.Show("Update is fail", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Please fill up all field");
                }
            }
            else
            {
                MessageBox.Show("Please provide Id to update user information");
            }




            con.Close();
            ClearFunction();
        }

        private void Deletebutton_Click(object sender, EventArgs e)
        {
            if (IDnumericUpDown.Value != 0)
            {


                SqlConnection con = new SqlConnection(cs);
                string query = "delete from Registration where Id=@id and Firstname=@firstname";
                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@id", IDnumericUpDown.Value);
                cmd.Parameters.AddWithValue("@firstname", FirstNametxt.Text);


                con.Open();



                try
                {
                    int a = cmd.ExecuteNonQuery();
                    if (a > 0)
                    {

                        MessageBox.Show("Deleted successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    }

                    else
                    {
                        MessageBox.Show("Deletion  is fail", "Failed", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                    }
                }
                catch (Exception)
                {

                    MessageBox.Show("Please fill up all field");
                }
               
                con.Close();
            }
            else
            {
                MessageBox.Show("Please provide id number to delete user !!");
            }
            ClearFunction();





        }

        private void Searchbutton_Click(object sender, EventArgs e)
        {
            if (SearchtextBox.Text != "")
            {
                // working on search
                SqlConnection con = new SqlConnection(cs);
                string query = "select*from Registration where Firstname like @Firstname + '%'";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.Parameters.AddWithValue("@Firstname", SearchtextBox.Text.Trim());

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
                ClearFunction();



            }

            else
            {

                MessageBox.Show("Please enter some value to search !!");

            }

        }

        private void SearchtextBox_TextChanged(object sender, EventArgs e)
        {
            if (SearchtextBox.Text != "")
            {
                // auto generated in text bar search
                SqlConnection con = new SqlConnection(cs);
                string query = "select*from Registration where Firstname like @firstname + '%'";
                SqlDataAdapter sda = new SqlDataAdapter(query, con);
                sda.SelectCommand.Parameters.AddWithValue("@firstname", SearchtextBox.Text.Trim());

                DataTable data = new DataTable();
                sda.Fill(data);

                if (data.Rows.Count > 0)
                {
                    dataGridView1.DataSource = data;

                }
                else
                {

                    dataGridView1.DataSource = null;
                }


            }
            else
            {
                SearchtextBox.Focus();
            }



        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                IDnumericUpDown.Value =Convert.ToInt32( dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                FirstNametxt.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();

                lastnametextBox.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                Username1textBox.Text =dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                Password1textBox.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                emailtextBox.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                ContacttextBox.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            }
            catch (Exception)
            {

                MessageBox.Show("Please select in id row");
            }

         
        }
    }
}

