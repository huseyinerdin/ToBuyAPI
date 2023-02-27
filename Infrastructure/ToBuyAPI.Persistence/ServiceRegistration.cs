using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToBuyApı.Domain.Entities.Identity;
using ToBuyAPI.Application.Abstractions.Services;
using ToBuyAPI.Application.Repositories;
using ToBuyAPI.Persistence.Contexts;
using ToBuyAPI.Persistence.Repositories;
using ToBuyAPI.Persistence.Services;

namespace ToBuyAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ToBuyAPIDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
            services.AddIdentity<AppUser, IdentityRole>().AddRoles<IdentityRole>().AddEntityFrameworkStores<ToBuyAPIDbContext>();

            //Repositories
            services.AddScoped<ICategoryReadRepository, CategoryReadRepository>();
            services.AddScoped<ICategoryWriteRepository, CategoryWriteRepository>();
                    
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

            services.AddScoped<IToBuyListReadRepository, ToBuyListReadRepository>();
            services.AddScoped<IToBuyListWriteRepository, ToBuyListWriteRepository>();

            //Services
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductImageFileService, ProductImageFileService>();
            services.AddScoped<IToBuyListService, ToBuyListService>();
        }
    }
}
