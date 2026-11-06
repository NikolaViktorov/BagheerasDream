using BagheerasDream.Application.Interfaces;
using BagheerasDream.Application.Interfaces.Admin;
using BagheerasDream.Infrastructure.Data;
using BagheerasDream.Infrastructure.Identity;
using BagheerasDream.Infrastructure.Services;
using BagheerasDream.Infrastructure.Services.Admin;
using BagheerasDream.Infrastructure.Services.Files;
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

		builder.Services.AddRazorPages();

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
		builder.Services.AddScoped<ILittersService, LittersService>();

		builder.Services.AddScoped<IDashboardService, DashboardService>();
		builder.Services.AddScoped<IAdminCatsService, AdminCatsService>();
		builder.Services.AddScoped<IImageUploadService>(serviceProvider =>
		{
			var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

			return new ImageUploadService(environment.WebRootPath);
		});

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

		app.MapRazorPages();
		app.MapStaticAssets();
		app.MapRazorComponents<App>()
			.AddInteractiveServerRenderMode();

		app.Run();
	}
}