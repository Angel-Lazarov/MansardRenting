using MansardRenting.Services.Data.Interfaces;
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

		public HouseController(ICategoryService _categoryService, IAgentService _agentService)
		{
			categoryService = _categoryService;
			agentService = _agentService;
		}

		[AllowAnonymous]
		public async Task<IActionResult> All()
		{
			return View();
		}

		public async Task<IActionResult> Mine()
		{
			return View();
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
	}
}
