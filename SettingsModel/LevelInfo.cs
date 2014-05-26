using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Utlis;

namespace Bomberman.SettingsModel
{
    public static class LevelInfo
    {
        public class Size
        {
            public Size(int width, int height)
            {
                Width = width;
                Height = height;
            }
            public int Width { get; set; }
            public int Height { get; set; }
        }

        public static Dictionary<Level, Size> LevelToSize = new Dictionary<Level, Size>
        {
            {Level.Easy, new Size(20, 22)}
        };  
    }
}
