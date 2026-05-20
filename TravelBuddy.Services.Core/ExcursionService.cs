using Microsoft.EntityFrameworkCore;
using TravelBuddy.Data;
using TravelBuddy.Data.Models;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels;

namespace TravelBuddy.Services.Core
{
    public class ExcursionService : IExcursionService
    {
        private readonly TravelBuddyDbContext dbContext;

        public ExcursionService(TravelBuddyDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<IEnumerable<ExcursionViewModel>> GetAllExcursionsAsync()
        {
            IEnumerable<Excursion> excursions = await dbContext
                                                     .Excursions                                                
                                                     .OrderBy(e => e.StartDate)
                                                     .ToListAsync();

            IEnumerable<ExcursionViewModel> excursionsViewModel = excursions
                                                                 .Select(e => new ExcursionViewModel
                                                                 {
                                                                     Id = e.Id,
                                                                     Title = e.Title,
                                                                     Destination = e.Destination,
                                                                     StartDate = e.StartDate,
                                                                     EndDate = e.EndDate,
                                                                     Price = e.Price,
                                                                     Capacity = e.Capacity
                                                                 });


            return excursionsViewModel;
        }
    }
}
