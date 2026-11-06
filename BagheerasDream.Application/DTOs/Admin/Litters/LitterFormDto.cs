using System.ComponentModel.DataAnnotations;

namespace BagheerasDream.Application.DTOs.Admin.Litters;

public class LitterFormDto
{
	public Guid LitterId { get; set; }

	[Required(ErrorMessage = "Името на котилото е задължително.")]
	[StringLength(50, ErrorMessage = "Името не може да надвишава 50 символа.")]
	public string Name { get; set; } = string.Empty;

	[Required(ErrorMessage = "Датата на раждане е задължителна.")]
	public DateOnly Birthday { get; set; }

	[Required(ErrorMessage = "Моля, изберете майка.")]
	public Guid MotherId { get; set; }

	public bool HasInternalFather { get; set; }

	public Guid? FatherId { get; set; }

	[StringLength(100)]
	public string? ExternalFatherName { get; set; }

	[StringLength(100)]
	public string? ExternalFatherCattery { get; set; }

	[StringLength(1000)]
	public string? Description { get; set; }

	public string? PhotoOfLitter { get; set; }
}