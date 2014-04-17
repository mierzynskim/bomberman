using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Utlis;

namespace Bomberman.Commands
{
    public class MoveUnitCommand: ICommand
    {
        public MoveUnitCommand(Direction direction, GameActor actor)
        {
            this.direction = direction;
            Actor = actor;
        }

        public int X { get; set; }
        public int Y { get; set; }

        private Direction direction;

        public GameActor Actor { get; set; }

        public void Execute(GameActor actor)
        {
            actor.Move(direction);
        }
    }
}
