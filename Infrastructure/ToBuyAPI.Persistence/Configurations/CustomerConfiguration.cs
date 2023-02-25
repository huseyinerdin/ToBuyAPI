using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToBuyApı.Domain.Entities;

namespace ToBuyAPI.Persistence.Configurations
{
	public class CustomerConfiguration : BaseConfiguration<Customer>
	{
		public override void Configure(EntityTypeBuilder<Customer> builder)
		{
			base.Configure(builder);
			builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
			builder.HasMany(c => c.ToBuyLists).WithOne(t => t.Customer).HasForeignKey(t => t.CustomerId);
		}
	}
}
