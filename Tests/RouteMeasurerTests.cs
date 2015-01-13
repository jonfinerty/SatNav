using Microsoft.VisualStudio.TestTools.UnitTesting;
using SatNav;

namespace Tests
{
    [TestClass]
    public class RouteMeasurerTests
    {
        [TestMethod]
        public void MeasureRouteWhichDoesNotExist()
        {
            var vertex1 = new Vertex();
            var vertex2 = new Vertex();

            try
            {
                RouteMeasurer.MeasureRoute(new []{vertex1, vertex2});
            }
            catch (NoSuchRouteException)
            {
                return;
            }

            Assert.Fail("Exception not thrown");
        }

        [TestMethod]
        public void MeasureEmptyRoute()
        {
            Assert.AreEqual(0, RouteMeasurer.MeasureRoute(new Vertex[]{}));
        }

        [TestMethod]
        public void MeasureRouteWithOneVertex()
        {
            var vertex1 = new Vertex();

            Assert.AreEqual(0, RouteMeasurer.MeasureRoute(new []{vertex1}));
        }

        [TestMethod]
        public void MeasureRoute()
        {
            var vertex1 = new Vertex();
            var vertex2 = new Vertex();
            var vertex3 = new Vertex();

            vertex1.AddNeighbour(vertex2, 1);
            vertex2.AddNeighbour(vertex3, 2);            

            Assert.AreEqual(3, RouteMeasurer.MeasureRoute(new []{ vertex1, vertex2, vertex3 }));
        }
    }
}
