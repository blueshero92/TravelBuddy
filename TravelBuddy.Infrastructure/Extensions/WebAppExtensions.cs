using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TravelBuddy.Data.Seeding.Contracts;

namespace TravelBuddy.Infrastructure.Extensions
{
    public static class WebAppExtensions
    {
        //Use this extension method in Program.cs to seed the roles when the application starts.
        public static IApplicationBuilder UseRolesSeeder(this IApplicationBuilder applicationBuilder)
        {
            // Create a new scope to resolve the IIdentitySeeder service.
            using IServiceScope serviceScope = applicationBuilder.ApplicationServices.CreateScope();

            // Resolve the IIdentitySeeder service from the application's service provider.
            IIdentitySeeder identitiySeeder
                = serviceScope.ServiceProvider.GetRequiredService<IIdentitySeeder>();

            // Call the SeedRolesAsync method to seed the roles. Since this is an asynchronous method, we use GetAwaiter().GetResult() to wait for it to complete.
            identitiySeeder.SeedRolesAsync()
                           .GetAwaiter()
                           .GetResult();

            return applicationBuilder;
        }

        public static IApplicationBuilder UseAdminUserSeeder(this IApplicationBuilder applicationBuilder)
        {
            // Create a new scope to resolve the IIdentitySeeder service.
            using IServiceScope serviceScope
                = applicationBuilder.ApplicationServices.CreateScope();

            // Resolve the IIdentitySeeder service from the application's service provider.
            IIdentitySeeder identitiySeeder
                = serviceScope.ServiceProvider.GetRequiredService<IIdentitySeeder>();

            // Call the SeedAdminUserAsync method to seed the admin user. Since this is an asynchronous method, we use GetAwaiter().GetResult() to wait for it to complete.
            identitiySeeder.SeedAdminUserAsync()
                           .GetAwaiter()
                           .GetResult();

            return applicationBuilder;
        }
    }
}
