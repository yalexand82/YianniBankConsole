using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLibrary.Models
{
    public partial class Transaction
    {
        public override string ToString()
        {
            return $"{TransactionType} { string.Format("{0:C2}", Amount) }: {Description} {TransactionDate}";
        }
    }
}
