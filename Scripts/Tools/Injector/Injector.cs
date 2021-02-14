﻿using System;
using System.Collections.Generic;
using Core.Scripts.Tools.Injector;
using Core.Tools.ExtensionMethods;
using Core.Units.Model;
using UnityEngine;

namespace Core.Tools
{
    internal class Injector
    {
#region internal Singleton (for Core only)
        internal static Injector Instance { get; } = new Injector();
#endregion

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

            if (type.IsAssignableToGenericType(typeof(Model<>)))
            {
                object instance = Activator.CreateInstance(type);
                Add(instance);
                return instance;
            }

            Debug.LogError($"[Injector] Cant find {type.Name.Color(Color.red)}");
            return null;
        }

        public T Get<T>() => (T) Get(typeof(T));

        public void Clear() => container.Clear();
    }
}