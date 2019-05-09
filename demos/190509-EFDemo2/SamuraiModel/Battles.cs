using System;
using System.Collections.Generic;

namespace SamuraiModel
{
    public partial class Battles
    {
        public Battles()
        {
            SamuraiBattle = new HashSet<SamuraiBattle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<SamuraiBattle> SamuraiBattle { get; private set; }
    }
}
