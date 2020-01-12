namespace Core.Utils.ExtensionMethods
{
	public static class StringFormatEx
	{
		public static string ToCommaString(this ulong number)
		{
			return string.Format("{0:n0}", number);
		}

		public static string ToCommaString(this double number)
		{
			return string.Format("{0:0.##}", number);
		}

		public static string ToCommaString(this int number)
		{
			return string.Format("{0:n0}", number);
		}

		public static string ToCommaString(this long number)
		{
			return string.Format("{0:n0}", number);
		}

		public static string ToCommaString(this float number)
		{
			return string.Format("{0:0.##}", number);
		}

	}
}