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
            var vertex1 = new Vertex();
            var vertex2 = new Vertex();            

            try
            {
                vertex1.ShortestDistanceTo(vertex2);
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
            var vertex1 = new Vertex();
            var vertex2 = new Vertex();
            vertex1.AddNeighbour(vertex2, 1);
            var vertex3 = new Vertex();
            vertex2.AddNeighbour(vertex3, 1);            

            Assert.AreEqual(2, vertex1.ShortestDistanceTo(vertex3));
        }

        [TestMethod]
        public void ShortestPathHasMoreJumpsThanOtherPossibility()
        {
            var startVertex = new Vertex();
            var endVertex = new Vertex();
            startVertex.AddNeighbour(endVertex, 10);

            var vertexB = new Vertex();
            startVertex.AddNeighbour(vertexB, 1);
            vertexB.AddNeighbour(endVertex, 1);            

            Assert.AreEqual(2, startVertex.ShortestDistanceTo(endVertex));
        }

        [TestMethod]
        public void StartAndEndAreTheSame()
        {
            var startVertex = new Vertex();
            var vertexB = new Vertex();
            startVertex.AddNeighbour(vertexB, 5);
            vertexB.AddNeighbour(startVertex, 5);

            Assert.AreEqual(10, startVertex.ShortestDistanceTo(startVertex));
        }
    }
}
