using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace SimpleWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting web server...");

            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, 34567);

            Socket listener = new Socket(IPAddress.Any.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            listener.Bind(localEndPoint);
            listener.Listen(100);

            Console.WriteLine("Waiting for a connection on http://127.0.0.1:34567/");

            while (true)
            {
                Socket handler = listener.Accept();

                var ns = new NetworkStream(handler);
                var reader = new StreamReader(ns);
                var writer = new StreamWriter(ns);

                var command = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(command)) continue;
                var parts = command.Split(' ');
                var verb = parts[0];
                var path = parts[1];
                Console.WriteLine($"Command {command}");

                var headers = new List<string>();

                var header = reader.ReadLine();
                while (!string.IsNullOrEmpty(header))
                {
                    headers.Add(header);
                    Console.WriteLine($"Header _> {header}");
                    header = reader.ReadLine();
                }

                Console.WriteLine("Got command and headers in REQUEST!");

                Console.WriteLine("Writing RESPONSE!");

                if (path.StartsWith("/home"))
                {
                    writer.WriteLine("HTTP/1.1 200 OK");
                    writer.WriteLine("Content-Type: text/html; charset=UTF-8");
                    writer.WriteLine("Connection: close");
                    writer.WriteLine("");
                    writer.WriteLine("<h1>Du är hemma</h1>");
                    writer.WriteLine("<p>CMS18 is Awesome!</p>");
                }
                else if (path.StartsWith("/sayhello"))
                {
                    var index = path.IndexOf("name=", StringComparison.InvariantCultureIgnoreCase);
                    var name = "";
                    if (index > 0)
                    {
                        name = path.Substring(index + 5);
                    }

                    writer.WriteLine("HTTP/1.1 200 OK");
                    writer.WriteLine("Content-Type: text/html; charset=UTF-8");
                    writer.WriteLine("Connection: close");
                    writer.WriteLine("");
                    writer.WriteLine($"<h1>Hej {name}!</h1>");
                    writer.WriteLine($"<form><label>Namn: <input name='name'></label><button>Skicka</button></form>");
                }
                else 
                {
                    writer.WriteLine("HTTP/1.1 404 Not Found");
                    writer.WriteLine("Content-Type: text/html; charset=UTF-8");
                    writer.WriteLine("Connection: close");
                    writer.WriteLine("");
                    writer.WriteLine("<h1>404 Finns i sjön</h1>");
                }
                writer.Flush();

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();

                Console.WriteLine("Done!");
            };


        }
    }
}
