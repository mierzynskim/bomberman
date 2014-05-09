using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Commands;
using Bomberman.Players;
using Bomberman.Utlis;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bomberman
{
    public class GameSession
    {
        private ContentManager contentManager;
        public int Velocity { get; set; }
        public int LivesCount { get; set; }
        public DateTime GameDuration { get; set; }
        public int LevelPoints { get; set; }
        public int CurrentLevel { get; set; }
        public int OverallPoints { get; set; }
        public Level DifficultyLevel { get; set; }
        public int EnemiesLeft { get; set; }
        public int KilledEnemies { get; set; }
        public static Board GameBoard { get; private set; }
        public TreasureState TreasureState { get; set; }

        public GameSession(ContentManager manager)
        {
            contentManager = manager;
            GameBoard = new Board(20, 20, manager);

        }

        public void RedrawBoard(SpriteBatch target)
        {
            for (var i = 0; i < GameBoard.Height; i++)
                for (var j = 0; j < GameBoard.Width; j++)
                {
                    GameBoard.Units[i, j].Draw(target);
                }
        }

        public ICommand HandleInput(KeyboardState currentKeyboardState, GameActor actor)
        {
            //TODO make switch
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                return new MoveUnitCommand(Direction.Left, actor);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                return new MoveUnitCommand(Direction.Right, actor);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                return new MoveUnitCommand(Direction.Down, actor);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                return new MoveUnitCommand(Direction.Up, actor);
            }
            if (currentKeyboardState.IsKeyDown(Keys.D1))
            {
                return new PutNormalBomb(contentManager);
            }
            
            return null;
        }

        public static bool IsMoveValid(int x, int y)
        {
            return x > 0 && y > 0 && x < GameBoard.Width && y < GameBoard.Height &&
                   GameBoard.Units[x, y].UnitState != State.Wall && GameBoard.Units[x, y].UnitState != State.Concrete;

        }


    }
}
