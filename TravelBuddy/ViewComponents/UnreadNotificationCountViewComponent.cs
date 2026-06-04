using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelBuddy.Services.Core.Contracts;

namespace TravelBuddy.ViewComponents
{
    public class UnreadNotificationCountViewComponent : ViewComponent
    {
        private readonly INotificationService notificationService;

        public UnreadNotificationCountViewComponent(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            string? idValue = UserClaimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(idValue) || !Guid.TryParse(idValue, out Guid userId))
            {
                return View(0);
            }

            int count = await notificationService.GetUnreadCountAsync(userId);

            return View(count);
        }
    }
}
