using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure_Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            //TestSomething();
            //TestNumerical();
            //TestSorting();
            //TestSimilarStrings();
            TestConnectedPaths(); // Union Find problem.
        }

        static void TestNumerical()
        {
            var numerical = new NumericalAlgorithms();

            var result = numerical.CalculateExponentiation(7, 11);
            numerical.IsPrime(17);

            numerical.GCD(78, 66);
            numerical.GCD(66, 78);
            numerical.GCD(12, 12);

            numerical.LCM(20, 30);
            numerical.LCM(100, 35);
        }

        static void TestSorting()
        {
            Sort s = new Sort();
            s.InsertionSort();
            s.SelectionSort();
            s.BubbleSort();
            s.HeapSort();
            s.QuickSort();
            s.QuickSort2();
            s.QuickSort2Iterative();
        }

        static void TestSimilarStrings()
        {
            SimilarString str = new SimilarString();
            string[] inputGroup = { "kccomwcgcs", "socgcmcwkc", "sgckwcmcoc", "coswcmcgkc", "cowkccmsgc", "cosgmccwkc", "sgmkwcccoc", "coswmccgkc", "kowcccmsgc", "kgcomwcccs" };

            str.NumSimilarGroups(inputGroup);
        }

        static void TestConnectedPaths()
        {
            int numberOfPoints = 10;
            IConnectedPaths connectedPaths;
            connectedPaths = new ConnectedPathsQuickFind(numberOfPoints);
            //connectedPaths = new ConnectedPathsQuickConnect(numberOfPoints);
            //connectedPaths = new ConnectedPathsQuickConnectWeighted(numberOfPoints);
             //connectedPaths = new ConnectedPathsQuickConnectWeightedPathCompression(numberOfPoints);

            // Initially all are disconnected.
            // Test if 1 and 7 are connected?
            var isConnected =  connectedPaths.IsConnected(4, 3);
            Console.WriteLine("4 and 3 connection result: " + isConnected);
            connectedPaths.Connect(4, 3);
            isConnected = connectedPaths.IsConnected(4, 3);
            Console.WriteLine("4 and 3 connection result: " + isConnected);
            
            connectedPaths.Connect(3, 8);
            isConnected = connectedPaths.IsConnected(8, 4);
            Console.WriteLine("8 and 4 connection result: " + isConnected);

            connectedPaths.Connect(6,5);
            connectedPaths.Connect(9,4);
            connectedPaths.Connect(2,1);
            connectedPaths.Connect(5,0);
            connectedPaths.Connect(7,2);
            connectedPaths.Connect(6,1);
            connectedPaths.Connect(7,3);

        }

        static void TestSomething()
        {
            
            List<TestClass> tc = new List<TestClass>();
            for (int i = 0; i < 20; i++)
            {
                tc.Add(new TestClass() {Band = i});
            }
            var bands = (from singleClass in tc
                select singleClass.Band).ToArray();
            foreach (var band in bands)
            {
                Console.WriteLine(band);
            }
            
        }
    }

   
}
