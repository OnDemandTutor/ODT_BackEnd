using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ODT_Repository.Entity;

namespace ODT_Repository.Repository
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {

        IEnumerable<TEntity> Get(
    Expression<Func<TEntity, bool>> filter = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
    string includeProperties = "",
    int? pageIndex = null,
    int? pageSize = null);

        TEntity GetByID(object id);
        void Insert(TEntity entity);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
        void Update(TEntity entityToUpdate);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetByFilterAsync(Expression<Func<TEntity, bool>> filterExpression);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(long id);
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<RolePermission>> GetRolePermissionsByRoleIdAsync(long roleId);
        Task<Token> GetUserToken(long id);
        Task<IEnumerable<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            int? pageIndex = null,
            int? pageSize = null);
        Task SaveChangesAsync();
        Task<Order> GetOrderByPaymentAsync(long transactionId);
        Task<TEntity> GetByIdWithInclude(long id, string includeProperties = "");

        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

    }
}
