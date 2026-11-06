using BagheerasDream.Application.DTOs.Admin.Cats;
using BagheerasDream.Application.Interfaces.Admin;
using BagheerasDream.Application.Interfaces;
using BagheerasDream.Domain.Models;
using BagheerasDream.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagheerasDream.Infrastructure.Services.Admin
{
	public class AdminCatsService : IAdminCatsService
	{
		private readonly IImageUploadService _imageUploadService;
		private readonly ApplicationDbContext _context;

		public AdminCatsService(ApplicationDbContext context, IImageUploadService imageUploadService)
		{
			this._context = context;
			this._imageUploadService = imageUploadService;
		}

		public async Task<Guid> CreateAsync(CatFormDto dto)
		{
			var cat = new Cat
			{
				CatId = Guid.NewGuid(),
				Name = dto.Name,
				Birthday = dto.Birthday,
				Color = dto.Color,
				Breed = dto.Breed,
				Gender = dto.Gender,
				ProfileImage = dto.ProfileImage,
				FatherId = dto.FatherId,
				MotherId = dto.MotherId
			};

			await this._context.AddAsync(cat);
			await this._context.SaveChangesAsync();

			return cat.CatId;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var cat = await this._context.Cats.FirstOrDefaultAsync(c => c.CatId == id);

			if (cat is null)
			{
				return false;
			}

			var isParent = await this._context.Cats.AnyAsync(c => c.FatherId == id || c.MotherId == id);

			var isLitterParent = await this._context.Litters.AnyAsync(l => l.MotherId == id || l.FatherId == id);

			if (isParent || isLitterParent)
			{
				return false;
			}

			var imagePath = cat.ProfileImage;

			this._context.Cats.Remove(cat);
			await this._context.SaveChangesAsync();

			if (!string.IsNullOrWhiteSpace(imagePath))
			{
				await this._imageUploadService.DeleteAsync(imagePath);
			}

			return true;
		}

		public async Task<CatFormDto?> GetForEditAsync(Guid id)
		{
			return await this._context.Cats
				.AsNoTracking()
				.Where(c => c.CatId == id)
				.Select(c => new CatFormDto
				{
					CatId = c.CatId,
					Name = c.Name,
					Birthday = c.Birthday,
					Color = c.Color,
					Breed = c.Breed,
					Gender = c.Gender,
					ProfileImage = c.ProfileImage,
					Description = c.Description,
					FatherId = c.FatherId,
					MotherId = c.MotherId
				})
				.FirstOrDefaultAsync();
		}

		public async Task<bool> UpdateAsync(CatFormDto dto)
		{
			var cat = await this._context.Cats.FirstOrDefaultAsync(c => c.CatId == dto.CatId);

			if (cat is null)
			{
				return false;
			}

			cat.Name = dto.Name;
			cat.Birthday = dto.Birthday;
			cat.Color = dto.Color;
			cat.Breed = dto.Breed;
			cat.Gender = dto.Gender;
			cat.ProfileImage = dto.ProfileImage;
			cat.Description = dto.Description;
			cat.FatherId = dto.FatherId;
			cat.MotherId = dto.MotherId;

			await this._context.SaveChangesAsync();

			return true;
		}
	}
}
