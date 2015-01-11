using System.Collections.Generic;
using System.Linq;

namespace SatNav
{
    /* This class implements Dijkstra's Algorithm - http://en.wikipedia.org/wiki/Dijkstra%27s_algorithm */
    public class DijkstraShortestRouteCalculator
    {
        private readonly Graph _graph;
        private ICollection<Vertex> _verticesWithNoPathFoundToThemAtAll;
        private IDictionary<Vertex, int> _verticesWithTentativeShortestPaths;
        private IDictionary<Vertex, int> _verticesWithShortestPathFound;
        private Vertex _startVertex;
        private Vertex _targetVertex;        

        public DijkstraShortestRouteCalculator(Graph graph)
        {
            _graph = graph;
        }

        public int ShortestDistanceBetween(string startVertexName, string endVertexName)
        {
            _startVertex = _graph.GetVertex(startVertexName);
            _targetVertex = _graph.GetVertex(endVertexName);

            if (_startVertex.Equals(_targetVertex))
            {
                _startVertex = CreateTempSourceVertex(_startVertex);
            }

            _verticesWithNoPathFoundToThemAtAll = _graph.Vertices.Where(v => v.Equals(_startVertex) == false).ToList();
            _verticesWithTentativeShortestPaths = new Dictionary<Vertex, int>{ {_startVertex, 0} };
            _verticesWithShortestPathFound = new Dictionary<Vertex, int>();

            return RunDijkstrasAlgorithm();
        }

        private static Vertex CreateTempSourceVertex(Vertex startVertex)
        {
            var newStartVertex = new Vertex(string.Format("NewSourceVertex generated from vertex: {0}", startVertex.Name));

            foreach (var neighbour in startVertex.GetNeighbours())
            {
                newStartVertex.AddNeighbour(neighbour, startVertex.GetDistanceTo(neighbour));
            }

            startVertex = newStartVertex;
            return startVertex;
        }

        private int RunDijkstrasAlgorithm()
        {
            while (ShortestPathToTargetFound() == false && MoreVerticesReachable())
            {
                var currentVertex = GetVertexWithShortestTentativeShortestPath();

                LockdownVertexShortestDistance(currentVertex);

                var nonLockedDownNeighbours = currentVertex.GetNeighbours().Where(neighbour => _verticesWithShortestPathFound.ContainsKey(neighbour) == false);

                foreach (var neighbour in nonLockedDownNeighbours)
                {
                    var lockedDownShortestDistanceToCurrentVertex = _verticesWithShortestPathFound[currentVertex];

                    var pathDistanceToNeighbour = lockedDownShortestDistanceToCurrentVertex + currentVertex.GetDistanceTo(neighbour);

                    if (NoPathFoundToVertexYet(neighbour))
                    {
                        SetTentativeShortestPathFound(neighbour, pathDistanceToNeighbour);
                    }
                    else if (pathDistanceToNeighbour < ShortestDistanceFoundSoFarTo(neighbour))
                    {
                        ImproveTentativeShortestPathFound(neighbour, pathDistanceToNeighbour);
                    }
                }
            }

            if (ShortestPathToTargetFound())
            {
                return _verticesWithShortestPathFound[_targetVertex];
            }
            
            throw new NoSuchRouteException();
        }

        private void SetTentativeShortestPathFound(Vertex neighbour, int pathDistanceToNeighbour)
        {
            _verticesWithNoPathFoundToThemAtAll.Remove(neighbour);
            _verticesWithTentativeShortestPaths.Add(neighbour, pathDistanceToNeighbour);
        }

        private int ShortestDistanceFoundSoFarTo(Vertex vertex)
        {
            return _verticesWithTentativeShortestPaths[vertex];
        }

        private void ImproveTentativeShortestPathFound(Vertex neighbour, int routeDistanceToNeighbourThroughCurrentNode)
        {
            _verticesWithTentativeShortestPaths[neighbour] = routeDistanceToNeighbourThroughCurrentNode;
        }

        private bool NoPathFoundToVertexYet(Vertex vertex)
        {
            return _verticesWithNoPathFoundToThemAtAll.Contains(vertex);
        }

        private void LockdownVertexShortestDistance(Vertex vertex)
        {
            var shortestDistanceFound = _verticesWithTentativeShortestPaths[vertex];
            _verticesWithShortestPathFound.Add(vertex, shortestDistanceFound);
            _verticesWithTentativeShortestPaths.Remove(vertex);
        }

        private Vertex GetVertexWithShortestTentativeShortestPath()
        {
            return _verticesWithTentativeShortestPaths.OrderBy(v => v.Value).First().Key;
        }

        private bool ShortestPathToTargetFound()
        {
            return _verticesWithShortestPathFound.ContainsKey(_targetVertex);
        }

        private bool MoreVerticesReachable()
        {
            return _verticesWithTentativeShortestPaths.Any();
        }
    }
}