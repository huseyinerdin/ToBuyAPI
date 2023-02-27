using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToBuyApı.Domain.Entities;

namespace ToBuyAPI.Persistence.Configurations
{
	public class ToBuyListConfiguration : BaseConfiguration<ToBuyList>
	{
		public override void Configure(EntityTypeBuilder<ToBuyList> builder)
		{
			base.Configure(builder);
			builder.Property(t => t.Name).HasMaxLength(100).IsRequired();
			builder.Property(t => t.CompletedDate).HasColumnType("date").IsRequired(false);
			builder.HasMany(t => t.Categories).WithMany(c => c.ToBuyLists);
			builder.HasOne(t => t.AppUser).WithMany(a => a.ToBuyLists).HasForeignKey(t => t.AppUserId);
		}
	}
}
