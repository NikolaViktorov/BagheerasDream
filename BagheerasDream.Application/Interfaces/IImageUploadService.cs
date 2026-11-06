namespace BagheerasDream.Application.Interfaces;

public interface IImageUploadService
{
	Task<string> UploadAsync(
		Stream fileStream,
		string fileName,
		string folder);

	Task DeleteAsync(string? imagePath);
}