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

        private AiAlgorithm algorithm { get; set; }
        private List<Direction> Directions { get; set; }


        /// <summary>
        /// Metoda odpowiadająca za poruszanie sie gracza sterowanego przez komputer
        /// </summary>
        /// <param name="direction">Kierunek ruchu gracza</param>
        public override void Move(Direction direction)
        {
            if (Delay == Velocity / 2)
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
                if (Directions.Count > 0)
                Directions.RemoveAt(Directions.Count - 1);
            }
            Delay = (Delay + 1) % Velocity;
        }
        /// <summary>
        /// Wykonuje następny ruch z listy ruchów wyznaczonych algorytmem
        /// Jeśli gracz znajduje się w pobliżu, stawiana jest bomba i w miarę możliwości gracze nie blokują swojej pozycji
        /// </summary>
        public void MakeMove()
        {
            if (GameSession.GameBoard.IsPlayerAround(CurrentUnit))
            {
                var command = new PutNormalBomb(GameSession.Manager, CurrentUnit.X, CurrentUnit.Y, false);
                command.Execute(this);
                var randomMoves = new List<Direction> {Direction.Up, Direction.Down, Direction.Right, Direction.Left};
                foreach (var randomMove in randomMoves)
                {
                    Move(randomMove);
                }
            }
            if (Directions != null && Directions.Count > 0)
                Move(Directions.Last());
        }
        /// <summary>
        /// Event sprawdzający czy przeciwnik zmienił pozycje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
