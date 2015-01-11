namespace SatNav
{
    internal class DistanceLimitedRouteSearch : AbstractDepthFirstRouteSearch
    {
        private readonly int _distanceLeft;

        public DistanceLimitedRouteSearch(Vertex currentVertex, Vertex targetVertex, int underDistance) : base(currentVertex, targetVertex)
        {
            _distanceLeft = underDistance;
        }

        protected override AbstractDepthFirstRouteSearch NewSearchStartingFrom(Vertex newVertex)
        {
            var newDistanceLeft = _distanceLeft - CurrentVertex.GetDistanceTo(newVertex);

            return new DistanceLimitedRouteSearch(newVertex, TargetVertex, newDistanceLeft);
        }

        protected override bool SearchSizeConstraintHit()
        {
            return _distanceLeft < 1;
        }

        protected override bool ValidRouteConstraintMet()
        {
            return _distanceLeft > 0 && CurrentVertex.Equals(TargetVertex);
        }
    }
}