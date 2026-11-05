using BagheerasDream.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagheerasDream.Infrastructure.Data.Configurations
{
	public class KittenConfiguration : IEntityTypeConfiguration<Kitten>
	{
		public void Configure(EntityTypeBuilder<Kitten> builder)
		{
			builder.HasKey(k => k.KittenId);
		}
	}
}
