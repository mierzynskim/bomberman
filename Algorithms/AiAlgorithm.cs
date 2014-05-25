using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Bomberman.Utlis;

namespace Bomberman.Algorithms
{
    [XmlInclude(typeof(RandomMove)), XmlInclude(typeof (AStarAlgorithm))]
    public abstract class AiAlgorithm
    {
        public abstract IEnumerable<Direction> FindPath(Unit start, Unit end);

    }
}