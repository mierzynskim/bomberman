using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Sounds;
using Bomberman.Utlis;

namespace Bomberman
{
    /// <summary>
    /// Klasa ułatwiająca odwoływanie się do zasobów gry
    /// </summary>
    public static class ResourceInfo
    {
        public static readonly Dictionary<State, string> ImageResources = new Dictionary<State, string>
        {
            {State.Concrete, "concrete.png"},
            {State.Wall, "wall.png"},
            {State.Player, "player.png"},
            {State.NormalBomb, "bomb.png"},
            {State.Enemy, "enemy.png"},
            {State.RemoteBomb, "bomb.png"},
            {State.Glove, "glove.png"},
            {State.RollerSkates, "rollerskates.png"},
            {State.Fire, "fire.png"},
            {State.Exit, "exit.png"},
            {State.NewRemoteBomb, "newRemoteBomb.png"}
            //{State.EndlessBombs, "endlessBombs.jpg"}

        };
        public static readonly Dictionary<Sound, string> SoundResources = new Dictionary<Sound, string>
        {
            {Sound.Explosion, "BOM_11_S"}
        };  

    }
}
