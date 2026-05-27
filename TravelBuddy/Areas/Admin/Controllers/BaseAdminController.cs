using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TravelBuddy.Areas.Admin.Controllers
{
    // Base controller for all admin-related controllers, providing security and enforcing authorization for admin users.
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    [AutoValidateAntiforgeryToken]
    public class BaseAdminController : Controller
    {
        
    }
}
