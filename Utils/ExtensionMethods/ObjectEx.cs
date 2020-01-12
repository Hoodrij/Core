using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Core.Utils.ExtensionMethods
{
	public static class ObjectEx
	{
		public static void print(this object o, string tag = "")
		{
#if UNITY_EDITOR
			Debug.Log(tag + o);
#endif
		}

		public static void printClear(this object o, string tag = "")
		{
#if UNITY_EDITOR

			var assembly = Assembly.GetAssembly(typeof(SceneView));
			var type = assembly.GetType("UnityEditor.LogEntries");
			var method = type.GetMethod("Clear");
			method.Invoke(new object(), null);

			o.print(tag);
#endif
		}
	}
}
