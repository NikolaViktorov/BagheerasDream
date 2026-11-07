using BagheerasDream.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BagheerasDream.Infrastructure.Data.Configurations
{
	public class LitterImageConfiguration : IEntityTypeConfiguration<LitterImage>
	{
		public void Configure(EntityTypeBuilder<LitterImage> builder)
		{
			builder.HasKey(li => li.LitterImageId);

			builder.Property(li => li.ImagePath)
				.IsRequired()
				.HasMaxLength(500);

			builder.HasOne(li => li.Litter)
				.WithMany(l => l.Images)
				.HasForeignKey(li => li.LitterId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.HasIndex(li => new { li.LitterId, li.DisplayOrder });
		}
	}
}