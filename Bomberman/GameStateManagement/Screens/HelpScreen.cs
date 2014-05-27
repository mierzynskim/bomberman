using GameStateManagement;
using Microsoft.Xna.Framework;


namespace Bomberman.GameStateManagement.Screens
{

    public class HelpScreen : MenuScreen
    {
        private const string Message = "HELP Use keyboard arrows to move the hero.\n Type:\n1 - put normal bomb\n2 - put remote bomb\n3 - use glove\nSpace - use remote bomb";

        /// <summary>
        /// Constructor.
        /// </summary>
        public HelpScreen()
            : base("HELP")
        {


        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            ScreenManager.SpriteBatch.Begin();
            Vector2 strSize = ScreenManager.Font.MeasureString(Message);
            Vector2 strLoc = new Vector2(0, Game1.WindowHeight / 4);
            ScreenManager.SpriteBatch.DrawString(ScreenManager.Font, Message, strLoc, Color.White);
            ScreenManager.SpriteBatch.End();
        }
    }
}
