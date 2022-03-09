using System;
using System.Collections.Generic;

namespace Samola.Numbers.Enumerables
{
    public class Calendar
    {
        public static readonly DateTime MIN_DATE = new DateTime(1900, 1, 1);
        public static readonly DateTime MAX_DATE = new DateTime(2099, 12, 31);

        public static IEnumerable<DateTime> WeekDays(DayOfWeek dayOfWeek)
        {
            return WeekDays(dayOfWeek, MIN_DATE, MAX_DATE);
        }
        public static IEnumerable<DateTime> WeekDays(DayOfWeek dayOfWeek, DateTime from)
        {
            return WeekDays(dayOfWeek, from, MAX_DATE);
        }
        public static IEnumerable<DateTime> WeekDays(DayOfWeek dayOfWeek, DateTime from, DateTime to)
        {
            // set tempDate to the first occurence of the desired day of the week
            int offset = dayOfWeek - from.DayOfWeek;
            if (offset < 0) offset += 7;

            DateTime tempDate;
            if (offset == 0)
                tempDate = from;
            else if (offset > 0)
                tempDate = from.AddDays(offset);
            else
                tempDate = from.AddDays(7 + offset);

            // return while within range
            while (to >= tempDate)
            {
                yield return tempDate;
                tempDate = tempDate.AddDays(7);
            }
        }
    }
}
