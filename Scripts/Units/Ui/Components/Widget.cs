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
            RebindAll();
        }

        protected abstract void OnOpen();
    }
}