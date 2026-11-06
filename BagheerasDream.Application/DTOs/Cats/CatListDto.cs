using BagheerasDream.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagheerasDream.Application.DTOs.Cats
{
	public class CatListDto
	{
		public Guid CatId { get; set; }

		public string Name { get; set; } = string.Empty;

		public DateOnly Birthday { get; set; }

		public CatColor Color { get; set; }

		public Breed Breed { get; set; }

		public Gender Gender { get; set; }

		public string? ProfileImage { get; set; }
	}
}
