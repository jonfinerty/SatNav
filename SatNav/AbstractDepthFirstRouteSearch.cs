using System.Linq;

namespace SatNav
{
    public interface ISearchForNumberOfRoutes
    {
        int CountNumberOfValidRoutes();
    }

    internal abstract class AbstractDepthFirstRouteSearch : ISearchForNumberOfRoutes
    {
        protected readonly Vertex CurrentVertex;
        protected readonly Vertex TargetVertex;

        protected abstract AbstractDepthFirstRouteSearch NewSearchStartingFrom(Vertex newVertex);
        protected abstract bool SearchSizeConstraintHit();
        protected abstract bool ValidRouteConstraintMet();

        protected AbstractDepthFirstRouteSearch(Vertex currentVertex, Vertex targetVertex)
        {
            CurrentVertex = currentVertex;
            TargetVertex = targetVertex;
        }

        public int CountNumberOfValidRoutes()
        {
            var numberOfRoutes = 0;

            if (SearchSizeConstraintHit() == false)
            {
                numberOfRoutes += CurrentVertex.GetNeighbours().Sum(neighbour => NewSearchStartingFrom(neighbour).CountNumberOfValidRoutes());
            }

            if (ValidRouteConstraintMet())
            {
                numberOfRoutes++;
            }

            return numberOfRoutes;
        }
    }
}