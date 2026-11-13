using BagheerasDream.Application.DTOs.HappyFamilies;
using BagheerasDream.Application.Interfaces;
using BagheerasDream.Application.Interfaces.Admin;
using BagheerasDream.Domain.Models;
using BagheerasDream.Infrastructure.Data;

namespace BagheerasDream.Infrastructure.Services.Admin
{
	public class AdminHappyFamiliesService : IAdminHappyFamiliesService
	{
		private readonly ApplicationDbContext _context;
		private readonly IImageUploadService _imageUploadService;

		public AdminHappyFamiliesService(
			ApplicationDbContext context,
			IImageUploadService imageUploadService)
		{
			this._context = context;
			this._imageUploadService = imageUploadService;
		}

		public async Task<Guid> CreateAsync(HappyFamilyListDto dto)
		{
			var happyFamily = new HappyFamily
			{
				HappyFamilyId = Guid.NewGuid(),
				Image = dto.Image
			};

			await this._context.HappyFamilies.AddAsync(happyFamily);
			await this._context.SaveChangesAsync();

			return happyFamily.HappyFamilyId;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			var happyFamily = this._context.HappyFamilies.FirstOrDefault(h => h.HappyFamilyId == id);

			if (happyFamily is null)
			{
				return false;
			}

			this._context.HappyFamilies.Remove(happyFamily);
			await this._context.SaveChangesAsync();

			if (!string.IsNullOrEmpty(happyFamily.Image))
			{
				await this._imageUploadService.DeleteAsync(happyFamily.Image);
			}

			return true;
		}
	}
}
