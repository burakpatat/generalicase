using Generali.Data.Context;
using Generali.Data.Repositories.Abstractions;
using Generali.Data.Repositories.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generali.Data.UnitOfWorks
{
    public class UnitOfWorks : IUnitOfWorks
    {
        private readonly AppDbContext dbContext;
        public UnitOfWorks(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();
        }

        public int Save()
        {
            return dbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        IRepository<T> IUnitOfWorks.GetRepository<T>()
        {
            return new Repository<T>(dbContext);
        }
    }
}
