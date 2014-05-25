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
    public class PutNormalBomb : ICommand
    {
        private ContentManager manager;
        private static int delay = 5;
        private bool isTimerOn;
        private const int DelayTime = 5;
        private int x;
        private int y;
        private readonly bool inst;

        public PutNormalBomb(ContentManager manager, int x, int y, bool inst)
        {
            this.manager = manager;
            this.inst = inst;
            this.x = x;
            this.y = y;
        }
        public void Execute(GameActor actor)
        {
            if (actor.TreasureState.BombsCount > 0 && (delay == 4 || inst))
            {
                GameSession.GameBoard.Units[x, y].Initialize(manager, State.NormalBomb, !inst);
                isTimerOn = true;
                var thread = new Thread(() =>
                {
                    Thread.Sleep(DelayTime * 1000);
                    isTimerOn = false;
                    var fireCommand = new NormalBombFire(manager, x, y);
                    fireCommand.Execute(actor);
                    Thread.CurrentThread.Abort();
                });
                thread.Start();   
            }
            delay = (delay + 1) % 5;

        }
    }
}
