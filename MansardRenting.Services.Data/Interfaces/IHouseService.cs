﻿using MansardRenting.Services.Data.Models.House;
using MansardRenting.Web.ViewModels.Home;
using MansardRenting.Web.ViewModels.House;

namespace MansardRenting.Services.Data.Interfaces
{
	public interface IHouseService
	{
		Task<IEnumerable<IndexViewModel>> LastThreeHousesAsync();

		Task<string> CreateAndReturnIdAsync(HouseFormModel formModel, string agentId);

		Task<AllHousesFilteredAndPagedServiceModel> AllAsync(AllHousesQueryModel queryModel);

		Task<IEnumerable<HouseAllViewModel>> AllByAgentIdAsync(string agentId);

		Task<IEnumerable<HouseAllViewModel>> AllByUserIdAsync(string userId);

		Task<bool> ExistsByIdAsync(string houseId);

		Task<HouseDetailsViewModel> GetDetailsByIdAsync(string houseId);

		Task<HouseFormModel> GetHouseForEditByIdAsync(string houseId);

		Task<bool> IsAgentWithIdOwnerOfHouseWithIdAsync(string houseId, string agentId);

		Task EditHouseByIdAndFormModelAsync(string houseId, HouseFormModel formModel);

		Task<HousePreDeleteDetailsViewModel> GetHouseForDeleteByIdAsync(string houseId);

		Task DeleteHouseByIdAsync(string houseId);
	}
}
