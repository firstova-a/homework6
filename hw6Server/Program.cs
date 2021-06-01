using hw6Server.EF;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace hw6Server
{
	class Program
	{
		private static TcpListener listener;
        private const int serverPort = 2365;
        private static bool run;

        static async Task Main(string[] args)
        {
            Console.WriteLine("Server start");
            using (AtistContext context = new AtistContext())
                context.Database.EnsureCreated();
            listener = new TcpListener(IPAddress.Any, serverPort);
            run = true;
            await Listen();
        }

        private static async Task Listen()
        {
            List<Task> registerTasks = new List<Task>();
            listener.Start();
            while (run)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                registerTasks.Add(RegisterCustomer(client));
                registerTasks.RemoveAll(t => t.IsCompleted);
            }
            listener.Stop();
            foreach (Task task in registerTasks)
                await task;
        }

        private static async Task RegisterCustomer(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            StreamReader reader = new StreamReader(stream);
            StreamWriter writer = new StreamWriter(stream);
            string fullmessage = await reader.ReadLineAsync();
            using (AtistContext context = new AtistContext())
            {
                IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();
                try
                {
                    string[] query = fullmessage.Split("_");
                    Console.WriteLine(query[0] + Environment.NewLine + query[1] + Environment.NewLine +
                            query[2] + Environment.NewLine + query[3] + Environment.NewLine);
                    if (IsValidEmail(query[1]))
                    {
                        try
                        {
                            byte[] tmpSource;
                            byte[] tmpHash;
                            tmpSource = ASCIIEncoding.ASCII.GetBytes(query[3]);
                            tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
                            Customer newUser = new Customer()
                            {
                                FIO = query[0],
                                ContactEmail = query[1],
                                Login = query[2],
                                PasswordHash = tmpHash
                            };
                            context.Customers.Add(newUser);
                            await transaction.CommitAsync();
                            await context.SaveChangesAsync();
                            await writer.WriteLineAsync("SUCCESS REGISTRATION");
                        }
                        catch
                        {
                            await writer.WriteLineAsync("FAILED REGISTRATION");
                        }

                    }
                    else
                    {
                        await writer.WriteLineAsync("FAILED EMAIL");
                    }
                }
                catch
                {
                    await writer.WriteLineAsync("FAILED OPERATION");
                }
                await writer.FlushAsync();
            }
            client.Close();
        }
        public static bool IsValidEmail(string source)
        {
            return new EmailAddressAttribute().IsValid(source);
        } 
	}
}
