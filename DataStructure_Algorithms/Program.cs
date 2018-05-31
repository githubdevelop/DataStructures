using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure_Algorithms
{
    class Program
    {
        static void Main(string[] args)
        {
            SimilarString str = new SimilarString();
            string[] inputGroup = {"kccomwcgcs","socgcmcwkc","sgckwcmcoc","coswcmcgkc","cowkccmsgc","cosgmccwkc","sgmkwcccoc","coswmccgkc","kowcccmsgc","kgcomwcccs"};

            str.NumSimilarGroups(inputGroup);

            //TestSomething();
            Sort s = new Sort();
            var p = new Program();
            p.IsPrime(17);

            p.GCD(78, 66);
            p.GCD(66,78);
            p.GCD(12, 12);

            p.LCM(20, 30);
            p.LCM(100, 35);
            
            s.InsertionSort();
            s.SelectionSort();
            s.BubbleSort();
            s.HeapSort();
            s.QuickSort();
            s.QuickSort2();
            s.QuickSort2Iterative();
        }

        long GCD (long A, long B)
        {

            long a = A, b = B;
            while(B!= 0)
            {
                long r = A % B;
                A = B;
                B = r;
            }
            Console.WriteLine("GCD of " + a + " and " + b + " is " + A);
            return A;

        }

        long LCM (long A, long B)
        {
            long gcd = GCD(A, B);
            long product = A * B;
            long lcm = product / gcd;
            Console.WriteLine("LCM of " + A + " and " + B + " is " + lcm);
            return lcm;
        }

        // Fermat's little theorem
        bool IsPrime(int p)
        {
            // If p is prime and 1 <= n <= p, then n^p-1 = 1 mod p, where n is any number between 1 and p.
            // Some times this may not work so we try multiple times. we can try upto 100 times which is pretty good.
            Random r = new Random();
            int k = 100;// Number of times to try to see if it is really prime.

            while ( k-- > 0)
            {
                long n = r.Next(1, p);
                double np_1 = Math.Pow(n, p - 1);
                if (np_1 % p != 1)
                    return false;
            }
            return true;
            
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
