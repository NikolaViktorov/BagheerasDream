using BagheerasDream.Domain.Enums;

namespace BagheerasDream.Web.Extensions
{
	public static class CatColorExtensions
	{
		public static string ToDisplayName(this CatColor color)
		{
			return color switch
			{
				CatColor.Lilac => "Lilac",
				CatColor.Blue => "Blue",
				CatColor.White => "White",
				CatColor.Chocolate => "Chocolate",
				CatColor.Black => "Black",
				CatColor.Cream => "Cream",
				CatColor.Fawn => "Fawn",
				CatColor.Cinnamon => "Cinnamon",

				CatColor.WhiteBlueBicolor => "White / Blue Bicolor",
				CatColor.WhiteChocolateBicolor => "White / Chocolate Bicolor",
				CatColor.WhiteLilacBicolor => "White / Lilac Bicolor",
				CatColor.WhiteCinnamonBicolor => "White / Cinnamon Bicolor",
				CatColor.WhiteBlackBicolor => "White / Black Bicolor",

				CatColor.BlackGoldenShadedPoint => "Black Golden Shaded Point",
				CatColor.SealSilverShadedPoint => "Seal Silver Shaded Point",
				CatColor.SealGoldenShadedPoint => "Seal Golden Shaded Point",
				CatColor.BlackGoldenShaded => "Black Golden Shaded",

				_ => color.ToString()
			};
		}
	}
}