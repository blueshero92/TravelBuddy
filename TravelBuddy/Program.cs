using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TravelBuddy.Data;
using TravelBuddy.Data.Models;
using TravelBuddy.Data.Seeding;
using TravelBuddy.Data.Seeding.Contracts;
using TravelBuddy.Services.Core;
using TravelBuddy.Services.Core.Contracts;

namespace TravelBuddy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("TravelBuddyDbConnection") 
                ?? throw new InvalidOperationException("Connection string 'TravelBuddyDbConnection' not found.");

            builder.Services.AddDbContext<TravelBuddyDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Register application services.
            builder.Services.AddScoped<IExcursionService, ExcursionService>();
            builder.Services.AddScoped<IBookingService, BookingService>();

            // Register the identity seeder for seeding roles and admin user.
            builder.Services.AddTransient<IIdentitySeeder, IdentitySeeder>();

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<TravelBuddyDbContext>();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }



            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Seed roles and admin user on startup.
            using (var scope = app.Services.CreateScope())
            {
                var seeder = scope.ServiceProvider.GetRequiredService<IIdentitySeeder>();
                seeder.SeedRolesAsync().GetAwaiter().GetResult();
                seeder.SeedAdminUserAsync().GetAwaiter().GetResult();
            }

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=HomeAdmin}/{action=Index}/{id?}")
               .WithStaticAssets();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.MapRazorPages()
               .WithStaticAssets();

            app.Run();
        }
    }
}
