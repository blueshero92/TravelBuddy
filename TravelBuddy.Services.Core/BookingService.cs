using Microsoft.EntityFrameworkCore;
using TravelBuddy.Data;
using TravelBuddy.Data.Models;
using TravelBuddy.Data.Models.Enums;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels.Booking;
using TravelBuddy.ViewModels.BookingCancellationRequest;
using static TravelBuddy.GCommon.OutputMessages;

namespace TravelBuddy.Services.Core
{
    public class BookingService : IBookingService
    {
        // Dependency injection of the TravelBuddyDbContext to interact with the database.
        private readonly TravelBuddyDbContext dbContext;

        // Constructor to initialize the BookingService with the provided TravelBuddyDbContext.
        public BookingService(TravelBuddyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Get all bookings for a specific user, including excursion details.
        public async Task<IEnumerable<BookingViewModel>> GetUserBookingsAsync(Guid userId)
        {
            // Fetch bookings for the user and project them to BookingViewModel.
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

            //Return the collection of bookings for the user.
            return bookings;
        }

        // Action for booking confimation.
        public async Task<BookingViewModel> CreateBookingGetAsync(Guid userId, Guid excursionId)
        {
            //Fetch the excursion that the user wants to book.
            Excursion? excursion = await dbContext
                                        .Excursions
                                        .AsNoTracking()
                                        .SingleOrDefaultAsync(e => e.Id == excursionId);

            //Create a BookingViewModel to return the details of the excursion for booking confirmation.
            BookingViewModel bookingVm = new BookingViewModel()
            {
                UserId = userId,
                ExcursionId = excursionId,
                ExcursionTitle = excursion!.Title,
                ExcursionDestination = excursion.Destination,
                ExcursionStartDate = excursion.StartDate,
                ExcursionEndDate = excursion.EndDate,
                ExcursionPrice = excursion.Price,
                ExcursionImageUrl = excursion.ImageUrl
            };

            return bookingVm;
        }

        // Create a new booking for a user when button is clicked and return the details of the created booking.
        public async Task<BookingViewModel> CreateBookingPostAsync(Guid userId, Guid excursionId)
        {
            //Fetch the excursion that the user wants to book.
            Excursion? excursion = await dbContext
                                        .Excursions
                                        .SingleOrDefaultAsync(e => e.Id == excursionId);

            //Check if the excursion exists. If not, return null to indicate that the booking cannot be created.
            if (excursion == null)
            {
                return null;
            }

            // If the excursion is fully booked, prevent booking creation.
            if (excursion.Capacity <= 0)
            {
                return null;
            }

            //Create a new booking entity with the provided userId and excursionId, and set the booking date and status.
            Booking booking = new Booking()
            {
                UserId = userId,
                ExcursionId = excursionId,
                BookedOn = DateTime.Now,
                Status = Status.Confirmed
            };

            excursion.Capacity -= 1;

            //Add the new booking to the database context and save changes to persist it in the database.
            dbContext.Bookings.Add(booking);

            await dbContext.SaveChangesAsync();


            //Create a BookingViewModel to return the details of the created booking..
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


            //Return the details of the created booking.
            return bookingVm;
        }

        // Get a specific booking by its ID for a user.
        public async Task<BookingViewModel?> GetBookingByIdAsync(Guid bookingId, Guid userId)
        {
            //Fetch the booking with the specified bookingId and userId, including excursion details, and project it to BookingViewModel.
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

            //Check if the booking was found. If not, return null.
            if (bookingVm == null)
            {
                return null;
            }

            //Return the booking details if found.
            return bookingVm;
        }

        // Cancel a booking by creating a cancellation request and updating the booking status to pending.
        public async Task<bool> CancelBookingAsync(Guid userId, Guid bookingId)
        {
            //Fetch the booking that the user wants to cancel to ensure it exists and belongs to the user.
            Booking? booking = await dbContext
                                    .Bookings
                                    .SingleOrDefaultAsync(b => b.Id == bookingId && b.UserId == userId);

            if (booking == null)
            {
                return false;
            }

            // If a declined or approved request already exists, forbid re-submission.
            bool declinedRequestExists = await dbContext
                                              .BookingCancellationRequests
                                              .AnyAsync(r => r.BookingId == bookingId && r.Status == CancellationRequestStatus.Declined);

            // If a declined cancellation request already exists for this booking, return false to prevent re-submission.
            if (declinedRequestExists)
            {
                return false;
            }

            // Check if an approved cancellation request already exists for this booking.
            bool approvedRequestExists = await dbContext
                                              .BookingCancellationRequests
                                              .AnyAsync(r => r.BookingId == bookingId && r.Status == CancellationRequestStatus.Approved);

            // If an approved cancellation request already exists for this booking, return false to prevent re-submission.
            if (approvedRequestExists)
            {
                return false;
            }

            // Check if a pending cancellation request already exists for this booking.
            bool pendingRequestExists = await dbContext
                                      .BookingCancellationRequests
                                      .AnyAsync(r => r.BookingId == bookingId && r.Status == CancellationRequestStatus.Pending);
            try
            {
                // If no pending cancellation request exists, create a new one with the status set to pending.
                if (!pendingRequestExists)
                {
                    BookingCancellationRequest cancellationRequest = new BookingCancellationRequest()
                    {
                        UserId = userId,
                        BookingId = bookingId,
                        RequestedOn = DateTime.Now,
                        Status = CancellationRequestStatus.Pending
                    };

                    dbContext.BookingCancellationRequests.Add(cancellationRequest);
                }

                booking.Status = Status.Pending;

                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here for brevity).
                return false;
            }
        }


