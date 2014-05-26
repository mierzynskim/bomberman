#region File Description

#endregion

#region Using Statements

using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class HighScoresScreen : MenuScreen
    {
        #region Fields


        #endregion

        #region Initialization

        private Level level;
        /// <summary>
        /// Constructor.
        /// </summary>
        public HighScoresScreen()
            : base("High Scores")
        {
            // Create our menu entries.

            var menuitems = new List<MenuEntry>();


            foreach (var highScore in MonoGameFileSystem.Instance.CurrentPlayerSettings.HighScores)
            {
                var entry = new MenuEntry(highScore.Level + " " + highScore.Points.ToString(CultureInfo.InvariantCulture));
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

        #endregion
    }
}
