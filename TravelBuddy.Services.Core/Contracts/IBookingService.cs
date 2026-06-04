using TravelBuddy.ViewModels.Booking;
using TravelBuddy.ViewModels.BookingCancellationRequest;

namespace TravelBuddy.Services.Core.Contracts
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingViewModel>> GetUserBookingsAsync(Guid userId);
        Task<BookingViewModel> CreateBookingGetAsync(Guid userId, Guid excursionId);
        Task<BookingViewModel> CreateBookingPostAsync(Guid userId, Guid excursionId);
        Task<BookingViewModel?> GetBookingByIdAsync(Guid bookingId, Guid userId);
        Task<bool> CancelBookingAsync(Guid userId, Guid bookingId);
        Task<IEnumerable<BookingCancellationRequestViewModel>> GetUserCancellationRequestsAsync(Guid userId);
        Task<IEnumerable<BookingCancellationRequestViewModel>> GetAllCancellationRequestsAsync();
        Task<bool> ApproveCancellationAsync(Guid bookingId);
        Task<bool> DeclineCancellationAsync(Guid bookingId);
        Task<bool> HasDeclinedCancellationAsync(Guid bookingId);
        Task<bool> HasApprovedCancellationAsync(Guid bookingId);
        Task<bool> HasPendingCancellationAsync(Guid bookingId);

    }
}
