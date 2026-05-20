using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels;

namespace TravelBuddy.Controllers
{
    public class ExcursionController : Controller
    {
        private readonly IExcursionService excursionService;

        public ExcursionController(IExcursionService excursionService)
        {
            this.excursionService = excursionService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            IEnumerable<ExcursionViewModel> excursions 
                = await excursionService.GetAllExcursionsAsync();

            return View(excursions);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            ExcursionViewModel excursion = await excursionService.GetExcursionByIdAsync(id);

            if (excursion == null)
            {
                return NotFound();
            }

            return View(excursion);
        }

    }
}
