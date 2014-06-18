
using GameStateManagement;
using Microsoft.Xna.Framework;



namespace Bomberman.GameStateManagement.Screens
{

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
            const string message = "Are you sure you want to exit?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += ConfirmExitMessageBoxAccepted;

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }

        void ConfirmExitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            ScreenManager.Game.Exit();
        }

    }
}
