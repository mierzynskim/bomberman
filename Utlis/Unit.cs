using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Priority_Queue;

namespace Bomberman.Utlis
{
    public class Unit : PriorityQueueNode
    {
        private Vector2 Position { get; set; }

        private Texture2D texture;
        private Texture2D overlappedTexture;

        private readonly static Random Rnd = new Random();

        private static bool exitVisible;


        public int X { get; set; }
        public int Y { get; set; }

        public Unit Parent { get; set; }

        public State UnitState { get; set; }

        public State OverlappedState { get; set; }


        public Unit(Vector2 position)
        {
            Position = position;
        }

        private Unit()
        {

        }

        //TODO remove actor dependency?
        public void MoveTo(int x, int y, GameActor actor)
        {
            //TODO make transparent overlapping sprites
            if (GameSession.IsMoveValid(x, y))
            {
                IsTreasure(x, y, actor);
                GameSession.GameBoard.Units[X, Y] = Clone();
                Position = new Vector2(33 * x, 33 * y);
                X = x;
                Y = y;
                var previousUnit = GameSession.GameBoard.Units[X, Y];

                if (previousUnit.UnitState == State.NormalBomb || previousUnit.UnitState == State.RemoteBomb)
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
            var unit = new Unit { X = this.X, Y = this.Y, UnitState = OverlappedState, texture = overlappedTexture, Position = this.Position };
            return unit;
        }

        public void Initialize(ContentManager manager, State state)
        {

            if (state == State.NormalBomb)
            {
                OverlappedState = state;
                overlappedTexture = manager.Load<Texture2D>(ResourceInfo.ImageResources[state]);
            }
            else if (state == State.Empty)
                UnitState = state;
            else
            {
                UnitState = state;
                texture = manager.Load<Texture2D>(ResourceInfo.ImageResources[state]);
            }

        }

        private void IsTreasure(int x, int y, GameActor actor)
        {
            switch (GameSession.GameBoard.Units[x, y].UnitState)
            {
                case State.Fire:
                    actor.TreasureState.IsFlame = true;
                    break;
                case State.Glove:
                    actor.TreasureState.GlovesCount++;
                    break;
                case State.RollerSkates:
                    actor.TreasureState.IsRollerSkates = true;
                    break;
            }
        }

        public void ResetState()
        {
            OverlappedState = State.Empty;
            overlappedTexture = null;
            texture = null;
            UnitState = State.Empty;
        }

        public void PlaceTreasure(ContentManager manager)
        {
            int probability = 4;
            State unitState = State.Empty;
            switch (Rnd.Next(3))
            {
                case 0:
                    unitState = State.Fire;
                    break;
                case 1:
                    if (!exitVisible)
                    {
                        unitState = State.Exit;
                        exitVisible = true;
                    }
                    break;
                case 2:
                    unitState = State.Glove;
                    break;
                case 3:
                    unitState = State.RollerSkates;
                    break;
            }
            if (unitState != State.Empty)
                texture = manager.Load<Texture2D>(ResourceInfo.ImageResources[unitState]);
            UnitState = unitState;
            OverlappedState = State.Empty;
            overlappedTexture = null;
        }
        public void Draw(SpriteBatch target)
        {
            if (UnitState == State.Empty) return;
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
