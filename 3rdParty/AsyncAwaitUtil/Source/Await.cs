using System;
using UnityEngine;

public static class Await
{
    private static readonly WaitForUpdate _waitForUpdate = new WaitForUpdate();
    private static readonly WaitForFixedUpdate _waitForFixedUpdate = new WaitForFixedUpdate();
    private static readonly WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();

    public static WaitForUpdate NextFrame => _waitForUpdate;

    public static WaitForFixedUpdate FixedUpdate => _waitForFixedUpdate;

    public static WaitForEndOfFrame EndOfFrame => _waitForEndOfFrame;

    public static WaitForSeconds Seconds(float seconds)
    {
        return new WaitForSeconds(seconds);
    }

    public static WaitForSecondsRealtime SecondsRealtime(float seconds)
    {
        return new WaitForSecondsRealtime(seconds);
    }

    public static WaitUntil Until(Func<bool> predicate)
    {
        return new WaitUntil(predicate);
    }

    public static WaitWhile While(Func<bool> predicate)
    {
        return new WaitWhile(predicate);
    }
}