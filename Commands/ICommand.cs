using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Commands
{
    interface ICommand
    {
        void Execute(GameActor actor);
    }
}
