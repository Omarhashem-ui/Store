using Store.G02.Domain;
using Store.G02.Domain.Contracts;
using Store.G02.Persistence.Data.Contexts;
using Store.G02.Persistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Persistence
{
    public class UnitOfWork(StoreDbContext _context) : IUnitOfWork
    {
        //private Dictionary<string,object> _repositorys = new Dictionary<string,object>();
        private ConcurrentDictionary<string, object> _repositories = new ConcurrentDictionary<string, object>();
        //public IGenericRepository<Tkey, TEntity> GetRepository<Tkey, TEntity>() where TEntity : BaseEntity<Tkey>
        //{
        //    var key = typeof(TEntity).Name;
        //    if(!_repositorys.ContainsKey(key))
        //    {
        //        var repository = new GenericRepository<Tkey, TEntity>(_context);
        //        _repositorys.Add(key, repository);
        //    }
        //  return (IGenericRepository<Tkey, TEntity>) _repositorys[key];

        //}

        public IGenericRepository<Tkey, TEntity> GetRepository<Tkey, TEntity>() where TEntity : BaseEntity<Tkey>
        {
            return (IGenericRepository<Tkey, TEntity>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<Tkey, TEntity>(_context));
        }


        public async Task<int> SaveChangesAsync()
        {
         return await _context.SaveChangesAsync();   
        }
    }
}
