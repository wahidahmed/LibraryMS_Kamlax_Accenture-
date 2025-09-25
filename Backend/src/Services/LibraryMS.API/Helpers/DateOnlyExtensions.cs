namespace LibraryMS.API.Helpers
{
    public static class DateOnlyExtensions
    {
        public static DateOnly ToDateOnly(this DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime);
        }
    }
}
