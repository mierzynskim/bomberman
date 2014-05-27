using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Bomberman.Players;
using Bomberman.Sounds;
using Bomberman.Utlis;
using Microsoft.Xna.Framework.Content;

namespace Bomberman.Commands
{
    public class PutRemoteBomb : ICommand
    {
        private ContentManager manager;
        private static int delay = 5;
        private bool isTimerOn;
        private const int DelayTime = 5;
        private int x;
        private int y;

        private static List<Tuple<int,int>> remoteBombUnits;

        public static List<Tuple<int, int>> RemoteBombUnits
        {
            get { return remoteBombUnits ?? (remoteBombUnits = new List<Tuple<int, int>>()); }
        } 
        public PutRemoteBomb(ContentManager manager)
        {
            this.manager = manager;
        }
        /// <summary>
        /// Implementacja wzorca komenda. Metoda jest wywoływana, kiedy uczestnik gry chce ustawić bombę zdalną
        /// </summary>
        /// <param name="actor">Uczestnik gry</param>
        public void Execute(GameActor actor)
        {
            if (actor.TreasureState.BombsCount > 0 && delay == 4)
            {
                x = actor.CurrentUnit.X;
                y = actor.CurrentUnit.Y;
                actor.CurrentUnit.Initialize(manager, State.NormalBomb, true);
                RemoteBombUnits.Add(new Tuple<int, int>(x, y));
            }
            delay = (delay + 1) % 5;

        }
    }
}
