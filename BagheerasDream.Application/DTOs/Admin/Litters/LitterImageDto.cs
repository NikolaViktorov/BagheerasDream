namespace BagheerasDream.Application.DTOs.Admin.Litters
{
	public class LitterImageDto
	{
		public Guid LitterImageId { get; set; }

		public string ImagePath { get; set; } = string.Empty;

		public int DisplayOrder { get; set; }
	}
}