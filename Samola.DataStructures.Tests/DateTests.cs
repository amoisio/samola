using System;
using Samola.DataStructures.Extensions;
using Xunit;

namespace Samola.DataStructures.Tests
{
    public class DateTests
    {
        [Fact]
        public void Date_behaves_like_a_value()
        {
            DateTime dt = new DateTime(2019, 9, 1);
            Date d = new Date(dt);

            dt.AddDays(1);

            Assert.Equal(1, d.Day);
        }

        [Fact]
        public void Date_comparisons_work_as_expected()
        {
            DateTime a = new DateTime(2019, 8, 15);

            DateTime b0 = new DateTime(2019, 8, 14);
            DateTime b1 = new DateTime(2019, 8, 15);
            DateTime b2 = new DateTime(2019, 8, 16);

            DateTime c0 = new DateTime(2019, 7, 15);
            DateTime c1 = new DateTime(2019, 8, 15);
            DateTime c2 = new DateTime(2019, 9, 15);

            DateTime d0 = new DateTime(2018, 8, 15);
            DateTime d1 = new DateTime(2019, 8, 15);
            DateTime d2 = new DateTime(2020, 8, 15);

            Assert.Equal(a > b0, a.ToDate() > b0.ToDate());
            Assert.Equal(a > b1, a.ToDate() > b1.ToDate());
            Assert.Equal(a > b2, a.ToDate() > b2.ToDate());
            Assert.Equal(a > c0, a.ToDate() > c0.ToDate());
            Assert.Equal(a > c1, a.ToDate() > c1.ToDate());
            Assert.Equal(a > c2, a.ToDate() > c2.ToDate());
            Assert.Equal(a > d0, a.ToDate() > d0.ToDate());
            Assert.Equal(a > d1, a.ToDate() > d1.ToDate());
            Assert.Equal(a > d2, a.ToDate() > d2.ToDate());

            Assert.Equal(a < b0, a.ToDate() < b0.ToDate());
            Assert.Equal(a < b1, a.ToDate() < b1.ToDate());
            Assert.Equal(a < b2, a.ToDate() < b2.ToDate());
            Assert.Equal(a < c0, a.ToDate() < c0.ToDate());
            Assert.Equal(a < c1, a.ToDate() < c1.ToDate());
            Assert.Equal(a < c2, a.ToDate() < c2.ToDate());
            Assert.Equal(a < d0, a.ToDate() < d0.ToDate());
            Assert.Equal(a < d1, a.ToDate() < d1.ToDate());
            Assert.Equal(a < d2, a.ToDate() < d2.ToDate());

            Assert.Equal(a >= b0, a.ToDate() >= b0.ToDate());
            Assert.Equal(a >= b1, a.ToDate() >= b1.ToDate());
            Assert.Equal(a >= b2, a.ToDate() >= b2.ToDate());
            Assert.Equal(a >= c0, a.ToDate() >= c0.ToDate());
            Assert.Equal(a >= c1, a.ToDate() >= c1.ToDate());
            Assert.Equal(a >= c2, a.ToDate() >= c2.ToDate());
            Assert.Equal(a >= d0, a.ToDate() >= d0.ToDate());
            Assert.Equal(a >= d1, a.ToDate() >= d1.ToDate());
            Assert.Equal(a >= d2, a.ToDate() >= d2.ToDate());

            Assert.Equal(a <= b0, a.ToDate() <= b0.ToDate());
            Assert.Equal(a <= b1, a.ToDate() <= b1.ToDate());
            Assert.Equal(a <= b2, a.ToDate() <= b2.ToDate());
            Assert.Equal(a <= c0, a.ToDate() <= c0.ToDate());
            Assert.Equal(a <= c1, a.ToDate() <= c1.ToDate());
            Assert.Equal(a <= c2, a.ToDate() <= c2.ToDate());
            Assert.Equal(a <= d0, a.ToDate() <= d0.ToDate());
            Assert.Equal(a <= d1, a.ToDate() <= d1.ToDate());
            Assert.Equal(a <= d2, a.ToDate() <= d2.ToDate());

            Assert.Equal(a == b0, a.ToDate() == b0.ToDate());
            Assert.Equal(a == b1, a.ToDate() == b1.ToDate());
            Assert.Equal(a == b2, a.ToDate() == b2.ToDate());
            Assert.Equal(a == c0, a.ToDate() == c0.ToDate());
            Assert.Equal(a == c1, a.ToDate() == c1.ToDate());
            Assert.Equal(a == c2, a.ToDate() == c2.ToDate());
            Assert.Equal(a == d0, a.ToDate() == d0.ToDate());
            Assert.Equal(a == d1, a.ToDate() == d1.ToDate());
            Assert.Equal(a == d2, a.ToDate() == d2.ToDate());

        }
    }
}
