using System;
using System.Collections.Generic;
using System.Linq;

namespace SatNav
{
    public class GraphBuilder
    {
        public static IDictionary<char, Vertex> MakeGraph(params string[] edgeTuples)
        {
            var graph = new Dictionary<char, Vertex>();

            foreach (var edgeTuple in edgeTuples)
            {
                if (edgeTuple.Length < 3)
                {
                    throw new ArgumentException("Edge tuples should be specified in 'AB1' format. Two single character node names followed by an integer distance");
                }                

                var firstVertexName = edgeTuple.First();
                var secondVectexName = edgeTuple.Skip(1).First();

                var firstVertex = graph.ContainsKey(firstVertexName) ? graph[firstVertexName] : new Vertex();
                var secondVertex = graph.ContainsKey(secondVectexName) ? graph[secondVectexName] : new Vertex();

                int distance = 0;

                try
                {
                    distance = int.Parse(string.Concat(edgeTuple.Skip(2)));
                }
                catch (OverflowException)
                {
                    throw new ArgumentException("Edge tuples should be specified in 'AB1' format. Two single character node names followed by an integer distance");
                }
                

                firstVertex.AddNeighbour(secondVertex, distance);

                graph[firstVertexName] = firstVertex;
                graph[secondVectexName] = secondVertex;
            }

            return graph;
        }
    }
    
    public class Graph
    {
        private readonly IDictionary<string, Vertex> _vertices;

        public Graph()
        {
            _vertices = new Dictionary<string, Vertex>();
        }

        public Graph AddDirectedEdge(string startVertexName, string endVertexName, int distance)
        {
            var startVertex = GetOrCreateVertex(startVertexName);
            var endVertex = GetOrCreateVertex(endVertexName);

            startVertex.AddNeighbour(endVertex, distance);

            return this;
        }

        internal Vertex GetVertex(string vertexName)
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