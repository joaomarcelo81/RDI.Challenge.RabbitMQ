using RDI.Challenge.Domain.Interfaces.Business;
using RDI.Challenge.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RDI.Challenge.Business
{
    public class BaseBusiness<TEntity> : IDisposable, IBaseBusiness<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _repository;

        public BaseBusiness(IBaseRepository<TEntity> serviceBase)
        {
            _repository = serviceBase;
        }

        public async Task<TEntity> Get(long id)
        {
            return await _repository.Get(id);
        }

        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
            => await _repository.FirstOrDefault(predicate);
        public async Task Add(TEntity obj)
        {
            await _repository.Add(obj);
          
        }

        public Task Update(long id, TEntity obj)
        {
            return _repository.Update(id, obj);
        }

        public Task Remove(TEntity obj)
        {
            return _repository.Remove(obj);
   
        }



        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await _repository.GetAll();
        }


        public async Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repository.GetWhere(predicate);
        }


        public async Task<int> CountAll() => await _repository.CountAll();

        public async Task<int> CountWhere(Expression<Func<TEntity, bool>> predicate)
            => await _repository.CountWhere(predicate);




        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}
