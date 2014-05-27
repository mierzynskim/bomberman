using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Players;

namespace Bomberman.Commands
{
    /// <summary>
    /// Interfejs wzorca komenta odpowiadającego za akcje podczas rozgrywki
    /// </summary>
    public interface ICommand
    {
        void Execute(GameActor actor);
    }
}
