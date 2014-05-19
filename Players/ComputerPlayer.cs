using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Algorithms;
using Bomberman.Utlis;

namespace Bomberman.Players
{
    public class ComputerPlayer: GameActor
    {
        public ComputerPlayer(IAiAlgorithm algorithm)
        {
            Algorithm = algorithm;
        }

        public int Delay { get; set; }
        public IAiAlgorithm Algorithm { get; set; }
        public List<Direction> Directions { get; set; }

        public override void Move(Direction direction)
        {
            if (Delay == 4)
            {
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

                Directions.RemoveAt(Directions.Count - 1);
            }
            Delay = (Delay + 1) % 10;
        }

        public void MakeMove()
        {
            if (Directions != null && Directions.Count > 0)
                Move(Directions.Last());
        }

        public void PlayerChangedPosition(object sender, EventArgs e)
        {
            HumanPlayer player = sender as HumanPlayer;
            if (player != null)
            {
                var directions = Algorithm.FindPath(CurrentUnit, player.CurrentUnit);
                if (directions != null)
                    Directions = directions.ToList();
            }
        }
    }

}
