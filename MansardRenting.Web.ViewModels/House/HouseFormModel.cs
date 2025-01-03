﻿using MansardRenting.Web.ViewModels.Category;
using System.ComponentModel.DataAnnotations;
using static MansardRenting.Common.EntityValidationConstants.House;

namespace MansardRenting.Web.ViewModels.House
{
	public class HouseFormModel
	{
		public HouseFormModel()
		{
			Categories = new HashSet<HouseSelectCategoryFormModel>();
		}

		[Required]
		[StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
		public string Title { get; set; } = null!;

		[Required]
		[StringLength(AddressMaxLength, MinimumLength = AddressMinLength)]
		public string Address { get; set; } = null!;

		[Required]
		[StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
		public string Description { get; set; } = null!;

		[Required]
		[MaxLength(ImageUrlMaxLength)]
		[Display(Name = "Image URL")]
		public string ImageUrl { get; set; } = null!;

		[Required]
		[Range(typeof(decimal), PricePerMonthMinValue, PricePerMonthMaxValue)]
		[Display(Name = "Price Per Month")]
		public decimal PricePerMonth { get; set; }

		[Display(Name = "Category")]
		public int CategoryId { get; set; }

		public IEnumerable<HouseSelectCategoryFormModel> Categories { get; set; }


	}
}
