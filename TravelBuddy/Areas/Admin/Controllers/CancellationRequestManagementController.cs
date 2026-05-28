using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels;

namespace TravelBuddy.Areas.Admin.Controllers
{
    public class CancellationRequestManagementController : BaseAdminController
    {
        // Dependency injection of the booking service to manage booking cancellation requests in the admin area.
        private readonly IBookingService bookingService;

        // Constructor to initialize the CancellationRequestManagementController with the provided IBookingService.
        public CancellationRequestManagementController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        // Task for retrieving and displaying a list of all booking cancellation requests for management purposes on the Index page.
        public async Task<IActionResult> Index()
        {
            //Retrieve all booking cancellation requests using the booking service and store them in a variable.
            IEnumerable<BookingCancellationRequestViewModel> cancellationRequestsVm = await bookingService.GetAllCancellationRequestsAsync();

            //Check if the retrieved cancellation requests are null, and if so, return a NotFound result.
            if (cancellationRequestsVm == null)
            {
                return NotFound();
            }

            //If the cancellation requests are successfully retrieved, return the view with the cancellation requests to be displayed on the Index page.
            return View(cancellationRequestsVm);
        }
    }
}
