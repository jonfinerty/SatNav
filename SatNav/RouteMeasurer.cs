using System.Collections.Generic;

namespace SatNav
{
    public static class RouteMeasurer
    {
        public static int MeasureRoute(IEnumerable<Vertex> vertices)
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