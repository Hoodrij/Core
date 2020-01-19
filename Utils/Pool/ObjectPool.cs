﻿using System.Collections.Generic;
using UnityEngine;

namespace Core.Utils.Pool
{
	public class ObjectPool
	{
		private APoolable prefab;
		public GameObject parent;

		private Stack<APoolable> stack;
		private int itemsInUse;
		private string name;

		public ObjectPool(APoolable prefab, GameObject parent, int initialCap = 0)
		{
			this.prefab = prefab;
			this.parent = parent;

			stack = new Stack<APoolable>();
			itemsInUse = 0;
			name = prefab.name;

			for (int i = 0; i < initialCap; i++)
			{
				AddInstance();
			}
		}

		#region Push item

		public virtual void Push(APoolable item)
		{
#if DEBUG
			if (item == null || item.gameObject == null) return;

			if (stack.Contains(item))
			{
				Debug.LogError("Tried to pool already pooled object. Ignoring...Check for duplicate return to pool" + name);
				return;
			}
			if (!item.gameObject.activeSelf)
			{
				Debug.LogError("Tried to pool inactive object. Ignoring...Check for duplicate return to pool" + name);
				return;
			}

			if (itemsInUse < 1)
			{
				Debug.LogError("Tried to pool object while pool had no items in use. Pool: " + name);
				return;
			}
#endif

			item.gameObject.SetActive(false);
			stack.Push(item);
			itemsInUse--;
		}

		#endregion

		#region Pop item

		public virtual APoolable Pop()
		{
			if (stack.Count == 0)
				AddInstance();

			APoolable item = stack.Pop();
			item.OnPop();

			itemsInUse++;
			return item;
		}

		#endregion

		void AddInstance()
		{
			APoolable item = Object.Instantiate(prefab, parent.transform);
			item.gameObject.SetActive(false);
			item.Pool = this;
			stack.Push(item);

#if UNITY_EDITOR
			item.name = prefab.name + (stack.Count + itemsInUse);
#endif
		}
	}
}