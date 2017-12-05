using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class zadanie7
    {
        static AutoResetEvent autoEvent = new AutoResetEvent(false);
        public static FileStream fs;
        public static byte[] buffer;

        public void zad7()
        {
            fs = new FileStream("plik.txt", FileMode.Open, FileAccess.Read, FileShare.Read);        //odczyt
            buffer = new byte[1024];
            IAsyncResult ar = fs.BeginRead(buffer, 0, 1024, null, new object[] { fs, buffer });

            fs.EndRead(ar);                                                                         //zamykanie

            Console.WriteLine(Encoding.Default.GetString(buffer));

            autoEvent.WaitOne();
        }
    }
}
