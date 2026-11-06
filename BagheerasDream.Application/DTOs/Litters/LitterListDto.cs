using System;
using System.Collections.Generic;
using System.Text;

namespace BagheerasDream.Application.DTOs.Litters
{
	public class LitterListDto
	{
		public Guid LitterId { get; set; }

		public string Name { get; set; } = string.Empty;

		public DateOnly Birthday { get; set; }

		public int KittenCount { get; set; }

		public string? PhotoOfLitter { get; set; }

		public string MotherName { get; set; } = string.Empty;
	}
}
