using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Opg5
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 4646;
            int clientNr = 0;

            Console.WriteLine("Hello Echo Server!");

            IPAddress ip = GetIp();
            TcpListener ServerListener = StartServer(ip, port);

            do
            {
                TcpClient ClientConnection = GetConnectionSocket(ServerListener, ref clientNr);
                Task.Run(() => ReadWriteStream(ClientConnection, ref clientNr));

            } while (clientNr != 0);

            StopServer(ServerListener);
        }

        private static void StopServer(TcpListener serverListener)
        {
            serverListener.Stop();
            Console.WriteLine("listener stopped");
        }

        private static TcpClient GetConnectionSocket(TcpListener serverListener, ref int clientNr)
        {

            TcpClient connectionSocket = serverListener.AcceptTcpClient();
            clientNr++;
            Console.WriteLine("Client " + clientNr + " connected");
            return connectionSocket;
        }

        private static void ReadWriteStream(TcpClient connectionSocket, ref int clientNr)
        {
            List<Bog> books = new List<Bog>()
            {
                new Bog("Mit harem og mig!", "Lars den store", 222, "1234567890123"),
                new Bog("Hvordan grapefrugt ændrede mit liv!", "Lars den store", 222, "1234567890124"),
                new Bog("Wife mass exodus", "Lars den store", 222, "1234567890125"),
                new Bog("NO THIS IS PATRICK", "Lars den store", 222, "1234567890126"),
                new Bog("Hvis bare jeg var Tom", "Lars den store", 222, "1234567890127")
            };

        Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string message = sr.ReadLine();
            Thread.Sleep(1000);
            while (message != null && message != "")
            {
                Console.WriteLine("Client " + clientNr + ": " + message);

                string[] messageArray = message.Split(' ');
                string subMessage = message.Substring(message.IndexOf(' ') + 1);
                Thread.Sleep(1000);

                switch (messageArray[0])
                {
                    case "GetAll":
                        sw.WriteLine("Getting all books");
                        sw.WriteLine(JsonConvert.SerializeObject(books));
                        break;
                    case "Get":
                        sw.WriteLine("Getting book with isbn13: "+subMessage);
                        sw.WriteLine(JsonConvert.SerializeObject(books.Find(x => x.Isbn13 == subMessage)));
                        break;
                    case "Save":
                        sw.WriteLine("Book Saved");
                        Bog bog = JsonConvert.DeserializeObject<Bog>(subMessage);
                        books.Add(bog);
                        break; ;
                        
                }
                message = sr.ReadLine();
            }

            Console.WriteLine("Empty message detected");
            ns.Close();
            connectionSocket.Close();
            clientNr--;
            Console.WriteLine("connection socket " + clientNr + " closed");

        }

        private static TcpListener StartServer(IPAddress ip, int port)
        {
            TcpListener serverSocket = new TcpListener(ip, port);
            serverSocket.Start();

            Console.WriteLine("server started waiting for connection!");

            return serverSocket;
        }

        private static IPAddress GetIp()
        {
            string name = "google.com";
            IPAddress[] addrs = Dns.GetHostEntry(name).AddressList;
            Console.WriteLine("Google IP returned by GetHostEntry" + addrs[0]);

            IPAddress ip = IPAddress.Parse("192.168.0.100");
            Console.WriteLine("Local host IP:" + ip);
            return ip;
        }
    }
}
