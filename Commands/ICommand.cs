using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Players;

namespace Bomberman.Commands
{
    public interface ICommand
    {
        void Execute(GameActor actor);
    }
}
