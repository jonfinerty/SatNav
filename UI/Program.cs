using System;
using System.Collections.Generic;

using SatNav;

namespace UI
{
    class Program
    {
        private static IDictionary<char, Vertex> graph;

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
            graph = GraphBuilder.MakeGraph("AB5", "BC4", "CD7", "DC8", "DE6", "AD5", "CE2", "EB3", "AE7");
        }

        private static void TestCase1()
        {
            PrintHumanFriendlyOutput(() => new []{graph['A'], graph['B'], graph['C']}.MeasureRoute());
        }
        
        private static void TestCase2()
        {
            PrintHumanFriendlyOutput(() => new[] { graph['A'], graph['D']}.MeasureRoute());
        }
        
        private static void TestCase3()
        {
            PrintHumanFriendlyOutput(() => new[] { graph['A'], graph['D'], graph['C'] }.MeasureRoute());
        }
        
        private static void TestCase4()
        {
            PrintHumanFriendlyOutput(() =>new []{graph['A'], graph['E'], graph['B'], graph['C'], graph['D']}.MeasureRoute());
        }
        
        private static void TestCase5()
        {
            PrintHumanFriendlyOutput(() => new []{graph['A'], graph['E'], graph['D']}.MeasureRoute());
        }

        private static void TestCase6()
        {
            var search = new VertexLimitedRouteSearch(graph['C'], graph['C'], 3);

            PrintHumanFriendlyOutput(() => search.CountNumberOfValidRoutes());
        }

        private static void TestCase7()
        {
            var search = new ExactVertexCountRouteSearch(graph['A'], graph['C'], 4);            

            PrintHumanFriendlyOutput(() => search.CountNumberOfValidRoutes());
        }

        private static void TestCase8()
        {
            PrintHumanFriendlyOutput(() => graph['A'].ShortestDistanceTo(graph['C']));
        }

        private static void TestCase9()
        {
            PrintHumanFriendlyOutput(() => graph['B'].ShortestDistanceTo(graph['B']));
        }

        private static void TestCase10()
        {
            var search = new DistanceLimitedRouteSearch(graph['C'], graph['C'], 30);

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
