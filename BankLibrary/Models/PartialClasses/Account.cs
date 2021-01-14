using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Models
{
    public partial class Account
    {
        public override string ToString()
        {
            return $"{AccountID}: {AccountName}";
        }

        public List<string> ListTransactions()
        {
            List<string> output = new List<string>();
            foreach (Transaction transaction in Transactions)
                output.Add(transaction.ToString());
            return output;
        }

        public bool HasTransactions()
        {
            return this.Transactions.Any();
        }

        public List<Transaction> GetTransactions()
        {
            return this.Transactions.ToList();
        }
    }
}
