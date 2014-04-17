using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
