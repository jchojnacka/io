using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp3
{
    class zadanie5
    {
        static object obj = new object();
        static double[] tab;
        static int rozmiartab = 20;

        static double suma1 = 0;


        public static void suma(object ob)      //sumowanie elementow w tab
        {
            double suma = 0;
            int o = (int)ob;
            for (int i = o; i < o + 10; i++)
            {
                suma += tab[i];
            }

            lock (obj) suma1 += suma;
            Console.WriteLine("o: " + o + " " + "suma1: " + suma1);
        }

        public void zad5()
        {
            Random r = new Random();
            tab = new double[rozmiartab];
            for (int i = 0; i < rozmiartab; i++)            //wypelnienie random
            {
                tab[i] = r.NextDouble();
                Console.WriteLine("i: " + i + " " + "random: " + tab[i]);
            }


            Thread[] t = new Thread[rozmiartab];            //tablica watkow
            for (int i = 0; i < 10; i++)
            {
                t[i] = new Thread(suma);
                t[i].Start(i);
                Console.WriteLine("Thread " + i + " start");
            }


            for (int i = 0; i < 10; i++)        //zakonczenie
            {
                t[i].Join();
            }
            Console.ReadKey();
        }
    }
}