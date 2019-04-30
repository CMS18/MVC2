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

            // Vänta på ett anslutning
            Socket handler = listener.Accept();

            // Öppna upp en kanal för att läsa vad webbläsaren skickar för request
            // och för att kunna skicka tillbaka ett svar.
            var ns = new NetworkStream(handler);
            var reader = new StreamReader(ns);
            var writer = new StreamWriter(ns);

            // Läs första raden i Request med Verb, Path och protokoll.
            var command = reader.ReadLine();
            Console.WriteLine($"Command {command}");
            // Läs in följande rader med Headers.
            var headers = new List<string>();
            var header = reader.ReadLine();
            while (!string.IsNullOrEmpty(header))
            {
                headers.Add(header);
                Console.WriteLine($"Header => {header}");
                header = reader.ReadLine();
            }
            Console.WriteLine("Request Received!");

            // Dags att skicka svaret...
            Console.WriteLine("Writing RESPONSE!");

            // TODO: Ersätt nedanstående för att skriva ut olika 
            //       svar beroende på anropet.

            // Börja med att skicka statuskod och statustext
            writer.WriteLine("HTTP/1.1 200 OK");
            // Sedan följer Headers som avslutas med en tom rad.
            writer.WriteLine("Content-Type: text/html; charset=UTF-8");
            writer.WriteLine();
            // Och slutligen response dokumentet
            writer.WriteLine("<h1>Hello World!</h1>");

            // Precis som på dass... spola för att skicka iväg svaret till webbläsaren.
            writer.Flush();

            // Stäng förbindelsen.
            handler.Shutdown(SocketShutdown.Both);
            handler.Close();

            Console.WriteLine("Done!");



        }
    }
}
