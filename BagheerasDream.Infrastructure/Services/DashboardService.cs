using BagheerasDream.Application.DTOs.Admin;
using BagheerasDream.Application.Interfaces;
using BagheerasDream.Domain.Enums;
using BagheerasDream.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BagheerasDream.Infrastructure.Services;

public class DashboardService : IDashboardService
{
	private readonly ApplicationDbContext _context;

	public DashboardService(ApplicationDbContext context)
	{
		this._context = context;
	}

	public async Task<DashboardStatsDto> GetStatsAsync()
	{
		return new DashboardStatsDto
		{
			TotalCats = await this._context.Cats.CountAsync(),

			MaleCats = await this._context.Cats
				.CountAsync(c => c.Gender == Gender.Male),

			FemaleCats = await this._context.Cats
				.CountAsync(c => c.Gender == Gender.Female),

			TotalLitters = await this._context.Litters.CountAsync(),

			TotalKittens = await this._context.Kittens.CountAsync(),

			AvailableKittens = await this._context.Kittens
				.CountAsync(k => k.Status == KittenStatus.Available),

			ReservedKittens = await this._context.Kittens
				.CountAsync(k => k.Status == KittenStatus.Reserved),

			SoldKittens = await this._context.Kittens
				.CountAsync(k => k.Status == KittenStatus.Sold)
		};
	}
}