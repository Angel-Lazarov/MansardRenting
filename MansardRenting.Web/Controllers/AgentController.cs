using MansardRenting.Services.Data.Interfaces;
using MansardRenting.Web.Infrastructure.Extensions;
using MansardRenting.Web.ViewModels.Agent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static MansardRenting.Common.NotificationMessagesConstants;

namespace MansardRenting.Web.Controllers
{
	[Authorize]
	public class AgentController : Controller
	{
		private readonly IAgentService agentService;

		public AgentController(IAgentService _agentService)
		{
			agentService = _agentService;
		}

		[HttpGet]
		public async Task<IActionResult> Become()
		{
			string? userId = User.GetId();

			bool isAgent = await agentService.AgentExistsByUserIdAsync(userId!);

			if (isAgent)
			{
				TempData[ErrorMessage] = "You are already an agent!";

				return RedirectToAction("Index", "Home");
			}

			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Become(BecomeAgentFormModel model)
		{
			string? userId = User.GetId();

			bool isAgent = await agentService.AgentExistsByUserIdAsync(userId!);

			if (isAgent)
			{
				TempData[ErrorMessage] = "You are already an agent!";

				return RedirectToAction("Index", "Home");
			}

			bool isPhoneNumberTaken = await agentService.AgentExistsByPhoneNumberAsync(model.PhoneNumber);
			if (isPhoneNumberTaken)
			{
				ModelState.AddModelError(nameof(model.PhoneNumber), "Agent with the provided phone number already exists!");

				if (!ModelState.IsValid)
				{
					return View(model);
				}
			}

			bool userHasActiveRents = await agentService.HasRentsByUserIdAsync(userId!);

			if (userHasActiveRents)
			{
				TempData[ErrorMessage] = "You must not have any active rents in order to become an agent!";

				return RedirectToAction("Mine", "House");
			}

			try
			{
				await agentService.Create(userId!, model);
			}
			catch (Exception)
			{
				TempData[ErrorMessage] = "Unexpected error occurred while registering you as an agent! Please try again later or contact administrator.";
				RedirectToAction("index", "Home");
			}

			return RedirectToAction("All", "House");
		}
	}
}
