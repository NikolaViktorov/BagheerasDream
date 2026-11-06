using BagheerasDream.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BagheerasDream.Application.DTOs.Admin.Kittens;

public class KittenFormDto
{
	public Guid KittenId { get; set; }

	[StringLength(100,
		ErrorMessage = "Името не може да надвишава 100 символа.")]
	public string? Name { get; set; }

	public CatColor Color { get; set; }

	[StringLength(500)]
	public string? ProfileImage { get; set; }

	public KittenStatus Status { get; set; } = KittenStatus.Available;

	[Required(ErrorMessage = "Моля, изберете котило.")]
	public Guid LitterId { get; set; }
}