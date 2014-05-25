#region File Description

#endregion

#region Using Statements

using System;
using System.Collections.Generic;
using Bomberman.SettingsModel;
using Bomberman.StateImplementation;
using Bomberman.StateInterfaces;
using GameStateManagement;
using Microsoft.Xna.Framework;

#endregion

namespace Bomberman.GameStateManagement.Screens
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    public class LoginList : MenuScreen
    {
        #region Fields
        private ILoadGameStorage loadGameStorage;
        private ISaveGameStorage saveGameStorage;

        #endregion

        #region Initialization


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

            //login1.Selected += Login1OnSelected;

            back.Selected += OnCancel;

            // Add entries to the menu.
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

        #endregion
    }
}
