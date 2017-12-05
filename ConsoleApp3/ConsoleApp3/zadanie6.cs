using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class zadanie6
    {
        public static FileStream fs;
        public static byte[] buffer;
        static AutoResetEvent autoEvent = new AutoResetEvent(false);

        public static void callback(IAsyncResult state)
        {
            fs.EndRead(state);
            Console.WriteLine(Encoding.Default.GetString(buffer));
        }

        public static void Read()
        {
            fs = new FileStream("plik.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
            buffer = new byte[1024];
            fs.BeginRead(buffer, 0, 1024, new AsyncCallback(callback), new object[] { fs, buffer });
        }

        public void zad6()
        {
            Read();
            autoEvent.WaitOne();
        }
    }
}
