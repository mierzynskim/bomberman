using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Utlis;

namespace Bomberman.SettingsModel
{
    public class PlayerSettings
    {

        public string Login { get; set; }
        public List<HighScore> HighScores { get; set; }
        public Level Level { get; set; }
        public int EasyLevelsUnlocked { get; set; }
        public int MediumLevelsUnlocked { get; set; }
        public int HardLevelsUnlocked { get; set; }

    }
}
