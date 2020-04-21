using Core.Tools;

namespace Core
{
    public abstract class Unit
    {
        protected Unit()
        {
            Injector.Instance.Add(this);
            Injector.Instance.Populate(this);
        }
    }
}