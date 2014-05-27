using System;
using System.Collections.Generic;
using Bomberman.Algorithms;
using Bomberman.Players;
using Bomberman.SettingsModel;
using Bomberman.StateImplementation;
using Bomberman.Utlis;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bomberman.GameStateManagement.Screens
{
    /// <summary>
    /// G³ówny ekran gry
    /// </summary>
    class GameplayScreen : GameScreen
    {


        private ContentManager content;
        private float pauseAlpha;
        private GameSession gameController;
        private Texture2D grayRectangle;



        private double prevSeconds;
        private Level level;
        private double seconds;
        public static bool IsPaused { private get; set; }

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
            seconds = 0;
            level = MonoGameFileSystem.Instance.CurrentPlayerSettings.Level;
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");
            var previousSession = MonoGameFileSystem.Instance.LoadGame(new LoadGame());

            if (previousSession != null)
                gameController = previousSession;
            else
            {
                //TODO refactor?
                var list = new List<ComputerPlayer>();
                for (int i = 0; i < LevelConsts.LevelProperties[level].EnemiesCount; i++)
                {
                    if (MonoGameFileSystem.Instance.CurrentPlayerSettings.Stage <= i)
                        list.Add(new ComputerPlayer(new RandomMove()));
                    else
                        list.Add(new ComputerPlayer(new AStarAlgorithm()));
                }
                gameController = new GameSession(ScreenManager.Game.Content) { HumanPlayer = new HumanPlayer(), ComputerPlayers = list };

                GameSession.GameBoard.AddPlayer(gameController.HumanPlayer);

                gameController.HumanPlayer.GameOver += GameOver;
                foreach (var computerPlayer in gameController.ComputerPlayers)
                {
                    GameSession.GameBoard.AddComputerPlayer(computerPlayer);
                    gameController.HumanPlayer.Changed += computerPlayer.PlayerChangedPosition;
                }
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
            //var savegame = new SaveGame(gameController);
            //MonoGameFileSystem.Instance.SaveGame(savegame);
            MonoGameFileSystem.Instance.SaveGameStorage(new SaveGameStorage());
            content.Unload();
        }

        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, false);

            pauseAlpha = coveredByOtherScreen ? Math.Min(pauseAlpha + 1f / 32, 1) : Math.Max(pauseAlpha - 1f / 32, 0);

            if (IsActive)
            {
                var humanPlayerCommand = gameController.HandleInput(Keyboard.GetState());
                if (humanPlayerCommand != null)
                    humanPlayerCommand.Execute(gameController.HumanPlayer);

                foreach (var computerPlayer in gameController.ComputerPlayers)
                {
                    computerPlayer.MakeMove();
                }
                gameController.CheckForKilled();
                if (gameController.CheckLevelChanged(MonoGameFileSystem.Instance.CurrentPlayerSettings.Stage))
                    LevelUp();

            }
        }

        private void LevelUp()
        {
            IsPaused = true;
            gameController.HumanPlayer.OverallPoints += gameController.HumanPlayer.LevelPoints;
            string message = String.Format("LEVEL UP!\nEnemies killed: {0}\nBombs left: {1}\nLevel points: {2}\nPlayer points: {3}",
                LevelConsts.LevelProperties[level].EnemiesCount - gameController.ComputerPlayers.Count,
                gameController.HumanPlayer.TreasureState.BombsCount,
                gameController.HumanPlayer.LevelPoints,
                gameController.HumanPlayer.OverallPoints);
            MonoGameFileSystem.Instance.CurrentPlayerSettings.HighScores.Add(new HighScore { Level = level, Points = gameController.HumanPlayer.OverallPoints });
            MessageBoxScreen levelupScreen = new MessageBoxScreen(message);
            levelupScreen.Accepted +=
                (sender, e) => LoadingScreen.Load(ScreenManager, true, e.PlayerIndex, new GameplayScreen());
            ScreenManager.AddScreen(levelupScreen, ControllingPlayer);
        }


        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            if (ControllingPlayer == null) return;
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];


            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                ScreenManager.AddScreen(new PauseMenuScreen(), ControllingPlayer);
                IsPaused = true;
            }
        }


        /// <summary>
        /// Draws the gameplay screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            if (!IsPaused)
            {
                seconds += gameTime.ElapsedGameTime.TotalSeconds;
                if ((int)seconds - (int)prevSeconds == 1)
                    gameController.HumanPlayer.LevelPoints -=
                        LevelConsts.LevelProperties[level].DurationPenalty;

                prevSeconds = seconds;
                gameController.HumanPlayer.LevelPoints = Math.Abs(gameController.HumanPlayer.LevelPoints);
            }
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target, Color.Green, 0, 0);
            ScreenManager.SpriteBatch.Begin();
            string levelText = String.Format("Level {0}", MonoGameFileSystem.Instance.CurrentPlayerSettings.Stage);

            string timerText = String.Format("Time: {0}", (int)seconds);


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
            ScreenManager.SpriteBatch.Draw(grayRectangle, new Rectangle(0, Game1.WindowWidth, Game1.WindowWidth, 66),
                Color.LightGray);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, levelText, strLoc, Color.White);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, timerText, timerLoc, Color.White);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, pointsText, pointsLoc, Color.White);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, enemiesText, enemiesLoc, Color.White);

            ScreenManager.SpriteBatch.End();

            gameController.RedrawBoard(ScreenManager.SpriteBatch);

        }

        private void GameOver(object sender, PlayerIndexEventArgs e)
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
        }

        void ConfirmQuitMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(), new MainMenuScreen());
            ExitScreen();
        }

    }
}
