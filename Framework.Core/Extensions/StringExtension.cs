using System;
using System.Globalization;

namespace Framework.Core.Extensions
{
    public static class StringExtension
    {
        public static int? ToNullableInt(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return null;

            int.TryParse(str, out int intStr);

            return intStr;

        }

        public static int ToInt(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return 0;

            int.TryParse(str, out int intStr);

            return intStr;

        }

        public static DateTime? ToNullableDateTime(this string persianDateTime)
        {
            if (string.IsNullOrEmpty(persianDateTime))
                return null;
            try
            {
                var spilited = persianDateTime.Split('/');
                PersianCalendar persianCalendar = new PersianCalendar();

                DateTime dt = new DateTime(spilited[0].ToInt(), spilited[1].ToInt(), spilited[2].ToInt(), persianCalendar);
                return dt;
            }
            catch (Exception)
            {

                return null;
            }

        }
    }
}
