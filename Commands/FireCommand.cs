using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Bomberman.Commands
{
    public class FireCommand : ICommand
    {
        private const int DelayTime = 10;
        public void Execute(GameActor actor)
        {
            Thread.Sleep(DelayTime * 1000);
            actor.TreasureState.IsFlame = false;
        }
    }
}
