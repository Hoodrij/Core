using System;
using System.Runtime.CompilerServices;
using Core.Tools;

namespace UnityAsync
{
    public partial class Wait
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitForFrames> WaitUpdate(this Job job) 
			=> Update.ConfigureAwait(job.Token);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitForFrames> WaitLateUpdate(this Job job) 
			=> LateUpdate.ConfigureAwait(job.Token);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitForFrames> WaitFixedUpdate(this Job job) 
			=> FixedUpdate.ConfigureAwait(job.Token);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitForSeconds> WaitSeconds(this Job job, float duration) 
			=> Seconds(duration).ConfigureAwait(job.Token);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitForSecondsRealtime> WaitSecondsRealtime(this Job job, float duration) 
			=> SecondsRealtime(duration).ConfigureAwait(job.Token);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitUntil> WaitUntil(this Job job, Func<bool> condition) 
			=> Until(condition).ConfigureAwait(job.Token);
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static AwaitInstructionAwaiter<WaitWhile> WaitWhile(this Job job, Func<bool> condition) 
			=> While(condition).ConfigureAwait(job.Token);
    
    }
}