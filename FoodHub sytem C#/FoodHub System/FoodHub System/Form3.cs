using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FoodHub_System
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private void btnCusInf_Click(object sender, EventArgs e)
        {
            FormCustomer customerForm = new FormCustomer();
            customerForm.Show();
        }

        private void txtRInfo_Click(object sender, EventArgs e)
        {
            FormRider riderForm = new FormRider();
            riderForm.Show();
        }

        private void btnOrdInf_Click(object sender, EventArgs e)
        {
            FormOrder orderForm = new FormOrder();
            orderForm.Show();
        }

        private void btnIteInf_Click(object sender, EventArgs e)
        {
            FormItem itemForm = new FormItem();
            itemForm.Show();
        }

        private void btnIngInf_Click(object sender, EventArgs e)
        {
            FormIngredient ingredientForm = new FormIngredient();
            ingredientForm.Show();
        }

        private void btnBikInf_Click(object sender, EventArgs e)
        {
            FormBike bikeForm = new FormBike();
            bikeForm.Show();
        }

        private void btnDepInf_Click(object sender, EventArgs e)
        {
            FormDependent dependentForm = new FormDependent();
            dependentForm.Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult resEX = MessageBox.Show("Are you Sure, You want to Exit?", "Confirm to Exit!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (resEX == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
