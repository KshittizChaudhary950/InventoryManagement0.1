﻿using System;
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


        public AddNewUser()
        {
            InitializeComponent();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login f1 = new Login();
            f1.Show();
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
            cmd.Parameters.AddWithValue("@email",emailtextBox.Text);
            cmd.Parameters.AddWithValue("@contact", ContacttextBox.Text);

            con.Open();



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

        private void AddNewUser_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
            con.Open();
        }
    }
}