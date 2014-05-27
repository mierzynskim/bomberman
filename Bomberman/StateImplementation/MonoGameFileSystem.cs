using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.SettingsModel;
using Bomberman.StateInterfaces;
using Bomberman.Utlis;

namespace Bomberman.StateImplementation
{
    /// <summary>
    /// Klasa implementująca wzorzec singleton
    /// Służy do operacji logowania/odczytu/zapisu w frameworku MonoGame
    /// </summary>
    public class MonoGameFileSystem : FileSystem
    {
        private static MonoGameFileSystem instance;
        public PlayerSettings CurrentPlayerSettings { get; set; }

        private GameStorage currentGameStorage;

        public static MonoGameFileSystem Instance
        {
            get { return instance ?? (instance = new MonoGameFileSystem()); }
        }
        /// <summary>
        /// Loguje istniejącego gracza, który ma już login w grze
        /// </summary>
        /// <param name="login">Login gracza </param>
        /// <param name="loadGameStorage">Metoda ładowania stanu gry</param>
        /// <param name="saveGameStorage">Metoda zapisu stanu gry</param>
        /// <returns></returns>
        public override PlayerSettings Login(string login, ILoadGameStorage loadGameStorage, ISaveGameStorage saveGameStorage)
        {
            currentGameStorage = loadGameStorage.Load();
            var player = currentGameStorage.PlayerSettings.Find(settings => settings.Login == login);
            CurrentPlayerSettings = player;
            return player;
        }
        /// <summary>
        /// Loguje i zapisuje gracza, który nie miał jeszcze loginu w grze
        /// </summary>
        /// <param name="login">Login gracza </param>
        /// <param name="loadGameStorage">Metoda ładowania stanu gry</param>
        /// <param name="saveGameStorage">Metoda zapisu stanu gry</param>
        /// <returns></returns>
        public override PlayerSettings NewPlayer(string login, ILoadGameStorage loadGameStorage, ISaveGameStorage saveGameStorage)
        {
            currentGameStorage = loadGameStorage.Load();
            var player = currentGameStorage.PlayerSettings.Find(settings => settings.Login == login);
            if (player != null)
                return null;

            player = new PlayerSettings{HighScores = new List<HighScore>(), Level = Level.Easy, 
                EasyLevelsUnlocked = 1, HardLevelsUnlocked = 1, MediumLevelsUnlocked = 1, Login = login};
            currentGameStorage.PlayerSettings.Add(player);
            saveGameStorage.Save(currentGameStorage);
            CurrentPlayerSettings = player;
            return player;
        }
        /// <summary>
        /// Ładuje stan rozgrywki
        /// </summary>
        /// <param name="loadType"></param>
        /// <returns></returns>
        public override GameSession LoadGame(ILoadGameState loadType)
        {
            //return (GameSession) loadType.LoadGameState<GameSession>();
            return null;
        }

        public override void SaveGame(ISaveGameState saveType)
        {
            saveType.Save();
        }

        /// <summary>
        /// Ładuje stan gry
        /// </summary>
        /// <param name="loadGameStorage">Metoda ładowania stanu gry</param>
        /// <returns></returns>
        public override GameStorage LoadGameStorage(ILoadGameStorage loadGameStorage)
        {
            currentGameStorage = loadGameStorage.Load();
            return currentGameStorage;
        }
        /// <summary>
        /// Zapisuje stan gry
        /// </summary>
        /// <param name="saveGameStorage">Metoda zapisu stanu gry</param>
        public override void SaveGameStorage(ISaveGameStorage saveGameStorage)
        {
            var settings = currentGameStorage.PlayerSettings.Find(s => s.Login == CurrentPlayerSettings.Login);
            if (settings != null)
                currentGameStorage.PlayerSettings.Remove(settings);
            currentGameStorage.PlayerSettings.Add(CurrentPlayerSettings);
            saveGameStorage.Save(currentGameStorage);
        }
    }
}
