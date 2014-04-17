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

        public int X { get; set; }
        public int Y { get; set; }

        public Unit(Vector2 position)
        {
            Position = position;
        }

        public State UnitState { get; set; }
        public int? PlayerId { get; set; }
        public void MoveTo(int x, int y)
        {

        }

        public void Initialize(ContentManager manager, State state)
        {
            UnitState = state;

            texture = manager.Load<Texture2D>(ResourceInfo.Resources[state]);

        }


        public void Draw(SpriteBatch target)
        {
            if (UnitState == State.Empty) return;
            target.Begin();
            target.Draw(texture, Position, Color.White);
            target.End();
        }

    }
}
