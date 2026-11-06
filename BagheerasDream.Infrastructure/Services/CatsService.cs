using BagheerasDream.Application.DTOs.Cats;
using BagheerasDream.Application.Interfaces;
using BagheerasDream.Domain.Enums;
using BagheerasDream.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagheerasDream.Infrastructure.Services
{
	public class CatsService : ICatsService
	{
		private readonly ApplicationDbContext _context;

		public CatsService(ApplicationDbContext context)
		{
			this._context = context;
		}

		public async Task<IEnumerable<CatListDto>> GetAllAsync()
		{
			return await this._context.Cats
				.AsNoTracking()
				.Select(cat => new CatListDto
				{
					CatId = cat.CatId,
					Name = cat.Name,
					Birthday = cat.Birthday,
					Color = cat.Color,
					Breed = cat.Breed,
					Gender = cat.Gender,
					ProfileImage = cat.ProfileImage,
				})
				.ToListAsync();
		}

		public async Task<IEnumerable<CatListDto>> GetMalesAsync()
		{
			return await GetByGenderAsync(Gender.Male);
		}

		public async Task<IEnumerable<CatListDto>> GetFemalesAsync()
		{
			return await GetByGenderAsync(Gender.Female);
		}

		public async Task<CatDetailsDto?> GetByIdAsync(Guid id)
		{
			return await this._context.Cats
				.AsNoTracking()
				.Where(cat => cat.CatId == id)
				.Select(cat => new CatDetailsDto
				{
					CatId = cat.CatId,
					Name = cat.Name,
					Birthday = cat.Birthday,
					Color = cat.Color,
					Breed = cat.Breed,
					Gender = cat.Gender,
					ProfileImage = cat.ProfileImage,
					Description = cat.Description,
					FatherId = cat.FatherId,
					FatherName = cat.Father != null ? cat.Father.Name : null,
					FatherProfileImage = cat.Father != null ? cat.Father.ProfileImage : null,
					MotherId = cat.MotherId,
					MotherName = cat.Mother != null ? cat.Mother.Name : null,
					MotherProfileImage = cat.Mother != null ? cat.Mother.ProfileImage : null,
				})
				.FirstOrDefaultAsync();
		}

		private async Task<IEnumerable<CatListDto>> GetByGenderAsync(Gender gender)
		{
			return await this._context.Cats
				.AsNoTracking()
				.Where(cat => cat.Gender == gender)
				.Select(cat => new CatListDto
				{
					CatId = cat.CatId,
					Name = cat.Name,
					Birthday = cat.Birthday,
					Color = cat.Color,
					Breed = cat.Breed,
					Gender = cat.Gender,
					ProfileImage = cat.ProfileImage,
				})
				.ToListAsync();
		}
	}
}
