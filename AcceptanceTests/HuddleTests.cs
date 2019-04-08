using System;
using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using SatNav;

using UI;

namespace AcceptanceTests
{
    [TestClass]
    public class HuddleTests
    {        
        private IDictionary<char, Vertex> graph;

        [TestInitialize]
        public void SetupGraph()
        {
            graph = GraphBuilder.MakeGraph("AB5", "BC4", "CD7", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7");
        }

        [TestMethod]
        public void Test1()
        {
            var routeVertices = new[]{graph['A'], graph['B'], graph['C']};
            Assert.AreEqual(9, routeVertices.MeasureRoute());
        }

        [TestMethod]
        public void Test2()
        {
            var routeVertices = new[] { graph['A'], graph['D'] };
            Assert.AreEqual(5, routeVertices.MeasureRoute());
        }

        [TestMethod]
        public void Test3()
        {
            var routeVertices = new[] { graph['A'], graph['D'], graph['C'] };
            Assert.AreEqual(13, routeVertices.MeasureRoute());
        }

        [TestMethod]
        public void Test4()
        {
            var routeVertices = new[] { graph['A'], graph['E'], graph['B'], graph['C'], graph['D'] };
            Assert.AreEqual(21, routeVertices.MeasureRoute());
        }

        [TestMethod]
        public void Test5()
        {
            var thrownException = new Exception();
            
            try
            {
                var routeVertices = new[] { graph['A'], graph['E'], graph['D'] };
                routeVertices.MeasureRoute();
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
            var search = new VertexLimitedRouteSearch(graph['A'], graph['E'], 3);
            Assert.AreEqual(2, search.CountNumberOfValidRoutes());
        }

        // [Ignore] // This test is wrong, there is only one route from A->C with 4 vertices in it: A-E-B-C
        [TestMethod]
        public void Test7()
        {
            var search = new ExactVertexCountRouteSearch(graph['A'], graph['C'], 4);
            Assert.AreEqual(3, search.CountNumberOfValidRoutes());
        }

        [TestMethod]
        public void Test8()
        {
            Assert.AreEqual(9, graph['A'].ShortestDistanceTo(graph['C']));
        }

        [TestMethod]
        public void Test9()
        {
            Assert.AreEqual(9, graph['B'].ShortestDistanceTo(graph['B']));
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
