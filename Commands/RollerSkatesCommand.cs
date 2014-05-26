using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Bomberman.Players;
using Bomberman.Sounds;
using Microsoft.Xna.Framework.Content;

namespace Bomberman.Commands
{
    public class RollerSkatesCommand : ICommand
    {
        private ContentManager manager;

        private bool isTimerOn;
        private const int DelayTime = 10;
        private int x;
        private int y;
        private int previousVelocity;

        public RollerSkatesCommand(ContentManager manager)
        {
            this.manager = manager;
        }
        /// <summary>
        /// Implementacja wzorca komenda. Metoda jest wywoływana, kiedy uczestnik gry wybierze wrotki
        /// Tworzony jest wątek odliczający czas użycia wrotek.
        /// Po danym czasie prędkość uczestnika wraca to wartości początkowej
        /// </summary>
        /// <param name="actor">Uczestnik gry</param>
        public void Execute(GameActor actor)
        {
            if (actor.TreasureState.IsRollerSkates)
            {
                x = actor.CurrentUnit.X;
                y = actor.CurrentUnit.Y;
                isTimerOn = true;
                var thread = new Thread(() =>
                {
                    previousVelocity = actor.Velocity;
                    actor.Velocity = 3;
                    Thread.Sleep(DelayTime * 1000);
                    actor.TreasureState.IsRollerSkates = false;
                    actor.Velocity = previousVelocity;
                    Thread.CurrentThread.Abort();
                });
                thread.Start(); 
            }
        }
    }
}
