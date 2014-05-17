using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Bomberman.Utlis
{
    public class Board
    {
        private static readonly Random rnd = new Random();
        public Board(int width, int height, ContentManager manager)
        {
            Width = width;
            Height = height;
            Units = new Unit[Height, Width];
            for (var i = 0; i < Height; i++)
                for (var j = 0; j < Width; j++)
                {
                    Units[i, j] = new Unit(new Vector2(33 * i, 33 * j)) {X = i, Y = j};
                    if (i == 0 || j == 0 || (j % 2 == 0 && i % 2 == 0))
                    {
                        Units[i, j].Initialize(manager, State.Concrete);
                    }
                    else
                    {
                        var result = rnd.Next(0, 8);
                        if (result != 1)
                            Units[i, j].UnitState = State.Empty;
                        else
                        {
                            Units[i, j].Initialize(manager, State.Wall);
                        }

                    }
                }
        }
        public int Width { get; set; }
        public int Height { get; set; }
        public Unit[,] Units { get; set; }

        public void AddPlayer(HumanPlayer player, ContentManager manager)
        {
            bool isAdded = false;
            for (var i = 0; i < Height; i++)
            {
                for (var j = 0; j < Width; j++)
                {
                    if (Units[i, j].UnitState == State.Empty)
                    {
                        Units[i, j].Initialize(manager, State.Player);
                        Units[i, j].UnitState = State.Player;
                        isAdded = true;
                        player.CurrentUnit = Units[i, j];
                        break;
                    }
                }
                if (isAdded)
                    break;
            }
        }

        public void AddComputerPlayer(ComputerPlayer player, ContentManager manager)
        {
            bool isAdded = false;
            for (var i = Height - 1; i >= 0; i--)
            {
                for (var j = Width - 1; j >= 0; j--)
                {
                    if (Units[i, j].UnitState == State.Empty)
                    {
                        Units[i, j].Initialize(manager, State.Enemy);
                        Units[i, j].UnitState = State.Enemy;
                        isAdded = true;
                        player.CurrentUnit = Units[i, j];
                        break;
                    }
                }
                if (isAdded)
                    break;
            }
        }

        public IEnumerable<Unit> GetFreeNeighbors(Unit unit)
        {
            var neighbors = new List<Unit>();
            if (unit.X + 1 < Width && (Units[unit.X + 1, unit.Y].UnitState == State.Empty || Units[unit.X + 1, unit.Y].UnitState == State.Player))
                neighbors.Add(Units[unit.X + 1, unit.Y]);
            if (unit.X - 1 > 0 && (Units[unit.X - 1, unit.Y].UnitState == State.Empty || Units[unit.X - 1, unit.Y].UnitState == State.Player))
                neighbors.Add(Units[unit.X - 1, unit.Y]);
            if (unit.Y + 1 < Height && (Units[unit.X, unit.Y + 1].UnitState == State.Empty || Units[unit.X, unit.Y + 1].UnitState == State.Player))
                neighbors.Add(Units[unit.X, unit.Y + 1]);
            if (unit.Y - 1 > 0 && (Units[unit.X, unit.Y - 1].UnitState == State.Empty || Units[unit.X, unit.Y - 1].UnitState == State.Player))
                neighbors.Add(Units[unit.X, unit.Y - 1]);

            return neighbors;
        }

        private IEnumerable<Unit> GetBombRange(Unit unit)
        {
            var neighbors = new List<Unit>();
            if (unit.X + 1 < Width && Units[unit.X + 1, unit.Y].UnitState != State.Concrete)
                neighbors.Add(Units[unit.X + 1, unit.Y] );
            if (unit.X - 1 > 0 && Units[unit.X - 1, unit.Y].UnitState != State.Concrete)
                neighbors.Add(Units[unit.X - 1, unit.Y]);
            if (unit.Y + 1 < Height && Units[unit.X, unit.Y + 1].UnitState != State.Concrete)
                neighbors.Add(Units[unit.X, unit.Y + 1]);
            if (unit.Y - 1 > 0 && Units[unit.X, unit.Y - 1].UnitState != State.Concrete)
                neighbors.Add(Units[unit.X, unit.Y - 1]);
            neighbors.Add(unit);
            return neighbors;
        }

        public void ResetNeighbors(Unit unit)
        {
            foreach (var neighbor in GetBombRange(unit))
            {
                neighbor.ResetState();
            }
        }



    }
}
