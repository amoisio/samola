using Samola.Numbers.Enumerables;
using System;
using System.Linq;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class CalendarTests
    {
        [Fact]
        public void Calendar_enumerates_weekdays_correctly()
        {
            var from = new DateTime(2019, 8, 1);
            var to = new DateTime(2019, 9, 1);

            var days = Calendar.WeekDays(DayOfWeek.Monday, from, to).ToArray();
            Assert.Equal(new DateTime(2019, 8, 5), days[0]);
            Assert.Equal(new DateTime(2019, 8, 12), days[1]);
            Assert.Equal(new DateTime(2019, 8, 19), days[2]);
            Assert.Equal(new DateTime(2019, 8, 26), days[3]);
            Assert.Equal(4, days.Length);

            days = Calendar.WeekDays(DayOfWeek.Tuesday, from, to).ToArray();
            Assert.Equal(new DateTime(2019, 8, 6), days[0]);
            Assert.Equal(new DateTime(2019, 8, 13), days[1]);
            Assert.Equal(new DateTime(2019, 8, 20), days[2]);
            Assert.Equal(new DateTime(2019, 8, 27), days[3]);
            Assert.Equal(4, days.Length);

            days = Calendar.WeekDays(DayOfWeek.Wednesday, from, to).ToArray();
            Assert.Equal(new DateTime(2019, 8, 7), days[0]);
            Assert.Equal(new DateTime(2019, 8, 14), days[1]);
            Assert.Equal(new DateTime(2019, 8, 21), days[2]);
            Assert.Equal(new DateTime(2019, 8, 28), days[3]);
            Assert.Equal(4, days.Length);

            days = Calendar.WeekDays(DayOfWeek.Thursday, from, to).ToArray();
            Assert.Equal(new DateTime(2019, 8, 1), days[0]);
            Assert.Equal(new DateTime(2019, 8, 8), days[1]);
            Assert.Equal(new DateTime(2019, 8, 15), days[2]);
            Assert.Equal(new DateTime(2019, 8, 22), days[3]);
            Assert.Equal(new DateTime(2019, 8, 29), days[4]);
            Assert.Equal(5, days.Length);

            days = Calendar.WeekDays(DayOfWeek.Friday, from, to).ToArray();
            Assert.Equal(new DateTime(2019, 8, 2), days[0]);
            Assert.Equal(new DateTime(2019, 8, 9), days[1]);
            Assert.Equal(new DateTime(2019, 8, 16), days[2]);
            Assert.Equal(new DateTime(2019, 8, 23), days[3]);
            Assert.Equal(new DateTime(2019, 8, 30), days[4]);
            Assert.Equal(5, days.Length);

            days = Calendar.WeekDays(DayOfWeek.Saturday, from, to).ToArray();
            Assert.Equal(new DateTime(2019, 8, 3), days[0]);
            Assert.Equal(new DateTime(2019, 8, 10), days[1]);
            Assert.Equal(new DateTime(2019, 8, 17), days[2]);
            Assert.Equal(new DateTime(2019, 8, 24), days[3]);
            Assert.Equal(new DateTime(2019, 8, 31), days[4]);
            Assert.Equal(5, days.Length);

            days = Calendar.WeekDays(DayOfWeek.Sunday, from, to).ToArray();
            Assert.Equal(new DateTime(2019, 8, 4), days[0]);
            Assert.Equal(new DateTime(2019, 8, 11), days[1]);
            Assert.Equal(new DateTime(2019, 8, 18), days[2]);
            Assert.Equal(new DateTime(2019, 8, 25), days[3]);
            Assert.Equal(new DateTime(2019, 9, 1), days[4]);
            Assert.Equal(5, days.Length);
        }
    }
}
