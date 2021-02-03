using System;
using System.Collections.Generic;
using System.Text;

namespace RDI.Challenge.Domain.QueueConfig
{
    public abstract class QueueConfig
    {

        public abstract string QueueName
        {
            get;
            set;
        }

    }
}
