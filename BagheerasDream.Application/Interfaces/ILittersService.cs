using BagheerasDream.Application.DTOs.Litters;

namespace BagheerasDream.Application.Interfaces
{
	public interface ILittersService
	{
		Task<IEnumerable<LitterListDto>> GetAllAsync();

		Task<LitterDetailsDto?> GetByIdAsync(Guid id);
	}
}
