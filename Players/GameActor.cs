using System;
using Bomberman.Utlis;
using Microsoft.Xna.Framework;

namespace Bomberman
{
    [Serializable]
    public abstract class GameActor
    {
        public GameActor()
        {
            TreasureState = new TreasureState();
            Velocity = 5;
            TreasureState.BombsCount = 20;
        }
        public int Velocity { get; set; }
        public int LivesCount { get; set; }
        public int LevelPoints { get; set; }
        
        public int OverallPoints { get; set; }
        public Unit CurrentUnit { get; set; }
        public TreasureState TreasureState { get; set; }
        public GameTime Time { get; set; }
        public int Delay { get; set; }

        public abstract void Move(Direction direction);
    }


}
