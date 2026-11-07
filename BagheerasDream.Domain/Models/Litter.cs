using System.ComponentModel.DataAnnotations;

namespace BagheerasDream.Domain.Models
{
	public class Litter
	{
		public Guid LitterId { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; } = string.Empty;

		public DateOnly Birthday { get; set; }

		[MaxLength(500)]
		public string? PhotoOfLitter { get; set; }

		public Guid MotherId { get; set; }
		public Cat Mother { get; set; } = null!;

		public Guid? FatherId { get; set; }
		public Cat? Father { get; set; }

		[MaxLength(100)]
		public string? ExternalFatherName { get; set; }

		[MaxLength(100)]
		public string? ExternalFatherCattery { get; set; }

		public ICollection<Kitten> Kittens { get; set; } = new List<Kitten>();

		public ICollection<LitterImage> Images { get; set; } = new List<LitterImage>();

		[MaxLength(1000)]
		public string? Description { get; set; }
	}
}
