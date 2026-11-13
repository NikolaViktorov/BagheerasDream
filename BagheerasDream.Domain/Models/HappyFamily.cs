using System.ComponentModel.DataAnnotations;

namespace BagheerasDream.Domain.Models
{
	public class HappyFamily
	{
		public Guid HappyFamilyId { get; set; }

		[Required]
		[MaxLength(500)]
		public string Image { get; set; } = string.Empty;
	}
}