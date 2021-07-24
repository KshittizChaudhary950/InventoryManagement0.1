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
           // ClearFunction();

        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Hide();
            MDIParent1 mdi = new MDIParent1();
            mdi.Show();
        }
    }
}
