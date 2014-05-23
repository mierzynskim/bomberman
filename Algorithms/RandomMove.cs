using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bomberman.Utlis;

namespace Bomberman.Algorithms
{
    [Serializable]
    public class RandomMove: AiAlgorithm
    {
        private static readonly Random random = new Random();
        public override IEnumerable<Direction> FindPath(Unit start, Unit end)
        {
            List<Direction> directions = new List<Direction>();
            for (int i = 0; i < 30; i++)
            {
                directions.Add((Direction)random.Next(0, 4));
            }
            return directions;
        }
    }
}
