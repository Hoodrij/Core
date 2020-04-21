using System;
using System.Reflection;
using UnityEditor;
using Debug = UnityEngine.Debug;

public static class ObjectEx
{
    public static void log(this object o, string tag = "")
    {
#if UNITY_EDITOR
        Debug.Log(tag + o);
#endif
    }

    public static void logClear(this object o, string tag = "")
    {
#if UNITY_EDITOR
        Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
        Type type = assembly.GetType("UnityEditor.LogEntries");
        MethodInfo method = type.GetMethod("Clear");
        method.Invoke(new object(), null);

        o.log(tag);
#endif
    }
}
