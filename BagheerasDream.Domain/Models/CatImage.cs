using System.ComponentModel.DataAnnotations;

namespace BagheerasDream.Domain.Models
{
	public class CatImage
	{
		public Guid CatImageId { get; set; }

		public Guid CatId { get; set; }

		public Cat? Cat { get; set; }

		[Required]
		[MaxLength(500)]
		public string ImagePath { get; set; } = string.Empty;

		public int DisplayOrder { get; set; }
	}
}