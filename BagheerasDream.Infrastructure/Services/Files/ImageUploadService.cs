using BagheerasDream.Application.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace BagheerasDream.Infrastructure.Services.Files;

public class ImageUploadService : IImageUploadService
{
	private readonly string _webRootPath;

	private static readonly HashSet<string> AllowedExtensions =
	[
		".jpg",
		".jpeg",
		".png",
		".webp"
	];

	private const long MaxFileSize = 15 * 1024 * 1024;

	public ImageUploadService(string webRootPath)
	{
		if (string.IsNullOrWhiteSpace(webRootPath))
			throw new ArgumentException("Web root path is required.", nameof(webRootPath));

		_webRootPath = Path.GetFullPath(webRootPath);
	}

	public async Task<string> UploadAsync(
		Stream fileStream,
		string fileName,
		string folder)
	{
		ArgumentNullException.ThrowIfNull(fileStream);

		if (!fileStream.CanRead)
			throw new InvalidOperationException("Файлът не може да бъде прочетен.");

		var extension = Path.GetExtension(fileName).ToLowerInvariant();

		if (!AllowedExtensions.Contains(extension))
			throw new InvalidOperationException("Позволени са само JPG, PNG и WebP изображения.");

		ValidateFolder(folder);

		await using var memoryStream = new MemoryStream();

		var buffer = new byte[81920];
		long totalBytes = 0;
		int bytesRead;

		while ((bytesRead = await fileStream.ReadAsync(buffer)) > 0)
		{
			totalBytes += bytesRead;

			if (totalBytes > MaxFileSize)
				throw new InvalidOperationException("Снимката не може да бъде по-голяма от 15 MB.");

			await memoryStream.WriteAsync(buffer.AsMemory(0, bytesRead));
		}

		if (totalBytes == 0)
			throw new InvalidOperationException("Избраният файл е празен.");

		memoryStream.Position = 0;

		using var image = await Image.LoadAsync(memoryStream);

		image.Mutate(x => x.Resize(new ResizeOptions
		{
			Size = new Size(1200, 1200),
			Mode = ResizeMode.Max
		}));

		var uploadFolder = Path.Combine(
			_webRootPath,
			"uploads",
			folder);

		Directory.CreateDirectory(uploadFolder);

		var uniqueFileName = $"{Guid.NewGuid():N}.webp";
		var filePath = Path.Combine(uploadFolder, uniqueFileName);

		try
		{
			await using var destinationStream = new FileStream(
				filePath,
				FileMode.CreateNew,
				FileAccess.Write,
				FileShare.None,
				81920,
				useAsync: true);

			await image.SaveAsync(
				destinationStream,
				new WebpEncoder { Quality = 85 });
		}
		catch
		{
			if (File.Exists(filePath))
				File.Delete(filePath);

			throw;
		}

		return $"/uploads/{folder}/{uniqueFileName}";
	}

	public Task DeleteAsync(string? imagePath)
	{
		if (string.IsNullOrWhiteSpace(imagePath))
			return Task.CompletedTask;

		const string uploadsPrefix = "/uploads/";

		if (!imagePath.StartsWith(uploadsPrefix, StringComparison.OrdinalIgnoreCase))
			return Task.CompletedTask;

		var relativePath = imagePath[1..];

		var fullPath = Path.GetFullPath(
			Path.Combine(_webRootPath, relativePath.Replace('/', Path.DirectorySeparatorChar)));

		var uploadsRoot = Path.GetFullPath(
			Path.Combine(_webRootPath, "uploads")) + Path.DirectorySeparatorChar;

		if (!fullPath.StartsWith(uploadsRoot, StringComparison.OrdinalIgnoreCase))
			return Task.CompletedTask;

		if (File.Exists(fullPath))
			File.Delete(fullPath);

		return Task.CompletedTask;
	}

	private static void ValidateFolder(string folder)
	{
		if (string.IsNullOrWhiteSpace(folder) ||
			folder != Path.GetFileName(folder) ||
			folder.Contains(".."))
		{
			throw new ArgumentException("Невалидна папка за качване.", nameof(folder));
		}
	}
}
