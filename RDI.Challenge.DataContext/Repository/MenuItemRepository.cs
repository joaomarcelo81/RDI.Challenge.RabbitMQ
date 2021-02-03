using Microsoft.EntityFrameworkCore;
using RDI.Challenge.Domain.Entities;
using RDI.Challenge.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RDI.Challenge.DataContext.Repository
{
    public class MenuItemRepository : BaseRepository<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(ChallengeContext _context)
            : base(_context)
        {

        }

        public async Task<IEnumerable<MenuItem>> FinByParamsMenuItemAsync(Func<MenuItem, bool> lambda)
        {
            return await FinByParamsMenuItemAsync(lambda);
        }

        public async Task<IEnumerable<MenuItem>> GetAllMenuItemAsync()
        {
            return await GetAllMenuItemAsync();
        }

        public async Task<MenuItem> GetMenuItemAsync(long id)
        {
            return await GetMenuItemAsync(id);
           
        }
    }
}