using Microsoft.EntityFrameworkCore;
using TravelBuddy.Data;
using TravelBuddy.Data.Models;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels;

namespace TravelBuddy.Services.Core
{
    public class ExcursionService : IExcursionService
    {
        //Dependency Injection of the DbContext to access the database and perform operations related to excursions.
        private readonly TravelBuddyDbContext dbContext;

        // Constructor to initialize the ExcursionService with the provided TravelBuddyDbContext.
        public ExcursionService(TravelBuddyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Task for retrieving all excursions from the database.
        public async Task<IEnumerable<ExcursionViewModel>> GetAllExcursionsAsync()
        {
            // Query the database for all excursions, order them by start date, and project them into ExcursionViewModel instances.
            IEnumerable<ExcursionViewModel> excursionsViewModel = await dbContext
                                                                       .Excursions
                                                                       .OrderBy(e => e.StartDate)
                                                                       .Select(e => new ExcursionViewModel
                                                                       {
                                                                           Id = e.Id,
                                                                           Title = e.Title,
                                                                           Destination = e.Destination,
                                                                           StartDate = e.StartDate,
                                                                           EndDate = e.EndDate,
                                                                           Price = e.Price,
                                                                           Capacity = e.Capacity
                                                                       })
                                                                       .AsNoTracking()
                                                                       .ToListAsync();

            // Return the list of ExcursionViewModel instances.
            return excursionsViewModel;
        }

        // Task for retrieving a specific excursion by its Id for the excursion details page.
        public async Task<ExcursionViewModel> GetExcursionByIdAsync(Guid id)
        {
            // Query the database for a single excursion that matches the provided Id.
            Excursion? excursion = await dbContext
                                        .Excursions
                                        .AsNoTracking()
                                        .SingleOrDefaultAsync(e => e.Id == id);

            // If no excursion is found with the provided Id, return null.
            if (excursion == null)
            {
                return null;
            }

            // If an excursion is found, create an ExcursionViewModel instance with the excursion's details.
            ExcursionViewModel excursionViewModel = new ExcursionViewModel
            {
                Id = excursion.Id,
                Title = excursion.Title,
                Destination = excursion.Destination,
                StartDate = excursion.StartDate,
                EndDate = excursion.EndDate,
                Price = excursion.Price,
                Capacity = excursion.Capacity
            };

            // Return the ExcursionViewModel instance with the excursion's details.
            return excursionViewModel;
        }
    }
}
