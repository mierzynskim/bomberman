using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bomberman.Utlis
{
    public class Unit
    {
        private Vector2 Position { get; set; }

        private Texture2D texture;
        private Texture2D overlappedTexture;
        

        public int X { get; set; }
        public int Y { get; set; }
        

        public Unit(Vector2 position)
        {
            Position = position;
        }

        private Unit()
        {

        }

        public State UnitState { get; set; }

        public State OverlappedState { get; set; }
        public int? PlayerId { get; set; }
        public void MoveTo(int x, int y)
        {
            //TODO make transparent overlapping sprites
            if (GameSession.IsMoveValid(x, y))
            {
                GameSession.GameBoard.Units[X, Y] = Clone();
                Position = new Vector2(33 * x, 33 * y);
                X = x;
                Y = y;
                var previousUnit = GameSession.GameBoard.Units[X, Y];
                
                if (previousUnit.UnitState == State.NormalBomb)
                {
                    OverlappedState = previousUnit.UnitState;
                    overlappedTexture = previousUnit.texture;
                }
                else
                {
                    OverlappedState = State.Empty;
                    overlappedTexture = null;
    
                }
                GameSession.GameBoard.Units[X, Y] = this;
                
            }

        }

        private Unit Clone()
        {
            var unit = new Unit {X = this.X, Y = this.Y, UnitState = OverlappedState, texture = overlappedTexture, Position = this.Position};
            return unit;
        }

        public void Initialize(ContentManager manager, State state)
        {

            if (state == State.NormalBomb)
            {
                OverlappedState = state;
                overlappedTexture = manager.Load<Texture2D>(ResourceInfo.Resources[state]);
            }
            else
            {
                UnitState = state;
                texture = manager.Load<Texture2D>(ResourceInfo.Resources[state]);
            }

        }

        public void Draw(SpriteBatch target)
        {
            if (UnitState == State.Empty ) return;
            if (OverlappedState == State.NormalBomb && UnitState != State.Player)
            {
                target.Begin();

                target.Draw(overlappedTexture, Position, Color.White);
                target.End();
            }
            else
            {
                target.Begin();
                target.Draw(texture, Position, Color.White);
                target.End();
            }

        }

    }
}
