using System;

namespace Bomberman.Utlis
{
    /// <summary>
    /// Klasa opisująca stan skarbów zdobytych podczas gry
    /// </summary>
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
