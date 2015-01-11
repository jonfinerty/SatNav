using System.Collections.Generic;

namespace SatNav
{
    public class Graph
    {
        private readonly IDictionary<string, Vertex> _vertices;

        public Graph()
        {
            _vertices = new Dictionary<string, Vertex>();
        }

        public ICollection<Vertex> Vertices
        {
            get
            {
                return _vertices.Values;
            }            
        }

        public Graph AddEdge(string startVertexName, string endVertexName, int distance)
        {
            var startVertex = GetOrCreateVertex(startVertexName);
            var endVertex = GetOrCreateVertex(endVertexName);

            startVertex.AddNeighbour(endVertex, distance);

            return this;
        }

        public Vertex GetVertex(string vertexName)
        {
            if (_vertices.ContainsKey(vertexName))
            {
                return _vertices[vertexName];
            }

            throw new NoSuchVertexException();
        }

        private Vertex GetOrCreateVertex(string vertexName)
        {
            if (_vertices.ContainsKey(vertexName))
            {
                return _vertices[vertexName];
            }

            var newVertex = new Vertex();
            _vertices.Add(vertexName, newVertex);
            return newVertex;
        }
    }
}