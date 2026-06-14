namespace TravelBuddy.Data.Seeding.Contracts
{
    public interface IIdentitySeeder
    {
        Task SeedRolesAsync();
        Task SeedAdminUserAsync();

        Task SeedDemoAdminUserAsync();
    }
}
