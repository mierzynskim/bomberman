using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Sounds;

namespace Bomberman
{
    public static class ResourceInfo
    {
        public static readonly Dictionary<State, string> ImageResources = new Dictionary<State, string>
        {
            {State.Concrete, "concrete.png"},
            {State.Wall, "wall.png"},
            {State.Player, "player.png"},
            {State.NormalBomb, "bomb.png"},
            {State.RemoteBomb, "bomb.png"},
            {State.Enemy, "enemy.png"}
        };
        public static readonly Dictionary<Sound, string> SoundResources = new Dictionary<Sound, string>
        {
            {Sound.Explosion, "BOM_11_S"}
        };  

    }
}
