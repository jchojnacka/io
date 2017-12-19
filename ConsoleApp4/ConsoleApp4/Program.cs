using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;



// zadanie 11
namespace ConsoleApp4
{
    class Program
    {
        static BackgroundWorker background_worker = new BackgroundWorker();
        static void Main(string[] args)
        {

            TcpListener server = new TcpListener(IPAddress.Any, 2048);
            server.Start();
                                                                                //gdy wywolana RunWorkerAsync,ProgressReport
            background_worker.DoWork += Work;
            background_worker.ProgressChanged += NextProcess;
            background_worker.WorkerReportsProgress = true;



            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                background_worker.RunWorkerAsync(client);
            }


        }

        private static void Work(object sender, DoWorkEventArgs e)
        {
            TcpClient client = (TcpClient)e.Argument;
            int message = 0;
            while (message < 100)
            {

                byte[] buffer = new byte[1024];
                //odczyt
                client.GetStream().Read(buffer, 0, 1024);
                //wysylanie
                client.GetStream().Write(buffer, 0, buffer.Length);

                message += 1;
                background_worker.ReportProgress(message);

            }
        }
        private static void NextProcess(object sender, ProgressChangedEventArgs e)
        {
            Console.WriteLine("message: " + e.ProgressPercentage, ConsoleColor.Green);
        }
    }
}

