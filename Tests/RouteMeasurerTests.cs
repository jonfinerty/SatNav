using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatNav;

namespace Tests
{
    [TestClass]
    public class RouteMeasurerTests
    {
        [TestMethod]
        public void MeasureRouteWithVertexNotInGraph()
        {
            var graph = new Graph()
                .AddEdge("A", "B", 1);

            var routeMeasurer = new RouteMeasurer(graph);

            try
            {
                routeMeasurer.MeasureRoute("A", "C");
            }
            catch (NoSuchVertexException)
            {
                return;
            }

            Assert.Fail("Exception not thrown");
        }

        [TestMethod]
        public void MeasureEmptyRoute()
        {

            var routeMeasurer = new RouteMeasurer(new Graph());

            Assert.AreEqual(0, routeMeasurer.MeasureRoute());
        }

        [TestMethod]
        public void MeasureRouteWithOneVertex()
        {
            var graph = new Graph()
                .AddEdge("A", "B", 1);                

            var routeMeasurer = new RouteMeasurer(graph);

            Assert.AreEqual(0, routeMeasurer.MeasureRoute("A"));
        }

        [TestMethod]
        public void MeasureRoute()
        {
            var graph = new Graph()
                .AddEdge("A", "B", 1)
                .AddEdge("B", "C", 2);

            var routeMeasurer = new RouteMeasurer(graph);

            Assert.AreEqual(3, routeMeasurer.MeasureRoute("A", "B", "C"));
        }
    }
}
