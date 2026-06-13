using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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

            var connectionString =
                builder.Configuration.GetConnectionString("TravelBuddyPostGreDbConnection")
                ?? builder.Configuration["DATABASE_URL"]
                ?? builder.Configuration["ConnectionStrings__TravelBuddyPostGreDbConnection"]
                ?? Environment.GetEnvironmentVariable("DATABASE_URL")
                ?? Environment.GetEnvironmentVariable("ConnectionStrings__TravelBuddyPostGreDbConnection");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                var host = Environment.GetEnvironmentVariable("PGHOST")
                        ?? builder.Configuration["PGHOST"];
                var port = Environment.GetEnvironmentVariable("PGPORT")
                        ?? builder.Configuration["PGPORT"];
                var user = Environment.GetEnvironmentVariable("PGUSER")
                        ?? builder.Configuration["PGUSER"];
                var pass = Environment.GetEnvironmentVariable("PGPASSWORD")
                        ?? builder.Configuration["PGPASSWORD"];
                var db = Environment.GetEnvironmentVariable("PGDATABASE")
                        ?? builder.Configuration["PGDATABASE"];

                if (!string.IsNullOrWhiteSpace(host))
                {
                    connectionString =
                        $"Host={host};Port={port};Database={db};Username={user};Password={pass};SSL Mode=Require;Trust Server Certificate=true";
                }
            }

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                var allConfig = string.Join(", ", builder.Configuration.AsEnumerable().Select(c => c.Key).OrderBy(k => k));
                var allEnv = string.Join(", ", Environment.GetEnvironmentVariables().Keys.Cast<string>().OrderBy(k => k));
                throw new InvalidOperationException($"No valid PostgreSQL connection variables found. Config keys: {allConfig} | Env vars: {allEnv}");
            }

            // Add database exception filter for development environment to provide detailed error information.
            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            }

            builder.Services.AddDbContext<TravelBuddyDbContext>(options =>
                    options.UseNpgsql(connectionString));

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

            // Apply seed roles and admin user on startup.
            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<TravelBuddyDbContext>();

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
