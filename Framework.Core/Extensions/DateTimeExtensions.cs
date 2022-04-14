using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Framework.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToPersianDate(this DateTime dateTime)
        {
            var result = "{0:0000}/{1:00}/{2:00}";
            var calendar = new PersianCalendar();
            return string.Format(result,
                calendar.GetYear(dateTime),
                calendar.GetMonth(dateTime),
                calendar.GetDayOfMonth(dateTime));
        }

        public static DateTime? EndOfDay(this DateTime? date)
        {
            if (date == null)
                return date;

            return EndOfDay(date.Value);
        }

        public static DateTime EndOfDay(this DateTime date)
        {
            if (date == null)
                return date;

            return new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
        }
    }
}
