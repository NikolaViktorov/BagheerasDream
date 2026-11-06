namespace BagheerasDream.Application.DTOs.Admin
{
	public class DashboardStatsDto
	{
		public int TotalCats { get; set; }
		public int MaleCats { get; set; }
		public int FemaleCats { get; set; }

		public int TotalLitters { get; set; }

		public int TotalKittens { get; set; }
		public int AvailableKittens { get; set; }
		public int ReservedKittens { get; set; }
		public int SoldKittens { get; set; }
	}
}
