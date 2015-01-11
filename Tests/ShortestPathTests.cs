using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatNav;

namespace Tests
{
    [TestClass]
    public class ShortestPathTests
    {
        [TestMethod]
        public void NoValidPaths()
        {
            var disconnectedGraph = new Graph()
                .AddEdge("A", "B", 1)
                .AddEdge("C", "D", 1);

            var shortestPathFinder = new DijkstraShortestRouteCalculator(disconnectedGraph);

            try
            {
                shortestPathFinder.ShortestDistanceBetween("A", "D");
            }
            catch (NoSuchRouteException)
            {
                return;
            }

            Assert.Fail("Exception was not thrown");
        }

        [TestMethod]
        public void OnlyOneValidPath()
        {
            var simpleGraph = new Graph()
                .AddEdge("A", "B", 1)
                .AddEdge("B", "C", 1);

            var shortestPathFinder = new DijkstraShortestRouteCalculator(simpleGraph);

            Assert.AreEqual(2, shortestPathFinder.ShortestDistanceBetween("A", "C"));
        }

        [TestMethod]
        public void ShortestPathHasMoreJumpsThanOtherPossibility()
        {
            var graph = new Graph()
                .AddEdge("Start", "End", 10)
                .AddEdge("Start", "B", 1)
                .AddEdge("B", "End", 1);

            var shortestPathFinder = new DijkstraShortestRouteCalculator(graph);

            Assert.AreEqual(2, shortestPathFinder.ShortestDistanceBetween("Start", "End"));
        }

        [TestMethod]
        public void StartAndEndAreTheSame()
        {
            var graph = new Graph()
                .AddEdge("Start", "B", 5)
                .AddEdge("B", "Start", 5);

            var shortestPathFinder = new DijkstraShortestRouteCalculator(graph);

            Assert.AreEqual(10, shortestPathFinder.ShortestDistanceBetween("Start", "Start"));
        }
    }
}
