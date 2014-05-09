using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Utlis;

namespace Bomberman.Players
{
    public class HumanPlayer : GameActor
    {
        public int Delay { get; set; }

        public override void Move(Direction direction)
        {
            if (Delay == 4)
            {
                switch (direction)
                {
                    case Direction.Down:
                        CurrentUnit.MoveTo(CurrentUnit.X, CurrentUnit.Y - 1);
                        break;
                    case Direction.Up:
                        CurrentUnit.MoveTo(CurrentUnit.X, CurrentUnit.Y + 1);
                        break;
                    case Direction.Left:
                        CurrentUnit.MoveTo(CurrentUnit.X - 1, CurrentUnit.Y);
                        break;
                    case Direction.Right:
                        CurrentUnit.MoveTo(CurrentUnit.X + 1, CurrentUnit.Y);
                        break;
                }
            }
            Delay = (Delay + 1) % 5;

        }
    }
}
