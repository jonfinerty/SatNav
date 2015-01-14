using System.Collections.Generic;
using System.Linq;

namespace SatNav
{
    public static class RouteMeasuringExtension
    {
        public static int MeasureRoute(this IEnumerable<Vertex> vertices)
        {
            return vertices.Pairs().Sum(vertexPair => vertexPair.Item1.GetDistanceTo(vertexPair.Item2));
        }
    }    
}