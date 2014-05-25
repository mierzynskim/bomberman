#region File Description

#endregion

#region Using Statements

using System;
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
    public class LevelChoose : MenuScreen
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public LevelChoose()
            : base("Login")
        {
            // Create our menu entries.
            MenuEntry easyLevel = new MenuEntry("Easy");
            MenuEntry mediumLevel = new MenuEntry("Medium");
            MenuEntry hardLevel = new MenuEntry("Hard");


            MenuEntry back = new MenuEntry("Back");

            easyLevel.Selected += EasyLevelOnSelected;
            mediumLevel.Selected += MediumLevelOnSelected;
            hardLevel.Selected += HardLevelOnSelected;

            back.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(easyLevel);
            MenuEntries.Add(mediumLevel);
            MenuEntries.Add(back);
        }

        private void EasyLevelOnSelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new EasyLevelsUnlocked(), e.PlayerIndex);
        }
        private void MediumLevelOnSelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new EasyLevelsUnlocked(), e.PlayerIndex);
        }
        private void HardLevelOnSelected(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.AddScreen(new EasyLevelsUnlocked(), e.PlayerIndex);
        }



        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to exit this sample?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }


        /// <summary>
        /// Event handler for when the user selects ok on the "are you sure
        /// you want to exit" message box.
        /// </summary>
        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }

    }
}
