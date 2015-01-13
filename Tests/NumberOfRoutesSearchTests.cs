using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatNav;

namespace Tests
{
    [TestClass]
    public class NumberOfRoutesSearchTests
    {
        [TestMethod]
        public void MaximumVertexSearch()
        {
            var graph = new Graph()
                .AddDirectedEdge("Start", "End", 1)
                .AddDirectedEdge("Start", "A", 1)
                .AddDirectedEdge("A", "End", 1);

            var search = new NumberOfRoutesSearchBuilder(graph)
                .StartingFrom("Start")
                .EndingAt("End")
                .WithMaximumVerticesInRoute(2)
                .Build();

            Assert.AreEqual(1, search.CountNumberOfValidRoutes());
        }

        [TestMethod]
        public void MaximumVertexSearchWithSameStartAndFinish()
        {
            var graph = new Graph()
                .AddDirectedEdge("Start", "Start", 1);

            var search = new NumberOfRoutesSearchBuilder(graph)
                .StartingFrom("Start")
                .EndingAt("Start")
                .WithMaximumVerticesInRoute(2)
                .Build();

            Assert.AreEqual(2, search.CountNumberOfValidRoutes());
        }

        [TestMethod]
        public void ExactVertexSearch()
        {
            var graph = new Graph()
                .AddDirectedEdge("Start", "End", 1)
                .AddDirectedEdge("Start", "A", 1)
                .AddDirectedEdge("A", "End", 1);

            var search = new NumberOfRoutesSearchBuilder(graph)
                .StartingFrom("Start")
                .EndingAt("End")
                .WithExactVertexCountInRoute(3)
                .Build();

            Assert.AreEqual(1, search.CountNumberOfValidRoutes());
        }

        [TestMethod]
        public void ExactVertexSearchWithSameStartAndFinish()
        {
            var graph = new Graph()
                .AddDirectedEdge("Start", "Start", 1);

            var search = new NumberOfRoutesSearchBuilder(graph)
                .StartingFrom("Start")
                .EndingAt("Start")
                .WithExactVertexCountInRoute(1)
                .Build();

            Assert.AreEqual(1, search.CountNumberOfValidRoutes());
        }

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