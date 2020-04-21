﻿using System;
using Core.Tools;

namespace Core.Ui
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UIInfoAttribute : Attribute
    {
        public Type RootType { get; }
        public string Path { get; }
        public bool AsyncLoad { get; }

        public UIInfoAttribute(Type root, string path, bool asyncLoad = true)
        {
            RootType = root;
            Path = path;
            AsyncLoad = asyncLoad;
        }

        internal UIRoot Root => Injector.Instance.Get<UI>().GetRoot(RootType);
    }
}