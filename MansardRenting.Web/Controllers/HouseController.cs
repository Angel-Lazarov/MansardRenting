using MansardRenting.Services.Data.Interfaces;
using MansardRenting.Services.Data.Models.House;
using MansardRenting.Web.Infrastructure.Extensions;
using MansardRenting.Web.ViewModels.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static MansardRenting.Common.NotificationMessagesConstants;

namespace MansardRenting.Web.Controllers
{
    [Authorize]
    public class HouseController : Controller
    {
        private readonly ICategoryService categoryService;
        private readonly IAgentService agentService;
        private readonly IHouseService houseService;

        public HouseController(ICategoryService _categoryService, IAgentService _agentService, IHouseService _houseService)
        {
            categoryService = _categoryService;
            agentService = _agentService;
            houseService = _houseService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All([FromQuery] AllHousesQueryModel queryModel)
        {
            AllHousesFilteredAndPagedServiceModel serviceModel = await houseService.AllAsync(queryModel);

            queryModel.Houses = serviceModel.Houses;
            queryModel.TotalHouses = serviceModel.TotalHousesCount;
            queryModel.Categories = await categoryService.AllCategoryNamesAsync();

            return View(queryModel);
        }

        [HttpGet]
        public async Task<IActionResult> Mine()
        {
            List<HouseAllViewModel> myHouses = new List<HouseAllViewModel>();

            string userId = User.GetId()!;
            bool isUserAgent = await agentService.AgentExistsByUserIdAsync(userId);
            if (isUserAgent)
            {
                string? agentId = await agentService.GetAgentIdByUserIdAsync(userId);
                myHouses.AddRange(await houseService.AllByAgentIdAsync(agentId!));
            }
            else
            {
                myHouses.AddRange(await houseService.AllByUserIdAsync(userId));
            }
            return View(myHouses);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            bool isAgent = await agentService.AgentExistsByUserIdAsync(User.GetId()!);
            if (!isAgent)
            {
                TempData[ErrorMessage] = "You must become an agent in order to add new houses!";

                return RedirectToAction("Become", "Agent");
            }

            HouseFormModel formModel = new HouseFormModel();

            formModel.Categories = await categoryService.AllCategoriesAsync();

            return View(formModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(HouseFormModel model)
        {
            bool isAgent = await agentService.AgentExistsByUserIdAsync(User.GetId()!);
            if (!isAgent)
            {
                TempData[ErrorMessage] = "You must become an agent in order to add new houses!";

                return RedirectToAction("Become", "Agent");
            }

            bool isCategoryExists = await categoryService.ExistsByIdAsync(model.CategoryId);
            if (!isCategoryExists)
            {
                // Adding model error to ModelState automatically makes ModelState Invalid
                ModelState.AddModelError(nameof(model.CategoryId), "Category does not exist.");
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.AllCategoriesAsync();

                return View(model);
            }

            try
            {
                string? agentId = await agentService.GetAgentIdByUserIdAsync(User.GetId()!);

                await houseService.CreateAsync(model, agentId!);
            }
            catch (Exception _)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occured while trying to add your new house. Plese try again later or contact administrator!");

                model.Categories = await categoryService.AllCategoriesAsync();

                return View(model);
            }

            return RedirectToAction("All", "House");
        }
    }
}
