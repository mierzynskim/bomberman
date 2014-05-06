using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Bomberman.Commands
{
    public class PutNormalBomb: ICommand
    {
        private ContentManager manager;
        public PutNormalBomb(ContentManager manager)
        {
            this.manager = manager;
        }
        public void Execute(GameActor actor)
        {
            actor.CurrentUnit.Initialize(manager, State.NormalBomb);
        }
    }
}
