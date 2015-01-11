using System.Collections.Generic;
using System.Linq;

namespace SatNav
{
    public class Graph
    {
        private readonly ISet<Vertex> _vertices;

        public Graph()
        {
            _vertices = new HashSet<Vertex>();
        }

        public ISet<Vertex> Vertices
        {
            get
            {
                return _vertices;
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
            var vertex = _vertices.FirstOrDefault(v => v.Name == vertexName);

            if (vertex == null)
            {
                throw new NoSuchVertexException();
            }

            return vertex;
        }

        private Vertex GetOrCreateVertex(string vertexName)
        {
            var vertex = _vertices.FirstOrDefault(v => v.Name == vertexName);

            if (vertex == null)
            {
                vertex = new Vertex(vertexName);
                _vertices.Add(vertex);
            }

            return vertex;
        }
    }
}