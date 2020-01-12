using System;

namespace Core.Utils.ExtensionMethods
{
	public static class StringEx
	{
		public static bool IsNullOrEmpty(this string s)
		{
			return String.IsNullOrEmpty(s);
		}
	}
}
