using System;

namespace Samola.Numbers.CustomTypes
{

    public struct Date
    {
        private readonly int _year;
        private readonly int _month;
        private readonly int _day;

        public Date(int year, int month, int day) 
        {
            _year = year;
            _month = month;
            _day = day;
        }

        public Date(DateTime datetime)
            : this(datetime.Year, datetime.Month, datetime.Day)
        {

        }

        public int Day => _day;
        public int Month => _month;
        public int Year => _year;
        public DayOfWeek DayOfWeek
        {
            get
            {
                return DayOfWeek.Monday;
            }
        }

        public static bool operator <(Date a, Date b) => b > a;
        
        public static bool operator >(Date a, Date b)
        {
            if (a.Year == b.Year)
            {
                if (a.Month == b.Month)
                {
                    return a.Day > b.Day;
                }
                else
                {
                    return a.Month > b.Month;
                }
            }
            else
            {
                return a.Year > b.Year;
            }
        }

        public static bool operator ==(Date a, Date b)
        {
            return a.Day == b.Day
                && a.Month == b.Month
                && a.Year == b.Year;
        }

        public static bool operator !=(Date a, Date b)
        {
            return a.Day != b.Day
                || a.Month != b.Month
                || a.Year != b.Year;
        }

        public static bool operator <=(Date a, Date b) => !(a > b);

        public static bool operator >=(Date a, Date b) => !(b > a);

        public override bool Equals(object obj)
        {
            Date b = (Date)obj;
            return this == b;
        }
        public override int GetHashCode()
        {
            return 13 * this.Day + 17 * this.Month + 19 * this.Year;
        }
    }

    public static class DateTimeExtensions
    {
        public static Date ToDate(this DateTime datetime)
        {
            return new Date(datetime);
        }
    }
}
