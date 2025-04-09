namespace StoreManager.Infrastructure.Invoice
{
    public static class DateTimeExtensions
    {
        public static DateTime StartOfWeek(this DateTime dateTime)
        {
            var diff = dateTime.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0)
            {
                diff += 7;
            }
            return dateTime.AddDays(-diff).Date;

        }
    }
}
