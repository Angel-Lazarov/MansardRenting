using MansardRenting.Services.Data.Interfaces;
using MansardRenting.Web.ViewModels;
using MansardRenting.Web.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MansardRenting.Web.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly IHouseService houseService;

	public HomeController(ILogger<HomeController> logger, IHouseService _houseService)
	{
		_logger = logger;
		houseService = _houseService;
	}

	public async Task<IActionResult> Index()
	{
		IEnumerable<IndexViewModel> viewModel = await houseService.LastThreeHousesAsync();
		return View(viewModel);
		return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}
