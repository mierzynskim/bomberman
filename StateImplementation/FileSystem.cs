using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.StateInterfaces;

namespace Bomberman.StateImplementation
{
    public abstract class FileSystem
    {

        protected abstract void Login(ILogin loginType);

        protected abstract void LoadGame(ILoadGameState loadType);

        protected abstract void SaveGame(ISaveGameState saveType);

        protected abstract void LoadHighScores(ILoadHighScores loadScoresType);
    }
}
