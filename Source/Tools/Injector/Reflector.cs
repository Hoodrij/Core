using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.Assertions;

namespace Core.Tools
{
    internal static class Reflector
    {
        private static readonly Dictionary<Type, MemberInfo[]> cachedFieldInfos = new Dictionary<Type, MemberInfo[]>();
        private static readonly List<MemberInfo> reusableList = new List<MemberInfo>(1024);

        public static MemberInfo[] Reflect(Type type)
        {
            Assert.AreEqual(0, reusableList.Count, "Reusable list in Reflector was not empty!");

            if (cachedFieldInfos.TryGetValue(type, out MemberInfo[] cachedResult)) return cachedResult;

            BindingFlags flags = BindingFlags.Public |
                                 BindingFlags.NonPublic |
                                 BindingFlags.Static |
                                 BindingFlags.Instance |
                                 BindingFlags.DeclaredOnly;

            IEnumerable<MemberInfo> fields =
                from it in type.GetMembers(flags)
                where it is PropertyInfo || it is FieldInfo
                select it;

            foreach (Type baseType in GetBaseTypes(type))
                fields = fields.Concat(from it in baseType.GetMembers(flags)
                    where it is PropertyInfo || it is FieldInfo
                    select it);

            foreach (MemberInfo field in fields)
            {
                bool hasInjectAttribute = field.IsDefined(typeof(injectAttribute));
                if (hasInjectAttribute) reusableList.Add(field);
            }

            MemberInfo[] resultAsArray = reusableList.ToArray();
            reusableList.Clear();
            cachedFieldInfos[type] = resultAsArray;
            return resultAsArray;
        }

        private static List<Type> GetBaseTypes(Type type)
        {
            List<Type> result = new List<Type>();
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