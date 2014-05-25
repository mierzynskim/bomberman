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
    public class LoginScreen : MenuScreen
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public LoginScreen()
            : base("Login")
        {
            // Create our menu entries.
            MenuEntry newPlayer = new MenuEntry("New Player");
            MenuEntry login = new MenuEntry("Login");


            MenuEntry back = new MenuEntry("Exit");

            newPlayer.Selected += NewPlayerOnSelected;
            login.Selected +=  LoginOnSelected;

            back.Selected += OnCancel;

            // Add entries to the menu.
            MenuEntries.Add(newPlayer);
            MenuEntries.Add(login);
            MenuEntries.Add(back);
        }

        private void NewPlayerOnSelected(object sender, PlayerIndexEventArgs playerIndexEventArgs)
        {
            ScreenManager.AddScreen(new InputNewPlayer(), playerIndexEventArgs.PlayerIndex);

        }

        private void LoginOnSelected(object sender, PlayerIndexEventArgs playerIndexEventArgs)
        {
            ScreenManager.AddScreen(new LoginList(), playerIndexEventArgs.PlayerIndex);
            
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
