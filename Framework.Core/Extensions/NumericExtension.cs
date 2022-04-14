namespace Framework.Core.Extensions
{
	public static class NumericExtension
	{
		public static bool Between(this int num, int lower, int upper, bool inclusive = false)
		{
			return inclusive
				? lower <= num && num <= upper
				: lower < num && num < upper;
		}

		public static bool Between(this long num, long lower, long upper, bool inclusive = false)
		{
			return inclusive
				? lower <= num && num <= upper
				: lower < num && num < upper;
		}
	}
}
