namespace Bomberman.Utlis
{
    /// <summary>
    /// Właściwości poziomu gry
    /// </summary>
    public class LevelProperties
    {
        public int BombsStart { get; set; }
        public int EnemyKilledPoints { get; set; }
        public int TreasureFoundPoints { get; set; }
        public int DurationPenalty { get; set; }
        public int LevelFinishedPoints { get; set; }
        public int EnemiesCount { get; set; }
    }
}
