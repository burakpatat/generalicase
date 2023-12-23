using Generali.Core;
using Generali.Data.Context;
using Generali.Data.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Generali.Data.Repositories.Concretes
{
    public class Repository<T>: IRepository<T> where T: class, IEntityBase, new()
    {
        private readonly AppDbContext _dbContext;
        public Repository(AppDbContext appDbContext) 
        {
            this._dbContext = appDbContext;
        } 
        private DbSet<T>Table { get => _dbContext.Set<T>(); }

        public async Task AddAsync(T entity)
        {
            await Table.AddAsync(entity);
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            if(predicate != null)
                query = query.Where(predicate);

            if (includeProperties.Any())
            {
                foreach (var property in includeProperties)
                    query = query.Include(property);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = Table;
            query = query.Where(predicate);

            if (includeProperties.Any())
            {
                foreach (var property in includeProperties)
                    query = query.Include(property);
            }

            return await query.SingleAsync();
        }

        public async Task<T> GetByGuid(Guid guid)
        {
            return await Table.FindAsync(guid);
        }
        public async Task DeleteAsync(T entity)
        {
            await Task.Run(() => Table.Remove(entity));
        }
    }
}
