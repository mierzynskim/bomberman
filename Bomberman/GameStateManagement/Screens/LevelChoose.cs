

using System;
using Bomberman.Sounds;
using Bomberman.StateImplementation;
using Bomberman.Utlis;
using GameStateManagement;
using Microsoft.Xna.Framework;


namespace Bomberman.GameStateManagement.Screens
{

    public class LevelChoose : MenuScreen
    {

        /// <summary>
        /// Constructor.
        /// </summary>
        public LevelChoose()
            : base("Choose level")
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
            MenuEntries.Add(hardLevel);
            MenuEntries.Add(back);
        }

        private void EasyLevelOnSelected(object sender, PlayerIndexEventArgs e)
        {
            MonoGameFileSystem.Instance.CurrentPlayerSettings.Level = Level.Easy;
            ScreenManager.AddScreen(new LevelsUnlocked(), e.PlayerIndex);
        }
        private void MediumLevelOnSelected(object sender, PlayerIndexEventArgs e)
        {
            MonoGameFileSystem.Instance.CurrentPlayerSettings.Level = Level.Medium;
            ScreenManager.AddScreen(new LevelsUnlocked(), e.PlayerIndex);
        }
        private void HardLevelOnSelected(object sender, PlayerIndexEventArgs e)
        {
            MonoGameFileSystem.Instance.CurrentPlayerSettings.Level = Level.Hard;
            ScreenManager.AddScreen(new LevelsUnlocked(), e.PlayerIndex);
        }


    }
}
