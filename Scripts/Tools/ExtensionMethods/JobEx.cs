using System.Collections.Generic;

namespace Core.Tools.ExtensionMethods
{
    public static class JobEx
    {
        public static JobSequence ToJobSequence(this IEnumerable<Job> jobs)
        {
            JobSequence jobSequence = new JobSequence();
            jobs.ForEach(job => jobSequence.Add(job));
            return jobSequence;
        }
    }
}