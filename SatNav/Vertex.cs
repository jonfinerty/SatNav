using System;
using System.Collections.Generic;

namespace SatNav
{
    public class Vertex
    {        
        private readonly Dictionary<Vertex, int> _neighbours;

        public string Name { get; private set; }

        public Vertex(string vertexName)
        {
            Name = vertexName;
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

        protected bool Equals(Vertex other)
        {
            return string.Equals(Name, other.Name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Vertex) obj);
        }

        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

    }
}