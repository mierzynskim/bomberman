using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.SettingsModel;

namespace Bomberman.StateInterfaces
{
    public interface ISaveGameStorage
    {
        void Save(GameStorage gameStorage);
    }
}
