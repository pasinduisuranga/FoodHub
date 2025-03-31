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
    public partial class FormDependent : Form
    {
        private string connectionString = "Data Source=PASINDU-ISURANG;Initial Catalog=FoodHub_Delivery_System;Integrated Security=True";
        public FormDependent()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtDependentID.Clear();
            txtDependentName.Clear();
            txtRelationship.Clear();
            dtpDOB.Value = DateTime.Now;
            txtRiderID.Clear();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            FormMenu menuForm = new FormMenu();
            menuForm.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            {
                string dependentID = txtDependentID.Text;
                string dependentName = txtDependentName.Text;
                string relationship = txtRelationship.Text;
                string dob = dtpDOB.Value.ToString("yyyy-MM-dd");
                string riderID = txtRiderID.Text;

                if (string.IsNullOrEmpty(dependentID) || string.IsNullOrEmpty(dependentName) || string.IsNullOrEmpty(relationship) ||
                    string.IsNullOrEmpty(riderID))
                {
                    MessageBox.Show("Please fill in all fields.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "INSERT INTO DependentTable (Dependent_ID, Dependent_Name, Relationship, DOB, Rider_ID) " +
                               "VALUES (@Dependent_ID, @Dependent_Name, @Relationship, @DOB, @Rider_ID)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Dependent_ID", dependentID);
                    command.Parameters.AddWithValue("@Dependent_Name", dependentName);
                    command.Parameters.AddWithValue("@Relationship", relationship);
                    command.Parameters.AddWithValue("@DOB", dob);
                    command.Parameters.AddWithValue("@Rider_ID", riderID);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Dependent added successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
        }

        private void bntUpdate_Click(object sender, EventArgs e)
        {
            string dependentID = txtDependentID.Text;
            string dependentName = txtDependentName.Text;
            string relationship = txtRelationship.Text;
            string dob = dtpDOB.Value.ToString("yyyy-MM-dd");
            string riderID = txtRiderID.Text;

            if (string.IsNullOrEmpty(dependentID) || string.IsNullOrEmpty(dependentName) || string.IsNullOrEmpty(relationship) ||
                string.IsNullOrEmpty(riderID))
            {
                MessageBox.Show("Please fill in all fields.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "UPDATE DependentTable SET Dependent_Name = @Dependent_Name, Relationship = @Relationship, " +
                           "DOB = @DOB, Rider_ID = @Rider_ID WHERE Dependent_ID = @Dependent_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Dependent_ID", dependentID);
                command.Parameters.AddWithValue("@Dependent_Name", dependentName);
                command.Parameters.AddWithValue("@Relationship", relationship);
                command.Parameters.AddWithValue("@DOB", dob);
                command.Parameters.AddWithValue("@Rider_ID", riderID);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Dependent updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string dependentID = txtDependentID.Text;

            if (string.IsNullOrEmpty(dependentID))
            {
                MessageBox.Show("Please enter a Dependent ID to search.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT * FROM DependentTable WHERE Dependent_ID = @Dependent_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Dependent_ID", dependentID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtDependentName.Text = reader["Dependent_Name"].ToString();
                        txtRelationship.Text = reader["Relationship"].ToString();
                        dtpDOB.Value = Convert.ToDateTime(reader["DOB"]);
                        txtRiderID.Text = reader["Rider_ID"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Dependent not found!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
