namespace BagheerasDream.Application.DTOs.Litters
{
	public class LitterParentDto
	{
		public Guid? CatId { get; set; }

		public string Name { get; set; } = string.Empty;

		public string? ProfileImage { get; set; }

		public bool IsExternal { get; set; }

		public string? CatteryName { get; set; }
	}
}
