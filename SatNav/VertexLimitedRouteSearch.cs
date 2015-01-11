namespace SatNav
{
    internal class VertexLimitedRouteSearch : AbstractDepthFirstRouteSearch
    {
        private readonly int _maxVerticesInRoute;

        public VertexLimitedRouteSearch(Vertex currentVertex, Vertex targetVertex, int maxVerticesInRoute) : base(currentVertex, targetVertex)
        {            
            _maxVerticesInRoute = maxVerticesInRoute;
        }

        protected override AbstractDepthFirstRouteSearch NewSearchStartingFrom(Vertex newVertex)
        {
            return new VertexLimitedRouteSearch(newVertex, TargetVertex, _maxVerticesInRoute-1);
        }

        protected override bool SearchSizeConstraintHit()
        {
            return _maxVerticesInRoute == 1;
        }

        protected override bool ValidRouteConstraintMet()
        {
            return CurrentVertex.Equals(TargetVertex);
        }
    }
}