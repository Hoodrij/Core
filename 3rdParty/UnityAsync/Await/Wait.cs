using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

namespace UnityAsync
{
	public static partial class Wait
	{
		public static readonly AwaitInstructionAwaiter<WaitForFrames> Update = new AwaitInstructionAwaiter<WaitForFrames>(new WaitForFrames(1));
		public static readonly AwaitInstructionAwaiter<WaitForFrames> LateUpdate = new AwaitInstructionAwaiter<WaitForFrames>(new WaitForFrames(1), FrameScheduler.LateUpdate);
		public static readonly AwaitInstructionAwaiter<WaitForFrames> FixedUpdate = new AwaitInstructionAwaiter<WaitForFrames>(new WaitForFrames(1), FrameScheduler.FixedUpdate);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SynchronizationContext UnitySyncContext() => AsyncManager.UnitySyncContext;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static SynchronizationContext BackgroundSyncContext() => AsyncManager.BackgroundSyncContext;
		
		public static WaitUntil Until(Func<bool> func)
		{
			return new WaitUntil(func); 
		}
		
		public static WaitWhile While(Func<bool> func)
		{
			return new WaitWhile(func); 
		}
		
		public static WaitForSeconds Seconds(float duration)
		{
			return new WaitForSeconds(duration);
		}
		
		public static WaitForSecondsRealtime SecondsRealtime(float duration)
		{
			return new WaitForSecondsRealtime(duration);
		}
	}
}