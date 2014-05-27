using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Utlis
{
    /// <summary>
    /// Delegat wywoływany przy zmienie stanu obiektu (np. gracza)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ChangedEventHandler(object sender, EventArgs e);
}
