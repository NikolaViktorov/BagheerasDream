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

		public async Task<bool> AddImageAsync(Guid catId, Stream fileStream, string fileName)
		{
			var catExists = await this._context.Cats.AnyAsync(c => c.CatId == catId);

			if (!catExists)
			{
				return false;
			}

			var imageCount = await this._context.CatImages.CountAsync(ci => ci.CatId == catId);

			if (imageCount >= 5)
			{
				return false;
			}

			var imagePath = await this._imageUploadService.UploadAsync(fileStream, fileName, "cats-gallery");

			var catImage = new CatImage
			{
				CatImageId = Guid.NewGuid(),
				CatId = catId,
				ImagePath = imagePath,
				DisplayOrder = imageCount
			};

			try
			{
				await this._context.CatImages.AddAsync(catImage);
				await this._context.SaveChangesAsync();
			}
			catch (Exception)
			{
				await this._imageUploadService.DeleteAsync(imagePath);
				throw;
			}

			return true;
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
				PedigreeImage = dto.PedigreeImage,
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
			var pedigreeImagePath = cat.PedigreeImage;

			this._context.Cats.Remove(cat);
			await this._context.SaveChangesAsync();

			if (!string.IsNullOrWhiteSpace(imagePath))
			{
				await this._imageUploadService.DeleteAsync(imagePath);
				await this._imageUploadService.DeleteAsync(pedigreeImagePath);
			}

			return true;
		}

		public async Task<bool> DeleteImageAsync(Guid catImageId)
		{
			var image = await this._context.CatImages.FirstOrDefaultAsync(ci => ci.CatImageId == catImageId);

			if (image is null)
			{
				return false;
			}

			var imagePath = image.ImagePath;

			this._context.CatImages.Remove(image);

			await this._context.SaveChangesAsync();

			await this._imageUploadService.DeleteAsync(imagePath);

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
					PedigreeImage = c.PedigreeImage,
					Description = c.Description,
					FatherId = c.FatherId,
					MotherId = c.MotherId
				})
				.FirstOrDefaultAsync();
		}

		public async Task<List<CatImageDto>> GetImagesAsync(Guid catId)
		{
			return await this._context.CatImages
				.AsNoTracking()
				.Where(ci => ci.CatId == catId)
				.OrderBy(ci => ci.DisplayOrder)
				.Select(ci => new CatImageDto
				{
					CatImageId = ci.CatImageId,
					ImagePath = ci.ImagePath,
					DisplayOrder = ci.DisplayOrder
				})
				.ToListAsync();
		}

		public async Task<bool> ReorderImagesAsync(Guid catId, List<Guid> orderedImageIds)
		{
			var images = await _context.CatImages
				.Where(ci => ci.CatId == catId)
				.ToListAsync();

			if (orderedImageIds.Count != images.Count ||
				orderedImageIds.Distinct().Count() != orderedImageIds.Count)
			{
				return false;
			}

			var imageDictionary = images.ToDictionary(ci => ci.CatImageId);

			if (orderedImageIds.Any(id => !imageDictionary.ContainsKey(id)))
			{
				return false;
			}

			for (int i = 0; i < orderedImageIds.Count; i++)
			{
				imageDictionary[orderedImageIds[i]].DisplayOrder = i;
			}

			await _context.SaveChangesAsync();

			return true;
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
			cat.PedigreeImage = dto.PedigreeImage;
			cat.Description = dto.Description;
			cat.FatherId = dto.FatherId;
			cat.MotherId = dto.MotherId;

			await this._context.SaveChangesAsync();

			return true;
		}
	}
}
