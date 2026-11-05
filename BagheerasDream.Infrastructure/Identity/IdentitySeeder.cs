using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BagheerasDream.Infrastructure.Identity
{
	public class IdentitySeeder
	{
		public static async Task SeedAdminAsync(
			IServiceProvider serviceProvider,
			IConfiguration configuration)
		{
			var email = configuration["AdminUser:Email"];
			var password = configuration["AdminUser:Password"];

			if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
			{
				throw new InvalidOperationException("Admin credentials are missing from configuration.");
			}

			using var scope = serviceProvider.CreateScope();

			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

			const string adminRole = "Admin";

			// Create Admin role if it doesn't exist
			if (!await roleManager.RoleExistsAsync(adminRole))
			{
				var roleResult = await roleManager.CreateAsync(
					new IdentityRole(adminRole));

				if (!roleResult.Succeeded)
				{
					throw new InvalidOperationException(
						string.Join(", ", roleResult.Errors.Select(e => e.Description)));
				}
			}

			// Find admin user
			var adminUser = await userManager.FindByEmailAsync(email);

			// Create admin user if it doesn't exist
			if (adminUser is null)
			{
				adminUser = new ApplicationUser
				{
					UserName = email,
					Email = email,
				};

				var userResult = await userManager.CreateAsync(adminUser, password);

				if (!userResult.Succeeded)
				{
					throw new InvalidOperationException(
						string.Join(", ", userResult.Errors.Select(e => e.Description)));
				}
			}

			// Assign Admin role
			if (!await userManager.IsInRoleAsync(adminUser, adminRole))
			{
				var roleAssignment = await userManager.AddToRoleAsync(adminUser, adminRole);

				if (!roleAssignment.Succeeded)
				{
					throw new InvalidOperationException(
						string.Join(", ", roleAssignment.Errors.Select(e => e.Description)));
				}
			}
		}
	}
}
