using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bomberman.Utlis;
using Priority_Queue;

namespace Bomberman.Algorithms
{
    [Serializable]
    public class AStarAlgorithm: AiAlgorithm
    {
        /// <summary>
        /// Implementacja ruchu przeciwnika za pomocą algorytmu A*
        /// </summary>
        /// <param name="start">jednostka początkowa wyszukiwania</param>
        /// <param name="end">jednostka końcowa wyszukiwania</param>
        /// <returns></returns>
        public override IEnumerable<Direction> FindPath(Unit start, Unit end)
        {
            HeapPriorityQueue<Unit> openList = new HeapPriorityQueue<Unit>(GameSession.GameBoard.Height * GameSession.GameBoard.Width);
            ResetPriorities();

            start.G = 0;
            start.H = 0;
            start.Priority = 0;
            openList.Enqueue(start, 0);
            start.Opened = true;
            while (openList.Count != 0 && start.UnitState != State.Empty)
            {
                var unit = openList.Dequeue();
                unit.Closed = true;

                if (unit == end)
                {
                    return BacktracePath(unit);
                }

                var neighbors = GameSession.GameBoard.GetFreeNeighbors(unit).ToList();
                foreach (var neighbor in neighbors)
                {

                    if (neighbor.Closed) continue;

                    var distance = unit.G + 1;
                    if (!neighbor.Opened || distance < neighbor.G)
                    {
                        neighbor.G = distance;
                        neighbor.H = ManhattanHeuristic(neighbor, end);
                        neighbor.Priority = neighbor.G + neighbor.H;
                        neighbor.Parent = unit;
                        if (!neighbor.Opened)
                        {
                            openList.Enqueue(neighbor, neighbor.Priority);
                            neighbor.Opened = true;
                        }
                        else
                            openList.UpdatePriority(neighbor, neighbor.Priority);
                    }
                }
            }
            return null;
        }

        private double ManhattanHeuristic(Unit currentUnit, Unit goalUnit)
        {
            return Math.Abs(currentUnit.X - goalUnit.X) + Math.Abs(currentUnit.X - goalUnit.Y);
        }

        private IEnumerable<Direction> BacktracePath(Unit unit)
        {
            var path = new List<Direction>();
            var list = new List<Unit>();
            var node = unit;
            list.Add(node);
            while (node.Parent != null)
            {
                node = node.Parent;
                list.Add(node);
            }
            for (int i = 1; i < list.Count; i++)
            {
                path.Add(toDirection(list[i], list[i - 1]));
            }
            //path.Add(toDirection(list[list.Count - 1], ));
            //list.Remove(list.Last());
            return path;
        }

        private Direction toDirection(Unit unit1, Unit unit2)
        {
            if (unit1.X < unit2.X && unit1.Y == unit2.Y) return Direction.Right;
            if (unit1.X > unit2.X && unit1.Y == unit2.Y) return Direction.Left;
            if (unit1.X == unit2.X && unit1.Y < unit2.Y) return Direction.Down;
            return Direction.Up;
        }

        private void ResetPriorities()
        {
            foreach (var unit in GameSession.GameBoard.Units)
            {
                unit.Priority = 0;
                unit.G = 0;
                unit.H = 0;
                unit.QueueIndex = 0;
                unit.Opened = false;
                unit.Closed = false;
                unit.InsertionIndex = 0;
            }
        }
    }  
}