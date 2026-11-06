using BagheerasDream.Application.DTOs.Admin.Kittens;
using BagheerasDream.Application.DTOs.Litters;
using BagheerasDream.Application.Interfaces;
using BagheerasDream.Application.Interfaces.Admin;
using BagheerasDream.Domain.Models;
using BagheerasDream.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagheerasDream.Infrastructure.Services.Admin
{
	public class AdminKittensService : IAdminKittensService
	{
		private readonly ApplicationDbContext _context;
		private readonly IImageUploadService _imageUploadService;

		public AdminKittensService(
			ApplicationDbContext context,
			IImageUploadService imageUploadService)
		{
			this._context = context;
			this._imageUploadService = imageUploadService;
		}

		public async Task<Guid> CreateAsync(KittenFormDto dto)
		{
			await ValidateLitterAsync(dto.LitterId);

			var kitten = new Kitten
			{
				KittenId = dto.KittenId,
				Name = dto.Name,
				Color = dto.Color,
				Status = dto.Status,
				ProfileImage = dto.ProfileImage,
				LitterId = dto.LitterId
			};

			await this._context.Kittens.AddAsync(kitten);
			await this._context.SaveChangesAsync();

			return kitten.KittenId;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var kitten = await this._context.Kittens.FirstOrDefaultAsync(k => k.KittenId == id);

			if (kitten is null)
			{
				return false;
			}

			var imagePath = kitten.ProfileImage;

			_context.Kittens.Remove(kitten);

			await _context.SaveChangesAsync();

			if (!string.IsNullOrWhiteSpace(imagePath))
			{
				await this._imageUploadService.DeleteAsync(imagePath);
			}

			return true;
		}

		public async Task<IEnumerable<KittenListAdminDto>> GetAllAsync()
		{
			return await _context.Kittens
				.AsNoTracking()
				.OrderBy(k => k.Litter.Name)
				.ThenBy(k => k.Name)
				.Select(k => new KittenListAdminDto
				{
					KittenId = k.KittenId,
					Name = k.Name,
					Color = k.Color,
					ProfileImage = k.ProfileImage,
					Status = k.Status,
					LitterId = k.LitterId,
					LitterName = k.Litter.Name
				})
				.ToListAsync();
		}

		public async Task<KittenFormDto?> GetForEditAsync(Guid id)
		{
			return await this._context.Kittens
				.AsNoTracking()
				.Where(k => k.KittenId == id)
				.Select(k => new KittenFormDto
				{
					KittenId = k.KittenId,
					Name = k.Name,
					Color = k.Color,
					ProfileImage = k.ProfileImage,
					Status = k.Status,
					LitterId = k.LitterId
				})
				.FirstOrDefaultAsync();
		}

		public async Task<bool> UpdateAsync(KittenFormDto dto)
		{
			var kitten = await this._context.Kittens.FirstOrDefaultAsync(k => k.KittenId == dto.KittenId);

			if (kitten is null)
			{
				return false;
			}

			await ValidateLitterAsync(dto.LitterId);

			kitten.Name = dto.Name;
			kitten.Color = dto.Color;
			kitten.ProfileImage = dto.ProfileImage;
			kitten.Status = dto.Status;
			kitten.LitterId = dto.LitterId;

			await _context.SaveChangesAsync();

			return true;
		}

		private async Task ValidateLitterAsync(Guid litterId)
		{
			if (litterId == Guid.Empty)
			{
				throw new InvalidOperationException(
					"Моля, изберете котило.");
			}

			var litterExists = await _context.Litters
				.AnyAsync(l => l.LitterId == litterId);

			if (!litterExists)
			{
				throw new InvalidOperationException(
					"Избраното котило не съществува.");
			}
		}
	}
}
