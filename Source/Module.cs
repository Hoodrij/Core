using Core.Tools;

namespace Core
{
    public abstract class Module
    {
        protected Module()
        {
            Injector.Instance.Add(this);
            Injector.Instance.Populate(this);
        }
    }
}