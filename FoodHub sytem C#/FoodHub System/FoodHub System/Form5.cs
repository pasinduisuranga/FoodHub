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
    
    public partial class FormItem : Form
    {
        private string connectionString = "Data Source=PASINDU-ISURANG;Initial Catalog=FoodHub_Delivery_System;Integrated Security=True";
        public FormItem()
        {
            InitializeComponent();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            FormMenu menuForm = new FormMenu();
            menuForm.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtItemID.Clear();
            txtItemName.Clear();
            txtPrice.Clear();
            txtCategory.Clear();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to close the application?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit(); 
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string itemID = txtItemID.Text;
            string itemName = txtItemName.Text;
            decimal price;
            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("Please enter a valid price.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string category = txtCategory.Text;

            string query = "INSERT INTO ItemTable (Item_ID, Item_Name, Price, Category) " +
                           "VALUES (@Item_ID, @Item_Name, @Price, @Category)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Item_ID", itemID);
                command.Parameters.AddWithValue("@Item_Name", itemName);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Category", category);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Item added successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void bntUpdate_Click(object sender, EventArgs e)
        {
            string itemID = txtItemID.Text;
            string itemName = txtItemName.Text;
            decimal price;
            if (!decimal.TryParse(txtPrice.Text, out price))
            {
                MessageBox.Show("Please enter a valid price.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string category = txtCategory.Text;

            string query = "UPDATE ItemTable SET Item_Name = @Item_Name, Price = @Price, Category = @Category " +
                           "WHERE Item_ID = @Item_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Item_ID", itemID);
                command.Parameters.AddWithValue("@Item_Name", itemName);
                command.Parameters.AddWithValue("@Price", price);
                command.Parameters.AddWithValue("@Category", category);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Item updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string itemID = txtItemID.Text;

            string query = "SELECT * FROM ItemTable WHERE Item_ID = @Item_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Item_ID", itemID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtItemName.Text = reader["Item_Name"].ToString();
                        txtPrice.Text = reader["Price"].ToString();
                        txtCategory.Text = reader["Category"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Item not found!");
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
