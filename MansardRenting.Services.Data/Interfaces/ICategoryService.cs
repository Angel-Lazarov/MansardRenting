using MansardRenting.Web.ViewModels.Category;

namespace MansardRenting.Services.Data.Interfaces
{
	public interface ICategoryService
	{
		Task<IEnumerable<HouseSelectCategoryFormModel>> AllCategoriesAsync();
	}
}
