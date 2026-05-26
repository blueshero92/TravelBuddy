using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels;

namespace TravelBuddy.Controllers
{
    public class BookingController : BaseController
    {
        private readonly IBookingService bookingService;

        public BookingController(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> MyBookings()
        {
            Guid userId = GetUserId();

            IEnumerable<BookingViewModel> bookings
                = await bookingService.GetUserBookingsAsync(userId);

            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Guid excursionId)
        {
            Guid userId = GetUserId();

            BookingViewModel booking = await bookingService.CreateBookingAsync(userId, excursionId);

            return View(booking);
        }

        [HttpGet]
        public async Task<IActionResult> Cancel(Guid id)
        {
            Guid userId = GetUserId();

            BookingViewModel? booking = await bookingService.GetBookingByIdAsync(id, userId);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(Guid bookingId, BookingViewModel bookingVm)
        {
            Guid userId = GetUserId();

            await bookingService.CancelBookingAsync(userId, bookingId);

            return RedirectToAction(nameof(MyBookings));
        }
    }
}
