using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using TravelBuddy.Data.Models;
using TravelBuddy.Data.Seeding.Contracts;
using static TravelBuddy.GCommon.OutputMessages;

namespace TravelBuddy.Data.Seeding
{
    public class IdentitySeeder : IIdentitySeeder
    {
        //Roles for seeding.
        private readonly string[] AppRoles = new string[]
        {
            "Admin",
            "DemoAdmin"
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
                        throw new InvalidOperationException(string.Format(SeedRoleError, role));
                    }
                }
            }
        }

        public async Task SeedAdminUserAsync()
        {
            string adminUsername = configuration["UserSeed:AdminUser:Username"]
                ?? throw new InvalidOperationException(AdminUsernameNotFound);

            string adminEmail = configuration["UserSeed:AdminUser:Email"]
                ?? throw new InvalidOperationException(AdminEmailNotFound);

            string adminPassword = configuration["UserSeed:AdminUser:Password"]
                ?? throw new InvalidOperationException(AdminPasswordNotFound);

            string adminFullName = configuration["UserSeed:AdminUser:FullName"]
                ?? throw new InvalidOperationException(AdminFullNameNotFound);

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
                    throw new InvalidOperationException(SeedAdminUserError);
                }
            }

            bool isInRole = await userManager.IsInRoleAsync(adminUser, AppRoles[0]);

            if (!isInRole)
            {
                IdentityResult addToRoleResult
                    = await userManager.AddToRoleAsync(adminUser, AppRoles[0]);

                if (!addToRoleResult.Succeeded)
                {
                    throw new InvalidOperationException(SeedAdminRoleError);
                }
            }
        }

        public async Task SeedDemoAdminUserAsync()
        {
            string demoAdminUsername = configuration["UserSeed:DemoAdminUser:Username"]
                ?? throw new InvalidOperationException(DemoAdminUsernameNotFound);

            string demoAdminEmail = configuration["UserSeed:DemoAdminUser:Email"]
                ?? throw new InvalidOperationException(DemoAdminEmailNotFound);

            string demoAdminPassword = configuration["UserSeed:DemoAdminUser:Password"]
                ?? throw new InvalidOperationException(DemoAdminPasswordNotFound);

            string demoAdminFullName = configuration["UserSeed:DemoAdminUser:FullName"]
                ?? throw new InvalidOperationException(DemoAdminFullNameNotFound);

            ApplicationUser? demoAdminUser = await userManager.FindByEmailAsync(demoAdminEmail);

            if (demoAdminUser == null)
            {
                demoAdminUser = new ApplicationUser
                {
                    UserName = demoAdminUsername,
                    Email = demoAdminEmail,
                    FullName = demoAdminFullName
                };

                IdentityResult result
                    = await userManager.CreateAsync(demoAdminUser, demoAdminPassword);

                if (!result.Succeeded)
                {
                    throw new InvalidOperationException(SeedDemoAdminUserError);
                }
            }

            bool isInRole = await userManager.IsInRoleAsync(demoAdminUser, AppRoles[1]);

            if (!isInRole)
            {
                IdentityResult addToRoleResult
                    = await userManager.AddToRoleAsync(demoAdminUser, AppRoles[1]);

                if (!addToRoleResult.Succeeded)
                {
                    throw new InvalidOperationException(SeedDemoAdminRoleError);
                }
            }
        }
    }
}
