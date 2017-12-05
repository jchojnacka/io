using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        delegate int DelegateType(int x, int y);

        static void Main(string[] args)
        {

            int test = 0;
            byte[] buffer = new byte[128];
            Console.WriteLine("begin main");
            Task task = OperationTask(buffer);
            Thread.Sleep(test);
            Console.WriteLine("progress main");
            task.Wait();
            Console.WriteLine("end main");
            Console.ReadKey();
        }


        public static async Task OperationTask(object data)
        {
            Console.WriteLine("Begin task");
            await Task.Run(() =>
                {
                Console.WriteLine("begin async");
                Thread.Sleep(100);
                DelegateType foo = (x, y) => x = y;
                foo.Invoke(20, 7);
                Console.WriteLine("end async");
            });
            Console.WriteLine("end task");
        }
    }

    
}
