namespace TravelBuddy.GCommon
{
    public static class ValidationConstants
    {
        public class ApplicationUserConstants
        {
            public const int FullNameMinLength = 2;
            public const int FullNameMaxLength = 750;

        }

        public class BookingCancellationRequestConstants
        {
            public const int ReasonMinLength = 10;
            public const int ReasonMaxLength = 1000;
        }

        public class ExcursionConstants
        {
            public const int TitleMinLength = 5;
            public const int TitleMaxLength = 200;

            public const int DestinationMinLength = 5;
            public const int DestinationMaxLength = 300;

            public const string PriceType = "decimal(18,2)";
        }

        public class NotificationConstants
        {
            public const int MessageMinLength = 5;
            public const int MessageMaxLength = 1000;
        }
    }
}
