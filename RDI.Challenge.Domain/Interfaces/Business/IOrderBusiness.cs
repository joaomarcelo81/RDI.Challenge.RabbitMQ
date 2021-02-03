using RDI.Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RDI.Challenge.Domain.Interfaces.Business
{
    public interface IOrderBusiness : IBaseBusiness<Order>
    {
   
        void DirectToArea(string area, QueueItemOrder queueItemOrder);

        Task<IEnumerable<QueueItemOrder>> GetMenuItemsFromOrder(string area);

        Task AddAsync(Order Order);
    }
}
