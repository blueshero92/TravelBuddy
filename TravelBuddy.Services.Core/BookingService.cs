using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

            IEnumerable<BookingViewModel> bookings = await dbContext
                                                          .Bookings
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
                                                          .AsNoTracking()
                                                          .ToListAsync();


            return bookings;
        }

        public async Task<BookingViewModel> CreateBookingAsync(Guid userId, Guid excursionId)
        {
            Excursion? excursion = await dbContext
                                        .Excursions
                                        .AsNoTracking()
                                        .SingleOrDefaultAsync(e => e.Id == excursionId);

            Booking booking = new Booking()
            {
                UserId = userId,
                ExcursionId = excursionId,
                BookedOn = DateTime.Now,
                Status = Status.Confirmed
            };

            dbContext.Bookings.Add(booking);

            await dbContext.SaveChangesAsync();

            BookingViewModel bookingVm = new BookingViewModel()
            {
                UserId = booking.UserId,
                ExcursionId = booking.ExcursionId,
                ExcursionTitle = excursion!.Title,
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

        public async Task<BookingViewModel?> GetBookingByIdAsync(Guid bookingId, Guid userId)
        {
            BookingViewModel? bookingVm = await dbContext
                                               .Bookings
                                               .Where(b => b.Id == bookingId && b.UserId == userId)
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
                                               .AsNoTracking()
                                               .FirstOrDefaultAsync();

            return bookingVm;
        }

        public async Task<bool> CancelBookingAsync(Guid userId, Guid bookingId)
        {
            Booking? booking = await dbContext
                                    .Bookings
                                    .SingleOrDefaultAsync(b => b.Id == bookingId && b.UserId == userId);


            try
            {
                BookingCancellationRequest cancellationRequest = new BookingCancellationRequest()
                {
                    UserId = userId,
                    BookingId = bookingId,
                    RequestedOn = DateTime.Now,
                    Status = CancellationRequestStatus.Pending
                };

                dbContext.BookingCancellationRequests.Add(cancellationRequest);

                booking?.Status = Status.Pending;

                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<IEnumerable<BookingCancellationRequestViewModel>> GetUserCancellationRequestsAsync(Guid userId)
        {
            IEnumerable<BookingCancellationRequestViewModel> cancellationRequests
                = await dbContext
                       .BookingCancellationRequests
                       .Where(r => r.UserId == userId)
                       .Select(r => new BookingCancellationRequestViewModel()
                       {
                           UserId = r.UserId,
                           BookingId = r.BookingId,
                           ExcursionId = r.Booking.ExcursionId,
                           ExcursionTitle = r.Booking.Excursion.Title,
                           ExcursionDestination = r.Booking.Excursion.Destination,
                           ExcursionStartDate = r.Booking.Excursion.StartDate,
                           ExcursionEndDate = r.Booking.Excursion.EndDate,
                           ExcursionPrice = r.Booking.Excursion.Price,
                           ExcursionImageUrl = r.Booking.Excursion.ImageUrl,
                           RequestedOn = r.RequestedOn,
                           Reason = r.Reason,
                           CancellationStatus = (int)r.Status
                       })
                       .AsNoTracking()
                       .ToListAsync();

            return cancellationRequests;

        }

        public async Task<IEnumerable<BookingCancellationRequestViewModel>> GetAllCancellationRequestsAsync()
        {
            IEnumerable<BookingCancellationRequestViewModel> cancellationRequests
                = await dbContext
                       .BookingCancellationRequests
                       .Select(r => new BookingCancellationRequestViewModel()
                       {
                           UserId = r.UserId,
                           BookingId = r.BookingId,
                           ExcursionId = r.Booking.ExcursionId,
                           ExcursionTitle = r.Booking.Excursion.Title,
                           ExcursionDestination = r.Booking.Excursion.Destination,
                           ExcursionStartDate = r.Booking.Excursion.StartDate,
                           ExcursionEndDate = r.Booking.Excursion.EndDate,
                           ExcursionPrice = r.Booking.Excursion.Price,
                           ExcursionImageUrl = r.Booking.Excursion.ImageUrl,
                           RequestedOn = r.RequestedOn,
                           Reason = r.Reason,
                           CancellationStatus = (int)r.Status
                       })
                       .AsNoTracking()
                       .ToListAsync();

            return cancellationRequests;
        }
    }
}
