using System;
using System.Collections.Generic;

namespace SamuraiModel
{
    public partial class SamuraiBattle
    {
        public int SamuraiId { get; set; }
        public int BattleId { get; set; }
        public int NumberOfKills { get; set; }

        public virtual Battles Battle { get; set; }
        public virtual Samurais Samurai { get; set; }
    }
}
