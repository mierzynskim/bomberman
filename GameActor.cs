using Bomberman.Utlis;
using Microsoft.Xna.Framework;

namespace Bomberman
{
    public abstract class GameActor
    {
        public int Velocity { get; set; }
        public int LivesCount { get; set; }
        public int LevelPoints { get; set; }
        public int CurrentLevel { get; set; }
        public int OverallPoints { get; set; }
        public Unit CurrentUnit { protected get; set; }
        public GameTime Time { get; set; }
        public abstract void Move(Direction direction);
    }


}
