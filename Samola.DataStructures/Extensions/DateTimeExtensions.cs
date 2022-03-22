using System;

namespace Samola.DataStructures.Extensions
{
    public static class DateTimeExtensions
    {
        public static Date ToDate(this DateTime datetime)
        {
            return new Date(datetime);
        }
    }
}