using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.GameStateManagement
{
    public class LevelProperties
    {
        public int BombsStart { get; set; }
        public int EnemyKilledPoints { get; set; }
        public int TreasureFoundPoints { get; set; }
        public int DurationPenalty { get; set; }
    }
}
