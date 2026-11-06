using BagheerasDream.Domain.Enums;

namespace BagheerasDream.Application.DTOs.Litters;

public class KittenListDto
{
	public Guid KittenId { get; set; }

	public string? Name { get; set; }

	public CatColor Color { get; set; }

	public string? ProfileImage { get; set; }

	public KittenStatus Status { get; set; }

	public Guid LitterId { get; set; }
}