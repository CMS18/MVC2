using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace SimpleSocketApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting!");

            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 34567);
            Socket listener = new Socket(IPAddress.Any.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(100);

            Console.WriteLine("Waiting for a connection...");
            Socket handler = listener.Accept();

            IPEndPoint newclient = (IPEndPoint)handler.RemoteEndPoint;
            Console.WriteLine("Connected with {0} at port {1}", newclient.Address, newclient.Port);

            var ns = new NetworkStream(handler);
            var reader = new StreamReader(ns);
            var writer = new StreamWriter(ns);

            string line;
            do
            {
                line = reader.ReadLine();
                Console.WriteLine(line);

            } while (!string.IsNullOrEmpty(line));

            writer.WriteLine("HTTP/1.0 200 OK");
            writer.WriteLine("");
            writer.WriteLine("<h1> Hello World!");
            writer.Flush();



            handler.Shutdown(SocketShutdown.Both);
            handler.Close();
        }
    }
}
