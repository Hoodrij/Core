namespace Core.Utils.ExtensionMethods
{
	public static class ArrayEx
	{
		public static bool HasElement<T>(this T[] array, int i)
		{
			return i >= 0 && i < array.Length;
		}
	}
}
