#region Using Statements
using System;
using System.Collections.Generic;
using Bomberman.Commands;
using Bomberman.Players;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace Bomberman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {

        private readonly int windowHeight = 33 * 20;
        private readonly int windowWidth = 33 * 20;

        public Game1()
            : base()
        {
            GraphicsDeviceManager graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = windowHeight,
                PreferredBackBufferWidth = windowWidth
            };
            graphics.ApplyChanges();
            Content.RootDirectory = "Content";

            ScreenManager screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new MainMenuScreen(), null);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);
            // TODO: Add your drawing code here
            //

            base.Draw(gameTime);
        }
    }
}
