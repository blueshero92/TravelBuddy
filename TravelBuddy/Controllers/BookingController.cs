using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels;

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
        public async Task<IActionResult> MyBookings()
        {
            // Get the current user's Id using a method from the BaseController.
            Guid userId = GetUserId();

            // Asynchronously retrieves the current user's bookings using the booking service.
            IEnumerable<BookingViewModel> bookings
                = await bookingService.GetUserBookingsAsync(userId);

            // Returns the view with the retrieved bookings to be displayed to the user.
            return View(bookings);
        }

        [HttpGet]
        public async Task<IActionResult> Create(Guid excursionId)
        {
            Guid userId = GetUserId();

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

            // Asynchronously creates a new booking for the specified excursion and user using the booking service.
            BookingViewModel? booking = await bookingService.CreateBookingPostAsync(userId, excursionId);

            // If the booking creation fails (e.g., due to invalid excursionId or other issues), return a BadRequest response.
            if (booking == null)
            {
                return BadRequest();
            };

            // Redirect to MyBookings after successful booking creation.
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

            // Asynchronously cancels the specified booking for the user using the booking service.
            await bookingService.CancelBookingAsync(userId, bookingId);

            // If the cancellation process encounters issues add model error.
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Cannot cancel already cancelled booking.");
            }

            // After attempting to cancel the booking, redirect the user back to the MyBookings page to see the updated list of bookings.
            return RedirectToAction(nameof(MyBookings));
        }

        // Task for retrieving and displaying the current user's booking cancellation requests on the CancellationRequests page.
        [HttpGet]
        public async Task<IActionResult> CancellationRequests()
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
