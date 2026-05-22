using TravelBuddy.ViewModels;

namespace TravelBuddy.Services.Core.Contracts
{
    public interface IBookingService
    {
        Task<IEnumerable<BookingViewModel>> GetUserBookingsAsync(Guid userId);
        Task<BookingViewModel> CreateBookingAsync(Guid userId, Guid excursionId);
    }
}
