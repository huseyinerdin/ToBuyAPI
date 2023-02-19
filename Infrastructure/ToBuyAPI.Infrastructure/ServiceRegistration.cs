using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyAPI.Application.Abstractions.Storage;
using ToBuyAPI.Infrastructure.Services;
using ToBuyAPI.Infrastructure.Services.Storage;

namespace ToBuyAPI.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService, StorageService>();
        }

        public static void AddStorage<T>(this IServiceCollection services) where T : class,IStorage
        {
            services.AddScoped<IStorage, T>();
        }
    }
}
