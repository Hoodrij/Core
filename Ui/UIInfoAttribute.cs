using System;

namespace Core.Ui
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UIInfoAttribute : Attribute
    {
        public Type Root { get; }
        public string Path { get; }
        public bool IsAsyncLoad { get; }

        public UIInfoAttribute(Type root, string path, bool isAsyncLoad = true)
        {
            Root = root;
            Path = path;
            IsAsyncLoad = isAsyncLoad;
        }
    }
}