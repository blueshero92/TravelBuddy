namespace TravelBuddy.GCommon.Constants
{
    public static class AppConstants
    {
        //Messages for TempData keys used to display notification messages to the user.
        public const string ErrorTempDataKey = "ErrorMessage";
        public const string SuccessTempDataKey = "SuccessMessage";
        public const string WarningTempDataKey = "WarningMessage";
        public const string InfoTempDataKey = "InfoMessage";
        public const string FavoritesErrorTempDataKey = "FavoritesError";

        //Size of the page for pagination purposes, used in various controllers to determine how many items to display per page.
        public const int PageSize = 6;

        public const int NotificationsPageSize = 15;
    }
}
