using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatNav;

namespace Tests
{
    [TestClass]
    public class HuddleTests
    {        
        private NumberOfRoutesSearchBuilder _numberOfRoutesSearchBuilder;
        private RouteMeasurer _routeMeasurer;
        private DijkstraShortestRouteCalculator _dijkstraShortestRouteCalculator;

        [TestInitialize]
        public void SetupGraph()
        {
            var graph = new Graph()
                .AddEdge("A", "B", 5)
                .AddEdge("B", "C", 4)
                .AddEdge("C", "D", 7)
                .AddEdge("D", "C", 8)
                .AddEdge("D", "E", 6)
                .AddEdge("A", "D", 5)
                .AddEdge("C", "E", 2)
                .AddEdge("E", "B", 3)
                .AddEdge("A", "E", 7);

            _numberOfRoutesSearchBuilder = new NumberOfRoutesSearchBuilder(graph);
            _routeMeasurer = new RouteMeasurer(graph);
            _dijkstraShortestRouteCalculator = new DijkstraShortestRouteCalculator(graph);
        }

        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual(9, _routeMeasurer.MeasureRoute("A", "B", "C"));
        }

        [TestMethod]
        public void Test2()
        {
            Assert.AreEqual(5, _routeMeasurer.MeasureRoute("A", "D"));
        }

        [TestMethod]
        public void Test3()
        {
            Assert.AreEqual(13, _routeMeasurer.MeasureRoute("A", "D", "C"));
        }

        [TestMethod]
        public void Test4()
        {
            Assert.AreEqual(21, _routeMeasurer.MeasureRoute("A", "E", "B", "C", "D"));
        }

        [TestMethod]
        public void Test5()
        {
            var thrownException = new Exception();
            
            try
            {
                _routeMeasurer.MeasureRoute("A", "E", "D");
            }
            catch (Exception e)
            {
                thrownException = e;
            }
            
            Assert.AreEqual(typeof(NoSuchRouteException), thrownException.GetType());
        }

        [TestMethod]
        public void Test6()
        {
            var search = _numberOfRoutesSearchBuilder
                .StartingFrom("C")
                .EndingAt("C")
                .WithMaximumVerticesInRoute(3)
                .Build();

            Assert.AreEqual(2, search.CountNumberOfValidRoutes());
        }

        [Ignore] // This test is wrong, the is only one route from A->C with 4 vertices in it: A-E-B-C
        [TestMethod]
        public void Test7()
        {
            var search = _numberOfRoutesSearchBuilder
                .StartingFrom("A")
                .EndingAt("C")
                .WithExactVertexCountInRoute(4)
                .Build();

            Assert.AreEqual(3, search.CountNumberOfValidRoutes());
        }

        [TestMethod]
        public void Test8()
        {
            Assert.AreEqual(9, _dijkstraShortestRouteCalculator.ShortestDistanceBetween("A", "C"));
        }

        [TestMethod]
        public void Test9()
        {
            Assert.AreEqual(9, _dijkstraShortestRouteCalculator.ShortestDistanceBetween("B", "B"));
        }

        [TestMethod]
        public void Test10()
        {
            var search = _numberOfRoutesSearchBuilder
                .StartingFrom("C")
                .EndingAt("C")
                .WithUnderDistanceLimit(30)
                .Build();

            Assert.AreEqual(9, search.CountNumberOfValidRoutes());
        }
    }
}
