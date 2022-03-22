using System;
using System.Linq;
using Samola.Algorithms.Sequences;
using Samola.Algorithms.Utilities;
using Xunit;

namespace Samola.Algorithms.Tests
{
    public class CalendarTests
    {
        [Fact]
        public void Calendar_enumerates_weekdays_correctly()
        {
            var from = new DateTime(2019, 8, 1);
            var to = new DateTime(2019, 9, 1);

            var first = Calendar.First(DayOfWeek.Monday, from);
            var days = new DayOfTheWeekSequence(first).TakeWhile(d => d <= to).ToArray();
            Assert.Equal(new DateTime(2019, 8, 5), days[0]);
            Assert.Equal(new DateTime(2019, 8, 12), days[1]);
            Assert.Equal(new DateTime(2019, 8, 19), days[2]);
            Assert.Equal(new DateTime(2019, 8, 26), days[3]);
            Assert.Equal(4, days.Length);

            first = Calendar.First(DayOfWeek.Tuesday, from);
            days = new DayOfTheWeekSequence(first).TakeWhile(d => d <= to).ToArray();
            Assert.Equal(new DateTime(2019, 8, 6), days[0]);
            Assert.Equal(new DateTime(2019, 8, 13), days[1]);
            Assert.Equal(new DateTime(2019, 8, 20), days[2]);
            Assert.Equal(new DateTime(2019, 8, 27), days[3]);
            Assert.Equal(4, days.Length);

            first = Calendar.First(DayOfWeek.Wednesday, from);
            days = new DayOfTheWeekSequence(first).TakeWhile(d => d <= to).ToArray();
            Assert.Equal(new DateTime(2019, 8, 7), days[0]);
            Assert.Equal(new DateTime(2019, 8, 14), days[1]);
            Assert.Equal(new DateTime(2019, 8, 21), days[2]);
            Assert.Equal(new DateTime(2019, 8, 28), days[3]);
            Assert.Equal(4, days.Length);

            first = Calendar.First(DayOfWeek.Thursday, from);
            days = new DayOfTheWeekSequence(first).TakeWhile(d => d <= to).ToArray();
            Assert.Equal(new DateTime(2019, 8, 1), days[0]);
            Assert.Equal(new DateTime(2019, 8, 8), days[1]);
            Assert.Equal(new DateTime(2019, 8, 15), days[2]);
            Assert.Equal(new DateTime(2019, 8, 22), days[3]);
            Assert.Equal(new DateTime(2019, 8, 29), days[4]);
            Assert.Equal(5, days.Length);

            first = Calendar.First(DayOfWeek.Friday, from);
            days = new DayOfTheWeekSequence(first).TakeWhile(d => d <= to).ToArray();
            Assert.Equal(new DateTime(2019, 8, 2), days[0]);
            Assert.Equal(new DateTime(2019, 8, 9), days[1]);
            Assert.Equal(new DateTime(2019, 8, 16), days[2]);
            Assert.Equal(new DateTime(2019, 8, 23), days[3]);
            Assert.Equal(new DateTime(2019, 8, 30), days[4]);
            Assert.Equal(5, days.Length);

            first = Calendar.First(DayOfWeek.Saturday, from);
            days = new DayOfTheWeekSequence(first).TakeWhile(d => d <= to).ToArray();
            Assert.Equal(new DateTime(2019, 8, 3), days[0]);
            Assert.Equal(new DateTime(2019, 8, 10), days[1]);
            Assert.Equal(new DateTime(2019, 8, 17), days[2]);
            Assert.Equal(new DateTime(2019, 8, 24), days[3]);
            Assert.Equal(new DateTime(2019, 8, 31), days[4]);
            Assert.Equal(5, days.Length);

            first = Calendar.First(DayOfWeek.Sunday, from);
            days = new DayOfTheWeekSequence(first).TakeWhile(d => d <= to).ToArray();
            Assert.Equal(new DateTime(2019, 8, 4), days[0]);
            Assert.Equal(new DateTime(2019, 8, 11), days[1]);
            Assert.Equal(new DateTime(2019, 8, 18), days[2]);
            Assert.Equal(new DateTime(2019, 8, 25), days[3]);
            Assert.Equal(new DateTime(2019, 9, 1), days[4]);
            Assert.Equal(5, days.Length);
        }
    }
}
