using System;
using UnityEngine;

namespace Core.Utils.ExtensionMethods
{
	public static class StringEx
	{
		public static bool IsNullOrEmpty(this string s)
		{
			return String.IsNullOrEmpty(s);
		}

		public static string Color(this string text, Color color)
		{
			return $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{text}</color>";
		}
	}
}
