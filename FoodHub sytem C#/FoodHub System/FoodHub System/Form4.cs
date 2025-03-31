using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FoodHub_System
{
    public partial class FormOrder : Form
    {
        private string connectionString = "Data Source=PASINDU-ISURANG;Initial Catalog=FoodHub_Delivery_System;Integrated Security=True";

        public FormOrder()
        {
            InitializeComponent();
        }

        
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtOrderID.Clear();
            dtpOrderTime.Value = DateTime.Now;
            dtpOrderDate.Value = DateTime.Now;
            txtPaymentMethod.Clear();
            txtOrderStatus.Clear();
            txtTotalAmount.Clear();
            dtpDispatchTime.Value = DateTime.Now;
        }

        
        private void btnMenu_Click(object sender, EventArgs e)
        {
            FormMenu menuForm = new FormMenu();
            menuForm.Show();
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
            string orderID = txtOrderID.Text;
            string orderTime = dtpOrderTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string orderDate = dtpOrderDate.Value.ToString("yyyy-MM-dd");
            string paymentMethod = txtPaymentMethod.Text;
            string orderStatus = txtOrderStatus.Text;
            decimal totalAmount = Convert.ToDecimal(txtTotalAmount.Text);
            string dispatchTime = dtpDispatchTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

            string query = "INSERT INTO OrdersTable (Order_ID, Order_Time, Order_Date, Payment_Method, Order_Status, Total_Amount, Dispatch_Time) " +
                           "VALUES (@Order_ID, @Order_Time, @Order_Date, @Payment_Method, @Order_Status, @Total_Amount, @Dispatch_Time)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Order_ID", orderID);
                command.Parameters.AddWithValue("@Order_Time", orderTime);
                command.Parameters.AddWithValue("@Order_Date", orderDate);
                command.Parameters.AddWithValue("@Payment_Method", paymentMethod);
                command.Parameters.AddWithValue("@Order_Status", orderStatus);
                command.Parameters.AddWithValue("@Total_Amount", totalAmount);
                command.Parameters.AddWithValue("@Dispatch_Time", dispatchTime);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Order added successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string orderID = txtOrderID.Text;
            string orderTime = dtpOrderTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            string orderDate = dtpOrderDate.Value.ToString("yyyy-MM-dd");
            string paymentMethod = txtPaymentMethod.Text;
            string orderStatus = txtOrderStatus.Text;
            decimal totalAmount = Convert.ToDecimal(txtTotalAmount.Text);
            string dispatchTime = dtpDispatchTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

            string query = "UPDATE OrdersTable SET Order_Time = @Order_Time, Order_Date = @Order_Date, Payment_Method = @Payment_Method, " +
                           "Order_Status = @Order_Status, Total_Amount = @Total_Amount, Dispatch_Time = @Dispatch_Time " +
                           "WHERE Order_ID = @Order_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Order_ID", orderID);
                command.Parameters.AddWithValue("@Order_Time", orderTime);
                command.Parameters.AddWithValue("@Order_Date", orderDate);
                command.Parameters.AddWithValue("@Payment_Method", paymentMethod);
                command.Parameters.AddWithValue("@Order_Status", orderStatus);
                command.Parameters.AddWithValue("@Total_Amount", totalAmount);
                command.Parameters.AddWithValue("@Dispatch_Time", dispatchTime);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Order updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        
        private void btnView_Click(object sender, EventArgs e)
        {
            string orderID = txtOrderID.Text;

            string query = "SELECT * FROM OrdersTable WHERE Order_ID = @Order_ID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Order_ID", orderID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        dtpOrderTime.Value = Convert.ToDateTime(reader["Order_Time"]);
                        dtpOrderDate.Value = Convert.ToDateTime(reader["Order_Date"]);
                        txtPaymentMethod.Text = reader["Payment_Method"].ToString();
                        txtOrderStatus.Text = reader["Order_Status"].ToString();
                        txtTotalAmount.Text = reader["Total_Amount"].ToString();
                        dtpDispatchTime.Value = Convert.ToDateTime(reader["Dispatch_Time"]);
                    }
                    else
                    {
                        MessageBox.Show("Order not found!");
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
