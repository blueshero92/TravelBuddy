using Microsoft.AspNetCore.Mvc;

namespace TravelBuddy.Areas.Admin.Controllers
{
    public class HomeAdminController : BaseAdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
