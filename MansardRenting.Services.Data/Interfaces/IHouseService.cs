using MansardRenting.Services.Data.Models.House;
using MansardRenting.Web.ViewModels.Home;
using MansardRenting.Web.ViewModels.House;

namespace MansardRenting.Services.Data.Interfaces
{
	public interface IHouseService
	{
		Task<IEnumerable<IndexViewModel>> LastThreeHousesAsync();

		Task CreateAsync(HouseFormModel formModel, string agentId);

		Task<AllHousesFilteredAndPagedServiceModel> AllAsync(AllHousesQueryModel queryModel);
	}
}
