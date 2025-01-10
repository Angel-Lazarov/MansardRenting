using MansardRenting.Data;
using MansardRenting.Data.Models;
using MansardRenting.Services.Data.Interfaces;
using MansardRenting.Services.Data.Models.House;
using MansardRenting.Web.ViewModels.Home;
using MansardRenting.Web.ViewModels.Agent;
using MansardRenting.Web.ViewModels.House;
using MansardRenting.Web.ViewModels.House.Enums;
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
                .Where(h => h.IsActive)
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

        public async Task<string> CreateAndReturnIdAsync(HouseFormModel model, string agentId)
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

            return newHouse.Id.ToString();
        }

        public async Task<AllHousesFilteredAndPagedServiceModel> AllAsync(AllHousesQueryModel queryModel)
        {
            IQueryable<House> housesQuery = dbContext
                .Houses
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(queryModel.Category))
            {
                housesQuery = housesQuery
                    .Where(h => h.Category.Name == queryModel.Category);
            }

            if (!string.IsNullOrWhiteSpace(queryModel.SearchString))
            {
                string wildCards = $"%{queryModel.SearchString.ToLower()}%";

                housesQuery = housesQuery
                    .Where(h => EF.Functions.Like(h.Title, wildCards) ||
                                     EF.Functions.Like(h.Address, wildCards) ||
                                     EF.Functions.Like(h.Description, wildCards));

                //housesQuery = housesQuery
                //	.Where(h => h.Title.ToLower().Contains(queryModel.SearchString.ToLower()) ||
                //	                 h.Address.ToLower().Contains(queryModel.SearchString.ToLower()) ||
                //	                 h.Description.ToLower().Contains(queryModel.SearchString.ToLower()));
            }

            housesQuery = queryModel.HouseSorting switch
            {
                HouseSorting.Newest => housesQuery.OrderByDescending(h => h.CreatedOn),
                HouseSorting.Oldest => housesQuery.OrderBy(h => h.CreatedOn),
                HouseSorting.PriceAscending => housesQuery.OrderBy(h => h.PricePerMonth),
                HouseSorting.PriceDescending => housesQuery.OrderByDescending(h => h.PricePerMonth),
                _ => housesQuery.OrderBy(h => h.RenterId != null).ThenByDescending(h => h.CreatedOn)
            };

            IEnumerable<HouseAllViewModel> allHouses = await housesQuery
                .Where(h => h.IsActive)
                .Skip((queryModel.CurrentPage - 1) * queryModel.HousesPerPage)
                .Take(queryModel.HousesPerPage)
                .Select(h => new HouseAllViewModel
                {
                    Id = h.Id.ToString(),
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId.HasValue
                })
                .ToArrayAsync();

            int totalHouses = housesQuery.Count();

            return new AllHousesFilteredAndPagedServiceModel
            {
                TotalHousesCount = totalHouses,
                Houses = allHouses
            };
        }

        public async Task<IEnumerable<HouseAllViewModel>> AllByAgentIdAsync(string agentId)
        {
            IEnumerable<HouseAllViewModel> allAgentHouses = await dbContext
                .Houses
                .Where(h => h.IsActive &&
                            h.AgentId.ToString() == agentId)
                .Select(h => new HouseAllViewModel
                {
                    Id = h.Id.ToString(),
                    Title = h.Title,
                    Address = h.Address,
                    ImageUrl = h.ImageUrl,
                    PricePerMonth = h.PricePerMonth,
                    IsRented = h.RenterId.HasValue
                })
                .ToArrayAsync();

            return allAgentHouses;
        }

        public async Task<IEnumerable<HouseAllViewModel>> AllByUserIdAsync(string userId)
        {
            IEnumerable<HouseAllViewModel> allUserHouses = await dbContext
               .Houses
               .Where(h => h.IsActive &&
                           h.RenterId.HasValue &&
                           h.RenterId.ToString() == userId)
               .Select(h => new HouseAllViewModel
               {
                   Id = h.Id.ToString(),
                   Title = h.Title,
                   Address = h.Address,
                   ImageUrl = h.ImageUrl,
                   PricePerMonth = h.PricePerMonth,
                   IsRented = h.RenterId.HasValue
               })
               .ToArrayAsync();

            return allUserHouses;
        }

        public async Task<HouseDetailsViewModel> GetDetailsByIdAsync(string houseId)
        {
            House house = await dbContext.Houses
                .Include(h => h.Category)
                .Include(h => h.Agent)
                .ThenInclude(a => a.User)
                .Where(h => h.IsActive)
                 .FirstAsync(h => h.Id.ToString() == houseId);

            return new HouseDetailsViewModel()
            {
                Id = house.Id.ToString(),
                Title = house.Title,
                Address = house.Address,
                ImageUrl = house.ImageUrl,
                PricePerMonth = house.PricePerMonth,
                IsRented = house.RenterId.HasValue,
                Description = house.Description,
                Category = house.Category.Name,
                Agent = new AgentInfoOnHouseViewModel()
                {
                    Email = house.Agent.User.Email,
                    PhoneNumber = house.Agent.PhoneNumber
                }
            };
        }

        public async Task<bool> ExistsByIdAsync(string houseId)
        {
            bool result = await dbContext
                .Houses.Where(h => h.IsActive)
                .AnyAsync(h => h.Id.ToString() == houseId);

            return result;
        }

        public async Task<HouseFormModel> GetHouseForEditByIdAsync(string houseId)
        {
            House house = await dbContext.Houses
               .Include(h => h.Category)
               .Where(h => h.IsActive)
               .FirstAsync(h => h.Id.ToString() == houseId);

            return new HouseFormModel
            {
                Title = house.Title,
                Address = house.Address,
                Description = house.Description,
                ImageUrl = house.ImageUrl,
                PricePerMonth = house.PricePerMonth,
                CategoryId = house.CategoryId
            };
        }

        public async Task<bool> IsAgentWithIdOwnerOfHouseWithIdAsync(string houseId, string agentId)
        {
            House house = await dbContext.Houses
                .Where(h => h.IsActive)
                .FirstAsync(h => h.Id.ToString() == houseId);

            return house.AgentId.ToString() == agentId;
        }

        public async Task EditHouseByIdAndFormModel(string houseId, HouseFormModel formModel)
        {
            House house = await dbContext
                .Houses
                .Where(h => h.IsActive)
                .FirstAsync(h=>h.Id.ToString() == houseId);

            house.Title = formModel.Title;
            house.Address = formModel.Address;
            house.Description = formModel.Description;
            house.ImageUrl = formModel.ImageUrl;
            house.PricePerMonth = formModel.PricePerMonth;
            house.CategoryId = formModel.CategoryId;

            await dbContext.SaveChangesAsync();
        }
    }
}
