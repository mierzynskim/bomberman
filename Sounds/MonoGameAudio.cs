using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace Bomberman.Sounds
{
    public class MonoGameAudio: Audio
    {
        SoundEffect soundEffect;
        public override void PlaySound(Sound sound)
        {
            //TODO fix sound
            //soundEffect = GameSession.Manager.Load<SoundEffect>(ResourceInfo.SoundResources[sound]);
            //soundEffect.Play();
        }

        public override void StopSound(Sound sound)
        {
            throw new NotImplementedException();
        }

        public override void StopAllSounds()
        {
            throw new NotImplementedException();
        }
    }
}
