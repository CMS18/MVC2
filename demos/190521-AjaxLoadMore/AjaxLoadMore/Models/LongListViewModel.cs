using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AjaxLoadMore.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }
    }

    public class LongListViewModel
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalNumberOfItems { get; set; }

        public bool CanShowMore { get; set; }

        public IList<Transaction> Transactions { get; set; }

    }
}
