using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Models
{
    public partial class Customer
    {
        public string FullName 
        { 
            get { return $"{ FirstName } { LastName }"; } 
        }

        public override string ToString()
        {
            return $"{CustomerID}. {FirstName} {LastName}";
        }

        public bool HasAccounts()
        {
            return this.Accounts.Any();
        }

        public List<Account> GetAccounts()
        {
            return this.Accounts.ToList();
        }
    }
}
