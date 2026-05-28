using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels.BookingCancellationRequest;

namespace TravelBuddy.Controllers
{
    public class CancellationRequestController : BaseController
    {
        private readonly IBookingService bookingService;

        // Constructor that initializes the booking service through dependency injection.
        public CancellationRequestController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        // Task for retrieving and displaying the current user's booking cancellation requests on the CancellationRequests page.
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Get the current user's Id using a method from the BaseController.
            Guid userId = GetUserId();

            // Asynchronously retrieves the current user's booking cancellation requests using the booking service.
            IEnumerable<BookingCancellationRequestViewModel> cancellationRequests
                = await bookingService.GetUserCancellationRequestsAsync(userId);

            // Returns the view with the retrieved cancellation requests to be displayed to the user.
            return View(cancellationRequests);
        }
    }
}
