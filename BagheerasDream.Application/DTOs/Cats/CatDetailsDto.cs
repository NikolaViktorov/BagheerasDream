using BagheerasDream.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BagheerasDream.Application.DTOs.Cats
{
	public class CatDetailsDto
	{
		public Guid CatId { get; set; }

		public string Name { get; set; } = string.Empty;

		public DateOnly Birthday { get; set; }

		public CatColor Color { get; set; }

		public Breed Breed { get; set; }

		public Gender Gender { get; set; }

		public string? ProfileImage { get; set; }

		public string? Description { get; set; }

		public Guid? FatherId { get; set; }

		public string? FatherName { get; set; }

		public string? FatherProfileImage { get; set; }

		public Guid? MotherId { get; set; }

		public string? MotherName { get; set; }

		public string? MotherProfileImage { get; set; }
	}
}
