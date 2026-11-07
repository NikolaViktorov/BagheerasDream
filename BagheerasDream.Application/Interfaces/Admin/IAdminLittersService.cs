using BagheerasDream.Application.DTOs.Admin.Litters;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagheerasDream.Application.Interfaces.Admin
{
	public interface IAdminLittersService
	{
		Task<LitterFormDto?> GetForEditAsync(Guid id);

		Task<Guid> CreateAsync(LitterFormDto dto);

		Task<bool> UpdateAsync(LitterFormDto dto);

		Task<bool> DeleteAsync(Guid id);

		Task<List<LitterImageDto>> GetImagesAsync(Guid litterId);

		Task<bool> AddImageAsync(Guid litterId, Stream fileStream, string fileName);

		Task<bool> DeleteImageAsync(Guid litterImageId);

		Task<bool> ReorderImagesAsync(Guid litterId, List<Guid> orderedImageIds);
	}
}
