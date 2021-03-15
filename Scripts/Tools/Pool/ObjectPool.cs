using System.Collections.Generic;
using UnityEngine;

namespace Core.Tools.Pool
{
    public class ObjectPool
    {
        private PooledObject prefab;
        private GameObject parent;

        private Stack<PooledObject> stack;
        private int itemsInUse;
        private string name;

        public ObjectPool(PooledObject prefab, GameObject parent, int initialCap = 0)
        {
            this.prefab = prefab;
            this.parent = parent;

            stack = new Stack<PooledObject>();
            itemsInUse = 0;
            name = prefab.name;

            for (int i = 0; i < initialCap; i++) AddInstance();
        }

        #region Push item

        public virtual void Push(PooledObject item)
        {
            if (IS.DEBUG)
            {
                if (item == null || item.gameObject == null) return;

                if (stack.Contains(item))
                {
                    Debug.LogError(
                        "Tried to pool already pooled object. Ignoring...Check for duplicate return to pool" + name);
                    return;
                }

                if (!item.gameObject.activeSelf)
                {
                    Debug.LogError(
                        "Tried to pool inactive object. Ignoring...Check for duplicate return to pool" + name);
                    return;
                }

                if (itemsInUse < 1)
                {
                    Debug.LogError("Tried to pool object while pool had no items in use. Pool: " + name);
                    return;
                }
            }

            item.gameObject.SetActive(false);
            stack.Push(item);
            itemsInUse--;
        }

        #endregion

        #region Pop item

        public virtual PooledObject Pop()
        {
            if (stack.Count == 0)
                AddInstance();

            PooledObject item = stack.Pop();
            item.Pop();

            itemsInUse++;
            return item;
        }

        #endregion

        private void AddInstance()
        {
            PooledObject item = Object.Instantiate(prefab, parent.transform);
            item.gameObject.SetActive(false);
            item.Pool = this;
            stack.Push(item);

            if (IS.EDITOR) item.name = prefab.name + (stack.Count + itemsInUse);
        }
    }
}