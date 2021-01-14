using BankLibrary;
using BankLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YianniBankConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            YianniBankEntities db = new YianniBankEntities();
            //List<Customer> customers = db.Customers.ToList();
            List<Customer> customers = DataAccess.GetCustomers();
            Customer customer = DataAccess.GetCustomer(1);
            Console.WriteLine("Please select a customer:");
            customers.ForEach(Console.WriteLine);
            string input = Console.ReadLine();
            int id = Int32.Parse(input);

            //customer = customers.SingleOrDefault(c => c.CustomerID == id);
            customer = DataAccess.GetCustomer(id);

            Console.WriteLine($"You selected customer {customer.ToString()}");

            //Account checkingAccount = new Account
            //{
            //    AccountNum = "0000000003",
            //    AccountType = "Checking",
            //    AccountName = $" {customer.FirstName} Checking Account",
            //    Balance = 0.00m,
            //    CustomerID = customer.CustomerID
            //};
            //Account savingsAccount = new Account
            //{
            //    AccountNum = "0000000004",
            //    AccountType = "Savings",
            //    AccountName = $" {customer.FirstName} Savings Account",
            //    Balance = 0.00m,
            //    CustomerID = customer.CustomerID
            //};
            //db.Accounts.Add(checkingAccount);
            //db.Accounts.Add(savingsAccount);

            Console.WriteLine("Please select an account:");
            //foreach (Account account in customer.Accounts)
            foreach (Account account in DataAccess.GetCustomerAccounts(customer))
                Console.WriteLine(account.ToString());
            id = Int32.Parse(Console.ReadLine());
            //PrintAccountDetails(customer.Accounts.SingleOrDefault(a => a.AccountID == id));
            PrintAccountDetails(DataAccess.GetAccount(id));




            //List<Account> accounts = db.Accounts.ToList();
            //List<Account> yianniAccounts = accounts.Where(c => c.CustomerID == customer.CustomerID).ToList();
            //foreach (Account account in yianniAccounts)
            //    Console.WriteLine(account.AccountName);

            //db.Customers.Add(new Customer { FirstName = "John", LastName = "Smith" });
            //db.SaveChanges();

            Console.ReadKey();
        }

        static void PrintAccountDetails(Account account)
        {
            Console.WriteLine($"{account.AccountID} {account.AccountNum} {account.AccountType} {account.AccountName} {account.Balance}");
        }
    }
}
