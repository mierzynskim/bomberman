using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Bomberman.Sounds;
using Bomberman.Utlis;
using Microsoft.Xna.Framework.Content;

namespace Bomberman.Commands
{
    public class PutNormalBomb : ICommand
    {
        private ContentManager manager;
        private static int delay = 5;
        private bool isTimerOn;
        private const int DelayTime = 5;
        private int x;
        private int y;

        public PutNormalBomb(ContentManager manager)
        {
            this.manager = manager;
        }
        public void Execute(GameActor actor)
        {
            if (actor.TreasureState.BombsCount > 0 && delay == 4)
            {
                x = actor.CurrentUnit.X;
                y = actor.CurrentUnit.Y;
                actor.CurrentUnit.Initialize(manager, State.NormalBomb);
                isTimerOn = true;
                var thread = new Thread(() =>
                {
                    Thread.Sleep(DelayTime * 1000);
                    isTimerOn = false;
                    GameSession.GameBoard.ResetNeighbors(GameSession.GameBoard.Units[x, y], actor.TreasureState.IsFlame ? 2 : 1);

                    ServiceLocator.AudioSerivce.PlaySound(Sound.Explosion);
                    actor.TreasureState.BombsCount--;
                    Thread.CurrentThread.Abort();
                });
                thread.Start();   
            }
            delay = (delay + 1) % 5;

        }
    }
}
