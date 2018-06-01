using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure_Algorithms
{
    class NumericalAlgorithms
    {
        /// <summary>
        /// The greatest common divisor (GCD) of two integers is the largest integer that 
        /// evenly divides both of the numbers. For example, GCD(60, 24) is 12 because 12 is 
        /// the largest integer that evenly divides both 60 and 24.
        /// One way to find the GCD is to factor the two numbers and see which factors they have in common.
        /// Greek mathematician Euclid recorded a faster method.
        /// The key to Euclid's algorithm is the fact that GCD(A, B) = GCD(B, value Mod B).
        /// algorithm runs in time at most O(log B).
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public long GCD(long A, long B)
        {
            long a = A, b = B;
            while (B != 0)
            {
                long r = A % B;
                A = B;
                B = r;
            }
            Console.WriteLine("GCD of " + a + " and " + b + " is " + A);
            return A;

        }

        /// <summary>
        /// The least common multiple (LCM) of integers A and B is the smallest integer 
        /// that A and B both divide into evenly.
        /// GCD can be used to calculate LCM
        /// lcm = product / gcd
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public long LCM(long A, long B)
        {
            long gcd = GCD(A, B);
            long product = A * B;
            long lcm = product / gcd;
            Console.WriteLine("LCM of " + A + " and " + B + " is " + lcm);
            return lcm;
        }


        /// <summary>
        /// This function calculates value to the power of exponent.
        /// A^2*M = (value^M)^2
        /// A^(M+N) = value^M * value^N
        /// </summary>
        /// <returns></returns>
        public long CalculateExponentiation(long value, long exponent)
        {
            //The first fact lets you quickly create powers of A where the power itself is a power of 2.
            List<Tuple<long, long>> powersOfTwoAndValues = new List<Tuple<long, long>>();
            long lastPower = 1;
            long valueToPower = value;
            powersOfTwoAndValues.Add(new Tuple<long, long>(lastPower, valueToPower));
            while (lastPower < exponent)
            {
                lastPower = lastPower * 2;
                valueToPower = valueToPower * valueToPower;
                powersOfTwoAndValues.Add(new Tuple<long, long>(lastPower, valueToPower));
            }

            // use the second fact to combine values to make the required power.
            long result = 1;
            for (int index = powersOfTwoAndValues.Count - 1; index > 0; index--)
            {
                var item = powersOfTwoAndValues[index];
                if (item.Item1 <= exponent)
                {
                    exponent = exponent - item.Item1;
                    result = result * item.Item2;
                }
            }
            return result;
        }

        // Fermat's little theorem
        public bool IsPrime(int p)
        {
            // If p is prime and 1 <= n <= p, then n^p-1 = 1 mod p, where n is any number between 1 and p.
            // Some times this may not work so we try multiple times. we can try upto 100 times which is pretty good.
            Random r = new Random();
            int k = 100;// Number of times to try to see if it is really prime.

            while (k-- > 0)
            {
                long n = r.Next(1, p);
                double np_1 = Math.Pow(n, p - 1);
                if (np_1 % p != 1)
                    return false;
            }
            return true;

        }
    }
}
