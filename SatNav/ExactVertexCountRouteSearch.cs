namespace SatNav
{
    public class ExactVertexCountRouteSearch : AbstractDepthFirstRouteSearch
    {
        private readonly int _exactVertexCountInRoute;

        public ExactVertexCountRouteSearch(Vertex startVertex, Vertex targetVertex, int exactVertexCountInRoute) : base(startVertex, targetVertex)
        {            
            _exactVertexCountInRoute = exactVertexCountInRoute;
        }

        protected override AbstractDepthFirstRouteSearch NewSearchStartingFrom(Vertex newVertex)
        {
            return new ExactVertexCountRouteSearch(newVertex, TargetVertex, _exactVertexCountInRoute - 1);
        }

        protected override bool SearchSizeConstraintHit()
        {
            return _exactVertexCountInRoute == 0;
        }

        protected override bool ValidRouteConstraintMet()
        {
            return CurrentVertex.Equals(TargetVertex) && _exactVertexCountInRoute == 0;
        }
    }
}