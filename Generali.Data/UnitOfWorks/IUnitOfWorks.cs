using Generali.Core;
using Generali.Data.Repositories.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generali.Data.UnitOfWorks
{
    public interface IUnitOfWorks: IAsyncDisposable
    {
        IRepository<T> GetRepository<T>()where T : class, IEntityBase, new();
        Task<int> SaveAsync();
        int Save();
    }
}
