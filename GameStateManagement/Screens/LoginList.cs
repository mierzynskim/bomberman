

using System;
using System.Collections.Generic;
using Bomberman.SettingsModel;
using Bomberman.StateImplementation;
using Bomberman.StateInterfaces;
using GameStateManagement;
using Microsoft.Xna.Framework;


namespace Bomberman.GameStateManagement.Screens
{

    public class LoginList : MenuScreen
    {
        private readonly ILoadGameStorage loadGameStorage;
        private readonly ISaveGameStorage saveGameStorage;

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoginList()
            : base("Login")
        {
            // Create our menu entries.
            loadGameStorage = new LoadGameStorage();
            saveGameStorage = new SaveGameStorage();
            var gameStorage = MonoGameFileSystem.Instance.LoadGameStorage(loadGameStorage);
            var menuitems = new List<MenuEntry>();
            foreach (var playerSettings in gameStorage.PlayerSettings)
            {
                var entry = new MenuEntry(playerSettings.Login);
                entry.Selected += Login1OnSelected;
                menuitems.Add(entry);
            }

            MenuEntry back = new MenuEntry("Back");



            back.Selected += OnCancel;

            foreach (var menuEntry in menuitems)
            {
                MenuEntries.Add(menuEntry);
            }
            MenuEntries.Add(back);
        }

        private void Login1OnSelected(object sender, PlayerIndexEventArgs playerIndexEventArgs)
        {
            var player = sender as MenuEntry;
            if (player != null) 
                MonoGameFileSystem.Instance.Login(player.Text, loadGameStorage, saveGameStorage);
            ScreenManager.AddScreen(new MainMenuScreen(), playerIndexEventArgs.PlayerIndex);
        }

    }
}
