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
    public partial class FormBike : Form
    {
        private string connectionString = "Data Source=PASINDU-ISURANG;Initial Catalog=FoodHub_Delivery_System;Integrated Security=True";
        public FormBike()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Close the application
            }
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            FormMenu menuForm = new FormMenu();
            menuForm.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtBikeID.Clear();
            txtRegistrationNumber.Clear();
            txtBrand.Clear();
            txtModel.Clear();
            txtEngineNumber.Clear();
            txtColors.Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string bikeID = txtBikeID.Text;
            string registrationNumber = txtRegistrationNumber.Text;
            string brand = txtBrand.Text;
            string model = txtModel.Text;
            string engineNumber = txtEngineNumber.Text;
            string colors = txtColors.Text;

            if (string.IsNullOrEmpty(bikeID) || string.IsNullOrEmpty(registrationNumber) || string.IsNullOrEmpty(brand) ||
                string.IsNullOrEmpty(model) || string.IsNullOrEmpty(engineNumber) || string.IsNullOrEmpty(colors))
            {
                MessageBox.Show("Please fill in all fields.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "INSERT INTO BikeTable (Bike_ID, Registration_Number, Brand, Model, Engine_Number, Colors) " +
                           "VALUES (@Bike_ID, @Registration_Number, @Brand, @Model, @Engine_Number, @Colors)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Bike_ID", bikeID);
                command.Parameters.AddWithValue("@Registration_Number", registrationNumber);
                command.Parameters.AddWithValue("@Brand", brand);
                command.Parameters.AddWithValue("@Model", model);
                command.Parameters.AddWithValue("@Engine_Number", engineNumber);
                command.Parameters.AddWithValue("@Colors", colors);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Bike added successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void bntUpdate_Click(object sender, EventArgs e)
        {
            string bikeID = txtBikeID.Text;
            string registrationNumber = txtRegistrationNumber.Text;
            string brand = txtBrand.Text;
            string model = txtModel.Text;
            string engineNumber = txtEngineNumber.Text;
            string colors = txtColors.Text;

            if (string.IsNullOrEmpty(bikeID) || string.IsNullOrEmpty(registrationNumber) || string.IsNullOrEmpty(brand) ||
                string.IsNullOrEmpty(model) || string.IsNullOrEmpty(engineNumber) || string.IsNullOrEmpty(colors))
            {
                MessageBox.Show("Please fill in all fields.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "UPDATE BikeTable SET Registration_Number = @Registration_Number, Brand = @Brand, " +
                           "Model = @Model, Engine_Number = @Engine_Number, Colors = @Colors WHERE Bike_ID = @Bike_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Bike_ID", bikeID);
                command.Parameters.AddWithValue("@Registration_Number", registrationNumber);
                command.Parameters.AddWithValue("@Brand", brand);
                command.Parameters.AddWithValue("@Model", model);
                command.Parameters.AddWithValue("@Engine_Number", engineNumber);
                command.Parameters.AddWithValue("@Colors", colors);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Bike updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string bikeID = txtBikeID.Text;

            if (string.IsNullOrEmpty(bikeID))
            {
                MessageBox.Show("Please enter a Bike ID to search.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT * FROM BikeTable WHERE Bike_ID = @Bike_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Bike_ID", bikeID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtRegistrationNumber.Text = reader["Registration_Number"].ToString();
                        txtBrand.Text = reader["Brand"].ToString();
                        txtModel.Text = reader["Model"].ToString();
                        txtEngineNumber.Text = reader["Engine_Number"].ToString();
                        txtColors.Text = reader["Colors"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Bike not found!");
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
