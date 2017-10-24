using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IOlab1
{
    class Program
    {

        private static Object thisLock = new Object();


        static void Main(string[] args)
        {

            //tworzenie obiektu serwera i akceptacja polaczenia
            TcpListener Server = new TcpListener(IPAddress.Any, 2048);
            Server.Start();

            ThreadPool.QueueUserWorkItem(client);
            ThreadPool.QueueUserWorkItem(client);
            ThreadPool.QueueUserWorkItem(client);


            while (true)
            {
                //oczekiwanie na polaczenie
                TcpClient client = Server.AcceptTcpClient();        
                ThreadPool.QueueUserWorkItem(server, client);
            }


        }

        
        static void writeConsoleMessage(string message, ConsoleColor color)
        {
            message = message.Replace("\0", "");

            lock (thisLock)     // problem synchronizacji
                                // z uzyciem lock rozwiazalismy problem blednych kolorow przy otrzymaniu wiadomosci,
                                // bo wpuszcza tylko jeden watek
            {                   // jak watkow jest kilka i jeden zmieni kolor, drugi moze dostac procesor i wypisuje
                Console.ForegroundColor = color;
                Console.WriteLine(message);
                Console.ResetColor();
            }
            Thread.Sleep(300);
        }


        static void server(Object stateInfo)
        {

            TcpClient client = (TcpClient)stateInfo;


            while (true)
            {
                //tab do zapisywania tresci wiadomosci
                byte[] buffer = new byte[1024];
                client.GetStream().Read(buffer, 0, 1024);
                writeConsoleMessage("SERWER Otrzymalem wiadomosc: " + System.Text.Encoding.Default.GetString(buffer, 0, buffer.Length), ConsoleColor.DarkGreen);
                client.GetStream().Write(buffer, 0, buffer.Length);
            }
        }
        static void client(Object stateInfo)
        {
            TcpClient client = new TcpClient();
            client.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2048));

            while (true)
            {
                byte[] message = new ASCIIEncoding().GetBytes("Wiadomosc");
                client.GetStream().Write(message, 0, message.Length);
                byte[] buffer = new byte[1024];
                client.GetStream().Read(buffer, 0, buffer.Length);
                writeConsoleMessage("KLIENT  Otrzymalem wiadomosc: " + System.Text.Encoding.Default.GetString(buffer, 0, buffer.Length), ConsoleColor.DarkRed);
            }
        }
    }
}
