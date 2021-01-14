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
    public partial class AddAccountForm : Form
    {
        public AddAccountForm()
        {
            InitializeComponent();
            cboAccountType.Items.Add("Checking");
            cboAccountType.Items.Add("Savings");
            cboAccountType.SelectedIndex = 0;
        }

        private void btnSaveAccount_Click(object sender, EventArgs e)
        {
            Dashboard parent = (Dashboard)this.Owner;
            Customer customer = parent.GetCustomer();

            string maxAccountNum = DataAccess.GetAccounts().Select(account => account.AccountNum).Max();
            int len = maxAccountNum.Length;
            int maxAccountNumInt = Convert.ToInt32(maxAccountNum);
            string newAccountNum = (maxAccountNumInt + 1).ToString("D" + len);

            if (!txtAccountName.Text.Equals(String.Empty))
            {
                DataAccess.SaveAccount(newAccountNum, cboAccountType.Text, txtAccountName.Text, 0.00m, customer.CustomerID);
            }
            else
            {
                MessageBox.Show("Please enter an Account Name.", "Error");
            }

            //MessageBox.Show(maxAccountNum);
            //MessageBox.Show(newAccountNum);
            this.Close();
        }
    }
}
