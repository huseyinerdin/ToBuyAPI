using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyApı.Domain.Entities;
using ToBuyApı.Domain.Entities.Common;

namespace ToBuyAPI.Persistence.Contexts
{
    public class ToBuyAPIDbContext : DbContext
    {
        public ToBuyAPIDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ToBuyList> ToBuyLists { get; set; }
        public DbSet<ProductImageFile> ProductImages { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.Now,
                    _=>DateTime.Now,
                };
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
