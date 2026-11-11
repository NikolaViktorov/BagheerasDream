using BagheerasDream.Application.DTOs.Admin.Pets;
using BagheerasDream.Application.Interfaces;
using BagheerasDream.Application.Interfaces.Admin;
using BagheerasDream.Domain.Models;
using BagheerasDream.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BagheerasDream.Infrastructure.Services.Admin
{
	public class AdminPetsService : IAdminPetsService
	{
		private readonly IImageUploadService _imageUploadService;
		private readonly ApplicationDbContext _context;

		public AdminPetsService(ApplicationDbContext context, IImageUploadService imageUploadService)
		{
			this._context = context;
			this._imageUploadService = imageUploadService;
		}

		public async Task<Guid> CreateAsync(PetFormDto dto)
		{
			var pet = new Pet
			{
				PetId = Guid.NewGuid(),
				Name = dto.Name,
				Birthday = dto.Birthday,
				Breed = dto.Breed,
				Color = dto.Color,
				Gender = dto.Gender,
				ProfileImage = dto.ProfileImage
			};

			await this._context.Pets.AddAsync(pet);
			await this._context.SaveChangesAsync();

			return pet.PetId;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var pet = await this._context.Pets.FirstOrDefaultAsync(p => p.PetId == id);

			if (pet is null)
			{
				return false;
			}

			var imagePath = pet.ProfileImage;

			this._context.Pets.Remove(pet);
			await this._context.SaveChangesAsync();

			if (!string.IsNullOrEmpty(imagePath))
			{
				await this._imageUploadService.DeleteAsync(imagePath);
			}

			return true;
		}

		public async Task<PetFormDto?> GetForEditAsync(Guid id)
		{
			return await this._context.Pets
				.AsNoTracking()
				.Where(p => p.PetId == id)
				.Select(p => new PetFormDto
				{
					PetId = p.PetId,
					Name = p.Name,
					Birthday = p.Birthday,
					Breed = p.Breed,
					Color = p.Color,
					Gender = p.Gender,
					ProfileImage = p.ProfileImage
				})
				.FirstOrDefaultAsync();
		}

		public async Task<bool> UpdateAsync(PetFormDto dto)
		{
			var pet = await this._context.Pets.FirstOrDefaultAsync(p => p.PetId == dto.PetId);

			if (pet is null)
			{
				return false;
			}

			pet.Name = dto.Name;
			pet.Birthday = dto.Birthday;
			pet.Breed = dto.Breed;
			pet.Color = dto.Color;
			pet.Gender = dto.Gender;
			pet.ProfileImage = dto.ProfileImage;

			await this._context.SaveChangesAsync();

			return true;
		}
	}
}
