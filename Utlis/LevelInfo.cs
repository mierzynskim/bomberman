using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Utlis
{
    public static class LevelConsts
    {
        public static Dictionary<Level, LevelProperties> LevelProperties = new Dictionary<Level, LevelProperties>
        {
            {Level.Easy, new LevelProperties
            {
                BombsStart = 20,
                DurationPenalty = 3,
                EnemyKilledPoints = 20,
                TreasureFoundPoints = 10,
                LevelFinishedPoints = 100
            }},
            {Level.Medium, new LevelProperties
            {
                BombsStart = 20,
                DurationPenalty = 2,
                EnemyKilledPoints = 20,
                TreasureFoundPoints = 10,
                LevelFinishedPoints = 100
            }},
            {Level.Hard, new LevelProperties
            {
                BombsStart = 20,
                DurationPenalty = 1,
                EnemyKilledPoints = 20,
                TreasureFoundPoints = 10,
                LevelFinishedPoints = 100
            }}
        };
    }
}
