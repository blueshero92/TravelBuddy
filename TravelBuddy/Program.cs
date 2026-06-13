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
            // In production (Railway), DATABASE_URL is injected automatically by the PostgreSQL service.
            // Locally, TravelBuddyPostGreDbConnection is used from appsettings or user secrets.
            var connectionString = builder.Configuration.GetConnectionString("TravelBuddyPostGreDbConnection")
                ?? builder.Configuration["DATABASE_URL"]
                ?? throw new InvalidOperationException("No database connection string found. Set 'TravelBuddyPostGreDbConnection' or 'DATABASE_URL'.");

            builder.Services.AddDbContext<TravelBuddyDbContext>(options =>
                options.UseNpgsql(connectionString));

            // Add database exception filter for development environment to provide detailed error information.
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            }

            // Register application services.
            builder.Services.AddScoped<IExcursionService, ExcursionService>();
            builder.Services.AddScoped<IBookingService, BookingService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();

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
            }

            // HTTPS redirection is handled by Railway's edge proxy in production.
            if (app.Environment.IsDevelopment())
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Apply any pending migrations and seed roles and admin user on startup.
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TravelBuddyDbContext>();
                db.Database.Migrate();

                var seeder = scope.ServiceProvider.GetRequiredService<IIdentitySeeder>();
                seeder.SeedRolesAsync().GetAwaiter().GetResult();
                seeder.SeedAdminUserAsync().GetAwaiter().GetResult();
            }

            app.MapStaticAssets();

            app.MapControllerRoute(
                name: "adminArea",
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
