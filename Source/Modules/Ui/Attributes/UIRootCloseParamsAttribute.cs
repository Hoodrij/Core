using System;

namespace Core.Ui.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class UIRootCloseParamsAttribute : Attribute
    {
        public Type[] RootsToClose;

        public UIRootCloseParamsAttribute(params Type[] close)
        {
            RootsToClose = close;
        }
    }
}