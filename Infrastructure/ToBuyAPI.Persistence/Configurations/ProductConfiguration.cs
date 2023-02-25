using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToBuyApı.Domain.Entities;

namespace ToBuyAPI.Persistence.Configurations
{
	public class ProductConfiguration : BaseConfiguration<Product>
	{
		public override void Configure(EntityTypeBuilder<Product> builder)
		{
			base.Configure(builder);
			builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
			builder.Property(p => p.Description).IsRequired(false);
			builder.HasMany(p => p.ProductImageFiles).WithOne(i => i.Product).HasForeignKey(i => i.ProductId);
			builder.HasMany(p => p.Categories).WithMany(c => c.Products);
		}
	}
}
