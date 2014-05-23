using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.StateInterfaces;

namespace Bomberman.StateImplementation
{
    public class MonoGameFileSystem: FileSystem
    {
        private static MonoGameFileSystem instance;

        public static MonoGameFileSystem Instance
        {
            get { return instance ?? (instance = new MonoGameFileSystem()); }
        }

        public override void Login(ILogin loginType)
        {
            throw new NotImplementedException();
        }

        public override void LoadGame(ILoadGameState loadType)
        {
            loadType.LoadGameState();
        }

        public override void SaveGame(ISaveGameState saveType)
        {
            saveType.Save();
        }

        public override void LoadHighScores(ILoadHighScores loadScoresType)
        {
            throw new NotImplementedException();
        }
    }
}
