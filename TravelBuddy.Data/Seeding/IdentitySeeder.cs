using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using TravelBuddy.Data.Models;
using TravelBuddy.Data.Seeding.Contracts;

namespace TravelBuddy.Data.Seeding
{
    public class IdentitySeeder : IIdentitySeeder
    {
        //Roles for seeding.
        private readonly string[] AppRoles = new string[]
        {
            "Admin"
        };

        private readonly RoleManager<IdentityRole<Guid>> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        //Injecting RoleManager for managing roles in the database.
        public IdentitySeeder(RoleManager<IdentityRole<Guid>> roleManager, UserManager<ApplicationUser> userManager, IConfiguration configuration)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.configuration = configuration;
        }


        // This method seeds the roles defined in the AppRoles array into the database if they do not already exist.
        public async Task SeedRolesAsync()
        {
            foreach (string role in AppRoles)
            {
                // Check if the role already exists in the database to avoid duplicates.
                bool roleExists = await this.roleManager.RoleExistsAsync(role);

                // If the role does not exist, create a new IdentityRole and add it to the database.
                if (!roleExists)
                {
                    IdentityRole<Guid> newRole = new IdentityRole<Guid>(role);

                    IdentityResult idnetityRoleResult =
                         await roleManager.CreateAsync(newRole);

                    if (!idnetityRoleResult.Succeeded)
                    {
                        throw new InvalidOperationException(string.Format("Error while trying to seed role: {0}", role));
                    }
                }
            }
        }

        public async Task SeedAdminUserAsync()
        {
            string adminUsername = configuration["UserSeed:AdminUser:Username"]
                ?? throw new InvalidOperationException("Admin username not found in configuration.");

            string adminEmail = configuration["UserSeed:AdminUser:Email"]
                ?? throw new InvalidOperationException("Admin email not found in configuration.");

            string adminPassword = configuration["UserSeed:AdminUser:Password"]
                ?? throw new InvalidOperationException("Admin password not found in configuration.");

            string adminFullName = configuration["UserSeed:AdminUser:FullName"]
                ?? throw new InvalidOperationException("Admin full name not found in configuration.");

            ApplicationUser? adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminUsername,
                    Email = adminEmail,
                    FullName = adminFullName
                };

                IdentityResult result
                    = await userManager.CreateAsync(adminUser, adminPassword);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException("Error while trying to seed admin user.");
                }
            }

            bool isInRole = await userManager.IsInRoleAsync(adminUser, AppRoles[0]);

            if (!isInRole)
            {
                IdentityResult addToRoleResult
                    = await userManager.AddToRoleAsync(adminUser, AppRoles[0]);

                if (!addToRoleResult.Succeeded)
                {
                    throw new InvalidOperationException("Error while trying to add admin user to admin role.");
                }
            }
        }
    }
}
