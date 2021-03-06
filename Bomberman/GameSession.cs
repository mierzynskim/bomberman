﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Bomberman.Commands;
using Bomberman.Players;
using Bomberman.StateImplementation;
using Bomberman.Utlis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bomberman
{
    /// <summary>
    /// Kontroler gry
    /// </summary>
    [Serializable]
    public class GameSession
    {
        private HumanPlayer humanPlayer;


        public static ContentManager Manager
        {
            get { return manager; }
        }
        [NonSerialized]
        private static ContentManager manager;
        public static Board GameBoard { get; private set; }

        public Board GameBoardSerialize { get { return GameBoard; } set { GameBoard = value; } }

        public int CurrentLevel { get; set; }

        public HumanPlayer Player { get; set; }

        public List<ComputerPlayer> ComputerPlayers { get; set; }


        public HumanPlayer HumanPlayer
        {
            get { return humanPlayer; }
            set { humanPlayer = value; }
        }

        [OnSerializing]
        internal void OnSerializedMethod(StreamingContext context)
        {
            manager = null;
        }
        public GameSession(ContentManager contentManager)
        {
            manager = contentManager;
            GameBoard = new Board(20, 20, contentManager);
            CurrentLevel = MonoGameFileSystem.Instance.CurrentPlayerSettings.Stage;
        }

        public GameSession()
        {

        }


        /// <summary>
        /// Metoda odrysowująca jednostki zgodnie z ich stanem
        /// </summary>
        /// <param name="target"></param>
        public void RedrawBoard(SpriteBatch target)
        {
            for (var i = 0; i < GameBoard.Height; i++)
                for (var j = 0; j < GameBoard.Width; j++)
                {
                    GameBoard.Units[i, j].Draw(target);
                }
        }
        /// <summary>
        /// Sprawdza obecny stan klawiatury
        /// </summary>
        /// <param name="currentKeyboardState">Stan klawiatury</param>
        /// <returns>Komenda związana z daną kombinacja klawiszy</returns>
        public ICommand HandleInput(KeyboardState currentKeyboardState)
        {
            if (currentKeyboardState.GetPressedKeys().Contains(Keys.Up) && currentKeyboardState.GetPressedKeys().Contains(Keys.D3))
                return new GloveAction(Manager, Direction.Up);
            if (currentKeyboardState.IsKeyDown(Keys.D3) && currentKeyboardState.IsKeyDown(Keys.Down))
                return new GloveAction(Manager, Direction.Down);
            if (currentKeyboardState.IsKeyDown(Keys.D3) && currentKeyboardState.IsKeyDown(Keys.Right))
                return new GloveAction(Manager, Direction.Right);
            if (currentKeyboardState.IsKeyDown(Keys.D3) && currentKeyboardState.IsKeyDown(Keys.Left))
                return new GloveAction(Manager, Direction.Left);

            if (currentKeyboardState.IsKeyDown(Keys.Left))
                return new MoveUnitCommand(Direction.Left, Player);
            if (currentKeyboardState.IsKeyDown(Keys.Right))
                return new MoveUnitCommand(Direction.Right, Player);
            if (currentKeyboardState.IsKeyDown(Keys.Up))
                return new MoveUnitCommand(Direction.Up, Player);
            if (currentKeyboardState.IsKeyDown(Keys.Down))
                return new MoveUnitCommand(Direction.Down, Player);
            if (currentKeyboardState.IsKeyDown(Keys.D1))
                return new PutNormalBomb(Manager, humanPlayer.CurrentUnit.X, humanPlayer.CurrentUnit.Y, false);
            if (currentKeyboardState.IsKeyDown(Keys.D2))
                return new PutRemoteBomb(Manager);
            if (currentKeyboardState.IsKeyDown(Keys.Space))
                return new RemoteBombFire(Manager);


            return null;
        }
        /// <summary>
        /// Sprawdza czy dany ruch jest możliwy do wykonania
        /// </summary>
        /// <param name="x">Pozycja x na planszy</param>
        /// <param name="y">Pozycja y na planszy</param>
        /// <returns></returns>
        public static bool IsMoveValid(int x, int y)
        {
            return x > 0 && y > 0 && x < GameBoard.Width && y < GameBoard.Height &&
                   GameBoard.Units[x, y].UnitState != State.Wall && GameBoard.Units[x, y].UnitState != State.Concrete
                   && GameBoard.Units[x, y].UnitState != State.Enemy && GameBoard.Units[x, y].UnitState != State.Player;

        }
        /// <summary>
        /// Usuwa zabitych graczy z planszy
        /// </summary>
        public void CheckForKilled()
        {
            for (int i = 0; i < ComputerPlayers.Count; i++)
            {
                if (ComputerPlayers[i].CurrentUnit.UnitState == State.Empty)
                {
                    HumanPlayer.LevelPoints += LevelConsts.LevelProperties[MonoGameFileSystem.Instance.CurrentPlayerSettings.Level].EnemyKilledPoints;
                    ComputerPlayers.RemoveAt(i);
                }
            }
        }
        /// <summary>
        /// Sprawdza czy zmienił się poziom gry
        /// </summary>
        /// <param name="stage">Obecny poziom gry</param>
        /// <returns></returns>
        public bool CheckLevelChanged(int stage)
        {
            return stage != CurrentLevel;
        }
    }
}
