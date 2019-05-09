﻿using System;
using System.Collections.Generic;

namespace SamuraiApp.Domain
{
    public class Battle
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // 1-N
        //public List<Samurai> Samurais { get; set; }

        // N-N
        public List<SamuraiBattle> SamuraiBattles { get; set; }
    }
}
