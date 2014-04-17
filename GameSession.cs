using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Commands;
using Bomberman.Players;
using Bomberman.Utlis;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Bomberman
{
    public class GameSession
    {
        public int Velocity { get; set; }
        public int LivesCount { get; set; }
        public DateTime GameDuration { get; set; }
        public int LevelPoints { get; set; }
        public int CurrentLevel { get; set; }
        public int OverallPoints { get; set; }
        public Level DifficultyLevel { get; set; }
        public int EnemiesLeft { get; set; }
        public int KilledEnemies { get; set; }
        public Board Board { get; set; }
        public TreasureState TreasureState { get; set; }

        public GameSession(ContentManager manager)
        {
            Board = new Board(20, 20, manager);

        }

        public void RedrawBoard(SpriteBatch target)
        {
            for (var i = 0; i < Board.Height; i++)
                for (var j = 0; j < Board.Width; j++)
                {
                    Board.Units[i, j].Draw(target);
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
            return null;
        }


    }
}
