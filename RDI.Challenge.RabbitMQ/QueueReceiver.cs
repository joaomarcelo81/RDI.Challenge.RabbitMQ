using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDI.Challenge.RabbitMQ
{
    internal static class QueueReceiver
    {

        public static IEnumerable<string> Consume(IModel channel, string _queueName)
        {
            IList<string> messages = new List<string>();
            //var consumer = new EventingBasicConsumer(channel);            
            //consumer.Received += (model, ea) =>
            //{
            //    var body = ea.Body.ToArray();
            //    var message = Encoding.UTF8.GetString(body);
                
            //};

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (ch, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                channel.BasicAck(ea.DeliveryTag, false);
            };
            // this consumer tag identifies the subscription
            // when it has to be cancelled
            String consumerTag = channel.BasicConsume(_queueName, false, consumer);

            //channel.BasicConsume(_queueName,
            //                     autoAck: true,
            //                     consumer: consumer);

            
            return messages;
        }
    }
}
