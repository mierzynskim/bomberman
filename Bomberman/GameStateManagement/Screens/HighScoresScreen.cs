using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Bomberman.SettingsModel;
using Bomberman.StateImplementation;
using Bomberman.StateInterfaces;
using Bomberman.Utlis;
using GameStateManagement;
using Microsoft.Xna.Framework;


namespace Bomberman.GameStateManagement.Screens
{
    public class HighScoresScreen : MenuScreen
    {


        private Level level;
        /// <summary>
        /// Constructor.
        /// </summary>
        public HighScoresScreen()
            : base("High Scores")
        {
            // Create our menu entries.

            var menuitems = new List<MenuEntry>();
            var list = MonoGameFileSystem.Instance.CurrentPlayerSettings.HighScores.OrderByDescending(score => score.Points)
                .Take(10)
                .ToList();

            foreach (var highScore in list)
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
    }
}
