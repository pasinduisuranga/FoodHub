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

namespace FoodHub_System
{
    public partial class FormCustomer : Form
    {
        string connectionString = @"Data Source=PASINDU-ISURANG;Initial Catalog=FoodHub_Delivery_System;Integrated Security=True";

        public FormCustomer()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult resEX = MessageBox.Show("Are you Sure, You want to Exit?", "Confirm to Exit!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resEX == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void makanna()
        {
            txtCusID.Text = "";
            txtCusName.Text = "";
            dtpBOD.Text = "";
            txtNIC.Text = "";
            txtLocNo.Text = "";
            txtLine.Text = "";
            txtStreet.Text = "";

        }

        private void txtNIC_TextChanged(object sender, EventArgs e)
        {
            if (txtNIC.TextLength == 10 || txtNIC.TextLength == 12)
            {
                txtNIC.ForeColor = Color.Green;
            }
            else
            {
                txtNIC.ForeColor = Color.Red;
            }
        }

        private void txtCusNum_TextChanged(object sender, EventArgs e)
        {
            if (txtCusNum.TextLength < 10)
            {
                txtCusNum.ForeColor = Color.Red;
            }
            else
            {
                txtCusNum.ForeColor = Color.Green;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string CusID = txtCusID.Text;
            string cusName = txtCusName.Text;
            string cusNum = txtCusNum.Text;
            string dob = dtpBOD.Value.ToString("yyyy-MM-dd");
            string nic = txtNIC.Text;
            string locNo = txtLocNo.Text;
            string line = txtLine.Text;
            string street = txtStreet.Text;

            string query = "INSERT INTO CustomersTable (Cus_ID, Cus_name, Cus_num, BOD, NIC, Loc_no, line, street) " +
                           "VALUES (@Cus_ID, @Cus_name, @Cus_num, @BOD, @NIC, @Loc_no, @line, @street)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Cus_ID", CusID);
                command.Parameters.AddWithValue("@Cus_name", cusName);
                command.Parameters.AddWithValue("@Cus_num", cusNum);
                command.Parameters.AddWithValue("@BOD", dob);
                command.Parameters.AddWithValue("@NIC", nic);
                command.Parameters.AddWithValue("@Loc_no", locNo);
                command.Parameters.AddWithValue("@line", line);
                command.Parameters.AddWithValue("@street", street);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Customer added successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);

                }
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            makanna();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string cusID = txtCusID.Text;
            string cusName = txtCusName.Text;
            string cusNum = txtCusNum.Text;
            string dob = dtpBOD.Value.ToString("yyyy-MM-dd");
            string nic = txtNIC.Text;
            string locNo = txtLocNo.Text;
            string line = txtLine.Text;
            string street = txtStreet.Text;

            string query = "UPDATE CustomersTable SET Cus_name = @Cus_name, Cus_num = @Cus_num, BOD = @BOD, NIC = @NIC, " +
                           "Loc_no = @Loc_no, line = @line, street = @street WHERE Cus_ID = @Cus_ID";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Cus_ID", cusID);
                command.Parameters.AddWithValue("@Cus_name", cusName);
                command.Parameters.AddWithValue("@Cus_num", cusNum);
                command.Parameters.AddWithValue("@BOD", dob);
                command.Parameters.AddWithValue("@NIC", nic);
                command.Parameters.AddWithValue("@Loc_no", locNo);
                command.Parameters.AddWithValue("@line", line);
                command.Parameters.AddWithValue("@street", street);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Customer updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            String cusID = txtCusID.Text;

            String query = "SELECT * FROM CustomersTable WHERE Cus_ID = @Cus_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Cus_ID", cusID);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        txtCusName.Text = reader["Cus_name"].ToString();
                        txtCusNum.Text = reader["Cus_num"].ToString();
                        dtpBOD.Value = Convert.ToDateTime(reader["BOD"]);
                        txtNIC.Text = reader["NIC"].ToString();
                        txtLocNo.Text = reader["Loc_no"].ToString();
                        txtLine.Text = reader["line"].ToString();
                        txtStreet.Text = reader["street"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Customer not found!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            FormMenu MenuForm = new FormMenu();
            MenuForm.Show();
        }

        private void txtCusID_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
