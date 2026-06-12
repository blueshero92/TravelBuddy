namespace TravelBuddy.Infrastructure.Extensions
{
    public static class DateTimeExtensions
    {
        // Define the application's time zone (FLE Standard Time for Bulgaria/Eastern Europe).
        private static readonly TimeZoneInfo AppTimeZone =
            TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time"); // UTC+2 / UTC+3 DST (Bulgaria/Eastern Europe)

        // Extension method to convert a UTC DateTime to the application's local time zone.
        public static DateTime ToAppLocalTime(this DateTime utcDateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, AppTimeZone);
        }
    }
}
