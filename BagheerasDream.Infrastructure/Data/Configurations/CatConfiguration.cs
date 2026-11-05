using BagheerasDream.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BagheerasDream.Infrastructure.Data.Configurations
{
	public class CatConfiguration : IEntityTypeConfiguration<Cat>
	{
		public void Configure(EntityTypeBuilder<Cat> builder)
		{
			builder.HasKey(c => c.CatId);

			builder.HasOne(c => c.Father)
				.WithMany()
				.HasForeignKey(c => c.FatherId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(c => c.Mother)
				.WithMany()
				.HasForeignKey(c => c.MotherId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
