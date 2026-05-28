using Microsoft.EntityFrameworkCore;
using TravelBuddy.Data;
using TravelBuddy.Data.Models;
using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels.Excursion;

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
                                                                           Capacity = e.Capacity,
                                                                           ImageUrl = e.ImageUrl ?? string.Empty,
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
                Capacity = excursion.Capacity,
                ImageUrl = excursion.ImageUrl ?? string.Empty,
            };

            // Return the ExcursionViewModel instance with the excursion's details.
            return excursionViewModel;
        }

        // Task for adding a new excursion to the database based on the provided ExcursionInputModel.
        public async Task<bool> AddExcursionAsync(ExcursionInputModel? excursionIm)
        {
            // Check if the provided ExcursionInputModel is null. If it is null, return false to indicate that the excursion cannot be added.
            if (excursionIm == null)
            {
                return false;
            }

            try
            {
                // Create a new Excursion entity based on the provided ExcursionInputModel.
                Excursion excursion = new Excursion
                {
                    Id = Guid.NewGuid(),
                    Title = excursionIm.Title,
                    Destination = excursionIm.Destination,
                    StartDate = excursionIm.StartDate,
                    EndDate = excursionIm.EndDate,
                    Price = excursionIm.Price,
                    Capacity = excursionIm.Capacity,
                    ImageUrl = excursionIm.ImageUrl ?? string.Empty,
                };

                // Add the new Excursion entity to the database context and save changes to persist it in the database.
                await dbContext.Excursions.AddAsync(excursion);

                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                // If an exception occurs during the process of adding the excursion, return false to indicate that the excursion could not be added.
                return false;
            }
        }

        // Task for retrieving a specific excursion by its Id for editing purposes, returning an ExcursionInputModel with the excursion's details.
        public async Task<ExcursionInputModel> GetExcursionForEditByIdAsync(Guid excursionId)
        {
            Excursion? excursion = await dbContext
                                        .Excursions
                                        .AsNoTracking()
                                        .SingleOrDefaultAsync(e => e.Id == excursionId);

            if (excursion == null)
            {
                return null;
            }

            ExcursionInputModel excursionInputModel = new ExcursionInputModel
            {
                Id = excursionId,
                Title = excursion.Title,
                Destination = excursion.Destination,
                StartDate = excursion.StartDate,
                EndDate = excursion.EndDate,
                Price = excursion.Price,
                Capacity = excursion.Capacity,
                ImageUrl = excursion.ImageUrl ?? string.Empty,
            };

            return excursionInputModel;
        }

        // Task for editing an existing excursion in the database based on the provided ExcursionInputModel and excursion Id.
        public async Task<bool> EditExcursionAsync(Guid excursionId, ExcursionInputModel? excursionIm)
        {
            // Fetch the existing Excursion entity from the database using the provided excursion Id.
            Excursion? excursion = await dbContext
                                        .Excursions
                                        .SingleOrDefaultAsync(e => e.Id == excursionId);

            // Check if the fetched Excursion entity or the provided ExcursionInputModel is null. If either is null, return false to indicate that the excursion cannot be edited.
            if (excursion == null || excursionIm == null)
            {
                return false;
            }

            // Update the properties of the fetched Excursion entity with the values from the provided ExcursionInputModel.
            try
            {
                excursion.Title = excursionIm.Title;
                excursion.Destination = excursionIm.Destination;
                excursion.StartDate = excursionIm.StartDate;
                excursion.EndDate = excursionIm.EndDate;
                excursion.Price = excursionIm.Price;
                excursion.Capacity = excursionIm.Capacity;
                excursion.ImageUrl = excursionIm.ImageUrl;

                await dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                // If an exception occurs during the process of editing the excursion, return false to indicate that the excursion could not be edited.
                return false;
            }
        }

        // Task for retrieving a specific excursion by its Id for deletion purposes.
        public async Task<DeleteExcursionViewModel?> GetExcursionForDeleteionAsync(Guid excursionId)
        {
            // Fetch the Excursion entity from the database using the provided excursion Id.
            Excursion? excursion = await dbContext
                                        .Excursions
                                        .AsNoTracking()
                                        .SingleOrDefaultAsync(e => e.Id == excursionId);

            // Check if the fetched Excursion entity is null. If it is null, return null to indicate that the excursion cannot be retrieved for deletion.
            if (excursion == null)
            {
                return null;
            }

            // Create a DeleteExcursionViewModel instance with the Id and Title of the fetched Excursion entity.
            DeleteExcursionViewModel deleteExcursionViewModel = new DeleteExcursionViewModel
            {
                Id = excursionId,
                Title = excursion.Title,
                Destination = excursion.Destination,
            };

            // Return the DeleteExcursionViewModel instance with the Id and Title of the excursion to be deleted.
            return deleteExcursionViewModel;
        }

        public async Task<bool> DeleteExcursionAsync(Guid excursionId)
        {
            // Fetch the Excursion entity from the database using the provided excursion Id.
            Excursion? excursion = await dbContext
                                        .Excursions
                                        .SingleOrDefaultAsync(e => e.Id == excursionId);
            // Check if the fetched Excursion entity is null. If it is null, return false to indicate that the excursion cannot be deleted.
            if (excursion == null)
            {
                return false;
            }

            try
            {
                // Remove the fetched Excursion entity from the database.
                dbContext.Excursions.Remove(excursion);

                // Save the changes to the database.
                await dbContext.SaveChangesAsync();

                // Return true to indicate that the excursion was successfully deleted.
                return true;
            }
            catch (Exception ex)
            {
                // If an exception occurs during the process of deleting the excursion, return false.
                return false;
            }
        }
    }
}
