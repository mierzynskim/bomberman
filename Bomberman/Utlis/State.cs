using System;

namespace Bomberman.Utlis
{
    /// <summary>
    /// Enumeracja ułatwiająca idetyfikację stanu danego pola w grze
    /// </summary>
    [Serializable]
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
        Exit, // random
        //EndlessBombs // random
        NewRemoteBomb //random
    }
}
