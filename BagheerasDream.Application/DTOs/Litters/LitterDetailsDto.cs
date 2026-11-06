using BagheerasDream.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace BagheerasDream.Application.DTOs.Litters
{
	public class LitterDetailsDto
	{
		public Guid LitterId { get; set; }

		public string Name { get; set; } = string.Empty;

		public DateOnly Birthday { get; set; }

		public string? Description { get; set; }

		public LitterParentDto Mother { get; set; } = new();

		public LitterParentDto? Father { get; set; }

		public ICollection<KittenListDto> Kittens { get; set; } = new List<KittenListDto>();
	}
}
