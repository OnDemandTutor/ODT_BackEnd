using Microsoft.EntityFrameworkCore;
using ODT_Repository.Entity;
using ODT_Repository.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODT_Repository.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected MyDbContext _context;
        protected DbSet<TEntity> dbSet;

        public GenericRepository(MyDbContext context)
        {
            _context = context;
            dbSet = _context.Set<TEntity>();
        }

        public Task<TEntity> Add(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetByFilterAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> filterExpression)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
