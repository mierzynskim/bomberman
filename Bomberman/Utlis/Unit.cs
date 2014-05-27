using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.Serialization;
using System.Text;
using Bomberman.Commands;
using Bomberman.Players;
using Bomberman.StateImplementation;
using GameStateManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Priority_Queue;

namespace Bomberman.Utlis
{
    /// <summary>
    /// Klasa jednostki gry. Jest używana również jako obiekt w kolejce priorytetowej.
    /// </summary>
    [Serializable]
    public class Unit : PriorityQueueNode
    {

        private Vector2 Position { get; set; }

        private Texture2D texture;
        private Texture2D overlappedTexture;

        private readonly static Random Rnd = new Random();

        private static bool exitVisible;
        private static int emptyUnits;

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


        /// <summary>
        /// Metoda odpowiadająca za poruszanie się jednostki
        /// </summary>
        /// <param name="x">Współrzędna x na planszy</param>
        /// <param name="y">Współrzędna y na planszy</param>
        /// <param name="actor">Uczestnik gry</param>
        public void MoveTo(int x, int y, GameActor actor)
        {
            //TODO make transparent overlapping sprites
            if (GameSession.IsMoveValid(x, y) && actor.CurrentUnit.UnitState != State.Empty)
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
        /// <summary>
        /// Kopiuje daną jednostkę
        /// </summary>
        /// <returns>Kopia jednostki</returns>
        private Unit Clone()
        {
            var unit = new Unit { X = this.X, Y = this.Y, UnitState = OverlappedState, texture = overlappedTexture, Position = this.Position };
            return unit;
        }
        /// <summary>
        /// Nadanie jednostce stanu i odpowiadającej mu tekstury
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="state">Stan do nadania</param>
        /// <param name="actorOn">Zmienna sprawdzająca czy aktor gry jest na danej jednostce</param>
        public void Initialize(ContentManager manager, State state, bool actorOn = true)
        {
            if (state == State.NormalBomb && actorOn)
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
        /// <summary>
        /// Sprawdza czy na danej jednostce nie znajduje się skarb
        /// </summary>
        /// <param name="x">Współrzędna x na planszy</param>
        /// <param name="y">Współrzędna y na planszy</param>
        /// <param name="actor">Uczestnik gry</param>
        private void IsTreasure(int x, int y, GameActor actor)
        {
            switch (GameSession.GameBoard.Units[x, y].UnitState)
            {
                case State.Fire:
                    var fireCommand = new FireCommand();
                    fireCommand.Execute(actor);
                    actor.TreasureState.IsFlame = true;
                    actor.LevelPoints += LevelConsts.LevelProperties[MonoGameFileSystem.Instance.CurrentPlayerSettings.Level].TreasureFoundPoints;
                    break;
                case State.Glove:
                    actor.TreasureState.GlovesCount++;
                    actor.LevelPoints += LevelConsts.LevelProperties[MonoGameFileSystem.Instance.CurrentPlayerSettings.Level].TreasureFoundPoints;
                    break;
                case State.RollerSkates:
                    actor.TreasureState.IsRollerSkates = true;
                    var command = new RollerSkatesCommand(GameSession.Manager);
                    command.Execute(actor);

                    break;
                case State.NewRemoteBomb:
                    actor.LevelPoints += LevelConsts.LevelProperties[MonoGameFileSystem.Instance.CurrentPlayerSettings.Level].TreasureFoundPoints;
                    actor.TreasureState.RemoteBombsCount++;
                    break;
                case State.Exit:
                    if (actor is HumanPlayer)
                    {
                        switch (MonoGameFileSystem.Instance.CurrentPlayerSettings.Level)
                        {
                            case Level.Easy:
                                if (MonoGameFileSystem.Instance.CurrentPlayerSettings.EasyLevelsUnlocked <=
                                    MonoGameFileSystem.Instance.CurrentPlayerSettings.Stage)
                                    MonoGameFileSystem.Instance.CurrentPlayerSettings.EasyLevelsUnlocked++;
                                break;
                            case Level.Medium:
                                if (MonoGameFileSystem.Instance.CurrentPlayerSettings.MediumLevelsUnlocked <=
                                    MonoGameFileSystem.Instance.CurrentPlayerSettings.Stage)
                                    MonoGameFileSystem.Instance.CurrentPlayerSettings.MediumLevelsUnlocked++;
                                break;
                            case Level.Hard:
                                if (MonoGameFileSystem.Instance.CurrentPlayerSettings.HardLevelsUnlocked <=
                                    MonoGameFileSystem.Instance.CurrentPlayerSettings.Stage)
                                    MonoGameFileSystem.Instance.CurrentPlayerSettings.HardLevelsUnlocked++;
                                break;
                        }
                        MonoGameFileSystem.Instance.CurrentPlayerSettings.Stage++;
                        actor.LevelPoints += 100;
                    }
                    break;

                //case State.EndlessBombs:
                //    actor.TreasureState.EndlessBombs = true;
                //    break;


            }
        }
        /// <summary>
        /// Zmienia stan jednostki na Empty
        /// </summary>
        public void ResetState()
        {
            OverlappedState = State.Empty;
            overlappedTexture = null;
            texture = null;
            UnitState = State.Empty;
            emptyUnits++;
        }
        /// <summary>
        /// Ustawia stan skarbu po wybuchu bomby
        /// </summary>
        /// <param name="manager"></param>
        public void PlaceTreasure(ContentManager manager)
        {
            int probability = emptyUnits > Game1.WindowWidth * Game1.WindowHeight / 2 ? 0 : 5;
            State unitState = State.Empty;
            switch (Rnd.Next(probability))
            {
                case 1:
                    unitState = State.Fire;
                    break;
                case 0:
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
                case 4:
                    unitState = State.NewRemoteBomb;
                    break;
                //case 4:
                //    unitState = State.EndlessBombs;
                //    break;
            }
            if (unitState != State.Empty)
                texture = manager.Load<Texture2D>(ResourceInfo.ImageResources[unitState]);
            UnitState = unitState;
            OverlappedState = State.Empty;
            overlappedTexture = null;
        }
        /// <summary>
        /// Odrysowuje daną jednostkę
        /// </summary>
        /// <param name="target"></param>
        public void Draw(SpriteBatch target)
        {
            if (UnitState == State.Empty) return;
            if (OverlappedState == State.NormalBomb && UnitState != State.Player && UnitState != State.Enemy)
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
