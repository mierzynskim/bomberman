using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Utlis;
using Microsoft.Xna.Framework.Content;

namespace Bomberman.Commands
{
    public class GloveAction : ICommand
    {
        private readonly Direction direction;
        private ContentManager manager;
        private static int delay = 5;

        public GloveAction(ContentManager manager, Direction direction)
        {
            this.direction = direction;
            this.manager = manager;
        }
        public void Execute(GameActor actor)
        {
            int x = actor.CurrentUnit.X;
            int y = actor.CurrentUnit.Y;
            if (actor.TreasureState.GlovesCount > 0 && delay == 4)
            {
                switch (direction)
                {
                    case Direction.Up:
                        while (true)
                        {
                            var newUnit = GameSession.GameBoard.Units[x, (y - 1) % GameSession.GameBoard.Height];
                            if (newUnit.UnitState == State.Empty)
                            {
                                var command = new PutNormalBomb(manager, newUnit.X, newUnit.Y, true);
                                command.Execute(actor);
                                break;
                            }
                            y--;
                        }
                        break;
                    case Direction.Down:
                        while (true)
                        {
                            var newUnit = GameSession.GameBoard.Units[x, (y + 1) % GameSession.GameBoard.Height];
                            if (newUnit.UnitState == State.Empty)
                            {
                                var command = new PutNormalBomb(manager, newUnit.X, newUnit.Y, true);
                                command.Execute(actor);
                                break;
                            }
                            y++;
                        }
                        break;
                    case Direction.Left:
                        while (true)
                        {
                            var newUnit = GameSession.GameBoard.Units[(x - 1) % GameSession.GameBoard.Width, y];
                            if (newUnit.UnitState ==
                                State.Empty)
                            {
                                var command = new PutNormalBomb(manager, newUnit.X, newUnit.Y, true);
                                command.Execute(actor);
                                break;
                            }
                            x--;
                        }
                        break;
                    case Direction.Right:
                        while (true)
                        {
                            var newUnit = GameSession.GameBoard.Units[(x + 1) % GameSession.GameBoard.Width, y];
                            if (newUnit.UnitState ==
                                State.Empty)
                            {
                                var command = new PutNormalBomb(manager,newUnit.X, newUnit.Y, true);
                                command.Execute(actor);
                                break;
                            }
                            x++;
                        }
                        break;
                }
            }
            delay = (delay + 1) % 5;
        }
    }
}
