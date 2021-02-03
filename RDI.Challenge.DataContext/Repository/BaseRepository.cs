using RDI.Challenge.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace RDI.Challenge.DataContext.Repository
{
    public class BaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity> where TEntity : class
    {


        protected ChallengeContext Context;

        //   private System.Data.Entity.DbContextTransaction dbTran;
        //http://www.entityframeworktutorial.net/entityframework6/transaction-in-entity-framework.aspx


        public BaseRepository(ChallengeContext _context)
        {
            Context = _context;        
        }
        public async Task<TEntity> Get(long id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
            => await Context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        public async Task Add(TEntity obj)
        {
            await Context.Set<TEntity>().AddAsync(obj);
       //     await Context.SaveChangesAsync();
        }

        public Task Update(long id, TEntity obj)
        {
            if (Context.Set<TEntity>().Find(id) != null)
            {
                Context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
               return Context.SaveChangesAsync();
            }
            else
            {
                return Add(obj);
            }
        }

        public Task Remove(TEntity obj)
        {
            Context.Set<TEntity>().Remove(obj);
            return Context.SaveChangesAsync();
        }

     

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }
    

        public async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>().Where(predicate).ToListAsync();
        }


        public async Task<int> CountAll() => await Context.Set<TEntity>().CountAsync();

        public async Task<int> CountWhere(Expression<Func<TEntity, bool>> predicate)
            => await Context.Set<TEntity>().CountAsync(predicate);



        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public virtual void Dispose()
        {


            Dispose(true);
            GC.SuppressFinalize(this);

        }

    
    }
}
