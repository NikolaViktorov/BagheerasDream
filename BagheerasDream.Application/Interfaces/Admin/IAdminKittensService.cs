using BagheerasDream.Application.DTOs.Admin.Kittens;
using BagheerasDream.Application.DTOs.Litters;

namespace BagheerasDream.Application.Interfaces.Admin;

public interface IAdminKittensService
{
	Task<IEnumerable<KittenListAdminDto>> GetAllAsync();

	Task<KittenFormDto?> GetForEditAsync(Guid id);

	Task<Guid> CreateAsync(KittenFormDto dto);

	Task<bool> UpdateAsync(KittenFormDto dto);

	Task<bool> DeleteAsync(Guid id);
}