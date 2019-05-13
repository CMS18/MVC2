using System;
using System.Collections.Generic;

namespace BankAppModel
{
    public partial class Disposition
    {
        public int DispositionId { get; set; }
        public int CustomerId { get; set; }
        public int AccountId { get; set; }
        public string Type { get; set; }

        public virtual Account Account { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
