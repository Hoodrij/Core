using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class injectAttribute : Attribute { }

namespace Core.Tools
{
    public class Injector
    {
        private readonly Dictionary<Type, object> objects = new Dictionary<Type, object>();

        public Injector()
        {
            Add(this);
        }

        public void Add(object obj)
        {
            var type = obj.GetType();

            objects[type] = obj;
        }

        public void Inject(object obj)
        {
            var members = Reflector.Reflect(obj.GetType());
            foreach (var member in members)
            {
                var type = Reflector.GetUnderlyingType(member);
                var value = Get(type);

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
            if (!objects.TryGetValue(type, out var obj)) Debug.LogError($"Cant find {type} in injector");

            return obj;
        }

        public T GetInstance<T>()
        {
            return (T) Get(typeof(T));
        }

        private static class Reflector
        {
            private static readonly Type injectAttributeType = typeof(injectAttribute);
            private static readonly Dictionary<Type, MemberInfo[]> cachedFieldInfos = new Dictionary<Type, MemberInfo[]>();
            private static readonly List<MemberInfo> reusableList = new List<MemberInfo>(1024);

            public static MemberInfo[] Reflect(Type type)
            {
                Assert.AreEqual(0, reusableList.Count, "Reusable list in Reflector was not empty!");

                if (cachedFieldInfos.TryGetValue(type, out var cachedResult)) return cachedResult;

                var flags = BindingFlags.Public |
                            BindingFlags.NonPublic |
                            BindingFlags.Static |
                            BindingFlags.Instance |
                            BindingFlags.DeclaredOnly;

                var fields =
                    from it in type.GetMembers(flags)
                    where it is PropertyInfo || it is FieldInfo
                    select it;

                foreach (var baseType in GetBaseTypes(type))
                    fields = fields.Concat(from it in baseType.GetMembers(flags)
                        where it is PropertyInfo || it is FieldInfo
                        select it);

                foreach (var field in fields)
                {
                    var hasInjectAttribute = field.IsDefined(injectAttributeType);
                    if (hasInjectAttribute) reusableList.Add(field);
                }

                var resultAsArray = reusableList.ToArray();
                reusableList.Clear();
                cachedFieldInfos[type] = resultAsArray;
                return resultAsArray;
            }

            private static List<Type> GetBaseTypes(Type type)
            {
                var result = new List<Type>();
                if (type.BaseType != null)
                {
                    result.Add(type.BaseType);
                    result.AddRange(GetBaseTypes(type.BaseType));
                }

                return result;
            }

            public static Type GetUnderlyingType(MemberInfo member)
            {
                switch (member.MemberType)
                {
                    case MemberTypes.Field:
                        return ((FieldInfo) member).FieldType;
                    case MemberTypes.Property:
                        return ((PropertyInfo) member).PropertyType;
                    default:
                        throw new ArgumentException
                        (
                            "Input MemberInfo must be if type FieldInfo or PropertyInfo"
                        );
                }
            }
        }
    }
}