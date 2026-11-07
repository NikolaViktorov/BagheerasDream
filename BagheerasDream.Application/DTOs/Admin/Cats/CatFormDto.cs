using BagheerasDream.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BagheerasDream.Application.DTOs.Admin.Cats;

public class CatFormDto
{
	public Guid CatId { get; set; }

	[Required(ErrorMessage = "Името е задължително.")]
	[StringLength(100, MinimumLength = 2, ErrorMessage = "Името трябва да бъде между 2 и 100 символа")]
	public string Name { get; set; } = string.Empty;

	public DateOnly Birthday { get; set; }

	public CatColor Color { get; set; }

	public Breed Breed { get; set; }

	public Gender Gender { get; set; }

	public string? ProfileImage { get; set; }

	public string? PedigreeImage { get; set; }

	[StringLength(2000,
	ErrorMessage = "Описанието не може да надвишава 2000 символа.")]
	public string? Description { get; set; }

	public Guid? FatherId { get; set; }

	public Guid? MotherId { get; set; }
}