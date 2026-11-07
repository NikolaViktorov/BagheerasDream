namespace BagheerasDream.Application.DTOs.Admin.Cats;

public class CatImageDto
{
	public Guid CatImageId { get; set; }

	public string ImagePath { get; set; } = string.Empty;

	public int DisplayOrder { get; set; }
}