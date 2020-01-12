using UnityEngine;

namespace Core.Utils.ExtensionMethods
{
	public static class BoundsEx
	{
		public static Vector3 GetRandomPoint(this Bounds bounds, string seed = "random")
		{
			if (seed == "random")
				seed = Time.realtimeSinceStartup.ToString();

			System.Random pseudoRandom = new System.Random(seed.GetHashCode());

			float x = pseudoRandom.Next((int)(bounds.min.x * 100), (int)(bounds.max.x * 100)) / 100f;
			float y = pseudoRandom.Next((int)(bounds.min.y * 100), (int)(bounds.max.y * 100)) / 100f;
			float z = pseudoRandom.Next((int)(bounds.min.z * 100), (int)(bounds.max.z * 100)) / 100f;
			return new Vector3(x, y, z);
		}

		public static Rect ToRect(this Bounds b)
		{
			Vector3 c = b.center;
			Vector3 e = b.extents;
			Vector3 s = b.size;
			return new Rect(c.x - e.x, c.y - e.y, s.x, s.y);
		}
	}
}
