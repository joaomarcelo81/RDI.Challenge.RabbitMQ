using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

namespace RDI.Challenge
{
    class Program
    {
        public static void Main()
        {

            while (true)
            {
                var builder = new ConfigurationBuilder()
     .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                IConfigurationRoot config = builder.Build();
                var _hostname = config.GetSection("RabbitMQ").GetSection("HostName").Value;
                var _username = config.GetSection("RabbitMQ").GetSection("UserName").Value;
                var _password = config.GetSection("RabbitMQ").GetSection("Password").Value;
                var _queuePreFix = config.GetSection("RabbitMQ").GetSection("QueuePreFix").Value;



                var factory = new ConnectionFactory
                {
                    HostName = _hostname,
                    UserName = _username,
                    Password = _password


                };
                using var connection = factory.CreateConnection();
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "salad-queue.rdi.challenge", durable: false, exclusive: false, autoDelete: false, arguments: null);



                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine(" [x] Received {0}", message);
                    };
                    channel.BasicConsume(queue: "salad-queue.rdi.challenge",
                                         autoAck: true,
                                         consumer: consumer);

                    Console.WriteLine("Executando listagem");

                }
                Thread.Sleep(5000);
            }

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
