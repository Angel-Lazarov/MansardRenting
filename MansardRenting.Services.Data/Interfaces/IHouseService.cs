using MansardRenting.Web.ViewModels.Home;

namespace MansardRenting.Services.Data.Interfaces
{
	public interface IHouseService
	{
		Task<IEnumerable<IndexViewModel>> LastThreeHousesAsync();
	}
}
