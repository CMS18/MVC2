using System;
using System.Collections.Generic;

namespace BankAppModel
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }
        public DateTime? Date { get; set; }
        public string Type { get; set; }
        public string Operation { get; set; }
        public decimal Amount { get; set; }
        public decimal? Balance { get; set; }
        public string Symbol { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }

        public virtual Account AccountNavigation { get; set; }
    }
}
