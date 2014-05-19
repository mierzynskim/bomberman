﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman
{
    public enum State
    {
        Empty,
        Wall,
        Concrete,
        Player,
        NormalBomb,
        RemoteBomb,
        Enemy,
        Glove, //random
        RollerSkates, //random
        Fire, //random
        Exit // random
    }
}
