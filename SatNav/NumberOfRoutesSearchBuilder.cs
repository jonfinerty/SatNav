using System;

namespace SatNav
{
    public class NumberOfRoutesSearchBuilder
    {
        private readonly Graph _graph;        
        private Vertex _startVertex;
        private Vertex _endVertex;
        private SearchType _searchType;
        private int _maxVertices;
        private int _exactVertexCount;
        private int _underDistance;

        private enum SearchType
        {
            None,
            VertexLimited,
            ExactVertexCount,
            DistanceLimited
        }

        public NumberOfRoutesSearchBuilder(Graph graph)
        {
            _graph = graph;            
        }

        public NumberOfRoutesSearchBuilder StartingFrom(string startVertexName)
        {
            _startVertex = _graph.GetVertex(startVertexName);
            return this;
        }

        public NumberOfRoutesSearchBuilder EndingAt(string endVertexName)
        {
            _endVertex = _graph.GetVertex(endVertexName);
            return this;
        }

        public NumberOfRoutesSearchBuilder WithMaximumVerticesInRoute(int maxVertices)
        {
            _maxVertices = maxVertices;
            _searchType = SearchType.VertexLimited;
            return this;
        }
        
        public NumberOfRoutesSearchBuilder WithExactVertexCountInRoute(int vertexCount)
        {
            _exactVertexCount = vertexCount;
            _searchType = SearchType.ExactVertexCount;
            return this;
        }

        public NumberOfRoutesSearchBuilder WithUnderDistanceLimit(int underDistance)
        {
            _underDistance = underDistance;
            _searchType = SearchType.DistanceLimited;
            return this;
        }

        public ISearchForNumberOfRoutes Build()
        {
            /* The '_startVertex.Equals(_endVertex)' code is ugly. This is because the definition of
             * a valid route in this scenario does not include doing no moves at all. The other option
             * than creating a fake start vertex would be to push this 'first move' logic down into 
             * the search implementations, which makes them uglier, as they then have a special case
             * on their first iteration and cannot be implemented in a fully recursive way.
             */            

            switch (_searchType)
            {
                case SearchType.None:
                    break;
                
                case SearchType.VertexLimited:
                    
                    if (_startVertex.Equals(_endVertex))
                    {
                        CreateTempStartVertex();
                        _maxVertices++;
                    }
                    return new VertexLimitedRouteSearch(_startVertex, _endVertex, _maxVertices);

                case SearchType.ExactVertexCount:
                    
                    if (_startVertex.Equals(_endVertex))
                    {
                        CreateTempStartVertex();
                        _exactVertexCount++;
                    }
                    return new ExactVertexCountRouteSearch(_startVertex, _endVertex, _exactVertexCount);

                case SearchType.DistanceLimited:

                    if (_startVertex.Equals(_endVertex))
                    {
                        CreateTempStartVertex();
                    }
                    return new DistanceLimitedRouteSearch(_startVertex, _endVertex, _underDistance);
                
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return null;
        }

        private void CreateTempStartVertex()
        {
            var newStartVertex = new Vertex(string.Format("New source Vertex generated from Vertex: {0}", _startVertex.Name));

            foreach (var neighbour in _startVertex.GetNeighbours())
            {
                newStartVertex.AddNeighbour(neighbour, _startVertex.GetDistanceTo(neighbour));
            }

            _startVertex = newStartVertex;            
        }

    }
}