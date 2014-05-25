using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    [Serializable]
    public class TreasureState
    {
        public int BombsCount { get; set; }
        public int RemoteBombsCount { get; set; }
        public bool IsFlame { get; set; }
        public bool IsRollerSkates { get; set; }
        public int GlovesCount { get; set; }
        public bool EndlessBombs { get; set; }
    }
}
