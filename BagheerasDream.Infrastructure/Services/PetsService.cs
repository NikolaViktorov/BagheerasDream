using BagheerasDream.Application.DTOs.Pets;
using BagheerasDream.Application.Interfaces;
using BagheerasDream.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BagheerasDream.Infrastructure.Services
{
	public class PetsService : IPetsService
	{
		private readonly ApplicationDbContext _context;

		public PetsService(ApplicationDbContext context)
		{
			this._context = context;
		}

		public async Task<IEnumerable<PetListDto>> GetAllAsync()
		{
			return await this._context.Pets
				.AsNoTracking()
				.Select(pet => new PetListDto
				{
					PetId = pet.PetId,
					Name = pet.Name,
					Birthday = pet.Birthday,
					Color = pet.Color,
					Breed = pet.Breed,
					Gender = pet.Gender,
					ProfileImage = pet.ProfileImage
				})
				.ToListAsync();
		}
	}
}
