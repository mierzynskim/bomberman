using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Utlis
{
    /// <summary>
    /// Klasa zawierająca słownik stałych dla danego poziomu gry
    /// </summary>
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
                LevelFinishedPoints = 100,
                EnemiesCount = 5
                
            }},
            {Level.Medium, new LevelProperties
            {
                BombsStart = 20,
                DurationPenalty = 2,
                EnemyKilledPoints = 20,
                TreasureFoundPoints = 10,
                LevelFinishedPoints = 200,
                EnemiesCount = 10
            }},
            {Level.Hard, new LevelProperties
            {
                BombsStart = 20,
                DurationPenalty = 1,
                EnemyKilledPoints = 20,
                TreasureFoundPoints = 10,
                LevelFinishedPoints = 300,
                EnemiesCount = 15
            }}
        };
    }
}
