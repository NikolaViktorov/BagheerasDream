using BagheerasDream.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BagheerasDream.Domain.Models
{
	public class Cat
	{
		public Guid CatId { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; } = string.Empty;

		public DateOnly Birthday { get; set; }

		public CatColor Color { get; set; }

		public Breed Breed { get; set; }

		public Gender Gender { get; set; }

		[MaxLength(500)]
		public string? ProfileImage { get; set; }

		public string? Description { get; set; }

		// Parents
		public Guid? FatherId { get; set; }
		public Cat? Father { get; set; }

		public Guid? MotherId { get; set; }
		public Cat? Mother { get; set; }
	}
}
