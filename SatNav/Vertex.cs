using System;
using System.Collections.Generic;

namespace SatNav
{
    public class Vertex
    {        
        private readonly Dictionary<Vertex, int> _neighbours;

        public Vertex()
        {            
            _neighbours = new Dictionary<Vertex, int>();
        }

        public void AddNeighbour(Vertex vertexB, int distance)
        {
            if (_neighbours.ContainsKey(vertexB))
            {
                throw new ArgumentException("Vertex: {0} is already connected to Vectex: {1} with a distance of {2}, cannot add a second connection");
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