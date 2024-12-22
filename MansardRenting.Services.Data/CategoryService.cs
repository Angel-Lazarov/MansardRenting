using MansardRenting.Data;
using MansardRenting.Services.Data.Interfaces;
using MansardRenting.Web.ViewModels.Category;
using Microsoft.EntityFrameworkCore;

namespace MansardRenting.Services.Data
{
	public class CategoryService : ICategoryService
	{
		private readonly MansardRentingDbContext dbContext;

		public CategoryService(MansardRentingDbContext _dbContext)
		{
			dbContext = _dbContext;
		}

		public async Task<IEnumerable<HouseSelectCategoryFormModel>> AllCategoriesAsync()
		{
			IEnumerable<HouseSelectCategoryFormModel> allCategories = await dbContext
				.Categories
				.AsNoTracking()
				.Select(c => new HouseSelectCategoryFormModel()
				{
					Id = c.Id,
					Name = c.Name,
				})
				.ToArrayAsync();

			return allCategories;
		}

		public async Task<bool> ExistsByIdAsync(int categoryId)
		{
			return await dbContext.Categories.AnyAsync(c => c.Id == categoryId);
		}
	}
}
