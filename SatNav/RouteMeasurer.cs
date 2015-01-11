using System.Collections.Generic;
using System.Linq;

namespace SatNav
{
    public class RouteMeasurer
    {
        private readonly Graph _graph;

        public RouteMeasurer(Graph graph)
        {
            _graph = graph;            
        }

        public int MeasureRoute(params string[] vertexNames)
        {
            return MeasureRoute(vertexNames.Select(_graph.GetVertex).ToList());
        }

        private static int MeasureRoute(IEnumerable<Vertex> vertices)
        {
            var totalDistance = 0;

            foreach (var vertexPair in vertices.Pairs())
            {
                var vertex1 = vertexPair.Item1;
                var vertex2 = vertexPair.Item2;

                if (vertex1.IsConnectedTo(vertex2))
                {
                    totalDistance += vertex1.GetDistanceTo(vertex2);
                }
                else
                {
                    throw new NoSuchRouteException();
                }
            }

            return totalDistance;
        }
    }
}