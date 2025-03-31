using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace FoodHub_System
{
    public partial class FormRider : Form
    {
        private string connectionString = "Data Source=PASINDU-ISURANG;Initial Catalog=FoodHub_Delivery_System;Integrated Security=True";

        public FormRider()
        {
            InitializeComponent();
        }

        private int CalculateAge(DateTime birthDate)
        {
            int age = DateTime.Now.Year - birthDate.Year;
            if (DateTime.Now.DayOfYear < birthDate.DayOfYear)
            {
                age--;
            }
            return age;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            FormMenu menuForm = new FormMenu();
            menuForm.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string riderID = txtRID.Text;
            string firstName = txtFirstName.Text;
            string middleName = txtMiddleName.Text;
            string lastName = txtLastName.Text;
            string contactNumber = txtContactNumber.Text;
            string licenseNumber = txtLicenseNumber.Text;
            string address = txtAddress.Text;
            string dob = dtpDOB.Value.ToString("yyyy-MM-dd");
            string nic = txtNIC.Text;
            int age = CalculateAge(dtpDOB.Value); 

            string query = "INSERT INTO RiderTable (Rider_id, Fir_name, Mid_name, Las_name, Con_num, Lic_no, R_Address, BOF, NIC, Age) " +
                           "VALUES (@Rider_id, @Fir_name, @Mid_name, @Las_name, @Con_num, @Lic_no, @R_Address, @BOF, @NIC, @Age)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Rider_id", riderID);
                command.Parameters.AddWithValue("@Fir_name", firstName);
                command.Parameters.AddWithValue("@Mid_name", middleName);
                command.Parameters.AddWithValue("@Las_name", lastName);
                command.Parameters.AddWithValue("@Con_num", contactNumber);
                command.Parameters.AddWithValue("@Lic_no", licenseNumber);
                command.Parameters.AddWithValue("@R_Address", address);
                command.Parameters.AddWithValue("@BOF", dob);
                command.Parameters.AddWithValue("@NIC", nic);
                command.Parameters.AddWithValue("@Age", age);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Rider added successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void lblAge_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string riderID = txtRID.Text;
            string firstName = txtFirstName.Text;
            string middleName = txtMiddleName.Text;
            string lastName = txtLastName.Text;
            string contactNumber = txtContactNumber.Text;
            string licenseNumber = txtLicenseNumber.Text;
            string address = txtAddress.Text;
            string dob = dtpDOB.Value.ToString("yyyy-MM-dd");
            string nic = txtNIC.Text;
            int age = Convert.ToInt32(lblAge.Text);


            string query = "UPDATE RiderTable SET Fir_name = @Fir_name, Mid_name = @Mid_name, Las_name = @Las_name, " +
                   "Con_num = @Con_num, Lic_no = @Lic_no, R_Address = @R_Address, BOF = @BOF, NIC = @NIC, Age = @Age " +
                   "WHERE Rider_id = @Rider_id";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Rider_id", riderID);
                command.Parameters.AddWithValue("@Fir_name", firstName);
                command.Parameters.AddWithValue("@Mid_name", middleName);
                command.Parameters.AddWithValue("@Las_name", lastName);
                command.Parameters.AddWithValue("@Con_num", contactNumber);
                command.Parameters.AddWithValue("@Lic_no", licenseNumber);
                command.Parameters.AddWithValue("@R_Address", address);
                command.Parameters.AddWithValue("@BOF", dob);
                command.Parameters.AddWithValue("@NIC", nic);
                command.Parameters.AddWithValue("@Age", age);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                    MessageBox.Show("Rider updated successfully!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            string riderID = txtRID.Text;

            string query = "SELECT * FROM RiderTable WHERE Rider_id = @Rider_id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Rider_id", riderID);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        txtFirstName.Text = reader["Fir_name"].ToString();
                        txtMiddleName.Text = reader["Mid_name"].ToString();
                        txtLastName.Text = reader["Las_name"].ToString();
                        txtContactNumber.Text = reader["Con_num"].ToString();
                        txtLicenseNumber.Text = reader["Lic_no"].ToString();
                        txtAddress.Text = reader["R_Address"].ToString();
                        dtpDOB.Value = Convert.ToDateTime(reader["BOF"]);
                        txtNIC.Text = reader["NIC"].ToString();
                        lblAge.Text = reader["Age"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Rider not found!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            txtRID.Clear();
            txtFirstName.Clear();
            txtMiddleName.Clear();
            txtLastName.Clear();
            txtContactNumber.Clear();
            txtLicenseNumber.Clear();
            txtAddress.Clear();
            txtNIC.Clear();
            dtpDOB.Value = DateTime.Now;
        }
    }
}
