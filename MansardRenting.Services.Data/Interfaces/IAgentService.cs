using MansardRenting.Web.ViewModels.Agent;

namespace MansardRenting.Services.Data.Interfaces
{
	public interface IAgentService
	{
		Task<bool> AgentExistsByUserIdAsync(string userId);

		Task<bool> AgentExistsByPhoneNumberAsync(string phoneNumber);

		Task<bool> HasRentsByUserIdAsync(string userId);

		Task Create(string userId, BecomeAgentFormModel model);

		Task<string?> GetAgentIdByUserIdAsync(string userId);
	}
}
