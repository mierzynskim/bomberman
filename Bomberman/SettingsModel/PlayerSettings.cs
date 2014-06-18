using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Utlis;

namespace Bomberman.SettingsModel
{
    /// <summary>
    /// Klasa ustawień danego gracza
    /// </summary>
    public class PlayerSettings
    {
        private List<HighScore> highScores;
        public string Login { get; set; }
        public Level Level { get; set; }
        public int EasyLevelsUnlocked { get; set; }
        public int MediumLevelsUnlocked { get; set; }
        public int HardLevelsUnlocked { get; set; }
        public int Stage { get; set; }

        public List<HighScore> HighScores
        {
            get
            {
                return highScores;
            }
            set
            {
                highScores = value;
                highScores = highScores.OrderByDescending(score => score.Points).Take(10).ToList();
            }
        }
    }
}
