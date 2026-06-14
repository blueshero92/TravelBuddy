using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TravelBuddy.GCommon;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels.BookingCancellationRequest;

using static TravelBuddy.GCommon.AppConstants;
using static TravelBuddy.GCommon.OutputMessages;

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
        [HttpGet]
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

        // Approve a cancellation request – cancels the booking and notifies the user.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(Guid bookingId)
        {
            bool result = await bookingService.ApproveCancellationAsync(bookingId);

            TempData[SuccessTempDataKey] = result ? CancellationApproveSuccess
                                                  : CancellationRequestNotFound;

            return RedirectToAction(nameof(Index));
        }

        // Decline a cancellation request – restores booking to Confirmed and notifies the user.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Decline(Guid bookingId)
        {
            bool result = await bookingService.DeclineCancellationAsync(bookingId);

            TempData[SuccessTempDataKey] = result ? CancellationDeclineSuccess
                                                  : CancellationRequestNotFound;

            return RedirectToAction(nameof(Index));
        }
    }
}
