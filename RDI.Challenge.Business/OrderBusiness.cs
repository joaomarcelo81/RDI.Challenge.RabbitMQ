using Newtonsoft.Json;
using RDI.Challenge.Domain;
using RDI.Challenge.Domain.Entities;
using RDI.Challenge.Domain.Interfaces.Business;
using RDI.Challenge.Domain.Interfaces.Repositories;
using RDI.Challenge.Domain.Interfaces.Repositories.Rabbit;
using RDI.Challenge.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RDI.Challenge.Business
{
    public class OrderBusiness : BaseBusiness<Order>, IOrderBusiness
    {

        private IOrderRepository _Repository { get; set; }
        private IRabbitRepository _RabbitRepository { get; set; }

        public OrderBusiness(IOrderRepository serviceBase, IRabbitRepository rabbitRepository)
            : base(serviceBase)
        {
            _Repository = serviceBase;
            _RabbitRepository = rabbitRepository;
        }

        public async Task AddAsync(Order obj)
        {
            foreach (var item in obj.MenuItems)
            {
                DirectToArea(item.Area, new QueueItemOrder()
                {
                    Name = item.Name,
                    Content = obj.OrderId.ToString(),
                    OrderId = obj.OrderId,
                    tableNumber = obj.TableNumber

                });
            }
        await _Repository.Add(obj);
         
        }

        public void DirectToArea(string area, QueueItemOrder queueItemOrder)
        {
            _RabbitRepository.SendOrder(area, queueItemOrder);
        }

        public async Task<IEnumerable<QueueItemOrder>> GetMenuItemsFromOrder(string area)
        {
            return await Task.FromResult<IEnumerable<QueueItemOrder>>(GetMenuItemsFromOrderReturn(area));            
        }
        private IEnumerable<QueueItemOrder> GetMenuItemsFromOrderReturn(string area)
        {
            var list = _RabbitRepository.ReceiveOrder(area);
            var queueItemOrderList = new List<QueueItemOrder>();
            foreach (var item in list)
            {
                queueItemOrderList.Add(JsonConvert.DeserializeObject<QueueItemOrder>(item));
            }
            return queueItemOrderList;
        }


    }
}
