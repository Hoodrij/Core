using System.Collections.Generic;

namespace Core.Tools.Pool
{
    public static class ListPool<T>
    {
        private static readonly ObjectPoolForList<List<T>> pool = new ObjectPoolForList<List<T>>(null, l => l.Clear());

        public static List<T> Get()
        {
            return pool.Get();
        }

        public static void Release(List<T> toRelease)
        {
            pool.Release(toRelease);
        }
    }
}