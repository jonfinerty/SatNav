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
                .AddEdge("Start", "End", 1)
                .AddEdge("Start", "A", 1)
                .AddEdge("A", "End", 1);

            var search = new NumberOfRoutesSearchBuilder(graph)
                .StartingFrom("Start")
                .EndingAt("End")
                .WithMaximumVerticesInRoute(2)
                .Build();

            Assert.AreEqual(1, search.CountNumberOfValidRoutes());
        }

        [TestMethod]
        public void ExactVertexSearch()
        {
            var graph = new Graph()
                .AddEdge("Start", "End", 1)
                .AddEdge("Start", "A", 1)
                .AddEdge("A", "End", 1);

            var search = new NumberOfRoutesSearchBuilder(graph)
                .StartingFrom("Start")
                .EndingAt("End")
                .WithExactVertexCountInRoute(3)
                .Build();

            Assert.AreEqual(1, search.CountNumberOfValidRoutes());
        }

        [TestMethod]
        public void MaximumDistanceSearch()
        {
            var graph = new Graph()
                .AddEdge("Start", "End", 3)
                .AddEdge("Start", "A", 1)
                .AddEdge("A", "End", 1);

            var search = new NumberOfRoutesSearchBuilder(graph)
                .StartingFrom("Start")
                .EndingAt("End")
                .WithUnderDistanceLimit(3)
                .Build();

            Assert.AreEqual(1, search.CountNumberOfValidRoutes());
        }
    }
}