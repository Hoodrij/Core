﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Tools
{
    internal class Injector
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Create() => Instance = new Injector();
        internal static Injector Instance { get; private set; }
        
        private readonly Dictionary<Type, object> container = new Dictionary<Type, object>();

        public void Add(object obj)
        {
            Type type = obj.GetType();

            foreach (Type @interface in type.GetInterfaces())
            {
                container[@interface] = obj;
            }

            container[type] = obj;
        }

        public void Populate(object obj)
        {
            Reflector.Populate<InjectAttribute>(obj, Get);
        }

        private object Get(Type type)
        {
            if (container.TryGetValue(type, out var value))
                return value;

            if (SupportsLazyCreation(type))
            {
                object instance = Activator.CreateInstance(type);
                Add(instance);
                return instance;
            }

            Debug.LogError($"[Injector] Cant find {type.Name.Color(Color.red)}");
            return null;
        }

        private bool SupportsLazyCreation(Type type)
        {
            return typeof(Lazy).IsAssignableFrom(type);
        }

        public T Get<T>() => (T) Get(typeof(T));

        public void Clear() => container.Clear();
    }
}