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
    public class EasyLevelsUnlocked : MenuScreen
    {
        #region Fields


        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public EasyLevelsUnlocked()
            : base("Login")
        {
            // Create our menu entries.

            var menuitems = new List<MenuEntry>();
            for (int i = 0; i < MonoGameFileSystem.Instance.CurrentPlayerSettings.EasyLevelsUnlocked; i++)
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
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen());
        }

        #endregion
    }
}
