public static class Macros
{
    public static bool EDITOR =
#if UNITY_EDITOR
        true;
#else
			false;
#endif

    public static bool DEBUG =
#if DEBUG
        true;
#else
			false;
#endif
    
    public static bool ON_DEVICE =
#if UNITY_ANDROID || UNITY_IOS
        true;
#else
			false;
#endif

    public static bool ANDROID =
#if UNITY_ANDROID
        true;
#else
			false;
#endif

    public static bool UNITY_IOS =
#if IOS
			true;
#else
        false;
#endif
}