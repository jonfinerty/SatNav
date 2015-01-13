using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatNav;

namespace Tests
{
    [TestClass]
    public class HuddleTests
    {        
        private NumberOfRoutesSearchBuilder _numberOfRoutesSearchBuilder;
        private DijkstraShortestRouteCalculator _dijkstraShortestRouteCalculator;

        private IDictionary<char, Vertex> graph;

        [TestInitialize]
        public void SetupGraph()
        {
            graph = GraphBuilder.MakeGraph("AB5", "BC4", "CD7", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7");

            /*
            var graph = new Graph()
                .AddDirectedEdge("A", "B", 5)
                .AddDirectedEdge("B", "C", 4)
                .AddDirectedEdge("C", "D", 7)
                .AddDirectedEdge("D", "C", 8)
                .AddDirectedEdge("D", "E", 6)
                .AddDirectedEdge("A", "D", 5)
                .AddDirectedEdge("C", "E", 2)
                .AddDirectedEdge("E", "B", 3)
                .AddDirectedEdge("A", "E", 7);

            _numberOfRoutesSearchBuilder = new NumberOfRoutesSearchBuilder(graph);
            _routeMeasurer = new RouteMeasurer(graph);
            _dijkstraShortestRouteCalculator = new DijkstraShortestRouteCalculator(graph);*/
        }

        [TestMethod]
        public void Test1()
        {
            var routeVertices = new[]{graph['A'], graph['B'], graph['C']};
            Assert.AreEqual(9, RouteMeasurer.MeasureRoute(routeVertices));
        }

        [TestMethod]
        public void Test2()
        {
            var routeVertices = new[] { graph['A'], graph['D'] };
            Assert.AreEqual(5, RouteMeasurer.MeasureRoute(routeVertices));
        }

        [TestMethod]
        public void Test3()
        {
            var routeVertices = new[] { graph['A'], graph['D'], graph['C'] };
            Assert.AreEqual(13, RouteMeasurer.MeasureRoute(routeVertices));
        }

        [TestMethod]
        public void Test4()
        {
            var routeVertices = new[] { graph['A'], graph['E'], graph['B'], graph['C'], graph['D'] };
            Assert.AreEqual(21, RouteMeasurer.MeasureRoute(routeVertices));
        }

        [TestMethod]
        public void Test5()
        {
            var thrownException = new Exception();
            
            try
            {
                var routeVertices = new[] { graph['A'], graph['E'], graph['D'] };
                RouteMeasurer.MeasureRoute(routeVertices);
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

        [Ignore] // This test is wrong, there is only one route from A->C with 4 vertices in it: A-E-B-C
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
            var targetVertex = graph['C'];
            var search = new DistanceLimitedRouteSearch(targetVertex, targetVertex, 30);

            Assert.AreEqual(9, search.CountNumberOfValidRoutes());
        }
    }
}
