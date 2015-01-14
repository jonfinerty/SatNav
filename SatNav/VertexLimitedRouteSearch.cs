namespace SatNav
{
    public class VertexLimitedRouteSearch : AbstractDepthFirstRouteSearch
    {
        private readonly int _maxVerticesInRoute;

        public VertexLimitedRouteSearch(Vertex startVertex, Vertex targetVertex, int maxVerticesInRoute) : base(startVertex, targetVertex)
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