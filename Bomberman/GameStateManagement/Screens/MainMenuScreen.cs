

using GameStateManagement;
using Microsoft.Xna.Framework;


namespace Bomberman.GameStateManagement.Screens
{

    class MainMenuScreen : MenuScreen
    {




        public MainMenuScreen()
            : base("Main Menu")
        {
            // Create our menu entries.
            MenuEntry playGameMenuEntry = new MenuEntry("Start");
            MenuEntry optionsMenuEntry = new MenuEntry("Options");
            MenuEntry highScoreMenuEntry = new MenuEntry("High Score");
            
            MenuEntry helpMenuEntry = new MenuEntry("Help");
            MenuEntry exitMenuEntry = new MenuEntry("Exit");

            // Hook up menu event handlers.
            playGameMenuEntry.Selected += (o, e) => ScreenManager.AddScreen(new LevelChoose(), e.PlayerIndex);
            optionsMenuEntry.Selected += (o, e) => ScreenManager.AddScreen(new OptionsMenuScreen(), e.PlayerIndex);
            highScoreMenuEntry.Selected += (o, e) => ScreenManager.AddScreen(new HighScoresScreen(), e.PlayerIndex);
            helpMenuEntry.Selected += (o, e) => ScreenManager.AddScreen(new HelpScreen(), e.PlayerIndex);
            exitMenuEntry.Selected += OnCancel;


            MenuEntries.Add(playGameMenuEntry);
            MenuEntries.Add(optionsMenuEntry);
            MenuEntries.Add(highScoreMenuEntry);
            MenuEntries.Add(helpMenuEntry);
            MenuEntries.Add(exitMenuEntry);
        }


        protected override void OnCancel(PlayerIndex playerIndex)
        {
            const string message = "Are you sure you want to exit?";

            MessageBoxScreen confirmExitMessageBox = new MessageBoxScreen(message);

            confirmExitMessageBox.Accepted += (o, e) => ScreenManager.Game.Exit();

            ScreenManager.AddScreen(confirmExitMessageBox, playerIndex);
        }

    }
}
