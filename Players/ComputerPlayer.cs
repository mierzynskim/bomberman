using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Algorithms;
using Bomberman.Commands;
using Bomberman.Utlis;
using GameStateManagement;

namespace Bomberman.Players
{
    [Serializable]
    public class ComputerPlayer: GameActor
    {
        public ComputerPlayer(AiAlgorithm algorithm)
        {
            this.algorithm = algorithm;
            Velocity = 10;
        }

        public ComputerPlayer()
        {
            
        }

        private readonly AiAlgorithm algorithm;
        public List<Direction> Directions { get; set; }


        public override void Move(Direction direction)
        {
            if (Delay == Velocity / 2)
            {
                if (GameSession.GameBoard.IsPlayerAround(CurrentUnit))
                {
                    var command = new PutNormalBomb(GameSession.Manager);
                    command.Execute(this);
                }
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
            Delay = (Delay + 1) % Velocity;
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
                var directions = algorithm.FindPath(CurrentUnit, player.CurrentUnit);
                if (directions != null)
                    Directions = directions.ToList();
            }
        }
    }

}
