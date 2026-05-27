using TravelBuddy.ViewModels;

namespace TravelBuddy.Services.Core.Contracts
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingViewModel>> GetUserBookingsAsync(Guid userId);
        Task<BookingViewModel> CreateBookingAsync(Guid userId, Guid excursionId);
        Task<BookingViewModel?> GetBookingByIdAsync(Guid bookingId, Guid userId);
        Task CancelBookingAsync(Guid userId, Guid bookingId);

        Task<IEnumerable<BookingCancellationRequestViewModel>> GetUserCancellationRequestsAsync(Guid userId);
        Task<IEnumerable<BookingCancellationRequestViewModel>> GetAllCancellationRequestsAsync();

    }
}
