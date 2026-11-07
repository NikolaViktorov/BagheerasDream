using BagheerasDream.Application.DTOs.Litters;
using BagheerasDream.Application.Interfaces;
using BagheerasDream.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BagheerasDream.Infrastructure.Services
{
	public class LittersService : ILittersService
	{
		private readonly ApplicationDbContext _context;

		public LittersService(ApplicationDbContext context)
		{
			this._context = context;
		}

		public async Task<IEnumerable<LitterListDto>> GetAllAsync()
		{
			return await this._context.Litters
				.AsNoTracking()
				.Select(l => new LitterListDto()
				{
					LitterId = l.LitterId,
					Name = l.Name,
					Birthday = l.Birthday,
					MotherName = l.Mother.Name,
					PhotoOfLitter = l.PhotoOfLitter,
					KittenCount = l.Kittens.Count()
				})
				.ToListAsync();
		}

		public async Task<LitterDetailsDto?> GetByIdAsync(Guid id)
		{
			var litter = await this._context.Litters
				.AsNoTracking()
				.Where(l => l.LitterId == id)
				.Select(l => new
				{
					l.LitterId,
					l.Name,
					l.Birthday,
					l.Description,

					Mother = new LitterParentDto
					{
						CatId = l.Mother.CatId,
						Name = l.Mother.Name,
						ProfileImage = l.Mother.ProfileImage,
						IsExternal = false
					},

					FatherId = l.FatherId,

					Father = l.Father != null
						? new LitterParentDto
						{
							CatId = l.Father.CatId,
							Name = l.Father.Name,
							ProfileImage = l.Father.ProfileImage,
							IsExternal = false
						}
						: null,

					l.ExternalFatherName,
					l.ExternalFatherCattery,

					Kittens = l.Kittens.Select(k => new KittenListDto
					{
						KittenId = k.KittenId,
						Name = k.Name,
						Color = k.Color,
						Gender = k.Gender,
						ProfileImage = k.ProfileImage,
						Status = k.Status,
						LitterId = l.LitterId
					}).ToList()
				})
				.FirstOrDefaultAsync();

			if (litter is null)
			{
				return null;
			}

			LitterParentDto? father = null;

			if (litter.FatherId.HasValue)
			{
				father = litter.Father;
			}
			else if (!string.IsNullOrWhiteSpace(litter.ExternalFatherName))
			{
				father = new LitterParentDto
				{
					Name = litter.ExternalFatherName,
					CatteryName = litter.ExternalFatherCattery,
					IsExternal = true
				};
			}

			return new LitterDetailsDto
			{
				LitterId = litter.LitterId,
				Name = litter.Name,
				Birthday = litter.Birthday,
				Description = litter.Description,
				Mother = litter.Mother,
				Father = father,
				Kittens = litter.Kittens
			};
		}
	}
}