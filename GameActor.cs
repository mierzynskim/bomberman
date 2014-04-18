using Bomberman.Utlis;

namespace Bomberman
{
    public abstract class GameActor
    {
        public Unit CurrentUnit { protected get; set; }
        public abstract void Move(Direction direction);
    }


}
