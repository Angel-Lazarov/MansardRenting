using System.ComponentModel.DataAnnotations;

namespace MansardRenting.Web.ViewModels.House
{
	public class HouseAllViewModel
	{
		public string Id { get; set; } = null!;
		public string Title { get; set; } = null!;
		public string Address { get; set; } = null!;

		[Display(Name = "Image Url")]
		public string ImageUrl { get; set; } = null!;

		[Display(Name = "Price Per Month")]
		public decimal PricePerMonth { get; set; }

		[Display(Name = "Is Rented")]
		public bool IsRented { get; set; }
	}
}
