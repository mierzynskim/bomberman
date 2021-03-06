﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Players;
using Bomberman.Utlis;
using Microsoft.Xna.Framework.Content;

namespace Bomberman.Commands
{
    public class RemoteBombFire : ICommand
    {
        private ContentManager manager;
        private int x;
        private int y;

        public RemoteBombFire(ContentManager manager)
        {
            this.manager = manager;
        }
        /// <summary>
        /// Implementacja wzorca komenda. Metoda jest wywoływana, kiedy uczestnik gry wybierze używa bomby zdalnej
        /// </summary>
        /// <param name="actor">Uczestnik gry</param>
        public void Execute(GameActor actor)
        {
            x = actor.CurrentUnit.X;
            y = actor.CurrentUnit.Y;
            if (PutRemoteBomb.RemoteBombUnits.Count > 0)
            {
                var remotePosition = PutRemoteBomb.RemoteBombUnits.Last();
                GameSession.GameBoard.Units[remotePosition.Item1, remotePosition.Item2].Initialize(manager, State.NormalBomb);
                GameSession.GameBoard.ResetNeighbors(GameSession.GameBoard.Units[remotePosition.Item1, remotePosition.Item2], actor.TreasureState.IsFlame ? 2 : 1);
                PutRemoteBomb.RemoteBombUnits.RemoveAt(PutRemoteBomb.RemoteBombUnits.Count - 1);
            }

        }
    }
}
