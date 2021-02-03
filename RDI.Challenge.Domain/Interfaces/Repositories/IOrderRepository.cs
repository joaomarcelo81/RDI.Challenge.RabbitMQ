using RDI.Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RDI.Challenge.Domain.Interfaces.Repositories
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<Order> GetOrderAsync(long id);
        Task<IEnumerable<Order>> GetAllOrderAsync();
        Task<IEnumerable<Order>> FinByParamsOrderAsync(Func<Order, bool> lambda);
        Task AddAsync(Order Order);
    }
}
