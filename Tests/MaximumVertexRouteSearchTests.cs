using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatNav;

namespace Tests
{
    [TestClass]
    public class MaximumVertexRouteSearchTests
    {
        [TestMethod]
        public void MaximumVertexSearch()
        {
            var startVertex = new Vertex();
            var vertexA = new Vertex();
            var endVertex = new Vertex();

            startVertex.AddNeighbour(endVertex, 1);
            startVertex.AddNeighbour(vertexA, 1);
            vertexA.AddNeighbour(endVertex, 1);

            var search = new VertexLimitedRouteSearch(startVertex, endVertex, 2);
            Assert.AreEqual(1, search.CountNumberOfValidRoutes());
        }

        [TestMethod]
        public void MaximumVertexSearchWithSameStartAndFinish()
        {
            var startVertex = new Vertex();
            startVertex.AddNeighbour(startVertex, 1);

            var search = new VertexLimitedRouteSearch(startVertex, startVertex, 2);
            Assert.AreEqual(2, search.CountNumberOfValidRoutes());
        }
    }
}