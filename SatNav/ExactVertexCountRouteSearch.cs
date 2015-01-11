namespace SatNav
{
    internal class ExactVertexCountRouteSearch : AbstractDepthFirstRouteSearch
    {
        private readonly int _exactVertexCountInRoute;

        public ExactVertexCountRouteSearch(Vertex currentVertex, Vertex targetVertex, int exactVertexCountInRoute) : base(currentVertex, targetVertex)
        {            
            _exactVertexCountInRoute = exactVertexCountInRoute;
        }

        protected override AbstractDepthFirstRouteSearch NewSearchStartingFrom(Vertex newVertex)
        {
            return new ExactVertexCountRouteSearch(newVertex, TargetVertex, _exactVertexCountInRoute - 1);
        }

        protected override bool SearchSizeConstraintHit()
        {
            return _exactVertexCountInRoute == 1;
        }

        protected override bool ValidRouteConstraintMet()
        {
            return CurrentVertex.Equals(TargetVertex) && _exactVertexCountInRoute == 1;
        }
    }
}