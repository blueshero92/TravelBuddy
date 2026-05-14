using Microsoft.AspNetCore.Identity;

namespace TravelBuddy.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }
    }
}
