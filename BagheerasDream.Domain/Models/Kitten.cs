using BagheerasDream.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BagheerasDream.Domain.Models
{
	public class Kitten
	{
		public Guid KittenId { get; set; }

		[MaxLength(100)]
		public string? Name { get; set; } 

		public CatColor Color { get; set; }

		public Gender Gender { get; set; }

		[MaxLength(500)]
		public string? ProfileImage { get; set; }

		public KittenStatus Status { get; set; }

		public Guid LitterId { get; set; }
		public Litter Litter { get; set; } = null!;
	}
}
