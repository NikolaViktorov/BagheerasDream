namespace BagheerasDream.Domain.Models
{
	public class LitterImage
	{
		public Guid LitterImageId { get; set; }

		public Guid LitterId { get; set; }

		public Litter? Litter { get; set; }

		public string ImagePath { get; set; } = string.Empty;

		public int DisplayOrder { get; set; }
	}
}