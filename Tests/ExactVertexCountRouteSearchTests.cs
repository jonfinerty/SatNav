using Microsoft.VisualStudio.TestTools.UnitTesting;

using SatNav;

namespace Tests
{
    [TestClass]
    public class ExactVertexCountRouteSearchTests
    {
        [TestMethod]
        public void ExactVertexSearch()
        {
            var startVertex = new Vertex();
            var vertexA = new Vertex();
            var endVertex = new Vertex();

            startVertex.AddNeighbour(vertexA, 1);
            startVertex.AddNeighbour(endVertex, 1);
            vertexA.AddNeighbour(endVertex, 1);

            var search = new ExactVertexCountRouteSearch(startVertex, endVertex, 2);

            Assert.AreEqual(1, search.CountNumberOfValidRoutes());
        }

        [TestMethod]
        public void ExactVertexSearchWithSameStartAndFinish()
        {
            var startVertex = new Vertex();
            startVertex.AddNeighbour(startVertex, 1);

            var search = new ExactVertexCountRouteSearch(startVertex, startVertex, 1);

            Assert.AreEqual(1, search.CountNumberOfValidRoutes());
        }
    }
}