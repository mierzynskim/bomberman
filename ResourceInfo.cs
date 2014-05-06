using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    public static class ResourceInfo
    {
        public static readonly Dictionary<State, string> Resources = new Dictionary<State, string>
        {
            {State.Concrete, "concrete.png"},
            {State.Wall, "wall.png"},
            {State.Player, "player.png"},
            {State.NormalBomb, "bomb.png"},
            {State.RemoteBomb, "bomb.png"}
        };

    }
}
