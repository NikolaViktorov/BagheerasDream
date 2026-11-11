using BagheerasDream.Application.DTOs.Pets;

namespace BagheerasDream.Application.Interfaces
{
	public interface IPetsService
	{
		Task<IEnumerable<PetListDto>> GetAllAsync();
	}
}
