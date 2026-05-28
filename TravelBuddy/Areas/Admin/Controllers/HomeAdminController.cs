using Microsoft.AspNetCore.Mvc;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels;

namespace TravelBuddy.Areas.Admin.Controllers
{
    public class HomeAdminController : BaseAdminController
    {

        // Action method for the admin dashboard index page.
        public IActionResult Index()
        {
            return View();
        }

    }
}
