using BagheerasDream.Application.DTOs.Admin.Cats;

namespace BagheerasDream.Application.Interfaces.Admin;

public interface IAdminCatsService
{
	Task<CatFormDto?> GetForEditAsync(Guid id);

	Task<Guid> CreateAsync(CatFormDto dto);

	Task<bool> UpdateAsync(CatFormDto dto);

	Task<bool> DeleteAsync(Guid id);
}