#region File Description

#endregion

#region Using Statements

using System;
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
    public class InputNewPlayer : MenuScreen
    {
        #region Fields

        static string[] languages = { "C#", "French", "Deoxyribonucleic acid" };
        static int currentLanguage = 0;

        static bool frobnicate = true;

        private int Delay = 0;

        private MenuEntry login1;
        private ILoadGameStorage loadGameStorage;
        private ISaveGameStorage saveGameStorage;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public InputNewPlayer()
            : base("Login")
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
            if (state != null && Delay == 0 && state[0].GetPressedKeys().Length > 0 )
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
            Delay = (Delay + 1) % 6;

        }

        #endregion
    }
}
