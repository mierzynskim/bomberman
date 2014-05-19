using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Bomberman.Commands
{
    public class RemoteBombFire: ICommand
    {
        private ContentManager manager;
        private int x;
        private int y;
        public void Execute(GameActor actor)
        {
            if (actor.TreasureState.RemoteBombsCount > 0)
            {
                x = actor.CurrentUnit.X;
                y = actor.CurrentUnit.Y;
                actor.CurrentUnit.Initialize(manager, State.NormalBomb);
                
                //var thread = new Thread(() =>
                //{
                //    Thread.Sleep(DelayTime * 1000);
                //    isTimerOn = false;
                //    GameSession.GameBoard.ResetNeighbors(GameSession.GameBoard.Units[x, y]);
                //    ServiceLocator.AudioSerivce.PlaySound(Sound.Explosion);
                //    actor.TreasureState.BombsCount--;
                //    Thread.CurrentThread.Abort();
                //});
                //thread.Start(); 
            }
        }
    }
}
