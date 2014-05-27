using System;
using Bomberman.SettingsModel;
using Bomberman.StateImplementation;
using Bomberman.StateInterfaces;
using GameStateManagement;
using Microsoft.Xna.Framework;



namespace Bomberman.GameStateManagement.Screens
{
    public class InputNewPlayer : MenuScreen
    {


        private int delay;

        private readonly MenuEntry login1;
        private ILoadGameStorage loadGameStorage;
        private ISaveGameStorage saveGameStorage;


        /// <summary>
        /// Constructor.
        /// </summary>
        public InputNewPlayer()
            : base("Please type your login")
        {
            // Create our menu entries.
            login1 = new MenuEntry("");

            MenuEntry back = new MenuEntry("Back");

            login1.Selected += Login1OnSelected;

            back.Selected += OnCancel;

            MenuEntries.Add(login1);

            MenuEntries.Add(back);
        }

        private void Login1OnSelected(object sender, PlayerIndexEventArgs playerIndexEventArgs)
        {
            loadGameStorage = new LoadGameStorage();
            saveGameStorage = new SaveGameStorage();
            if (MonoGameFileSystem.Instance.NewPlayer(login1.Text, loadGameStorage, saveGameStorage) != null)
                ScreenManager.AddScreen(new MainMenuScreen(), playerIndexEventArgs.PlayerIndex);
        }

        public override void HandleInput(InputState input)
        {
            base.HandleInput(input);
            var state = input.LastKeyboardStates;
            if (state != null && delay == 0 && state[0].GetPressedKeys().Length > 0 )
            {
                if (state[0].GetPressedKeys()[0].ToString().Equals("Back") && login1.Text.Length > 0)
                {
                    login1.Text = login1.Text.Remove(login1.Text.Length - 1);
                }
                else if (state[0].GetPressedKeys()[0].ToString().Length == 1)
                {
                    login1.Text += state[0].GetPressedKeys()[0].ToString();
                }

            }
            delay = (delay + 1) % 6;

        }

    }
}
