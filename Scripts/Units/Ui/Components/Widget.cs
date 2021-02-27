using Core.Tools;
using Core.Tools.Bindings;

namespace Core.Ui
{
    public abstract class Widget : ABindableBehaviour
    {
        private void Awake()
        {
            Injector.Instance.Populate(this);
            OnOpen();
            Rebind();
        }

        protected virtual void OnOpen() { }
    }
}