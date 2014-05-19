using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Commands
{
    public class GloveAction : ICommand
    {
        public void Execute(GameActor actor)
        {
            if (actor.TreasureState.GlovesCount > 0)
            {
                
            }
        }
    }
}
