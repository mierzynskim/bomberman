using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.SettingsModel;

namespace Bomberman.StateImplementation
{
    public interface ILoadGameStorage
    {
        GameStorage Load();
    }
}
