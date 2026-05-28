using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services.Core;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels;

namespace TravelBuddy.Areas.Admin.Controllers
{
    public class ExcursionManagementController : BaseAdminController
    {
        // Dependency injection of the excursion service to manage excursions in the admin area.
        private readonly IExcursionService excursionService;

        // Constructor to initialize the ExcursionManagementController with the provided IExcursionService.
        public ExcursionManagementController(IExcursionService excursionService)
        {
            this.excursionService = excursionService;
        }

        // Task for retrieving and displaying a list of all excursions for management purposes on the Index page.
        public async Task<IActionResult> Index()
        {
            //Retrieve all excursions using the excursion service and store them in a variable.
            IEnumerable<ExcursionViewModel> excursionsVm = await excursionService.GetAllExcursionsAsync();

            //Check if the retrieved excursions are null, and if so, return a NotFound result.
            if (excursionsVm == null)
            {
                return NotFound();
            }

            //If the excursions are successfully retrieved, return the view with the excursions view model to be displayed on the Index page.
            return View(excursionsVm);
        }
    }
}
