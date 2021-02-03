using Newtonsoft.Json;
using RabbitMQ.Client;
using RDI.Challenge.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RDI.Challenge.RabbitMQ
{
    internal static class QueueProducer
    {
        public static void Publish(IModel channel, string _queueName, QueueItemOrder queueItemOrder)
        {
            channel.QueueDeclare(_queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

           


            var body = Encoding.UTF8.GetBytes(
                JsonConvert.SerializeObject(queueItemOrder)
                );

            channel.BasicPublish(exchange: "",
                              routingKey: _queueName,
                              basicProperties: null,
                              body: body);


        }
    }
}
