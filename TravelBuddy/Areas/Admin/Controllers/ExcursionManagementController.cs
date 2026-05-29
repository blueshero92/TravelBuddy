using Microsoft.AspNetCore.Mvc;

using TravelBuddy.Services.Core.Contracts;
using TravelBuddy.ViewModels.Excursion;

using static TravelBuddy.GCommon.AppConstants;
using static TravelBuddy.GCommon.OutputMessages;

namespace TravelBuddy.Areas.Admin.Controllers
{
    public class ExcursionManagementController : BaseAdminController
    {
        // Dependency injection of the excursion service to manage excursions in the admin area.
        private readonly IExcursionService excursionService;

        // Constructor to initialize the ExcursionManagementController with the provided IExcursionService.
        public ExcursionManagementController(IExcursionService excursionService)
        {
            this.excursionService = excursionService;
        }

        // Task for retrieving and displaying a list of all excursions for management purposes on the Index page.
        public async Task<IActionResult> Index()
        {
            //Retrieve all excursions using the excursion service and store them in a variable.
            IEnumerable<ExcursionViewModel> excursionsVm = await excursionService.GetAllExcursionsAsync();

            //Check if the retrieved excursions are null, and if so, return a NotFound result.
            if (excursionsVm == null)
            {
                return NotFound();
            }

            //If the excursions are successfully retrieved, return the view with the excursions view model to be displayed on the Index page.
            return View(excursionsVm);
        }

        // Task for displaying the form to add a new excursion on the AddExcursion page.
        [HttpGet]
        public async Task<IActionResult> AddExcursion()
        {
            ExcursionInputModel excursionInputModel = new ExcursionInputModel();

            return View(excursionInputModel);
        }

        // Task for handling the submission of the form to add a new excursion, validating the input, and adding the excursion using the excursion service.
        [HttpPost]
        public async Task<IActionResult> AddExcursion(ExcursionInputModel? excursionInputModel)
        {
            // Validate the input model and return the view with the model if it is not valid.
            if (!ModelState.IsValid)
            {
                return View(excursionInputModel);
            }

            // Check if the input model is null and return a BadRequest result if it is.
            if (excursionInputModel == null)
            {
                return BadRequest();
            }

            // Attempt to add the excursion using the excursion service and store the result in a variable.
            bool isAdded = await excursionService.AddExcursionAsync(excursionInputModel);

            //if adding fails add a model error to the ModelState.
            if (!isAdded)
            {
                TempData[ErrorTempDataKey] = ExcursionAddFailed;
                return View(excursionInputModel);
            }

            // If the destination is successfully added, redirect to the Index action to display the updated list of destinations.
            TempData[SuccessTempDataKey] = ExcursionAddSuccess;
            return RedirectToAction(nameof(Index));

        }

        // Task for displaying the form to edit an existing excursion, retrieving the excursion details by ID using the excursion service.
        [HttpGet]
        public async Task<IActionResult> EditExcursion(Guid excursionId)
        {
            // Retrieve the excursion details for editing using the excursion service and store them in a variable.
            ExcursionInputModel? excursionInputModel = await excursionService.GetExcursionForEditByIdAsync(excursionId);

            // Check if the retrieved excursion details are null, and if so, return a NotFound result.
            if (excursionInputModel == null)
            {
                return NotFound();
            }

            // If the excursion details are successfully retrieved, return the view with the excursion input model to be displayed on the EditExcursion page.
            return View(excursionInputModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditExcursion(Guid excursionId, ExcursionInputModel? excursionInputModel)
        {
            // Validate the input model and return the view with the model if it is not valid.
            if (!ModelState.IsValid)
            {
                return View(excursionInputModel);
            }

            // Check if the input model is null and return a BadRequest result if it is.
            if (excursionInputModel == null)
            {
                return BadRequest();
            }

            // Attempt to edit the excursion using the excursion service and store the result in a variable.
            bool isEdited = await excursionService.EditExcursionAsync(excursionId, excursionInputModel);

            // If editing fails, add a model error to the ModelState and return the view with the input model.
            if (!isEdited)
            {
                TempData[ErrorTempDataKey] = ExcursionEditFailed;
                return View(excursionInputModel);
            }

            // If the destination is successfully edited, redirect to the Index action to display the updated list of destinations.
            TempData[SuccessTempDataKey] = ExcursionEditSuccess;
            return RedirectToAction(nameof(Index));
        }

        // Task for displaying the confirmation page to delete an existing excursion, retrieving the excursion details by ID using the excursion service.
        [HttpGet]
        public async Task<IActionResult> DeleteExcursion(Guid excursionId)
        {
            // Retrieve the excursion details for deletion using the excursion service and store them in a variable.
            DeleteExcursionViewModel? deleteExcursionViewModel = await excursionService.GetExcursionForDeleteionAsync(excursionId);

            // Check if the retrieved excursion details are null, and if so, return a NotFound result.
            if (deleteExcursionViewModel == null)
            {
                return NotFound();
            }

            // If the excursion details are successfully retrieved, return the view.
            return View(deleteExcursionViewModel);
        }


        // Task for handling the confirmation of deleting an excursion, validating the input, and deleting the excursion using the excursion service.
        [HttpPost]
        public async Task<IActionResult> DeleteExcursion(Guid excursionId, DeleteExcursionViewModel? deleteExcursionViewModel)
        {
            // Check if the input model is null and return a BadRequest result if it is.
            if (deleteExcursionViewModel == null)
            {
                return BadRequest();
            }

            // Attempt to delete the excursion using the excursion service and store the result in a variable.
            bool isDeleted = await excursionService.DeleteExcursionAsync(excursionId);

            // If deletion fails, add a model error to the ModelState and return the view with the input model.
            if (!isDeleted)
            {
                TempData[ErrorTempDataKey] = ExcursionDeleteFailed;
                return View(deleteExcursionViewModel);
            }

            // If the destination is successfully deleted, redirect to the Index action to display the updated list of destinations.
            TempData[SuccessTempDataKey] = ExcursionDeleteSuccess;
            return RedirectToAction(nameof(Index));
        }
    }
}
