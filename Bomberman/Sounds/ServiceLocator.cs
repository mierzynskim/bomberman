using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Bomberman.Sounds
{
    public static class ServiceLocator
    {
        public static void Provide(Audio service)
        {
            AudioSerivce = service;
        }

        public static Audio AudioSerivce { get; private set; }
    }
}
