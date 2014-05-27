using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Players;
using Bomberman.Sounds;
using Microsoft.Xna.Framework.Content;

namespace Bomberman.Commands
{
    public class NormalBombFire: ICommand
    {
        private ContentManager manager;
        private readonly int y;
        private readonly int x;

        public NormalBombFire(ContentManager manager, int x, int y)
        {
            this.manager = manager;
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// Implementacja wzorca komenda. Metoda jest wywoływana, kiedy ustawiona jest bomba zwykła i minie czas do jej wybuchu
        /// </summary>
        /// <param name="actor">Uczestnik gry</param>

        public void Execute(GameActor actor)
        {
            GameSession.GameBoard.ResetNeighbors(GameSession.GameBoard.Units[x, y], actor.TreasureState.IsFlame ? 2 : 1);

            ServiceLocator.AudioSerivce.PlaySound(Sound.Explosion);
            actor.TreasureState.BombsCount--;

        }
    }
}
