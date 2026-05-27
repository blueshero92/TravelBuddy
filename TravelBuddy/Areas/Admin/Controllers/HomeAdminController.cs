using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels;

namespace TravelBuddy.Areas.Admin.Controllers
{
    public class HomeAdminController : BaseAdminController
    {
        //Dependency injection of services for excursions and bookings.
        private readonly IExcursionService excursionService;
        private readonly IBookingService bookingService;

        // Constructor to initialize the HomeAdminController with the provided IExcursionService and IBookingService.
        public HomeAdminController(IExcursionService excursionService, IBookingService bookingService)
        {
            this.excursionService = excursionService;
            this.bookingService = bookingService;
        }

        // Action method for the admin dashboard index page.
        public IActionResult Index()
        {
            return View();
        }

        // Task for retrieving and displaying a list of all excursions for management purposes on the ManageExcursions page.
        public async Task<IActionResult> ManageExcursions()
        {
            //Retrieve all excursions using the excursion service and store them in a variable.
            IEnumerable<ExcursionViewModel> excursionsVm = await excursionService.GetAllExcursionsAsync();

            //Check if the retrieved excursions are null, and if so, return a NotFound result.
            if (excursionsVm == null)
            {
                return NotFound();
            }

            //If the excursions are successfully retrieved, return the view with the excursions view model to be displayed on the ManageExcursions page.
            return View(excursionsVm);
        }

        // Task for retrieving and displaying a list of all booking cancellation requests for management purposes on the ManageCancellations page.
        public async Task<IActionResult> ManageCancellations()
        {
            //Retrieve all booking cancellation requests using the booking service and store them in a variable.
            IEnumerable<BookingCancellationRequestViewModel> cancellationRequestsVm = await bookingService.GetAllCancellationRequestsAsync();

            //Check if the retrieved cancellation requests are null, and if so, return a NotFound result.
            if (cancellationRequestsVm == null)
            {
                return NotFound();
            }

            //If the cancellation requests are successfully retrieved, return the view with the cancellation requests.
            return View(cancellationRequestsVm);
        }
    }
}
