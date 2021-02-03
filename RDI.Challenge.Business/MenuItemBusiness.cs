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
    public class MenuItemBusiness : BaseBusiness<MenuItem>, IMenuItemBusiness
    {

        private IMenuItemRepository _Repository { get; set; }
     

        public MenuItemBusiness(IMenuItemRepository serviceBase)
            : base(serviceBase)
        {
            _Repository = serviceBase;
          
        }

        public async Task<MenuItem> GetMenuItemAsync(long id)
        {
            return await _Repository.GetMenuItemAsync(id);
        }

        public async Task<IEnumerable<MenuItem>> GetAllMenuItemAsync()
        {
            return await _Repository.GetAllMenuItemAsync();
        }

        public async Task<IEnumerable<MenuItem>> FinByParamsMenuItemAsync(Func<MenuItem, bool> lambda)
        {
            return await _Repository.FinByParamsMenuItemAsync(lambda);
        }
    }
}
