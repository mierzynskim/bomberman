using Bomberman.StateInterfaces;

namespace Bomberman.FileSystem
{
    public abstract class FileSystem
    {
        public abstract void Login(ILogin login);
        public abstract void LoadGame((ILoadGameState loadGame));
        public abstract void SaveGame(ISaveGameState saveGame);
        public abstract void LoadHighScores(ILoadHighScores loadHighScores);

    }
}
