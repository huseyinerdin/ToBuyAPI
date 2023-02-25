using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToBuyApı.Domain.Entities;

namespace ToBuyAPI.Persistence.Configurations
{
	public class CategoryConfiguration : BaseConfiguration<Category>
	{
		public override void Configure(EntityTypeBuilder<Category> builder)
		{
			base.Configure(builder);
			builder.Property(c => c.Name).HasMaxLength(100).IsRequired();
			builder.HasMany(c => c.Products).WithMany(p => p.Categories);
			builder.HasMany(c => c.ToBuyLists).WithMany(t => t.Categories);
		}
	}
}
