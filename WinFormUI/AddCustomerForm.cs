using BankLibrary;
using BankLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormUI
{
    public partial class AddCustomerForm : Form
    {
        public AddCustomerForm()
        {
            InitializeComponent();
        }

        private void btnSaveCustomer_Click(object sender, EventArgs e)
        {
            Customer customer = new Customer
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text
            };
            DataAccess.SaveCustomer(customer);
            this.Close();
        }
    }
}
