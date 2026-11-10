using BagheerasDream.Application.Interfaces;
using BagheerasDream.Application.Interfaces.Admin;
using BagheerasDream.Infrastructure.Data;
using BagheerasDream.Infrastructure.Identity;
using BagheerasDream.Infrastructure.Services;
using BagheerasDream.Infrastructure.Services.Admin;
using BagheerasDream.Infrastructure.Services.Files;
using BagheerasDream.Web.Components;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

public partial class Program
{
	private static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		var supportedCultures = new[]
		{
			new CultureInfo("bg"),
			new CultureInfo("de"),
			new CultureInfo("en")
		};

		builder.Services.Configure<ForwardedHeadersOptions>(options =>
		{
			options.ForwardedHeaders =
				ForwardedHeaders.XForwardedFor |
				ForwardedHeaders.XForwardedProto;

			options.KnownIPNetworks.Clear();
			options.KnownProxies.Clear();

			options.ForwardLimit = 1;
		});

		builder.Services.Configure<RequestLocalizationOptions>(options =>
		{
			options.DefaultRequestCulture = new RequestCulture("en");

			options.SupportedCultures = supportedCultures;
			options.SupportedUICultures = supportedCultures;

			options.ApplyCurrentCultureToResponseHeaders = true;
		});

		// Add services to the container.
		builder.Services.AddRazorComponents()
			.AddInteractiveServerComponents();

		builder.Services.AddLocalization(options =>
		{
			options.ResourcesPath = "Resources";
		});

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
		})
		.AddEntityFrameworkStores<ApplicationDbContext>()
		.AddDefaultTokenProviders();

		builder.Services.ConfigureApplicationCookie(options =>
		{
			options.ExpireTimeSpan = TimeSpan.FromHours(1);
			options.SlidingExpiration = true;

			options.Cookie.HttpOnly = true;
			options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
			options.Cookie.SameSite = SameSiteMode.Lax;

			options.LoginPath = "/Account/Login";
			options.AccessDeniedPath = "/Account/AccessDenied";
		});

		builder.Services.AddAuthorization();

		if (builder.Environment.IsProduction())
		{
			var certificatePassword = builder.Configuration["DataProtection:CertificatePassword"];
			var certificatePath = "/app/secrets/dataprotection.pfx";

			if (string.IsNullOrWhiteSpace(certificatePassword))
			{
				throw new InvalidOperationException(
					"Data Protection certificate password is missing.");
			}

			var certificate = X509CertificateLoader.LoadPkcs12FromFile(
				certificatePath,
				certificatePassword,
				X509KeyStorageFlags.EphemeralKeySet);

			builder.Services.AddDataProtection()
				.PersistKeysToFileSystem(
					new DirectoryInfo("/home/app/.aspnet/DataProtection-Keys"))
				.ProtectKeysWithCertificate(certificate);
		}
		else
		{
			builder.Services.AddDataProtection();
		}

		builder.Services.AddCascadingAuthenticationState();

		builder.Services.AddScoped<ICatsService, CatsService>();
		builder.Services.AddScoped<ILittersService, LittersService>();

		builder.Services.AddScoped<IDashboardService, DashboardService>();
		builder.Services.AddScoped<IAdminCatsService, AdminCatsService>();
		builder.Services.AddScoped<IAdminKittensService, AdminKittensService>();
		builder.Services.AddScoped<IAdminLittersService, AdminLittersService>();
		builder.Services.AddScoped<IImageUploadService>(serviceProvider =>
		{
			var environment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

			return new ImageUploadService(environment.WebRootPath);
		});

		var app = builder.Build();

		app.UseForwardedHeaders();

		// Apply pending database migrations
		using (var scope = app.Services.CreateScope())
		{
			var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
			await db.Database.MigrateAsync();
		}

		// Seed admin user
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
		app.UseRequestLocalization();

		app.MapGet("/set-language", (string culture, string? returnUrl, HttpContext context) =>
		{
			var supportedCultures = new[] { "en", "de", "bg" };

			if (!supportedCultures.Contains(culture))
			{
				culture = "en";
			}

			context.Response.Cookies.Append(
				CookieRequestCultureProvider.DefaultCookieName,
				CookieRequestCultureProvider.MakeCookieValue(
					new RequestCulture(culture)),
				new CookieOptions
				{
					Expires = DateTimeOffset.UtcNow.AddYears(1),
					IsEssential = true,
					HttpOnly = true,
					Secure = context.Request.IsHttps,
					SameSite = SameSiteMode.Lax
				});

			return Results.LocalRedirect(
				!string.IsNullOrWhiteSpace(returnUrl) && returnUrl.StartsWith("/")
					? returnUrl
					: "/");
		});

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseAntiforgery();

		app.MapRazorPages();
		app.UseStaticFiles();
		app.MapStaticAssets();
		app.MapRazorComponents<App>()
			.AddInteractiveServerRenderMode();

		app.Run();
	}
}