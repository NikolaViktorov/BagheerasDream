using BagheerasDream.Domain.Enums;

namespace BagheerasDream.Application.DTOs.Admin.Kittens;

public class KittenListAdminDto
{
	public Guid KittenId { get; set; }

	public string? Name { get; set; }

	public CatColor Color { get; set; }

	public Gender Gender { get; set; }

	public string? ProfileImage { get; set; }

	public KittenStatus Status { get; set; }

	public Guid LitterId { get; set; }

	public string LitterName { get; set; } = string.Empty;
}