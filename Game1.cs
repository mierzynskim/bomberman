#region Using Statements

using Bomberman.GameStateManagement.Screens;
using Bomberman.Sounds;
using GameStateManagement;
using Microsoft.Xna.Framework;

#endregion

namespace Bomberman
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {


        public static int WindowHeight { get; set; }
        public static int WindowWidth { get; set; }

        public static GraphicsDeviceManager Graphics { get; set; }


        public Game1()
            : base()
        {
            
            WindowHeight = 33 * 22;
            WindowWidth = 33 * 20;
            Graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferHeight = WindowHeight,
                PreferredBackBufferWidth = WindowWidth
            };

            Graphics.ApplyChanges();
            Content.RootDirectory = @"Content";

            MonoGameAudio audio = new MonoGameAudio();
            ServiceLocator.Provide(audio);

            ScreenManager screenManager = new ScreenManager(this);

            Components.Add(screenManager);

            screenManager.AddScreen(new BackgroundScreen(), null);
            screenManager.AddScreen(new LoginScreen(), null);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Green);


            base.Draw(gameTime);
        }
    }
}
