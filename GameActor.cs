using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Utlis;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman
{
    public abstract class GameActor
    {
        public Unit CurrentUnit { protected get; set; }
        public abstract void Move(Direction direction);
    }


}
