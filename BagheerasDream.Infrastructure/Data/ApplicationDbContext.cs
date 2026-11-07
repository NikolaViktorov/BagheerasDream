using Microsoft.EntityFrameworkCore;
using BagheerasDream.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BagheerasDream.Infrastructure.Identity;

namespace BagheerasDream.Infrastructure.Data
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			:base(options)
		{
		}

		public DbSet<Cat> Cats { get; set; }

		public DbSet<CatImage> CatImages { get; set; }

		public DbSet<Litter> Litters { get; set; }

		public DbSet<Kitten> Kittens { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
		}
	}
}
