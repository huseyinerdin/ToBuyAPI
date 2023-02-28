using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using ToBuyAPI.Application.Abstractions.JWT;
using ToBuyAPI.Application.Abstractions.Storage;
using ToBuyAPI.Infrastructure.Services.JWT;
using ToBuyAPI.Infrastructure.Services.Storage;

namespace ToBuyAPI.Infrastructure
{
	public static class ServiceRegistration
	{
		public static void AddInfrastructureServices(this IServiceCollection services)
		{
			services.AddScoped<IStorageService, StorageService>();
			services.AddScoped<ITokenHandler, TokenHandler>();

		}


		public static void AddStorage<T>(this IServiceCollection services) where T : Storage, IStorage
		{
			services.AddScoped<IStorage, T>();
		}
	}
}
