using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Players;
using Microsoft.Xna.Framework.Content;

namespace Bomberman.Commands
{
    public class RemoteBombFire: ICommand
    {
        private ContentManager manager;
        private int x;
        private int y;

        public RemoteBombFire(ContentManager manager)
        {
            this.manager = manager;
        }
        public void Execute(GameActor actor)
        {
            if (actor.TreasureState.RemoteBombsCount > 0)
            {
                x = actor.CurrentUnit.X;
                y = actor.CurrentUnit.Y;
                if (PutRemoteBomb.RemoteBombUnits.Count > 0)
                {
                    var remotePosition = PutRemoteBomb.RemoteBombUnits.Last();
                    GameSession.GameBoard.Units[remotePosition.Item1, remotePosition.Item2].Initialize(manager, State.NormalBomb, true);
                    GameSession.GameBoard.ResetNeighbors(GameSession.GameBoard.Units[remotePosition.Item1, remotePosition.Item2], actor.TreasureState.IsFlame ? 2 : 1);
                    PutRemoteBomb.RemoteBombUnits.RemoveAt(PutRemoteBomb.RemoteBombUnits.Count - 1);
                }

                
            }
        }
    }
}
