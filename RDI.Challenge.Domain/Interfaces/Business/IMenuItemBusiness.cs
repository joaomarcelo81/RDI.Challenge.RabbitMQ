using RDI.Challenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RDI.Challenge.Domain.Interfaces.Business
{
    public interface IMenuItemBusiness : IBaseBusiness<MenuItem>
    {
        Task<MenuItem> GetMenuItemAsync(long id);
        Task<IEnumerable<MenuItem>> GetAllMenuItemAsync();
        Task<IEnumerable<MenuItem>> FinByParamsMenuItemAsync(Func<MenuItem, bool> lambda);
    }
}
