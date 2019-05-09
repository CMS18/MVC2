using System;
using System.Collections.Generic;
using System.Text;

namespace SamuraiApp.Domain
{
    public class Samurai
    {
        public Samurai()
        {
            Quotes = new List<Quote>();
        }

        public int Id { get; set; }
        public string Name { get; set; }


        // En till Många (1-N)
        public List<Quote> Quotes { get; set; }

        // Många till en (N-1)
        //public int BattleId { get; set; }
        
        // Många till många (N-N)
        public List<SamuraiBattle> SamuraiBattles { get; set; }

        // En till en 1-1
        public SecretIdentity SecretIdentity { get; set; }
    }
}
