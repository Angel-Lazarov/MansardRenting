using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MansardRenting.Web.Controllers
{
    [Authorize]
    public class HouseController : Controller
    {
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            return View();
        }

        public async Task<IActionResult> Mine() 
        {
            return View();
        }

        public async Task<IActionResult> Add() 
        {
            return View();
        }
    }
}
