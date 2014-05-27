using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Bomberman.Sounds
{
    /// <summary>
    /// Klasa implementująca dźwięki w MonoGame
    /// </summary>
    public class MonoGameAudio: Audio
    {
        SoundEffect soundEffect;
        /// <summary>
        /// Odtwarza dźwięk
        /// </summary>
        /// <param name="sound">Dźwięk, który ma być odtworzony</param>
        public override void PlaySound(Sound sound)
        {
            //TODO fix sound
            //soundEffect = GameSession.Manager.Load<SoundEffect>("killed.wav");
            //soundEffect.Play();
        }
        /// <summary>
        /// Zatrzymuje dźwięk
        /// </summary>
        /// <param name="sound">Dźwięk, który ma być zatrzyamny</param>
        public override void StopSound(Sound sound)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Zatrzymuje wszystkie dźwięki w aplikacji
        /// </summary>
        public override void StopAllSounds()
        {
            throw new NotImplementedException();
        }
    }
}
