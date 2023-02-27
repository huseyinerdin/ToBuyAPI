using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ToBuyApı.Domain.Entities.Common;

namespace ToBuyAPI.Persistence.Configurations
{
	public class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
	{
		public virtual void Configure(EntityTypeBuilder<T> builder)
		{
			builder.HasKey(b => b.Id);
			builder.Property(b => b.Id).ValueGeneratedOnAdd();
			builder.Property(b => b.CreatedDate).HasColumnType("date").IsRequired();
			builder.Property(b=>b.UpdatedDate).HasColumnType("date").IsRequired(false);
		}
	}
}
