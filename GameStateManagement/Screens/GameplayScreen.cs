#region File Description
//-----------------------------------------------------------------------------
// GameplayScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Collections.Generic;
using System.Threading;
using Bomberman;
using Bomberman.Algorithms;
using Bomberman.Players;
using Bomberman.StateImplementation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

#endregion

namespace GameStateManagement
{
    /// <summary>
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class GameplayScreen : GameScreen
    {
        #region Fields

        private ContentManager content;
        private float pauseAlpha;
        private GameSession gameController;
        private Texture2D grayRectangle;

        private static TimeSpan timer;
        private static TimeSpan prevTimer = new TimeSpan(0, 0, 0);

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public GameplayScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            var loadGame = new LoadGame();
            MonoGameFileSystem.Instance.LoadGame(loadGame);
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");

            //TODO refactor?
            var list = new List<ComputerPlayer> { new ComputerPlayer(new AStarAlgorithm()), new ComputerPlayer(new RandomMove()) };
            gameController = new GameSession(ScreenManager.Game.Content) {HumanPlayer = new HumanPlayer(), ComputerPlayers = list};
            
            GameSession.GameBoard.AddPlayer(gameController.HumanPlayer);
            
            gameController.HumanPlayer.GameOver += GameOver;
            foreach (var computerPlayer in gameController.ComputerPlayers)
            {
                GameSession.GameBoard.AddComputerPlayer(computerPlayer);
                gameController.HumanPlayer.Changed += computerPlayer.PlayerChangedPosition;
            }

            grayRectangle = new Texture2D(ScreenManager.GraphicsDevice, 1, 1);
            grayRectangle.SetData(new[] { Color.Gray });

            ScreenManager.Game.ResetElapsedTime();
        }


        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            var savegame = new SaveGame(gameController);
            MonoGameFileSystem.Instance.SaveGame(savegame);
            content.Unload();
        }


        #endregion

        #region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            // Gradually fade in or out depending on whether we are covered by the pause screen.
            if (coveredByOtherScreen)
                pauseAlpha = Math.Min(pauseAlpha + 1f / 32, 1);
            else
                pauseAlpha = Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                var humanPlayerCommand = gameController.HandleInput(Keyboard.GetState());
                gameController.HumanPlayer.Time = gameTime;
                if (humanPlayerCommand != null)
                    humanPlayerCommand.Execute(gameController.HumanPlayer);

                foreach (var computerPlayer in gameController.ComputerPlayers)
                {
                    computerPlayer.MakeMove();
                }

            }
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {

            timer = gameTime.TotalGameTime;
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.Green, 0, 0);
            ScreenManager.SpriteBatch.Begin();
            string levelText = String.Format("Level {0}", 1);
            string timerText = String.Format("Time: {0}", (int)gameTime.TotalGameTime.TotalSeconds - prevTimer.TotalSeconds);
            string pointsText = gameController.HumanPlayer.LevelPoints.ToString("D2");
            string enemiesText = String.Format("LEFT: {0}", gameController.ComputerPlayers.Count);
            Vector2 strSize = ScreenManager.Font.MeasureString(levelText);
            Vector2 strSize2 = ScreenManager.Font.MeasureString(timerText);
            Vector2 strSize3 = ScreenManager.Font.MeasureString(pointsText);

            Vector2 strLoc = new Vector2(0, Game1.WindowWidth);
            Vector2 timerLoc = new Vector2((int)strSize.X + 10, Game1.WindowWidth);
            Vector2 pointsLoc = new Vector2((int)strSize.X + strSize2.X + 50, Game1.WindowWidth);
            Vector2 enemiesLoc = new Vector2((int)strSize.X + strSize2.X + strSize3.X + 80, Game1.WindowWidth);
            //strLoc.X -= strSize.X / 2;
            //strLoc.Y -= strSize.Y / 2;
            ScreenManager.SpriteBatch.Draw(grayRectangle, new Rectangle(0, Game1.WindowWidth, Game1.WindowWidth, 66), Color.LightGray);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, levelText, strLoc, Color.White);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, timerText, timerLoc, Color.White);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, pointsText, pointsLoc, Color.White);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, enemiesText, enemiesLoc, Color.White);

            ScreenManager.SpriteBatch.End();
            gameController.RedrawBoard(ScreenManager.SpriteBatch);

        }

        public void GameOver(object sender, PlayerIndexEventArgs e)
        {
            const string message = "GAME OVER.\n Press Enter to retry. \n Press Esc to return to main menu";

            MessageBoxScreen gameOverMessageBox = new MessageBoxScreen(message);

            gameOverMessageBox.Accepted += GameOverMessageBoxOnAccepted;
            gameOverMessageBox.Cancelled += ConfirmQuitMessageBoxAccepted;

            ScreenManager.AddScreen(gameOverMessageBox, ControllingPlayer);
        }

        private void GameOverMessageBoxOnAccepted(object sender, PlayerIndexEventArgs playerIndexEventArgs)
        {
            ScreenManager.Game.ResetElapsedTime();
            LoadContent();
            prevTimer = new TimeSpan(0, 0, (int)timer.TotalSeconds);
        }

        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
            ExitScreen();
        }

        #endregion
    }
}
