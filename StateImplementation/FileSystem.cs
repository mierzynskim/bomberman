using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.SettingsModel;
using Bomberman.StateInterfaces;

namespace Bomberman.StateImplementation
{
    /// <summary>
    /// Klasa abstrakcyjna do operacji logowania/zapisu/odczytu
    /// </summary>
    public abstract class FileSystem
    {

        public abstract PlayerSettings Login(string login, ILoadGameStorage loadGameStorage, ISaveGameStorage saveGameStorage);

        public abstract PlayerSettings NewPlayer(string login, ILoadGameStorage loadGameStorage,
            ISaveGameStorage saveGameStorage);

        public abstract GameSession LoadGame(ILoadGameState loadType);

        public abstract void SaveGame(ISaveGameState saveType);

        public abstract GameStorage LoadGameStorage(ILoadGameStorage loadGameStorage);
        public abstract void SaveGameStorage(ISaveGameStorage saveGameStorage);
    }
}
