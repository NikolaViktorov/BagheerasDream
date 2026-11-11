using BagheerasDream.Domain.Enums;

namespace BagheerasDream.Application.DTOs.Pets
{
	public class PetListDto
	{
		public Guid PetId { get; set; }

		public string Name { get; set; } = string.Empty;

		public DateOnly Birthday { get; set; }

		public CatColor Color { get; set; }

		public Breed Breed { get; set; }

		public Gender Gender { get; set; }

		public string? ProfileImage { get; set; }
	}
}
