using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bomberman.Sounds
{
    /// <summary>
    /// Klasa abstrakcyjna dająca możliwość różnych konfiguracji dźwięku
    /// </summary>
    public abstract class Audio
    {
        public abstract void PlaySound(Sound sound);
        public abstract void StopSound(Sound sound);
        public abstract void StopAllSounds();
    }
}
