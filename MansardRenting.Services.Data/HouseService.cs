using MansardRenting.Data;
using MansardRenting.Data.Models;
using MansardRenting.Services.Data.Interfaces;
using MansardRenting.Web.ViewModels.Home;
using MansardRenting.Web.ViewModels.House;
using Microsoft.EntityFrameworkCore;

namespace MansardRenting.Services.Data
{
	public class HouseService : IHouseService
	{
		private readonly MansardRentingDbContext dbContext;

		public HouseService(MansardRentingDbContext _dbContext)
		{
			dbContext = _dbContext;
		}

		public async Task<IEnumerable<IndexViewModel>> LastThreeHousesAsync()
		{
			IEnumerable<IndexViewModel> lastThreeHouses = await dbContext.Houses
				.OrderByDescending(x => x.CreatedOn)
				.Take(3)
				.Select(h => new IndexViewModel
				{
					Id = h.Id.ToString(),
					Title = h.Title,
					ImageUrl = h.ImageUrl,
				})
				.ToArrayAsync();

			return lastThreeHouses;
		}

		public async Task CreateAsync(HouseFormModel model, string agentId)
		{
			House newHouse = new House
			{
				Title = model.Title,
				Address = model.Address,
				AgentId = Guid.Parse(agentId),
				CategoryId = model.CategoryId,
				Description = model.Description,
				ImageUrl = model.ImageUrl,
				PricePerMonth = model.PricePerMonth
			};

			await dbContext.Houses.AddAsync(newHouse);
			await dbContext.SaveChangesAsync();
		}
	}
}
