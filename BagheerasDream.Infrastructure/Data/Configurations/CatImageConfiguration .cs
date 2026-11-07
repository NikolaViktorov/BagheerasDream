using BagheerasDream.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BagheerasDream.Infrastructure.Data.Configurations
{
	public class CatImageConfiguration : IEntityTypeConfiguration<CatImage>
	{
		public void Configure(EntityTypeBuilder<CatImage> builder)
		{
			builder.HasKey(ci => ci.CatImageId);

			builder.Property(ci => ci.ImagePath)
				.IsRequired()
				.HasMaxLength(500);

			builder.HasOne(ci => ci.Cat)
				.WithMany(c => c.Images)
				.HasForeignKey(ci => ci.CatId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasIndex(ci => new { ci.CatId, ci.DisplayOrder });
		}
	}
}