using BagheerasDream.Application.DTOs.HappyFamilies;

namespace BagheerasDream.Application.Interfaces
{
	public interface IHappyFamiliesService
	{
		Task<IEnumerable<HappyFamilyListDto>> GetAllAsync();
	}
}