        // Get all cancellation requests for a specific user.
        public async Task<IEnumerable<BookingCancellationRequestViewModel>> GetUserCancellationRequestsAsync(Guid userId)
        {
            //Fetch all cancellation requests for the specified userId and project them to BookingCancellationRequestViewModel.
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

            //Return the collection of cancellation requests for the user.
            return cancellationRequests;

        }

        // Get all cancellation requests for admin user to review and manage.
        public async Task<IEnumerable<BookingCancellationRequestViewModel>> GetAllCancellationRequestsAsync()
        {
            //Fetch all cancellation requests and project them to a collection of BookingCancellationRequestViewModel.
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

            //Return the collection of all cancellation requests for admin review and management.
            return cancellationRequests;
        }


        // Approve a cancellation request: mark the booking as Cancelled (kept as history) and notify the user.
        public async Task<bool> ApproveCancellationAsync(Guid bookingId)
        {
            //Fetch the cancellation request for the specified bookingId, including the related booking and excursion details.
            BookingCancellationRequest? request = await dbContext
                                                       .BookingCancellationRequests
                                                       .Include(r => r.Booking)
                                                       .ThenInclude(b => b.Excursion)
                                                       .SingleOrDefaultAsync(r => r.BookingId == bookingId);

            //Check if the cancellation request and the related booking exist. If not, return false.
            if (request == null || request.Booking == null)
            {
                return false;
            }

            // Update the cancellation request status to Approved, set the review date, and update the booking status to Cancelled.
            request.Status = CancellationRequestStatus.Approved;
            request.ReviewedOn = DateTime.Now;
            request.Booking.Status = Status.Cancelled;

            // Prepare the notification message for the user about the approved cancellation.
            string excursionTitle = request.Booking.Excursion.Title;

            // Increment the excursion capacity to reflect the cancellation.
            request.Booking.Excursion.Capacity += 1;

            Guid userId = request.UserId;

            // Create a new notification for the user about the approved cancellation and add it to the database context.
            dbContext.Notifications.Add(new Notification
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Message = string.Format(CancellationApprovedNotification, excursionTitle),
                SentOn = DateTime.Now,
                IsRead = false
            });

            // Save changes to the database to persist the updated cancellation request, booking status, and new notification.
            await dbContext.SaveChangesAsync();

            return true;
        }

        // Decline a cancellation request: restore booking to Confirmed and notify the user.
        public async Task<bool> DeclineCancellationAsync(Guid bookingId)
        {
            //Fetch the cancellation request for the specified bookingId, including the related booking and excursion details.
            BookingCancellationRequest? request = await dbContext
                                                       .BookingCancellationRequests
                                                       .Include(r => r.Booking)
                                                       .ThenInclude(b => b.Excursion)
                                                       .SingleOrDefaultAsync(r => r.BookingId == bookingId);

            //Check if the cancellation request and the related booking exist. If not, return false.
            if (request == null || request.Booking == null)
            {
                return false;
            }

            // Update the cancellation request status to Declined, set the review date, and restore the booking status to Confirmed.
            request.Status = CancellationRequestStatus.Declined;
            request.ReviewedOn = DateTime.Now;
            request.Booking.Status = Status.Confirmed;

            // Prepare the notification message for the user about the declined cancellation.
            string excursionTitle = request.Booking.Excursion.Title;

            Guid userId = request.UserId;

            // Create a new notification for the user about the declined cancellation and add it to the database context.
            dbContext.Notifications.Add(new Notification
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Message = string.Format(CancellationDeclinedNotification, excursionTitle),
                SentOn = DateTime.Now,
                IsRead = false
            });
            // Save changes to the database to persist the updated cancellation request, booking status, and new notification.
            await dbContext.SaveChangesAsync();

            return true;
        }

        // Check if a declined cancellation request already exists for a specific booking.
        public async Task<bool> HasDeclinedCancellationAsync(Guid bookingId)
        {
            return await dbContext.BookingCancellationRequests
                .AnyAsync(r => r.BookingId == bookingId && r.Status == CancellationRequestStatus.Declined);
        }

        // Check if an approved cancellation request already exists for a specific booking.
        public async Task<bool> HasApprovedCancellationAsync(Guid bookingId)
        {
            return await dbContext.BookingCancellationRequests
                .AnyAsync(r => r.BookingId == bookingId && r.Status == CancellationRequestStatus.Approved);
        }

        // Check if a pending cancellation request already exists for a specific booking.
        public async Task<bool> HasPendingCancellationAsync(Guid bookingId)
        {
            return await dbContext.BookingCancellationRequests
                .AnyAsync(r => r.BookingId == bookingId && r.Status == CancellationRequestStatus.Pending);
        }

    }
}
