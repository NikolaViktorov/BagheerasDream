using BagheerasDream.Application.DTOs.HappyFamilies;
using BagheerasDream.Application.Interfaces;
using BagheerasDream.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BagheerasDream.Infrastructure.Services
{
	public class HappyFamiliesService : IHappyFamiliesService
	{
		private readonly ApplicationDbContext _context;

		public HappyFamiliesService(ApplicationDbContext context)
		{
			this._context = context;
		}

		public async Task<IEnumerable<HappyFamilyListDto>> GetAllAsync()
		{
			return await this._context.HappyFamilies
				.AsNoTracking()
				.Select(h => new HappyFamilyListDto
				{
					HappyFamilyId = h.HappyFamilyId,
					Image = h.Image
				})
				.ToListAsync();
		}
	}
}
