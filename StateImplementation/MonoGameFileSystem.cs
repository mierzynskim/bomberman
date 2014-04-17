using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.StateInterfaces;

namespace Bomberman.StateImplementation
{
    public class MonoGameFileSystem: FileSystem
    {
        private static readonly MonoGameFileSystem instance;

        public static MonoGameFileSystem Instance
        {
            get { return instance; }
        }
        protected override void Login(ILogin loginType)
        {
            throw new NotImplementedException();
        }

        protected override void LoadGame(ILoadGameState loadType)
        {
            throw new NotImplementedException();
        }

        protected override void SaveGame(ISaveGameState saveType)
        {
            throw new NotImplementedException();
        }

        protected override void LoadHighScores(ILoadHighScores loadScoresType)
        {
            throw new NotImplementedException();
        }
    }
}
