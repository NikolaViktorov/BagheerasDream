using BagheerasDream.Application.DTOs.Admin.Pets;

namespace BagheerasDream.Application.Interfaces.Admin
{
	public interface IAdminPetsService
	{
		Task<PetFormDto?> GetForEditAsync(Guid id);

		Task<Guid> CreateAsync(PetFormDto dto);

		Task<bool> UpdateAsync(PetFormDto dto);

		Task<bool> DeleteAsync(Guid id);
	}
}
