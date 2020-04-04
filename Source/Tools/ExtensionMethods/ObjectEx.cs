using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Debug = UnityEngine.Debug;

public static class ObjectEx
{
    public static void log(this object o, string tag = "")
    {
        if (!Macros.EDITOR) return;
        Debug.Log(tag + o);
    }

    public static void logClear(this object o, string tag = "")
    {
        if (!Macros.EDITOR) return;
        var assembly = Assembly.GetAssembly(typeof(SceneView));
        var type = assembly.GetType("UnityEditor.LogEntries");
        var method = type.GetMethod("Clear");
        method.Invoke(new object(), null);

        o.log(tag);
    }

    public static void AddToPreloadedAssets(this Object @this)
    {
        if (!Macros.EDITOR) return;
        
        var preloadedAssets = PlayerSettings.GetPreloadedAssets();

        var newAssets = preloadedAssets
                .Where(o => o != @this)
                .Where(o => o != null)
                .ToList()
            ;
        newAssets.Add(@this);

        PlayerSettings.SetPreloadedAssets(newAssets.ToArray());
    }
}
