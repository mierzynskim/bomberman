using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Players;
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
                            y = (y - 1)%GameSession.GameBoard.Height;
                            if (y < 0)
                                y = GameSession.GameBoard.Height - 1;
                            var newUnit = GameSession.GameBoard.Units[x, y];
                            if (newUnit.UnitState == State.Empty)
                            {
                                var command = new PutNormalBomb(manager, newUnit.X, newUnit.Y, true);
                                command.Execute(actor);
                                break;
                            }
                        }
                        break;
                    case Direction.Down:
                        while (true)
                        {
                            y = (y + 1) % GameSession.GameBoard.Height > 0 ? (y + 1) % GameSession.GameBoard.Height : 0;
                            var newUnit = GameSession.GameBoard.Units[x, y];
                            if (newUnit.UnitState == State.Empty)
                            {
                                var command = new PutNormalBomb(manager, newUnit.X, newUnit.Y, true);
                                command.Execute(actor);
                                break;
                            }
                        }
                        break;
                    case Direction.Left:
                        while (true)
                        {
                            x = (x - 1) % GameSession.GameBoard.Width;
                            if (x < 0)
                                x = GameSession.GameBoard.Width - 1;
                            var newUnit = GameSession.GameBoard.Units[x, y];
                            if (newUnit.UnitState == State.Empty)
                            {
                                var command = new PutNormalBomb(manager, newUnit.X, newUnit.Y, true);
                                command.Execute(actor);
                                break;
                            }
                        }
                        break;
                    case Direction.Right:
                        while (true)
                        {
                            x = (x + 1) % GameSession.GameBoard.Width > 0 ? (x + 1) % GameSession.GameBoard.Width : 0;
                            var newUnit = GameSession.GameBoard.Units[x, y];
                            if (newUnit.UnitState == State.Empty)
                            {
                                var command = new PutNormalBomb(manager,newUnit.X, newUnit.Y, true);
                                command.Execute(actor);
                                break;
                            }
                        }
                        break;
                }
            }
            delay = (delay + 1) % 5;
        }
    }
}
