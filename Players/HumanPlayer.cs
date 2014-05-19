using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Utlis;

namespace Bomberman.Players
{
    public class HumanPlayer : GameActor
    {
        public HumanPlayer()
        {
            Velocity = 5;
            TreasureState.BombsCount = 20;
        }
        public event ChangedEventHandler Changed;
        public override void Move(Direction direction)
        {
            if (Delay == Velocity / 2)
            {
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
