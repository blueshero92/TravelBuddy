namespace TravelBuddy.GCommon
{
    public static class OutputMessages
    {
        // Favorites
        public const string ExcursionAlreadyInFavorites = "The destination is already added to your favorites.";
        public const string ExcursionAddedToFavorites = "Destination added to your favourites!";
        public const string ExcursionNotInFavorites = "You can't remove a destination that is not added to your favorites.";
        public const string ExcursionRemovedFromFavorites = "Destination successfully removed from your favourites!";

        // Excursion management
        public const string ExcursionAddFailed = "Failed to add the destination. Please try again.";
        public const string ExcursionAddSuccess = "Destination added successfully.";
        public const string ExcursionEditFailed = "Failed to edit the destination. Please try again.";
        public const string ExcursionEditSuccess = "Destination edited successfully.";
        public const string ExcursionDeleteFailed = "Failed to delete the destination. Please try again.";
        public const string ExcursionDeleteSuccess = "Destination deleted successfully.";

        // Identity seeding
        public const string SeedRoleError = "Error while trying to seed role: {0}";
        public const string SeedAdminUserError = "Error while trying to seed admin user.";
        public const string SeedAdminRoleError = "Error while trying to add admin user to admin role.";
        public const string AdminUsernameNotFound = "Admin username not found in configuration.";
        public const string AdminEmailNotFound = "Admin email not found in configuration.";
        public const string AdminPasswordNotFound = "Admin password not found in configuration.";
        public const string AdminFullNameNotFound = "Admin full name not found in configuration.";

        // Booking
        public const string BookingCancelAlreadyCancelled = "Cannot cancel already cancelled booking.";
        public const string BookingCancelPending = "Booking cancellation pending. Wait for administrator approval.";
        public const string BookingCancelAlreadyPending = "Your cancellation request is already pending administrator approval.";
        public const string BookingCancelDeclined = "Your cancellation request was previously declined by an administrator and cannot be re-submitted.";
        public const string BookingCancelApproved = "Your cancellation request was already approved by an administrator.";
        public const string BookingCreateSuccess = "Booking created successfully.";
        public const string BookingCreateNoCapacity = "The destination trip you are trying to book doesn't exist or is fully booked and no spots are available.";
        public const string BookingCreateAdminForbidden = "Admin cannot book vacations.";

        // Cancellation request notifications (sent to user)
        public const string CancellationApprovedNotification = "Your cancellation request for \"{0}\" has been approved. Your booking has been removed.";
        public const string CancellationDeclinedNotification = "Your cancellation request for \"{0}\" has been declined. Your booking remains active.";

        // Cancellation management (admin TempData)
        public const string CancellationApproveSuccess = "Cancellation request approved. Booking has been deleted.";
        public const string CancellationDeclineSuccess = "Cancellation request declined. Booking restored to confirmed.";
        public const string CancellationRequestNotFound = "Cancellation request not found.";

        // ExcursionInputModel validation
        public const string ExcursionTitleRequired = "Title is required.";
        public const string ExcursionTitleMinLength = "Title must be at least {1} characters long.";
        public const string ExcursionTitleMaxLength = "Title cannot exceed {1} characters.";
        public const string ExcursionDestinationRequired = "Destination is required.";
        public const string ExcursionDestinationMinLength = "Destination must be at least {1} characters long.";
        public const string ExcursionDestinationMaxLength = "Destination cannot exceed {1} characters.";
        public const string ExcursionStartDateRequired = "Start date is required.";
        public const string ExcursionStartDateTooSoon = "Start date must be at least 14 days from today.";
        public const string ExcursionEndDateRequired = "End date is required.";
        public const string ExcursionEndDateTooSoon = "End date must be at least 2 days after the start date.";        
        public const string ExcursionPriceRequired = "Price is required.";
        public const string ExcursionPriceRange = "Price must be between {1} and {2}.";
        public const string ExcursionCapacityRequired = "Capacity is required.";
        public const string ExcursionCapacityRange = "Capacity must be between {1} and {2}.";
    }
}

