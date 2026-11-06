using BagheerasDream.Application.Interfaces;

namespace BagheerasDream.Infrastructure.Services.Files;

public class ImageUploadService : IImageUploadService
{
	private readonly string _webRootPath;

	private static readonly HashSet<string> AllowedExtensions =
	[
		".jpg", ".jpeg", ".png", ".webp"
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
			throw new InvalidOperationException(
				"Позволени са само JPG, PNG и WebP изображения.");

		ValidateFolder(folder);

		var uploadFolder = Path.Combine(_webRootPath, "uploads", folder);

		Directory.CreateDirectory(uploadFolder);

		var uniqueFileName = $"{Guid.NewGuid():N}{extension}";
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

			var buffer = new byte[81920];
			long totalBytes = 0;
			int bytesRead;

			var header = new byte[12];
			var headerBytesRead = await fileStream.ReadAsync(header);

			if (!HasValidImageSignature(header, extension))
			{
				throw new InvalidOperationException(
					"Съдържанието на файла не съответства на избрания формат на изображението.");
			}

			// Записваме вече прочетените 12 байта.
			await destinationStream.WriteAsync(
				header.AsMemory(0, headerBytesRead));

			totalBytes += headerBytesRead;

			while ((bytesRead = await fileStream.ReadAsync(buffer)) > 0)
			{
				totalBytes += bytesRead;

				if (totalBytes > MaxFileSize)
				{
					throw new InvalidOperationException(
						"Снимката не може да бъде по-голяма от 15 MB.");
				}

				await destinationStream.WriteAsync(
					buffer.AsMemory(0, bytesRead));
			}

			if (totalBytes == 0)
				throw new InvalidOperationException("Избраният файл е празен.");
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

		if (!imagePath.StartsWith(
			uploadsPrefix,
			StringComparison.OrdinalIgnoreCase))
		{
			return Task.CompletedTask;
		}

		var relativePath = imagePath[1..];

		var fullPath = Path.GetFullPath(
			Path.Combine(
				_webRootPath,
				relativePath.Replace('/', Path.DirectorySeparatorChar)));

		var uploadsRoot = Path.GetFullPath(
			Path.Combine(_webRootPath, "uploads")) + Path.DirectorySeparatorChar;

		if (!fullPath.StartsWith(
			uploadsRoot,
			StringComparison.OrdinalIgnoreCase))
		{
			return Task.CompletedTask;
		}

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
			throw new ArgumentException(
				"Невалидна папка за качване.",
				nameof(folder));
		}
	}

	private static bool HasValidImageSignature(
	byte[] header,
	string extension)
	{
		return extension switch
		{
			".jpg" or ".jpeg" =>
				header.Length >= 3 &&
				header[0] == 0xFF &&
				header[1] == 0xD8 &&
				header[2] == 0xFF,

			".png" =>
				header.Length >= 8 &&
				header[0] == 0x89 &&
				header[1] == 0x50 &&
				header[2] == 0x4E &&
				header[3] == 0x47 &&
				header[4] == 0x0D &&
				header[5] == 0x0A &&
				header[6] == 0x1A &&
				header[7] == 0x0A,

			".webp" =>
				header.Length >= 12 &&
				header[0] == 0x52 &&
				header[1] == 0x49 &&
				header[2] == 0x46 &&
				header[3] == 0x46 &&
				header[8] == 0x57 &&
				header[9] == 0x45 &&
				header[10] == 0x42 &&
				header[11] == 0x50,

			_ => false
		};
	}
}
