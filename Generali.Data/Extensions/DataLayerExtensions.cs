using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generali.Data.Repositories.Abstractions;
using Generali.Data.Repositories.Concretes;
using Generali.Data.Context;
using Microsoft.EntityFrameworkCore;
using Generali.Data.UnitOfWorks;

namespace Generali.Data.Extensions
{
    public static class DataLayerExtensions
    {
        public static IServiceCollection LoadDataLayerExtensions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>),typeof(Repository<>));
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(
            configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWorks, UnitOfWorks.UnitOfWorks>();

            return services;
        }
    }
}
