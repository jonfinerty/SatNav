using System.Collections.Generic;
using System.Linq;

namespace SatNav
{
    /* This class implements Dijkstra's Algorithm - http://en.wikipedia.org/wiki/Dijkstra%27s_algorithm */
    public static class DijkstraShortestRouteCalculator
    {
        public static int ShortestDistanceTo(this Vertex startVertex, Vertex targetVertex)
        {
            if (startVertex.Equals(targetVertex))
            {
                startVertex = CreateTempSourceVertex(startVertex);
            }

            return RunDijkstrasAlgorithm(startVertex, targetVertex);
        }

        private static Vertex CreateTempSourceVertex(Vertex startVertex)
        {
            var newStartVertex = new Vertex();

            foreach (var neighbour in startVertex.GetNeighbours())
            {
                newStartVertex.AddNeighbour(neighbour, startVertex.GetDistanceTo(neighbour));
            }

            startVertex = newStartVertex;
            return startVertex;
        }

        private static int RunDijkstrasAlgorithm(Vertex startVertex, Vertex targetVertex)
        {
            IDictionary<Vertex, int> verticesWithTentativeShortestPaths = new Dictionary<Vertex, int> { { startVertex, 0 } };
            IDictionary<Vertex, int> verticesWithShortestPathFound = new Dictionary<Vertex, int>();

            while (verticesWithShortestPathFound.ContainsKey(targetVertex) == false && verticesWithTentativeShortestPaths.Any())
            {
                var currentVertex = GetVertexWithShortestTentativeShortestPath(verticesWithTentativeShortestPaths);

                var shortestDistanceFound = verticesWithTentativeShortestPaths[currentVertex];
                verticesWithShortestPathFound.Add(currentVertex, shortestDistanceFound);
                verticesWithTentativeShortestPaths.Remove(currentVertex);

                var nonLockedDownNeighbours = currentVertex.GetNeighbours().Where(neighbour => verticesWithShortestPathFound.ContainsKey(neighbour) == false);

                foreach (var neighbour in nonLockedDownNeighbours)
                {
                    var lockedDownShortestDistanceToCurrentVertex = verticesWithShortestPathFound[currentVertex];

                    var pathDistanceToNeighbour = lockedDownShortestDistanceToCurrentVertex + currentVertex.GetDistanceTo(neighbour);

                    if (verticesWithTentativeShortestPaths.ContainsKey(neighbour))
                    {
                        if (pathDistanceToNeighbour < verticesWithTentativeShortestPaths[neighbour])
                        {
                            verticesWithTentativeShortestPaths[neighbour] = pathDistanceToNeighbour;
                        }
                    }
                    else
                    {
                        verticesWithTentativeShortestPaths.Add(neighbour, pathDistanceToNeighbour);
                    }
                }
            }

            if (verticesWithShortestPathFound.ContainsKey(targetVertex))
            {
                return verticesWithShortestPathFound[targetVertex];
            }
            
            throw new NoSuchRouteException();
        }

        private static Vertex GetVertexWithShortestTentativeShortestPath(IEnumerable<KeyValuePair<Vertex, int>> vertexDistanceDictionary)
        {
            return vertexDistanceDictionary.OrderBy(v => v.Value).First().Key;
        }
    }
}