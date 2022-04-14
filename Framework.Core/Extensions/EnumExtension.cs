using System;
using System.Collections.Generic;
using System.Net;
using System.Text;


namespace Framework.Core.Extensions
{
    public static class EnumExtension
    {
        public static int ToInt<TEnum>(this TEnum value) where TEnum : Enum => Convert.ToInt32(value);

        public static TEnum ToEnum<TEnum>(this int value, TEnum defaultValue) where TEnum : Enum
        {
            var type = typeof(TEnum);
            var result = Enum.IsDefined(type, value) ? (TEnum)Enum.ToObject(type, value) : defaultValue;
            return result;
        }

        public static TEnum ToEnum<TEnum>(this int? value, TEnum defaultValue) where TEnum : Enum
        {
            if (!value.HasValue)
                return defaultValue;

            var type = typeof(TEnum);
            var result = Enum.IsDefined(type, value) ? (TEnum)Enum.ToObject(type, value) : defaultValue;
            return result;
        }

        public static TEnum ToEnum<TEnum>(this string value, TEnum defaultValue, bool ignoreCase = false) where TEnum : struct, Enum
        {
            if (String.IsNullOrEmpty(value))
                return defaultValue;

            if (ignoreCase)
            {
                value = value.ToLower();
                value = $"{Char.ToUpper(value[0])}{value.Substring(1)}";
            }

            var type = typeof(TEnum);

            if (int.TryParse(value, out int intResult) && Enum.IsDefined(type, intResult))
                return intResult.ToEnum(defaultValue);

            if(!Enum.IsDefined(type, value))
                return defaultValue;

            TEnum result = (TEnum)Enum.Parse(type, value, true);
            return result;
        }
    }
}
