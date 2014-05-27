#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class OptionsMenuScreen : MenuScreen
    {
        private readonly MenuEntry soundsMenuEntry;

        static bool sounds = true;


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("Options")
        {

            soundsMenuEntry = new MenuEntry(string.Empty) {Text = "Sounds: " + (sounds ? "on" : "off")};

            MenuEntry back = new MenuEntry("Back");

            soundsMenuEntry.Selected += (o, e) =>
            {
                sounds = !sounds;

                soundsMenuEntry.Text = "Sounds: " + (sounds ? "on" : "off");
            };
            back.Selected += OnCancel;
            
            MenuEntries.Add(soundsMenuEntry);
            MenuEntries.Add(back);
        }




    }
}
