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
            {State.NormalBomb, "remoteBomb.png"},
            {State.Enemy, "enemy.png"},
            {State.RemoteBomb, "remoteBomb.png"},
            {State.Glove, "glove.png"},
            {State.RollerSkates, "rollerskates.png"},
            {State.Fire, "fire.png"},
            {State.Exit, "exit.png"},
            //{State.EndlessBombs, "endlessBombs.jpg"}

        };
        public static readonly Dictionary<Sound, string> SoundResources = new Dictionary<Sound, string>
        {
            {Sound.Explosion, "BOM_11_S"}
        };  

    }
}
