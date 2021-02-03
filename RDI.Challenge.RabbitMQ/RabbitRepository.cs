using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RDI.Challenge.Domain;
using RDI.Challenge.Domain.Interfaces.Repositories.Rabbit;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDI.Challenge.RabbitMQ
{
    public class RabbitRepository : IRabbitRepository
    {
        public void SendOrder(string queueName, QueueItemOrder queueItemOrder)
        {
            try
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
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    QueueProducer.Publish(channel, queueName + _queuePreFix, queueItemOrder);
                }
            }
            catch (Exception ex) 
            {

                throw ex;
            }
        }

        public IEnumerable<string> ReceiveOrder(string queueName)
        {

            IEnumerable<string> messages = new List<string>();
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
                messages = QueueReceiver.Consume(channel, queueName + _queuePreFix);
            }
            return messages;
        }
    }
}
