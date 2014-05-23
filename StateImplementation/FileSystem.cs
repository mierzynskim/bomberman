using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.StateInterfaces;

namespace Bomberman.StateImplementation
{
    public abstract class FileSystem
    {

        public abstract void Login(ILogin loginType);

        public abstract void LoadGame(ILoadGameState loadType);

        public abstract void SaveGame(ISaveGameState saveType);

        public abstract void LoadHighScores(ILoadHighScores loadScoresType);
    }
}
