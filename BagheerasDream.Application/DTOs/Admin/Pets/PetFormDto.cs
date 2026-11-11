using BagheerasDream.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BagheerasDream.Application.DTOs.Admin.Pets;

public class PetFormDto
{
	public Guid PetId { get; set; }

	[Required(ErrorMessage = "Името е задължително.")]
	[StringLength(100, MinimumLength = 2, ErrorMessage = "Името трябва да бъде между 2 и 100 символа")]
	public string Name { get; set; } = string.Empty;

	public DateOnly Birthday { get; set; }

	public CatColor Color { get; set; }

	public Breed Breed { get; set; }

	public Gender Gender { get; set; }

	public string? ProfileImage { get; set; }
}