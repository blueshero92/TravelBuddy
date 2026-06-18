using Microsoft.AspNetCore.Mvc;
using TravelBuddy.GCommon.Pagination;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels.Booking;

using static TravelBuddy.GCommon.Constants.AppConstants;
using static TravelBuddy.GCommon.Constants.OutputMessages;

namespace TravelBuddy.Controllers
{
    public class BookingController : BaseController
    {
        // Dependency Injection of the IBookingService to handle booking-related operations.
        private readonly IBookingService bookingService;

        // Constructor to initialize the BookingController with the provided IBookingService.
        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        // Task for retrieving and displaying the current user's bookings on the MyBookings page.
        [HttpGet]
        public async Task<IActionResult> MyBookings(int? pageNumber)
        {
            // Get the current user's Id using a method from the BaseController.
            Guid userId = GetUserId();

            // Asynchronously retrieves the current user's bookings using the booking service.
            IEnumerable<BookingViewModel> bookings
                = await bookingService.GetUserBookingsAsync(userId);

            int pageSize = PageSize;

            // Returns the view with the retrieved bookings to be displayed to the user.
            return View(await PaginatedList<BookingViewModel>.CreateAsync(bookings, pageNumber ?? 1, pageSize));
        }

        // Task for displaying the booking creation page for a specific excursion.
        [HttpGet]
        public async Task<IActionResult> Create(Guid excursionId)
        {
            // Get the current user's Id using a method from the BaseController.
            Guid userId = GetUserId();

            // If the user is an admin, they are not allowed to create bookings. Show an error message and redirect to the Excursion index page.
            if (User.IsInRole("Admin"))
            {
                TempData[ErrorTempDataKey] = BookingCreateAdminForbidden;
                return RedirectToAction("Index", "Excursion");
            }

            // Asynchronously retrieves the booking details for the specified excursion and user using the booking service.
            BookingViewModel? booking = await bookingService.CreateBookingGetAsync(userId, excursionId);

            // If the booking details retrieval fails (e.g., due to invalid excursionId or other issues), return a NotFound response.
            if (booking == null)
            {
                return NotFound();
            }

            // If the booking details are successfully retrieved, return the view with the booking details to be displayed to the user.
            return View(booking);
        }

        // Task for creating a new booking for a specified excursion.
        // View model is used simply for method override.
        [HttpPost]
        public async Task<IActionResult> Create(Guid excursionId, BookingViewModel bookingVm)
        {

            // Get the current user's Id using a method from the BaseController.
            Guid userId = GetUserId();

            // If the user is an admin, they are not allowed to create bookings. Show an error message and redirect to the Excursion index page.
            if (User.IsInRole("Admin"))
            {
                TempData[ErrorTempDataKey] = BookingCreateAdminForbidden;
                return RedirectToAction("Index", "Excursion");
            }

            // Asynchronously creates a new booking for the specified excursion and user using the booking service.
            BookingViewModel? booking = await bookingService.CreateBookingPostAsync(userId, excursionId);

            // If the booking creation fails (e.g., excursion not found or no capacity), show an error.
            if (booking == null)
            {
                TempData[ErrorTempDataKey] = BookingCreateNoCapacity;
                return RedirectToAction("Index", "Excursion");
            };

            // Redirect to MyBookings after successful booking creation.
            TempData[SuccessTempDataKey] = BookingCreateSuccess;
            return RedirectToAction(nameof(MyBookings));
        }

        // Task for displaying the cancellation confirmation page for a specific booking.
        [HttpGet]
        public async Task<IActionResult> Cancel(Guid id)
        {
            // Get the current user's Id using a method from the BaseController.
            Guid userId = GetUserId();

            // Asynchronously retrieves the booking details for the specified booking Id and user using the booking service.
            BookingViewModel? booking = await bookingService.GetBookingByIdAsync(id, userId);

            // If the booking is not found (e.g., invalid booking Id or the booking does not belong to the user), return a NotFound response.
            if (booking == null)
            {
                return NotFound();
            }

            // If the booking is found, return the view with the booking details to be displayed on the cancellation confirmation page.
            return View(booking);
        }

        // Task for handling the cancellation of a booking after the user confirms the cancellation.
        // View model is used simply for method override.
        [HttpPost]
        public async Task<IActionResult> Cancel(Guid bookingId, BookingViewModel bookingVm)
        {
            // Get the current user's Id using a method from the BaseController.
            Guid userId = GetUserId();

            // If a declined request already exists, block the user from re-submitting.
            bool isDeclined = await bookingService.HasDeclinedCancellationAsync(bookingId);

            if (isDeclined)
            {
                TempData[ErrorTempDataKey] = BookingCancelDeclined;
                return RedirectToAction(nameof(MyBookings));
            }

            // If an approved request already exists, block the user from re-submitting.
            bool isApproved = await bookingService.HasApprovedCancellationAsync(bookingId);

            if (isApproved)
            {
                TempData[ErrorTempDataKey] = BookingCancelApproved;
                return RedirectToAction(nameof(MyBookings));
            }

            // If a pending request already exists, block the user from re-submitting.
            bool isPending = await bookingService.HasPendingCancellationAsync(bookingId);

            if (isPending)
            {
                TempData[WarningTempDataKey] = BookingCancelAlreadyPending;
                return RedirectToAction(nameof(MyBookings));
            }

            // Asynchronously cancels the specified booking for the user using the booking service.
            await bookingService.CancelBookingAsync(userId, bookingId);

            // After attempting to cancel the booking, redirect the user back to the MyBookings page to see the updated list of bookings.
            TempData[WarningTempDataKey] = BookingCancelPending;
            return RedirectToAction(nameof(MyBookings));
        }

    }
}
