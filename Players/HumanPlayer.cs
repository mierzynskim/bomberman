using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Utlis;
using GameStateManagement;
using Microsoft.Xna.Framework;

namespace Bomberman.Players
{
    [Serializable]
    public class HumanPlayer : GameActor
    {
        public HumanPlayer()
        {
            Velocity = 5;
            TreasureState.BombsCount = 20;
            
        }

        public event ChangedEventHandler Changed;
        public event EventHandler<PlayerIndexEventArgs> GameOver;

        public override void Move(Direction direction)
        {
            if (Delay == Velocity / 2)
            {
                if (CurrentUnit.UnitState == State.Empty)
                    GameOver(this, new PlayerIndexEventArgs(PlayerIndex.One));
                Changed(this, new EventArgs());
                switch (direction)
                {
                    case Direction.Down:
                        CurrentUnit.MoveTo(CurrentUnit.X, CurrentUnit.Y + 1, this);
                        break;
                    case Direction.Up:
                        CurrentUnit.MoveTo(CurrentUnit.X, CurrentUnit.Y - 1, this);
                        break;
                    case Direction.Left:
                        CurrentUnit.MoveTo(CurrentUnit.X - 1, CurrentUnit.Y, this);
                        break;
                    case Direction.Right:
                        CurrentUnit.MoveTo(CurrentUnit.X + 1, CurrentUnit.Y, this);
                        break;
                }
            }
            Delay = (Delay + 1) % Velocity;
        }
    }
}
