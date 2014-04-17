using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    public class ResourceInfo
    {
        public static Dictionary<State, string> Resources = new Dictionary<State, string>
        {
            {State.Concrete, "concrete.png"},
            {State.Wall, "wall.png"}
        };

    }
}
