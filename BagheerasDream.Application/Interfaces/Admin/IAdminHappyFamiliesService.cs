using BagheerasDream.Application.DTOs.HappyFamilies;

namespace BagheerasDream.Application.Interfaces.Admin
{
	public interface IAdminHappyFamiliesService
	{
		Task<Guid> CreateAsync(HappyFamilyListDto dto);

		Task<bool> DeleteAsync(Guid id);
	}
}