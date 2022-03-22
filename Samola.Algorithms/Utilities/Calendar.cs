using System;

namespace Samola.Algorithms.Utilities
{
    public static class Calendar
    {
        public static readonly DateTime MinDate = new DateTime(1900, 1, 1);
        public static readonly DateTime MaxDate = new DateTime(2099, 12, 31);

        public static DateTime First(DayOfWeek dayOfWeek)
        {
            return First(dayOfWeek, MinDate);
        }
        
        public static DateTime First(DayOfWeek dayOfWeek, DateTime from)
        {
            int offset = dayOfWeek - from.DayOfWeek;
            if (offset < 0) offset += 7;
            return from.AddDays(offset);
        }
    }
}
