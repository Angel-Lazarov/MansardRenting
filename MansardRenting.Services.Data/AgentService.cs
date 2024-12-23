using MansardRenting.Data;
using MansardRenting.Data.Models;
using MansardRenting.Services.Data.Interfaces;
using MansardRenting.Web.ViewModels.Agent;
using Microsoft.EntityFrameworkCore;

namespace MansardRenting.Services.Data
{
	public class AgentService : IAgentService
	{
		private readonly MansardRentingDbContext dbContext;

		public AgentService(MansardRentingDbContext _dbContext)
		{
			dbContext = _dbContext;
		}

		public async Task<bool> AgentExistsByUserIdAsync(string userId)
		{
            bool result = await dbContext
                .Agents
                .AnyAsync(a => a.UserId.ToString() == userId);

            return result;
        }

		public async Task<bool> AgentExistsByPhoneNumberAsync(string phoneNumber)
		{
            bool result = await dbContext
                .Agents
                .AnyAsync(a => a.PhoneNumber == phoneNumber);

            return result;
        }

		public async Task<bool> HasRentsByUserIdAsync(string userId)
		{
            ApplicationUser? user = await dbContext
                .Users
                .FirstOrDefaultAsync(u => u.Id.ToString() == userId);
            if (user == null)
            {
                return false;
            }

            return user.RentedHouses.Any();
        }

		public async Task Create(string userId, BecomeAgentFormModel model)
		{
			Agent agent = new Agent()
			{
				PhoneNumber = model.PhoneNumber,
				UserId = Guid.Parse(userId)
			};

			await dbContext.Agents.AddAsync(agent);
			await dbContext.SaveChangesAsync();
		}

		public async Task<string?> GetAgentIdByUserIdAsync(string userId)
		{
			Agent? agent = await dbContext.Agents.FirstOrDefaultAsync(a => a.UserId.ToString() == userId);
			if (agent == null)
			{
				return null;
			}

			return agent.Id.ToString();
		}
	}
}
