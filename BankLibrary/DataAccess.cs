using BankLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary
{
    public static class DataAccess
    {
        private static YianniBankEntities db = new YianniBankEntities();

        public static void SaveChanges()
        {
            db.SaveChanges();
        }

        // CUSTOMERS
        public static List<Customer> GetCustomers()
        {
            return db.Customers.ToList();
        }

        public static Customer GetCustomer(int id)
        {
            return db.Customers.SingleOrDefault(c => c.CustomerID == id);
        }

        public static void SaveCustomer(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public static void SaveCustomer(string firstName, string lastName)
        {
            db.Customers.Add(new Customer { FirstName = firstName, LastName = lastName});
            db.SaveChanges();
        }

        public static void DeleteCustomer(int id)
        {
            db.Customers.Remove(GetCustomer(id));
            db.SaveChanges();
        }

        public static void DeleteCustomer(Customer customer)
        {
            db.Customers.Remove(customer);
            db.SaveChanges();
        }

        // ACCOUNTS
        public static List<Account> GetAccounts()
        {
            return db.Accounts.ToList();
        }

        public static List<Account> GetCustomerAccounts(Customer customer)
        {
            return db.Accounts.Where(a => a.CustomerID == customer.CustomerID).ToList();
        }

        public static Account GetAccount(int id)
        {
            return db.Accounts.SingleOrDefault(a => a.AccountID == id);
        }

        public static void SaveAccount(Account account)
        {
            db.Accounts.Add(account);
            db.SaveChanges();
        }

        public static void SaveAccount(string accountNum, string accountType, string accountName, decimal balance, int customerId)
        {
            db.Accounts.Add(new Account { 
                AccountNum = accountNum, 
                AccountType = accountType, 
                AccountName = accountName, 
                Balance = balance, 
                CustomerID = customerId
            });
            db.SaveChanges();
        }

        public static void DeleteCustomerAccounts(Customer customer)
        {
            db.Accounts.RemoveRange(customer.GetAccounts());
            db.SaveChanges();
        }

        public static void DeleteAccount(int id)
        {
            db.Accounts.Remove(GetAccount(id));
            db.SaveChanges();
        }

        public static void DeleteAccount(Account account)
        {
            db.Accounts.Remove(account);
            db.SaveChanges();
        }

        // TRANSACTIONS
        public static List<Transaction> GetTransactions()
        {
            return db.Transactions.ToList();
        }

        public static List<Transaction> GetAccountTransactions(Account account)
        {
            return db.Transactions.Where(t => t.AccountID == account.AccountID).ToList();
        }

        public static Transaction GetTransaction(int id)
        {
            return db.Transactions.SingleOrDefault(t => t.TransactionID == id);
        }

        public static void SaveTransaction(Transaction transaction)
        {
            db.Transactions.Add(transaction);
            db.SaveChanges();
        }

        public static void SaveTransaction(string transactionType, string description, decimal amount, DateTime transactionDate, int accountId)
        {
            db.Transactions.Add(new Transaction {
                TransactionType = transactionType,
                Description = description,
                Amount = amount,
                TransactionDate = transactionDate,
                AccountID = accountId
            });
            db.SaveChanges();
        }

        public static void DeleteAccountTransactions(Account account)
        {
            db.Transactions.RemoveRange(account.GetTransactions());
            db.SaveChanges();
        }
    }
}
