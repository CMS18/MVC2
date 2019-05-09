using System;
using System.Collections.Generic;

namespace SamuraiModel
{
    public partial class Samurais
    {
        public Samurais()
        {
            Quotes = new HashSet<Quotes>();
            SamuraiBattle = new HashSet<SamuraiBattle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual SecretIdentity SecretIdentity { get; set; }
        public virtual ICollection<Quotes> Quotes { get; private set; }
        public virtual ICollection<SamuraiBattle> SamuraiBattle { get; private set; }
    }
}
