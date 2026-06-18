using Microsoft.AspNetCore.Mvc;
using TravelBuddy.GCommon.Pagination;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels.BookingCancellationRequest;

using static TravelBuddy.GCommon.Constants.AppConstants;

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
        public async Task<IActionResult> Index(int? pageNumber)
        {
            // Get the current user's Id using a method from the BaseController.
            Guid userId = GetUserId();

            // Asynchronously retrieves the current user's booking cancellation requests using the booking service.
            IEnumerable<BookingCancellationRequestViewModel> cancellationRequests
                = await bookingService.GetUserCancellationRequestsAsync(userId);

            int pageSize = PageSize;

            // Returns the view with the retrieved cancellation requests to be displayed to the user.
            return View(await PaginatedList<BookingCancellationRequestViewModel>.CreateAsync(cancellationRequests, pageNumber ?? 1, pageSize));
        }
    }
}
