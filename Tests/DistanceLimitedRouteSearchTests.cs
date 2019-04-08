using Microsoft.VisualStudio.TestTools.UnitTesting;

using SatNav;

namespace Tests
{
    [TestClass]
    public class DistanceLimitedRouteSearchTests
    {
        [TestMethod]
        public void MaximumDistanceSearch()
        {
            var startVertex = new Vertex();
            var vertexB = new Vertex();
            var endVertex = new Vertex();

            startVertex.AddNeighbour(endVertex, 3);
            startVertex.AddNeighbour(vertexB, 1);
            vertexB.AddNeighbour(endVertex, 1);

            var search = new DistanceLimitedRouteSearch(startVertex, endVertex, 3);

            Assert.AreEqual(1, search.CountNumberOfValidRoutes());
        }
    }
}