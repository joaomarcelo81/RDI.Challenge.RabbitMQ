using System;
using System.Collections.Generic;
using System.Text;

namespace RDI.Challenge.Domain
{
    public class QueueItemOrder
    {
        public string Name { get; set; }

        public long OrderId { get; set; }

        public int tableNumber { get; set; }
        public string Content { get; set; }

    }
}
