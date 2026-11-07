namespace BagheerasDream.Application.DTOs.Cats;

public class CatGalleryImageDto
{
	public Guid CatImageId { get; set; }

	public string ImagePath { get; set; } = string.Empty;

	public int DisplayOrder { get; set; }
}