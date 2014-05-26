#region File Description

#endregion

#region Using Statements

using System;
using System.Collections.Generic;
using Bomberman.SettingsModel;
using Bomberman.StateImplementation;
using Bomberman.StateInterfaces;
using Bomberman.Utlis;
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
    public class LevelsUnlocked : MenuScreen
    {
        #region Fields


        #endregion

        #region Initialization

        private Level level;
        /// <summary>
        /// Constructor.
        /// </summary>
        public LevelsUnlocked()
            : base("Login")
        {
            // Create our menu entries.

            var menuitems = new List<MenuEntry>();
            int stagesCount = 0;
            level = MonoGameFileSystem.Instance.CurrentPlayerSettings.Level;
            switch (MonoGameFileSystem.Instance.CurrentPlayerSettings.Level)
            {
                case Level.Easy:
                    stagesCount = MonoGameFileSystem.Instance.CurrentPlayerSettings.EasyLevelsUnlocked;
                    break;
                case Level.Medium:
                    stagesCount = MonoGameFileSystem.Instance.CurrentPlayerSettings.MediumLevelsUnlocked;
                    break;
                case Level.Hard:
                    stagesCount = MonoGameFileSystem.Instance.CurrentPlayerSettings.HardLevelsUnlocked;
                    break;
            }
            for (int i = 0; i < stagesCount; i++)
            {
                var entry = new MenuEntry((i + 1).ToString());
                entry.Selected += Login1OnSelected;
                menuitems.Add(entry);
            }

            MenuEntry back = new MenuEntry("Back");


            back.Selected += OnCancel;

            // Add entries to the menu.
            foreach (var menuEntry in menuitems)
            {
                MenuEntries.Add(menuEntry);
            }
            MenuEntries.Add(back);
        }

        private void Login1OnSelected(object sender, PlayerIndexEventArgs e)
        {
            var menuEntry = sender as MenuEntry;
            if (menuEntry != null)
            {
                MonoGameFileSystem.Instance.CurrentPlayerSettings.Stage = int.Parse(menuEntry.Text);
                LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen());
            }
        }

        #endregion
    }
}
