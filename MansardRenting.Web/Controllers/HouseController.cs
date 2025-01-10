using MansardRenting.Services.Data.Interfaces;
using MansardRenting.Services.Data.Models.House;
using MansardRenting.Web.Infrastructure.Extensions;
using MansardRenting.Web.ViewModels.House;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

            try
            {
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
            catch (Exception)
            {
                return GeneralError();
            }
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

            try
            {
                HouseFormModel formModel = new HouseFormModel()
                {
                    Categories = await categoryService.AllCategoriesAsync()
                };

                return View(formModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }
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
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to add your new house. Please try again later or contact administrator!");

                model.Categories = await categoryService.AllCategoriesAsync();

                return View(model);
            }

            return RedirectToAction("All", "House");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            bool houseExists = await houseService.ExistsByIdAsync(id);
            if (!houseExists)
            {
                TempData[ErrorMessage] = "House with the provided id does not exist!";

                return RedirectToAction("All", "House");
            };

            try
            {
                HouseDetailsViewModel viewModel = await houseService
             .GetDetailsByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {

            bool houseExists = await houseService.ExistsByIdAsync(id);
            if (!houseExists)
            {
                TempData[ErrorMessage] = "House with the provided id does not exist!";

                return RedirectToAction("All", "House");
            };

            bool isUserAgent = await agentService.AgentExistsByUserIdAsync(User.GetId()!);
            if (!isUserAgent)
            {
                TempData[ErrorMessage] = "You must become an agent in order to edit house info!";

                return RedirectToAction("Become", "Agent");
            };

            string? agentId = await agentService.GetAgentIdByUserIdAsync(User.GetId()!);

            bool isAgentOwner = await houseService.IsAgentWithIdOwnerOfHouseWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";
                return RedirectToAction("Mine", "House");
            };

            try
            {
                HouseFormModel formModel = await houseService.GetHouseForEditByIdAsync(id);
                formModel.Categories = await categoryService.AllCategoriesAsync();
                return View(formModel);
            }
            catch (Exception)
            {
                return GeneralError();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, HouseFormModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await categoryService.AllCategoriesAsync();
                return View(model);
            }


            bool houseExists = await houseService.ExistsByIdAsync(id);
            if (!houseExists)
            {
                TempData[ErrorMessage] = "House with the provided id does not exist!";

                return RedirectToAction("All", "House");
            };

            bool isUserAgent = await agentService.AgentExistsByUserIdAsync(User.GetId()!);
            if (!isUserAgent)
            {
                TempData[ErrorMessage] = "You must become an agent in order to edit house info!";

                return RedirectToAction("Become", "Agent");
            };

            string? agentId = await agentService.GetAgentIdByUserIdAsync(User.GetId()!);

            bool isAgentOwner = await houseService.IsAgentWithIdOwnerOfHouseWithIdAsync(id, agentId!);

            if (!isAgentOwner)
            {
                TempData[ErrorMessage] = "You must be the agent owner of the house you want to edit!";
                return RedirectToAction("Mine", "House");
            };

            try
            {
                await houseService.EditHouseByIdAndFormModel(id, model);
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Unexpected error occurred while trying to update the house. Please try again later or contact administrator!");
                model.Categories = await categoryService.AllCategoriesAsync();
                return View(model);
            }
            return RedirectToAction("Details", "House", new { id });
        }

        private IActionResult GeneralError()
        {
            TempData[ErrorMessage] = "Unexpected error occurred! Please, try again later or contact administrator!";

            return RedirectToAction("Index", "Home");
        }
    }
}