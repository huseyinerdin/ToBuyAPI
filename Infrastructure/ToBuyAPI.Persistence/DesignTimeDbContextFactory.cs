using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ToBuyAPI.Persistence.Contexts;

namespace ToBuyAPI.Persistence
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ToBuyAPIDbContext>
    {
        public ToBuyAPIDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ToBuyAPIDbContext> dbContextOptionsBuilder = new();
            dbContextOptionsBuilder.UseLazyLoadingProxies().UseSqlServer(Configuration.ConnectionString);
            return new ToBuyAPIDbContext(dbContextOptionsBuilder.Options);
        }
    }
}
