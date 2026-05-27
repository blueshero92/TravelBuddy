using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels;

namespace TravelBuddy.Areas.Admin.Controllers
{
    public class HomeAdminController : BaseAdminController
    {
        private readonly IExcursionService excursionService;
        private readonly IBookingService bookingService;

        public HomeAdminController(IExcursionService excursionService, IBookingService bookingService)
        {
            this.excursionService = excursionService;
            this.bookingService = bookingService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageExcursions()
        {
            IEnumerable<ExcursionViewModel> excursionsVm = await excursionService.GetAllExcursionsAsync();

            return View(excursionsVm);
        }

        public async Task<IActionResult> ManageCancellations()
        {
            IEnumerable<BookingCancellationRequestViewModel> cancellationRequestsVm = await bookingService.GetAllCancellationRequestsAsync();

            if (cancellationRequestsVm == null)
            {
                return NotFound();
            }

            return View(cancellationRequestsVm);
        }
    }
}
