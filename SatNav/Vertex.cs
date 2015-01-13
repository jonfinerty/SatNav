using System;
using System.Collections.Generic;

namespace SatNav
{
    public class Vertex
    {        
        private readonly IDictionary<Vertex, int> _neighbours;

        public Vertex()
        {            
            _neighbours = new Dictionary<Vertex, int>();
        }

        public void AddNeighbour(Vertex vertexB, int distance)
        {
            if (_neighbours.ContainsKey(vertexB))
            {
                throw new ArgumentException(string.Format("Vertices are already connected with a distance of {0}, cannot add a second connection", distance));
            }

            _neighbours[vertexB] = distance;
        }

        public IEnumerable<Vertex> GetNeighbours()
        {
            return _neighbours.Keys;
        }

        public int GetDistanceTo(Vertex otherVertex)
        {
            return _neighbours[otherVertex];
        }

        public bool IsConnectedTo(Vertex otherVertex)
        {
            return _neighbours.ContainsKey(otherVertex);
        }
    }
}