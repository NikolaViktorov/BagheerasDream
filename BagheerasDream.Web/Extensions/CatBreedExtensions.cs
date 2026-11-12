using BagheerasDream.Domain.Enums;

namespace BagheerasDream.Web.Extensions
{
	public static class CatBreedExtensions
	{
		public static string ToDisplayName(this Breed breed)
		{
			return breed switch
			{
				Breed.BritishShorthair => "British Shorthair",
				Breed.BritishLonghair => "British Longhair",
				Breed.Other => "Other",

				_ => breed.ToString()
			};
		}
	}
}