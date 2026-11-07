namespace BagheerasDream.Application.DTOs.Litters;

public class LitterGalleryImageDto
{
	public Guid LitterImageId { get; set; }

	public string ImagePath { get; set; } = string.Empty;

	public int DisplayOrder { get; set; }
}