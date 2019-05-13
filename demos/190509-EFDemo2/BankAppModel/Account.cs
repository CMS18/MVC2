using System;
using System.Collections.Generic;

namespace BankAppModel
{
    public partial class Account
    {
        public Account()
        {
            Dispositions = new HashSet<Disposition>();
            Transactions = new HashSet<Transaction>();
        }

        public int AccountId { get; set; }
        public string Frequency { get; set; }
        public DateTime Created { get; set; }
        public decimal? Amount { get; set; }

        public virtual ICollection<Disposition> Dispositions { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
