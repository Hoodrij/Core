using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

//namespace Core.Tools.ExtensionMethods
//{
	public static class ObjectEx
	{
		public static void log(this object o, string tag = "")
		{
			if (Macros.EDITOR)
			{
				Debug.Log(tag + o);
			}
		}

		public static void logClear(this object o, string tag = "")
		{
			if (Macros.EDITOR)
			{
				var assembly = Assembly.GetAssembly(typeof(SceneView));
				var type = assembly.GetType("UnityEditor.LogEntries");
				var method = type.GetMethod("Clear");
				method.Invoke(new object(), null);

				o.log(tag);
			}
		}
		
		public static void AddToPreloadedAssets(this Object @this)
		{
			if (Macros.EDITOR)
			{
				Object[] preloadedAssets = PlayerSettings.GetPreloadedAssets();

				List<Object> newAssets = preloadedAssets
						.Where(o => o != @this)
						.Where(o => o != null)
						.ToList()
						;
				newAssets.Add(@this);
				
				PlayerSettings.SetPreloadedAssets(newAssets.ToArray());
			}
		}
	}
//}
