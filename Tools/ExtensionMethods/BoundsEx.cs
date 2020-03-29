using UnityEngine;

namespace Core.Tools.ExtensionMethods
{
    public static class BoundsEx
    {
        public static Vector3 GetRandomPoint(this Bounds bounds, string seed = "random")
        {
            if (seed == "random")
                seed = Time.realtimeSinceStartup.ToString();

            var pseudoRandom = new System.Random(seed.GetHashCode());

            var x = pseudoRandom.Next((int) (bounds.min.x * 100), (int) (bounds.max.x * 100)) / 100f;
            var y = pseudoRandom.Next((int) (bounds.min.y * 100), (int) (bounds.max.y * 100)) / 100f;
            var z = pseudoRandom.Next((int) (bounds.min.z * 100), (int) (bounds.max.z * 100)) / 100f;
            return new Vector3(x, y, z);
        }

        public static Rect ToRect(this Bounds b)
        {
            var c = b.center;
            var e = b.extents;
            var s = b.size;
            return new Rect(c.x - e.x, c.y - e.y, s.x, s.y);
        }
    }
}