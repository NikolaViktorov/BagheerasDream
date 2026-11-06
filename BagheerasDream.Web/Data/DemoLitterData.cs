using BagheerasDream.Domain.Models;
using BagheerasDream.Domain.Enums;

namespace BagheerasDream.Web.Data;

public static class DemoLitterData
{
	private static readonly Guid Mother1Id =
		Guid.Parse("11111111-1111-1111-1111-111111111111");

	private static readonly Guid Mother2Id =
		Guid.Parse("22222222-2222-2222-2222-222222222222");

	public static readonly Guid Litter1Id =
		Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");

	public static readonly Guid Litter2Id =
		Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

	public static readonly Guid Litter3Id =
		Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc");


	public static readonly List<Litter> Litters = new()
	{
		new Litter
		{
			LitterId = Litter1Id,
			Name = "The Royal Litter",
			Birthday = new DateOnly(2026, 8, 12),

			Mother = new Cat
			{
				CatId = Mother1Id,
				Name = "Bella"
			},

			Kittens = new List<Kitten>
			{
				new Kitten
				{
					KittenId = Guid.NewGuid(),
					LitterId = Litter1Id,
					Name = "Little Prince",
					Status = KittenStatus.Available
				},
				new Kitten
				{
					KittenId = Guid.NewGuid(),
					LitterId = Litter1Id,
					Name = "Little Princess",
					Status = KittenStatus.Reserved
				},
				new Kitten
				{
					KittenId = Guid.NewGuid(),
					LitterId = Litter1Id,
					Name = "Blue Star",
					Status = KittenStatus.Available
				}
			},

			Description = "Our beautiful Royal Litter is growing up surrounded by love, care and attention."
		},

		new Litter
		{
			LitterId = Litter2Id,
			Name = "The Velvet Litter",
			Birthday = new DateOnly(2026, 6, 20),

			Mother = new Cat
			{
				CatId = Mother2Id,
				Name = "Luna"
			},

			Kittens = new List<Kitten>
			{
				new Kitten
				{
					KittenId = Guid.NewGuid(),
					LitterId = Litter2Id,
					Name = "Velvet",
					Status = KittenStatus.Sold
				},
				new Kitten
				{
					KittenId = Guid.NewGuid(),
					LitterId = Litter2Id,
					Name = "Pearl",
					Status = KittenStatus.Reserved
				}
			},

			Description = "The Velvet Litter is known for its lovely personalities and beautiful coats."
		},

		new Litter
		{
			LitterId = Litter3Id,
			Name = "The Golden Litter",
			Birthday = new DateOnly(2026, 4, 5),

			Mother = new Cat
			{
				CatId = Guid.NewGuid(),
				Name = "Daisy"
			},

			Kittens = new List<Kitten>
			{
				new Kitten
				{
					KittenId = Guid.NewGuid(),
					LitterId = Litter3Id,
					Name = "Sunny",
					Status = KittenStatus.Available
				}
			},

			Description = "A lovely little family raised with dedication and affection."
		}
	};
}