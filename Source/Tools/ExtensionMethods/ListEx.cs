﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Core.Tools.ExtensionMethods
{
    public static class ListEx
    {
        public static bool HasIndex<T>(this IEnumerable<T> list, int i)
        {
            return i > -1 && i < list.Count();
        }

        public static bool IsEmpty<T>(this IEnumerable<T> list)
        {
            return !list.Any();
        }

        public static ICollection<T> AddAndGet<T>(this ICollection<T> list, T item)
        {
            list.Add(item);
            return list;
        }

        public static T GetRandom<T>(this IEnumerable<T> list)
        {
            var index = list.Count().RandomTo();
            return list.ElementAt(index);
        }
    }
}