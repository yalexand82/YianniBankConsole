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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            WireUpForm();
        }

        private void WireUpForm()
        {
            List<Customer> customers = DataAccess.GetCustomers();

            //var customerBindingSource = new BindingSource();
            //customerBindingSource.DataSource = customers;
            //cboCustomer.DataSource = customerBindingSource.DataSource;
            cboCustomer.DataSource = customers;
            cboCustomer.DisplayMember = "FullName";
            cboCustomer.ValueMember = "CustomerID";
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateCustomerAccounts((Customer)cboCustomer.SelectedItem);
        }

        private void cboAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            Account selectedAccount = (Account)cboAccount.SelectedItem;
            if (selectedAccount != null)
            {
                lblAccountBalance.Text = string.Format("{0:C2}", selectedAccount.Balance);
                lstTransactions.DataSource = selectedAccount.ListTransactions();
            }
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"Make a deposit of { string.Format("{0:C2}", nudAmount.Value) }: {txtMemo.Text}");
            Account selectedAccount = (Account)cboAccount.SelectedItem;
            Transaction transaction = new Transaction
            {
                TransactionType = "Deposit",
                Description = txtMemo.Text,
                Amount = nudAmount.Value,
                TransactionDate = DateTime.Now,
                AccountID = selectedAccount.AccountID
            };
            DataAccess.SaveTransaction(transaction);
            selectedAccount.Balance += transaction.Amount;
            UpdateAccounts(selectedAccount);
        }

        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            //MessageBox.Show($"Make a withdrawal of { string.Format("{0:C2}", nudAmount.Value) }: {txtMemo.Text}");
            Account selectedAccount = (Account)cboAccount.SelectedItem;
            Transaction transaction = new Transaction
            {
                TransactionType = "Withdrawal",
                Description = txtMemo.Text,
                Amount = nudAmount.Value,
                TransactionDate = DateTime.Now,
                AccountID = selectedAccount.AccountID
            };
            DataAccess.SaveTransaction(transaction);
            selectedAccount.Balance -= transaction.Amount;
            UpdateAccounts(selectedAccount);
        }

        public void UpdateCustomers()
        {
            cboCustomer.DataSource = DataAccess.GetCustomers();
            cboCustomer.DisplayMember = "FullName";
            cboCustomer.ValueMember = "CustomerID";

            Customer customer = DataAccess.GetCustomers().OrderByDescending(c => c.CustomerID).FirstOrDefault();
            cboCustomer.SelectedIndex = cboCustomer.FindString(customer.FullName);
        }

        public void ZeroAmounts()
        {
            cboAccount.DataSource = null;
            lblAccountBalance.Text = string.Format("{0:C2}", 0);
            lstTransactions.DataSource = null;
        }

        public void UpdateAccounts(Account account)
        {
            DataAccess.SaveChanges();
            lblAccountBalance.Text = string.Format("{0:C2}", account.Balance);
            lstTransactions.DataSource = account.ListTransactions();
            nudAmount.Value = 0;
            txtMemo.Text = "";
        }

        public Customer GetCustomer()
        {
            return (Customer)cboCustomer.SelectedItem;
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            AddCustomerForm addCustomerForm = new AddCustomerForm();
            addCustomerForm.ShowDialog(this);
            UpdateCustomers();
            //DataAccess.SaveChanges();
            //cboCustomer.DataSource = DataAccess.GetCustomers();
            //cboCustomer.DisplayMember = "FullName";
            //cboCustomer.ValueMember = "CustomerID";

            //Customer customer = DataAccess.GetCustomers().OrderByDescending(c => c.CustomerID).FirstOrDefault();
            //cboCustomer.SelectedIndex = cboCustomer.FindString(customer.FullName);
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            AddAccountForm addAccountForm = new AddAccountForm();
            addAccountForm.ShowDialog(this);

            Customer selectedCustomer = (Customer)cboCustomer.SelectedItem;
            PopulateCustomerAccounts(selectedCustomer);
        }

        private void btnDelCustomer_Click(object sender, EventArgs e)
        {
            Customer selectedCustomer = (Customer)cboCustomer.SelectedItem;

            // get all customer accounts
            if (selectedCustomer.HasAccounts())
            {
                List<Account> accounts = DataAccess.GetCustomerAccounts(selectedCustomer);
                foreach (Account account in accounts)
                {   
                    if (account.HasTransactions())
                    {
                        // delete all account transactions
                        DataAccess.DeleteAccountTransactions(account);
                    }
                }
                // delete all customer accounts
                DataAccess.DeleteCustomerAccounts(selectedCustomer);
            }
            DataAccess.DeleteCustomer(selectedCustomer);

            cboCustomer.DataSource = DataAccess.GetCustomers();
            cboCustomer.DisplayMember = "FullName";
            cboCustomer.ValueMember = "CustomerID";

            Customer customer = DataAccess.GetCustomers().Where(c => c.CustomerID < selectedCustomer.CustomerID).OrderByDescending(c => c.CustomerID).FirstOrDefault();
            cboCustomer.SelectedIndex = cboCustomer.FindString(customer.FullName);
        }

        private void btnDelAccount_Click(object sender, EventArgs e)
        {
            Account selectedAccount = (Account)cboAccount.SelectedItem;

            // get all account transactions
            if (selectedAccount.HasTransactions())
            {
                // delete all account transactions
                DataAccess.DeleteAccountTransactions(selectedAccount);
            }
            DataAccess.DeleteAccount(selectedAccount);

            PopulateCustomerAccounts((Customer)cboCustomer.SelectedItem);
        }

        private void PopulateCustomerAccounts(Customer selectedCustomer)
        {
            if (selectedCustomer.HasAccounts())
            {
                List<Account> accounts = DataAccess.GetCustomerAccounts(selectedCustomer);
                cboAccount.DataSource = accounts;
                cboAccount.DisplayMember = "AccountName";
                cboAccount.ValueMember = "AccountID";

                Account account = accounts.OrderBy(a => a.AccountID).FirstOrDefault();
                cboAccount.SelectedIndex = cboAccount.FindString(account.AccountName);
                lblAccountBalance.Text = string.Format("{0:C2}", account.Balance);
            }
            else
            {
                ZeroAmounts();
            }
        }

        /*Customer selectedCustomer = (Customer)cboCustomer.SelectedItem;
            if (selectedCustomer.HasAccounts())
            {
                List<Account> accounts = DataAccess.GetCustomerAccounts(selectedCustomer);
                cboAccount.DataSource = accounts;
                cboAccount.DisplayMember = "AccountName";
                cboAccount.ValueMember = "AccountID";

                Account selectedAccount = (Account)cboAccount.SelectedItem;
                lblAccountBalance.Text = string.Format("{0:C2}", selectedAccount.Balance);
            }
            else
            {
                ZeroAmounts();
            }*/
    }
}
