using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace hw6
{
	class Program
	{
        private const int serverPort = 2365;
        private static TcpClient client;
        private static string fullmessage;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Tcp client start");
            Console.Write("Enter fullname: ");
            string fio = Console.ReadLine();
            Console.Write("Enter email: ");
            string email = Console.ReadLine();
            Console.Write("Enter login: ");
            string login = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            fullmessage = fio + '_' + email + '_' + login + '_' + password;
            Console.WriteLine(fullmessage);
            client = new TcpClient();
            await client.ConnectAsync(IPAddress.Loopback, serverPort);
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            await writer.WriteLineAsync(fullmessage);
            await writer.FlushAsync();
            string answer = await reader.ReadLineAsync();
            Console.WriteLine($"Srver answer: {answer}");
            client.Close();
        }
    }
}
