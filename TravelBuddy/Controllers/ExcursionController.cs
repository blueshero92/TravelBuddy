using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels.Excursion;

namespace TravelBuddy.Controllers
{
    public class ExcursionController : BaseController
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


        [HttpGet]
        public async Task<IActionResult> MyFavoriteExcursions()
        {
            // Retrieves the user ID from the claims of the currently authenticated user.
            Guid userId = GetUserId();

            // Asynchronously retrieves a list of the user's favorite excursions using the excursion service and passes it to the view for display.
            IEnumerable<ExcursionViewModel?> favoriteExcursions
                = await excursionService.GetUserFavoriteExcursionsAsync(userId);

            // Returns the view with the list of the user's favorite excursions to be rendered on the MyFavoriteExcursions page.
            return View(favoriteExcursions);
        }

        // Task for adding a specific excursion to the user's favorites based on its Id.
        [HttpPost]
        public async Task<IActionResult> AddToFavorites(Guid excursionId)
        {
            // Retrieves the user ID from the claims of the currently authenticated user.
            Guid userId = GetUserId();

            // Attempts to add the specified excursion to the user's favorites using the excursion service.
            bool isAddedToFavorites = await excursionService.AddExcursionToFavoritesAsync(userId, excursionId);

            // If the operation to add to favorites fails, returns a BadRequest result indicating that the request was invalid.
            if (!isAddedToFavorites)
            {
                return BadRequest();
            }

            // If the operation is successful, redirects the user back to the details page of the excursion.
            return RedirectToAction(nameof(MyFavoriteExcursions));

        }

        // Task for removing a specific excursion from the user's favorites based on its Id.
        [HttpPost]
        public async Task<IActionResult> RemoveFromFavorites(Guid excursionId)
        {
            // Retrieves the user ID from the claims of the currently authenticated user.
            Guid userId = GetUserId();

            // Attempts to remove the specified excursion from the user's favorites using the excursion service.
            bool isRemovedFromFavorites = await excursionService.RemoveExcursionFromFavoritesAsync(userId, excursionId);

            // If the operation to remove from favorites fails, returns a BadRequest result indicating that the request was invalid.
            if (!isRemovedFromFavorites)
            {
                return BadRequest();
            }

            // If the operation is successful, redirects the user back to the MyFavoriteExcursions page.
            return RedirectToAction(nameof(MyFavoriteExcursions));
        }
    }
}
