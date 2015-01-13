namespace SatNav
{
    public class DistanceLimitedRouteSearch : AbstractDepthFirstRouteSearch
    {
        private readonly int _distanceLeft;

        private readonly int _routeVertexCount;

        public DistanceLimitedRouteSearch(Vertex startVertex, Vertex targetVertex, int underDistance) : base(startVertex, targetVertex)
        {
            _distanceLeft = underDistance;
            _routeVertexCount = 1;
        }

        private DistanceLimitedRouteSearch(Vertex startVertex, Vertex targetVertex, int underDistance, int routeVertexCount) : base(startVertex, targetVertex)
        {
            _distanceLeft = underDistance;
            _routeVertexCount = routeVertexCount;
        }

        protected override AbstractDepthFirstRouteSearch NewSearchStartingFrom(Vertex newVertex)
        {
            var newDistanceLeft = _distanceLeft - CurrentVertex.GetDistanceTo(newVertex);
            var newRouteVertexCount = _routeVertexCount + 1;

            return new DistanceLimitedRouteSearch(newVertex, TargetVertex, newDistanceLeft, newRouteVertexCount);
        }

        protected override bool SearchSizeConstraintHit()
        {
            return _distanceLeft < 1;
        }

        protected override bool ValidRouteConstraintMet()
        {
            return _routeVertexCount > 1 && _distanceLeft.IsPositive() && CurrentVertex.Equals(TargetVertex);
        }
    }
}