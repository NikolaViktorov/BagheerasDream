using BagheerasDream.Application.DTOs.Admin;

namespace BagheerasDream.Application.Interfaces
{
	public interface IDashboardService
	{
		Task<DashboardStatsDto> GetStatsAsync();
	}
}
