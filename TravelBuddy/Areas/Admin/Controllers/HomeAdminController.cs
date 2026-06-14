using Microsoft.AspNetCore.Mvc;

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
