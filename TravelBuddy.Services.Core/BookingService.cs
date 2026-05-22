using Microsoft.EntityFrameworkCore;
using TravelBuddy.Data;
using TravelBuddy.Data.Models;
using TravelBuddy.Data.Models.Enums;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels;

namespace TravelBuddy.Services.Core
{
    public class BookingService : IBookingService
    {
        private readonly TravelBuddyDbContext dbContext;

        public BookingService(TravelBuddyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<BookingViewModel>> GetUserBookingsAsync(Guid userId)
        {
            
            IEnumerable<BookingViewModel> bookings = await dbContext.Bookings
                .Where(b => b.UserId == userId)
                .Select(b => new BookingViewModel()
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    ExcursionId = b.ExcursionId,
                    ExcursionTitle = b.Excursion.Title,
                    ExcursionDestination = b.Excursion.Destination,
                    ExcursionStartDate = b.Excursion.StartDate,
                    ExcursionEndDate = b.Excursion.EndDate,
                    ExcursionPrice = b.Excursion.Price,
                    ExcursionImageUrl = b.Excursion.ImageUrl,
                    BookedOn = b.BookedOn,
                    Status = (int)b.Status
                })
                .ToListAsync();


            return bookings;
        }

        public async Task<BookingViewModel> CreateBookingAsync(Guid userId, Guid excursionId)
        {
            Excursion excursion = await dbContext.Excursions
                .FirstAsync(e => e.Id == excursionId);

            Booking booking = new Booking()
            {
                UserId = userId,
                ExcursionId = excursionId,
                BookedOn = DateTime.Now,
                Status = Status.Confirmed
            };

            dbContext.Bookings.Add(booking);

            await dbContext.SaveChangesAsync();

            BookingViewModel bookingVm  = new BookingViewModel()
            {
                UserId = booking.UserId,
                ExcursionId = booking.ExcursionId,
                ExcursionTitle = excursion.Title,
                ExcursionDestination = excursion.Destination,
                ExcursionStartDate = excursion.StartDate,
                ExcursionEndDate = excursion.EndDate,
                ExcursionPrice = excursion.Price,
                ExcursionImageUrl = excursion.ImageUrl,
                BookedOn = booking.BookedOn,
                Status = (int)booking.Status
            };

            return bookingVm;
        }

    }
}
