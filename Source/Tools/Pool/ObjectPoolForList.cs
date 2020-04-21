using System;
using System.Collections.Generic;

namespace Core.Tools.Pool
{
    public class ObjectPoolForList<T> where T : new()
    {
        private readonly Stack<T> stack = new Stack<T>();
        private event Action<T> actionOnGet;
        private event Action<T> actionOnRelease;

        public int countAll { get; private set; }
        public int countActive => countAll - countInactive;
        public int countInactive => stack.Count;

        public ObjectPoolForList(Action<T> actionOnGet, Action<T> actionOnRelease)
        {
            this.actionOnGet = actionOnGet;
            this.actionOnRelease = actionOnRelease;
        }

        public T Get()
        {
            T element;
            if (stack.Count == 0)
            {
                element = new T();
                countAll++;
            }
            else
            {
                element = stack.Pop();
            }

            actionOnGet?.Invoke(element);
            return element;
        }

        public void Release(T element)
        {
            actionOnRelease?.Invoke(element);
            stack.Push(element);
        }
    }
}