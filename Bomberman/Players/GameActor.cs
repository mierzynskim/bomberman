using System;
using Bomberman.StateImplementation;
using Bomberman.Utlis;
using GameStateManagement;

namespace Bomberman.Players
{
    /// <summary>
    /// Klasa abstrakcyjna dla uczestnika gry
    /// </summary>
    [Serializable]
    public abstract class GameActor
    {
        protected GameActor()
        {
            TreasureState = new TreasureState();
            Velocity = 5;
            TreasureState.BombsCount = LevelConsts.LevelProperties[MonoGameFileSystem.Instance.CurrentPlayerSettings.Level].BombsStart;
        }
        public int Velocity { get; set; }

        private int levelPoints;
        public int LevelPoints
        {
            get { return levelPoints; }
            set
            {
                levelPoints = value;
                if (levelPoints < 0)
                    levelPoints = 0;
            }
        }

        public int OverallPoints { get; set; }
        public Unit CurrentUnit { get; set; }
        public TreasureState TreasureState { get; set; }

        public int Delay { get; set; }



        public abstract void Move(Direction direction);
    }


}
