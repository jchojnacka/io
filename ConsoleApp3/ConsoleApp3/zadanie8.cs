using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class zadanie8
    {
        public delegate long del(long n);

        public static long silnia_rekurencja(long n)
        {
            if (n <= 1)
                return 1;
            else
                return n * silnia_rekurencja(n - 1);
        }

        public static long silnia_iteracyjnie(long n)
        {
            long temp = 1;
            if (n <= 1)
                return 1;
            else
            {
                while (n > 1)
                {
                    temp *= n;
                    n--;
                }
                return temp;
            }
        }

        public static long fibonacci_rekurencja(long n)
        {
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;
            else
                return fibonacci_rekurencja(n - 1) + fibonacci_rekurencja(n - 2);
        }

        public static long fibbonacci_iteracyjnie(long n)
        {
            long fib1 = 0;
            long fib2 = 1;
            if (n == 0) return 0;
            for (long i = 1; i < n; i++)
            {
                fib2 += fib1;
                fib1 = fib2 - fib1;
            }
            return fib2;
        }

        public void zad8()
        {
            del d1 = new del(silnia_iteracyjnie);
            del d2 = new del(silnia_rekurencja);
            del d3 = new del(fibbonacci_iteracyjnie);
            del d4 = new del(fibonacci_rekurencja);

            IAsyncResult ar1 = d1.BeginInvoke(20, null, null);
            long result1 = d1.EndInvoke(ar1);
            Console.WriteLine("Silnia obliczona iteracyjnie: " + result1);

            IAsyncResult ar2 = d2.BeginInvoke(10, null, null);
            long result2 = d2.EndInvoke(ar2);
            Console.WriteLine("Silnia obliczona rekurencyjnie: " + result2);

            IAsyncResult ar3 = d3.BeginInvoke(10, null, null);
            long result3 = d3.EndInvoke(ar3);
            Console.WriteLine("Fibonacci obliczony iteracyjnie: " + result3);

            IAsyncResult ar4 = d4.BeginInvoke(10, null, null);
            long result4 = d4.EndInvoke(ar4);
            Console.WriteLine("Fibonacci obliczony rekurencyjnie: " + result4);

            Console.ReadKey();
        }
    }
}
