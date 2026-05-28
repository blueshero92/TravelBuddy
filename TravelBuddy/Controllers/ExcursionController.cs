using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels.Excursion;

namespace TravelBuddy.Controllers
{
    public class ExcursionController : Controller
    {
        //Dependency injection of the excursion service to handle business logic related to excursions.
        private readonly IExcursionService excursionService;

        // Constructor to initialize the ExcursionController with the provided IExcursionService.
        public ExcursionController(IExcursionService excursionService)
        {
            this.excursionService = excursionService;
        }

        // Task for retrieving and displaying a list of all excursions on the index page.
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            // Asynchronously retrieves a list of all excursions using the excursion service and passes it to the view for display.
            IEnumerable<ExcursionViewModel> excursions 
                = await excursionService.GetAllExcursionsAsync();

            // Returns the view with the list of excursions to be rendered on the index page.
            return View(excursions);
        }

        // Task for retrieving and displaying the details of a specific excursion based on its Id.
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            // Asynchronously retrieves the details of a specific excursion using its Id through the excursion service.
            ExcursionViewModel excursion = await excursionService.GetExcursionByIdAsync(id);

            // If the excursion is not found (i.e., null), returns a NotFound result to indicate that the requested resource does not exist.
            if (excursion == null)
            {
                return NotFound();
            }

            // If the excursion is found, returns the view with the excursion details to be rendered on the details page.
            return View(excursion);
        }

    }
}
