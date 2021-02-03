using System;
using System.Collections.Generic;
using System.Text;

namespace RDI.Challenge.Domain.Interfaces.Repositories.Rabbit
{
    public interface IRabbitRepository
    {
        void SendOrder(string queueName, QueueItemOrder queueItemOrder);
        IEnumerable<string> ReceiveOrder(string queueName);
    }
}
