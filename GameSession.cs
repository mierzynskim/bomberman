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
        private HumanPlayer humanPlayer;

        public static ContentManager Manager { get; set; }
        public static Board GameBoard { get; private set; }

        public HumanPlayer Player { get; set; }

        public List<ComputerPlayer> ComputerPlayers { get; set; }

        public HumanPlayer HumanPlayer
        {
            get { return humanPlayer; }
            set { humanPlayer = value; }
        }

        public GameSession(ContentManager manager)
        {
            Manager = manager;
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

        public ICommand HandleInput(KeyboardState currentKeyboardState)
        {
            //TODO make switch
            if (currentKeyboardState.IsKeyDown(Keys.Left))
            {
                return new MoveUnitCommand(Direction.Left, Player);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Right))
            {
                return new MoveUnitCommand(Direction.Right, Player);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Up))
            {
                return new MoveUnitCommand(Direction.Up, Player);
            }
            if (currentKeyboardState.IsKeyDown(Keys.Down))
            {
                return new MoveUnitCommand(Direction.Down, Player);
            }
            if (currentKeyboardState.IsKeyDown(Keys.D1))
            {
                return new PutNormalBomb(Manager);
            }

            
            return null;
        }

        public static bool IsMoveValid(int x, int y)
        {
            return x > 0 && y > 0 && x < GameBoard.Width && y < GameBoard.Height &&
                   GameBoard.Units[x, y].UnitState != State.Wall && GameBoard.Units[x, y].UnitState != State.Concrete 
                   && GameBoard.Units[x, y].UnitState != State.Enemy && GameBoard.Units[x, y].UnitState != State.Player;

        }



    }
}
