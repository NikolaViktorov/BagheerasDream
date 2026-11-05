using BagheerasDream.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BagheerasDream.Infrastructure.Data.Configurations
{
	public class LitterConfiguration : IEntityTypeConfiguration<Litter>
	{
		public void Configure(EntityTypeBuilder<Litter> builder)
		{
			builder.HasKey(l => l.LitterId);

			builder.HasOne(l => l.Mother)
				.WithMany()
				.HasForeignKey(l => l.MotherId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(l => l.Father)
				.WithMany()
				.HasForeignKey(l => l.FatherId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.HasMany(l => l.Kittens)
				.WithOne(k => k.Litter)
				.HasForeignKey(k => k.LitterId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
