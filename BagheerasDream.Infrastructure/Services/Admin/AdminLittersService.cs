using BagheerasDream.Application.DTOs.Admin.Litters;
using BagheerasDream.Application.Interfaces;
using BagheerasDream.Application.Interfaces.Admin;
using BagheerasDream.Domain.Enums;
using BagheerasDream.Domain.Models;
using BagheerasDream.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagheerasDream.Infrastructure.Services.Admin
{
	public class AdminLittersService : IAdminLittersService
	{
		private readonly ApplicationDbContext _context;
		private readonly IImageUploadService _imageUploadService;

		public AdminLittersService(
			ApplicationDbContext context,
			IImageUploadService imageUploadService)
		{
			this._context = context;
			this._imageUploadService = imageUploadService;
		}

		public async Task<bool> AddImageAsync(Guid litterId, Stream fileStream, string fileName)
		{
			var litterExists = await this._context.Litters.AnyAsync(l => l.LitterId == litterId);

			if (!litterExists)
			{
				return false;
			}

			var imageCount = await this._context.LitterImages.CountAsync(i => i.LitterId == i.LitterId);

			if (imageCount >= 10)
			{
				return false;
			}

			var imagePath = await this._imageUploadService.UploadAsync(fileStream, fileName, "litter-gallery");

			var image = new LitterImage
			{
				LitterImageId = Guid.NewGuid(),
				ImagePath = imagePath,
				DisplayOrder = imageCount,
				LitterId = litterId
			};

			try
			{
				await this._context.LitterImages.AddAsync(image);
				await this._context.SaveChangesAsync();

				return true;
			}
			catch (Exception)
			{
				await this._imageUploadService.DeleteAsync(imagePath);
				throw;
			}
		}

		public async Task<Guid> CreateAsync(LitterFormDto dto)
		{
			await ValidateRelationshipsAsync(dto);

			var litter = new Litter
			{
				LitterId = dto.LitterId,
				Name = dto.Name,
				Birthday = dto.Birthday,
				MotherId = dto.MotherId,
				FatherId = dto.HasInternalFather ? dto.FatherId : null,
				ExternalFatherName = dto.HasInternalFather ? null : dto.ExternalFatherName,
				ExternalFatherCattery = dto.HasInternalFather ? null : dto.ExternalFatherCattery,
				Description = dto.Description,
				PhotoOfLitter = dto.PhotoOfLitter
			};

			await this._context.Litters.AddAsync(litter);
			await this._context.SaveChangesAsync();

			return litter.LitterId;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var litter = await this._context.Litters
				.Include(l => l.Kittens)
				.Include(l => l.Images)
				.FirstOrDefaultAsync(l => l.LitterId == id);

			if (litter is null)
			{
				return false;
			}

			var imagePaths = litter.Kittens
				.Select(k => k.ProfileImage)
				.Append(litter.PhotoOfLitter)
				.Concat(litter.Images.Select(i => i.ImagePath))
				.Where(path => !string.IsNullOrWhiteSpace(path))
				.ToList();

			this._context.Remove(litter);

			await this._context.SaveChangesAsync();

			foreach (var imagePath in imagePaths)
			{
				await _imageUploadService.DeleteAsync(imagePath);
			}

			return true;
		}

		public async Task<bool> DeleteImageAsync(Guid litterImageId)
		{
			var image = await this._context.LitterImages.FirstOrDefaultAsync(i => i.LitterImageId == litterImageId);

			if (image is null)
			{
				return false;
			}

			this._context.LitterImages.Remove(image);

			await this._context.SaveChangesAsync();

			await this._imageUploadService.DeleteAsync(image.ImagePath);

			return true;
		}

		public async Task<LitterFormDto?> GetForEditAsync(Guid id)
		{
			return await this._context.Litters
				.AsNoTracking()
				.Where(l => l.LitterId == id)
				.Select(l => new LitterFormDto
				{
					LitterId = l.LitterId,
					Name = l.Name,
					Birthday = l.Birthday,
					MotherId = l.MotherId,
					HasInternalFather = l.FatherId.HasValue,
					FatherId = l.FatherId,
					ExternalFatherName = l.ExternalFatherName,
					ExternalFatherCattery = l.ExternalFatherCattery,
					Description = l.Description,
					PhotoOfLitter = l.PhotoOfLitter
				})
				.FirstOrDefaultAsync();
		}

		public async Task<List<LitterImageDto>> GetImagesAsync(Guid litterId)
		{
			return await this._context.LitterImages
				.AsNoTracking()
				.Where(i => i.LitterId == litterId)
				.OrderBy(i => i.DisplayOrder)
				.Select(i => new LitterImageDto
				{
					ImagePath = i.ImagePath,
					LitterImageId = i.LitterImageId,
					DisplayOrder = i.DisplayOrder
				})
				.ToListAsync();
		}

		public async Task<bool> ReorderImagesAsync(Guid litterId, List<Guid> orderedImageIds)
		{
			var images = await this._context.LitterImages.Where(i => i.LitterId == litterId)
				.ToListAsync();

			if (images.Count != orderedImageIds.Count)
			{
				return false;
			}

			if (orderedImageIds.Distinct().Count() != orderedImageIds.Count)
			{
				return false;
			}

			if (!images.Select(i => i.LitterImageId).ToHashSet().SetEquals(orderedImageIds))
			{
				return false;
			}

			for (int i = 0; i < orderedImageIds.Count; i++)
			{
				var image = images.First(img => img.LitterImageId == orderedImageIds[i]);

				image.DisplayOrder = i;
			}

			await this._context.SaveChangesAsync();

			return true;
		}

		public async Task<bool> UpdateAsync(LitterFormDto dto)
		{
			var litter = await this._context.Litters.FirstOrDefaultAsync(l => l.LitterId == dto.LitterId);

			if (litter is null)
			{
				return false;
			}

			await ValidateRelationshipsAsync(dto);

			litter.Name = dto.Name;
			litter.Birthday = dto.Birthday;
			litter.MotherId = dto.MotherId;
			litter.FatherId = dto.HasInternalFather ? dto.FatherId : null;
			litter.ExternalFatherName = dto.HasInternalFather
				? null
				: dto.ExternalFatherName;
			litter.ExternalFatherCattery = dto.HasInternalFather
				? null
				: dto.ExternalFatherCattery;
			litter.Description = dto.Description;
			litter.PhotoOfLitter = dto.PhotoOfLitter;

			await this._context.SaveChangesAsync();

			return true;
		}

		private async Task ValidateRelationshipsAsync(LitterFormDto dto)
		{
			if (dto.MotherId == Guid.Empty)
				throw new InvalidOperationException("Моля, изберете майка.");

			var mother = await _context.Cats
				.AsNoTracking()
				.FirstOrDefaultAsync(c => c.CatId == dto.MotherId);

			if (mother is null || mother.Gender != Gender.Female)
				throw new InvalidOperationException(
					"Избраната майка не е валидна женска котка.");

			if (dto.HasInternalFather)
			{
				if (dto.FatherId is null || dto.FatherId == Guid.Empty)
					throw new InvalidOperationException(
						"Моля, изберете баща.");

				if (dto.FatherId == dto.MotherId)
					throw new InvalidOperationException(
						"Майката и бащата не могат да бъдат една и съща котка.");

				var father = await _context.Cats
					.AsNoTracking()
					.FirstOrDefaultAsync(c => c.CatId == dto.FatherId);

				if (father is null || father.Gender != Gender.Male)
					throw new InvalidOperationException(
						"Избраният баща не е валидна мъжка котка.");
			}
			else if (string.IsNullOrWhiteSpace(dto.ExternalFatherName))
			{
				throw new InvalidOperationException(
					"Моля, въведете името на външния баща.");
			}
		}
	}
}
