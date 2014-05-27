using System.Collections.Generic;
using Bomberman.Utlis;

namespace Bomberman.Algorithms
{
    public interface IAiAlgorithm
    {
        IEnumerable<Direction> FindPath(Unit start, Unit end);

    }
}