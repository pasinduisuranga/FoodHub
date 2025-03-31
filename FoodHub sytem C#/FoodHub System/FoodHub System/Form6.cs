using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FoodHub_System
{
    public partial class FormIngredient : Form
    {
        private string connectionString = "Data Source=PASINDU-ISURANG;Initial Catalog=FoodHub_Delivery_System;Integrated Security=True";

        public FormIngredient()
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

        
        private void btnMenu_Click(object sender, EventArgs e)
        {
            FormMenu menuForm = new FormMenu();
            menuForm.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string ingredientID = txtIngredientID.Text;
            string ingredientName = txtIngredientName.Text;

            if (string.IsNullOrEmpty(ingredientID) || string.IsNullOrEmpty(ingredientName))
            {
                MessageBox.Show("Please fill in all fields.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "INSERT INTO IngredientTable (Ingredient_ID, Ingredient_Name) " +
                           "VALUES (@Ingredient_ID, @Ingredient_Name)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Ingredient_ID", ingredientID);
                command.Parameters.AddWithValue("@Ingredient_Name", ingredientName);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Ingredient added successfully!");
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Database error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        
        private void btnView_Click(object sender, EventArgs e)
        {
            string ingredientID = txtIngredientID.Text;

            if (string.IsNullOrEmpty(ingredientID))
            {
                MessageBox.Show("Please enter an Ingredient ID to search.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT * FROM IngredientTable WHERE Ingredient_ID = @Ingredient_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Ingredient_ID", ingredientID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtIngredientName.Text = reader["Ingredient_Name"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Ingredient not found!");
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Database error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

       
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtIngredientID.Clear();
            txtIngredientName.Clear();
        }

        private void bntUpdate_Click(object sender, EventArgs e)
        {
            string ingredientID = txtIngredientID.Text;
            string ingredientName = txtIngredientName.Text;

            if (string.IsNullOrEmpty(ingredientID) || string.IsNullOrEmpty(ingredientName))
            {
                MessageBox.Show("Please fill in all fields.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "UPDATE IngredientTable SET Ingredient_Name = @Ingredient_Name " +
                           "WHERE Ingredient_ID = @Ingredient_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Ingredient_ID", ingredientID);
                command.Parameters.AddWithValue("@Ingredient_Name", ingredientName);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Ingredient updated successfully!");
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Database error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }
    }
}
