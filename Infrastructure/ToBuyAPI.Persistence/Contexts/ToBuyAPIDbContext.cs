using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToBuyApı.Domain.Entities;
using ToBuyApı.Domain.Entities.Common;
using ToBuyApı.Domain.Entities.Identity;
using ToBuyAPI.Persistence.Configurations;

namespace ToBuyAPI.Persistence.Contexts
{
	public class ToBuyAPIDbContext : IdentityDbContext<AppUser, IdentityRole, string>
	{
		public ToBuyAPIDbContext(DbContextOptions options) : base(options)
		{
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);
			optionsBuilder.UseLazyLoadingProxies();
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			new CategoryConfiguration().Configure(modelBuilder.Entity<Category>());
			new ProductConfiguration().Configure(modelBuilder.Entity<Product>());
			new ProductImageFileConfiguration().Configure(modelBuilder.Entity<ProductImageFile>());
			new ToBuyListConfiguration().Configure(modelBuilder.Entity<ToBuyList>());

			AddSeedDatas(modelBuilder);

		}
		public DbSet<Category> Categories { get; set; }
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
					_ => DateTime.Now,
				};
			}
			return await base.SaveChangesAsync(cancellationToken);
		}

		private void AddSeedDatas(ModelBuilder modelBuilder)
		{
			#region Identity
			IdentityRole adminRole = new IdentityRole
			{
				Id = Guid.NewGuid().ToString(),
				Name = "Admin",
				NormalizedName = "ADMIN"
			};
			IdentityRole userRole = new IdentityRole
			{
				Id = Guid.NewGuid().ToString(),
				Name = "User",
				NormalizedName = "USER"
			};
			modelBuilder.Entity<IdentityRole>().HasData(adminRole, userRole);

			AppUser appUser = new()
			{
				Id = Guid.NewGuid().ToString(),
				FullName = "Admin",
				Country = "Türkiye",
				UserName = "Admin",
				NormalizedUserName = "ADMIN"
			};
			appUser.PasswordHash = new PasswordHasher<AppUser>().HashPassword(appUser, "Admin61.");

			modelBuilder.Entity<AppUser>().HasData(appUser);
			modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
			{
				RoleId = adminRole.Id,
				UserId = appUser.Id
			});
			#endregion

			#region Category
			Category aksesuar = new()
			{
				Id = Guid.NewGuid(),
				Name = "Aksesuar",
			};
			Category giyim = new()
			{
				Id = Guid.NewGuid(),
				Name = "Giyim",
			};
			Category kirtasiye = new()
			{
				Id = Guid.NewGuid(),
				Name = "Kırtasiye",
			};
			Category mutfak = new()
			{
				Id = Guid.NewGuid(),
				Name = "Mutfak",
			};
			Category teknoloji = new()
			{
				Id = Guid.NewGuid(),
				Name = "Teknoloji",
			};
			modelBuilder.Entity<Category>().HasData(aksesuar, giyim, kirtasiye, mutfak, teknoloji);
			#endregion
		}
	}
}
