using BagheerasDream.Application.DTOs.Cats;

namespace BagheerasDream.Application.Interfaces;

public interface ICatsService
{
	Task<IEnumerable<CatListDto>> GetAllAsync();

	Task<IEnumerable<CatListDto>> GetMalesAsync();

	Task<IEnumerable<CatListDto>> GetFemalesAsync();

	Task<CatDetailsDto?> GetByIdAsync(Guid id);
}