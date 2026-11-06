using BagheerasDream.Application.Interfaces;
using BagheerasDream.Infrastructure.Data;
using BagheerasDream.Infrastructure.Identity;
using BagheerasDream.Infrastructure.Services;
using BagheerasDream.Web.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public partial class Program
{
	private static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddRazorComponents()
			.AddInteractiveServerComponents();

		var connectionString = builder.Configuration.
			GetConnectionString("DefaultConnection");

		builder.Services.AddDbContext<ApplicationDbContext>(options => 
		options.UseSqlServer(connectionString));

		builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
		{
			options.Password.RequiredLength = 8;
			options.Password.RequireDigit = true;
			options.Password.RequireUppercase = true;
			options.Password.RequireLowercase = true;
			options.Password.RequireNonAlphanumeric = false;
		}).AddEntityFrameworkStores<ApplicationDbContext>()
		.AddDefaultTokenProviders();

		builder.Services.AddAuthorization();
		builder.Services.AddCascadingAuthenticationState();

		builder.Services.AddScoped<ICatsService, CatsService>();

		var app = builder.Build();

		await IdentitySeeder.SeedAdminAsync(app.Services, app.Configuration);

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Error", createScopeForErrors: true);
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}
		app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
		app.UseHttpsRedirection();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseAntiforgery();

		app.MapStaticAssets();
		app.MapRazorComponents<App>()
			.AddInteractiveServerRenderMode();

		app.Run();
	}
}