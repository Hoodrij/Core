using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Core.Tools
{
    internal class Injector
    {
#region internal Singleton (for Core only)

        internal static Injector Instance { get; } = new Injector();

#endregion

        private readonly Dictionary<Type, object> objects = new Dictionary<Type, object>();

        public void Add(object obj)
        {
            Type type = obj.GetType();
            
            foreach (Type @interface in type.GetInterfaces())
            {
                objects[@interface] = obj;
            }
            
            objects[type] = obj;
        }

        public void Populate(object obj)
        {
            MemberInfo[] members = Reflector.Reflect(obj.GetType());
            foreach (MemberInfo member in members)
            {
                Type type = Reflector.GetUnderlyingType(member);
                object value = Get(type);

                switch (member.MemberType)
                {
                    case MemberTypes.Field:
                        ((FieldInfo) member).SetValue(obj, value);
                        break;
                    case MemberTypes.Property:
                        ((PropertyInfo) member).SetValue(obj, value, null);
                        break;
                }
            }
        }

        private object Get(Type type)
        {
            if (objects.TryGetValue(type, out var value))
                return value;
            
            throw new KeyNotFoundException($"Cant find {type.Name.Color(Color.red)} in Injector");
        }

        public T Get<T>() => (T) Get(typeof(T));

        public void Clear() => objects.Clear();
    }
}