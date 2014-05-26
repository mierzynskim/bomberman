using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Players;
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

        private readonly Direction direction;

        private GameActor Actor { get; set; }
        /// <summary>
        /// Implementacja wzorca komenda. Metoda jest wywoływana, kiedy uczestnik porusza sie po planszy
        /// </summary>
        /// <param name="actor">Uczestnik gry</param>
        public void Execute(GameActor actor)
        {
            actor.Move(direction);
        }
    }
}
