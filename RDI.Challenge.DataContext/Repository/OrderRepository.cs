using RDI.Challenge.Domain.Entities;
using RDI.Challenge.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RDI.Challenge.DataContext.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ChallengeContext _context)
            : base(_context)
        {

        }

        public async Task AddAsync(Order Order)
        {
            await this.Add(Order);
        }

        public async Task<IEnumerable<Order>> FinByParamsOrderAsync(Func<Order, bool> lambda)
        {
            return await FinByParamsOrderAsync(lambda);
        }

        public async Task<IEnumerable<Order>> GetAllOrderAsync()
        {
            return await GetAllOrderAsync();
        }

        public async Task<Order> GetOrderAsync(long id)
        {
            return await GetOrderAsync(id);

        }
    }
}
