using Generali.Data.Context;
using Generali.Data.Repositories.Abstractions;
using Generali.Data.Repositories.Concretes;
using Generali.Data.UnitOfWorks;
using Generali.Service.Services.Abstractions;
using Generali.Service.Services.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Generali.Service.Extension
{
    public static class ServiceLayerExtension
    {
        public static IServiceCollection LoadServiceLayerExtensions(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddScoped<IProductService, ProductService>();

            services.AddAutoMapper(assembly);
            return services;
        }
    }
}
