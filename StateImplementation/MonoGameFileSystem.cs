using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.SettingsModel;
using Bomberman.StateInterfaces;
using Bomberman.Utlis;

namespace Bomberman.StateImplementation
{
    public class MonoGameFileSystem : FileSystem
    {
        private static MonoGameFileSystem instance;
        public PlayerSettings CurrentPlayerSettings { get; set; }

        public static MonoGameFileSystem Instance
        {
            get { return instance ?? (instance = new MonoGameFileSystem()); }
        }

        public override PlayerSettings Login(string login, ILoadGameStorage loadGameStorage, ISaveGameStorage saveGameStorage)
        {
            var gameStorage = loadGameStorage.Load();
            var player = gameStorage.PlayerSettings.Find(settings => settings.Login == login);
            CurrentPlayerSettings = player;
            return player;
        }

        public override PlayerSettings NewPlayer(string login, ILoadGameStorage loadGameStorage, ISaveGameStorage saveGameStorage)
        {
            var gameStorage = loadGameStorage.Load();
            var player = gameStorage.PlayerSettings.Find(settings => settings.Login == login);
            if (player != null)
                return null;
            player = new PlayerSettings{HighScores = new List<HighScore>(), Level = Level.Easy, 
                EasyLevelsUnlocked = 1, HardLevelsUnlocked = 1, MediumLevelsUnlocked = 1, Login = login};
            gameStorage.PlayerSettings.Add(player);
            saveGameStorage.Save(gameStorage);
            CurrentPlayerSettings = player;
            return player;
        }

        public override GameSession LoadGame(ILoadGameState loadType)
        {
            //return (GameSession) loadType.LoadGameState<GameSession>();
            return null;
        }

        public override void SaveGame(ISaveGameState saveType)
        {
            saveType.Save();
        }

        public override void LoadHighScores(ILoadHighScores loadScoresType)
        {
            throw new NotImplementedException();
        }

        public override GameStorage LoadGameStorage(ILoadGameStorage loadGameStorage)
        {
            return loadGameStorage.Load();
        }

        public override void SaveGameStorage(ISaveGameStorage saveGameStorage, GameStorage gameStorage)
        {
            saveGameStorage.Save(gameStorage);
        }
    }
}
