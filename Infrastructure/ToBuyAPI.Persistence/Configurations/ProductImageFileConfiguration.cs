using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToBuyApı.Domain.Entities;

namespace ToBuyAPI.Persistence.Configurations
{
	public class ProductImageFileConfiguration : BaseConfiguration<ProductImageFile>
	{
		public override void Configure(EntityTypeBuilder<ProductImageFile> builder)
		{
			base.Configure(builder);
			builder.Property(i => i.FileName).HasMaxLength(100).IsRequired();
			builder.Property(i=>i.Path).IsRequired();
			builder.Property(i => i.Storage).HasMaxLength(50).IsRequired();
			builder.HasOne(i => i.Product).WithMany(p => p.ProductImageFiles).HasForeignKey(i => i.ProductId);
		}
	}
}
