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
			return await dbContext.Agents.AnyAsync(a => a.UserId.ToString().ToLower() == userId.ToLower());
		}

		public async Task<bool> AgentExistsByPhoneNumberAsync(string phoneNumber)
		{
			return await dbContext.Agents.AnyAsync(a => a.PhoneNumber == phoneNumber);
		}

		public async Task<bool> HasRentsByUserIdAsync(string userId)
		{
			ApplicationUser? user = await dbContext.Users.Include(applicationUser => applicationUser.RentedHouses).FirstOrDefaultAsync(u => u.Id.ToString().ToLower() == userId);

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


	}
}
