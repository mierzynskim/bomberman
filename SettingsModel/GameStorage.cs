using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.SettingsModel
{
    public class GameStorage
    {
        public GameStorage()
        {
            PlayerSettings = new List<PlayerSettings>();
        }
        public List<PlayerSettings> PlayerSettings { get; set; }
    }
}
