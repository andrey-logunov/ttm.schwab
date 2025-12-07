using System;

namespace ttm.schwab.common
{
    internal static class Calendar
    {
        public static bool IsRegularOptionDate(DateOnly date)
        {
            return
                date.DayOfWeek == DayOfWeek.Friday && date.AddDays(-14).Month == date.Month && date.AddDays(-21).Month != date.Month ||
                date.DayOfWeek == DayOfWeek.Thursday && date.AddDays(-13).Month == date.Month && date.AddDays(-20).Month != date.Month;
        }
    }
}
