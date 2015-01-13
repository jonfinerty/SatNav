using System;
using SatNav;

namespace UI
{
    class Program
    {
        private static NumberOfRoutesSearchBuilder _numberOfRoutesSearchBuilder;
        private static RouteMeasurer _routeMeasurer;
        private static DijkstraShortestRouteCalculator _dijkstraShortestRouteCalculator;

        static void Main()
        {
            InitialiseGraph();

            TestCase1();
            TestCase2();
            TestCase3();
            TestCase4();
            TestCase5();
            TestCase6();
            TestCase7();
            TestCase8();
            TestCase9();
            TestCase10();

            Console.WriteLine("Press any key to end.");
            Console.ReadKey();
        }

        private static void InitialiseGraph()
        {
            var graph = new Graph()
                .AddDirectedEdge("A", "B", 5)
                .AddDirectedEdge("B", "C", 4)
                .AddDirectedEdge("C", "D", 7)
                .AddDirectedEdge("D", "C", 8)
                .AddDirectedEdge("D", "E", 6)
                .AddDirectedEdge("A", "D", 5)
                .AddDirectedEdge("C", "E", 2)
                .AddDirectedEdge("E", "B", 3)
                .AddDirectedEdge("A", "E", 7);

            _numberOfRoutesSearchBuilder = new NumberOfRoutesSearchBuilder(graph);
            _routeMeasurer = new RouteMeasurer(graph);
            _dijkstraShortestRouteCalculator = new DijkstraShortestRouteCalculator(graph);
        }

        private static void TestCase1()
        {
            PrintHumanFriendlyOutput(() => _routeMeasurer.MeasureRoute("A", "B", "C"));
        }
        
        private static void TestCase2()
        {
            PrintHumanFriendlyOutput(() => _routeMeasurer.MeasureRoute("A", "D"));
        }
        
        private static void TestCase3()
        {
            PrintHumanFriendlyOutput(() => _routeMeasurer.MeasureRoute("A", "D", "C"));
        }
        
        private static void TestCase4()
        {
            PrintHumanFriendlyOutput(() => _routeMeasurer.MeasureRoute("A", "E", "B", "C", "D"));
        }
        
        private static void TestCase5()
        {
            PrintHumanFriendlyOutput(() => _routeMeasurer.MeasureRoute("A", "E", "D"));
        }

        private static void TestCase6()
        {
            var search = _numberOfRoutesSearchBuilder
                .StartingFrom("C")
                .EndingAt("C")
                .WithMaximumVerticesInRoute(3)
                .Build();

            PrintHumanFriendlyOutput(() => search.CountNumberOfValidRoutes());
        }

        private static void TestCase7()
        {
            var search = _numberOfRoutesSearchBuilder
                .StartingFrom("A")
                .EndingAt("C")
                .WithExactVertexCountInRoute(4)
                .Build();

            PrintHumanFriendlyOutput(() => search.CountNumberOfValidRoutes());
        }

        private static void TestCase8()
        {
            PrintHumanFriendlyOutput(() => _dijkstraShortestRouteCalculator.ShortestDistanceBetween("A", "C"));
        }

        private static void TestCase9()
        {
            PrintHumanFriendlyOutput(() => _dijkstraShortestRouteCalculator.ShortestDistanceBetween("B", "B"));
        }

        private static void TestCase10()
        {
            var search = _numberOfRoutesSearchBuilder
                .StartingFrom("C")
                .EndingAt("C")
                .WithUnderDistanceLimit(30)
                .Build();

            PrintHumanFriendlyOutput(() => search.CountNumberOfValidRoutes());
        }

        public static void PrintHumanFriendlyOutput(Func<object> method)
        {
            try
            {
                Console.WriteLine(method());
            }
            catch (NoSuchRouteException)
            {
                Console.Error.WriteLine("NO SUCH ROUTE");
            }
            catch (NoSuchVertexException)
            {
                Console.Error.WriteLine("VERTEX NOT FOUND IN GRAPH");
            }
        }
    }
}
