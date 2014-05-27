using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Utlis;

namespace Bomberman.SettingsModel
{
    /// <summary>
    /// Model najlepszych wyników
    /// </summary>
    public class HighScore
    {
        public Level Level { get; set; }
        public int Points { get; set; }
    }
}
