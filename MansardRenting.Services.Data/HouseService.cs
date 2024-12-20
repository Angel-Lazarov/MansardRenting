using MansardRenting.Data;
using MansardRenting.Services.Data.Interfaces;
using MansardRenting.Web.ViewModels.Home;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
    }
}
