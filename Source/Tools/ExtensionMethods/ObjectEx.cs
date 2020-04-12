using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

public static class ObjectEx
{
    public static void log(this object o, string tag = "")
    {
        if (!IS.EDITOR) return;
        Debug.Log(tag + o);
    }

    public static void logClear(this object o, string tag = "")
    {
        if (!IS.EDITOR) return;
        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
        Type type = assembly.GetType("UnityEditor.LogEntries");
        MethodInfo method = type.GetMethod("Clear");
        method.Invoke(new object(), null);

        o.log(tag);
    }

    public static void AddToPreloadedAssets(this Object @this)
    {
        if (!IS.EDITOR) return;
        
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
