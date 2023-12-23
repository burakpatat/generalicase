using Generali.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Generali.Data.Repositories.Abstractions
{
    public interface IRepository<T> where T : class, IEntityBase, new()
    {
        Task AddAsync(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        Task<T> GetByGuid(Guid guid);
        Task DeleteAsync(T entity);
    }
}
